using System.Text;
using System.Text.Json;
using static UserStoryGenerator.Model.GFSGeminiClientHost;

namespace UserStoryGenerator.Model
{
    public class IssueGeneratorBase
    {
        public delegate void CompletedEventHandler(IssueGeneratorBaseArgs args);
        public event CompletedEventHandler? Completed;

        //public delegate void ErrorEventHandler(object error);
        public delegate void CompletedInErrorEventHandler(object? error);
        public event CompletedInErrorEventHandler? Error;

        protected const string ADDITIONALINSTRUCTIONS = "Additional Instructions";
        protected const string QATESTSTRUCTIONS = "Under no circumstances generate 'Test' issues";
        protected const string NOSUBTASKINSTRUCTIONS = "Under no circumstances generate 'Sub-task' issues";
        protected const string NUMBEROFISSUES = "NUMBEROFISSUES";


        protected readonly string jiraProject;
        protected readonly string productName;
        protected readonly Settings.AICoaching? AICoaching;
        protected readonly bool AddQATests;
        protected readonly bool AddSubTasks;
        protected readonly int maxStories;

        protected readonly StringBuilder sbCoaching = new();
        protected String targetPrepend = "";

        protected readonly UserStoryGenerator.Model.GFSGeminiClientHost gfsGeminiClientHost;

        public IssueGeneratorBase(IssueGeneratorBaseInputArgs args)
        {
            string key = args.Key ?? throw new NullReferenceException(nameof(args.Key));
            string target = args.Target ?? throw new NullReferenceException(nameof(args.Target));
            this.jiraProject = args.JiraProject ?? throw new NullReferenceException(nameof(args.JiraProject));
            this.productName = args.ProductName ?? throw new NullReferenceException(nameof(args.ProductName));

            this.AICoaching = args.AICoaching;
            this.AddQATests = args.AddQATests;
            this.AddSubTasks = args.AddSubTasks;
            this.maxStories = args.MaxStories;

            gfsGeminiClientHost = new(key, AIType.GenerativeAI);
            gfsGeminiClientHost.LookupCompleted += LookupCompleted;

            string query = BuildQuery(target.Trim());
            gfsGeminiClientHost.Query = query.Replace(Environment.NewLine, " ").Trim();

        }

        public void RequestAnswer()
        {
            try
            {
                Thread thread = new(async () =>
                {
                    try
                    {
                        //throw new HttpRequestException("ServiceUnavailable");

                        await gfsGeminiClientHost.RequestAnswer();
                    }
                    catch( System.Net.Http.HttpRequestException ex )
                    {
                        //RequestFailed requestFailedTEST = new();//RequestFailed
                        //requestFailedTEST.error = new RequestFailed.Error();
                        //requestFailedTEST.error.message = "RequestFailed";
                        //Error?.Invoke(requestFailedTEST);
                        //return;

                        if( ex.Message.Contains("ServiceUnavailable") )
                        {
                            string errorMsg = ex.Message.Replace("Request failed with Status Code: ServiceUnavailable", "");
                            errorMsg = errorMsg.Replace("Request failed", "");

                            RequestFailed? requestFailed = JsonSerializer.Deserialize<RequestFailed>(errorMsg);
                            Error?.Invoke(requestFailed);
                            //
                        }
                        else if( ex.Message.Contains("BadRequest") )
                        {
                            string errorMsg = ex.Message.Replace("Request failed with Status Code: BadRequest", "");
                            errorMsg = errorMsg.Replace("Request failed", "");

                            BadRequestError? badRequestError = JsonSerializer.Deserialize<BadRequestError>(errorMsg);
                            Error?.Invoke(badRequestError);

                        }
                        else
                        {
                        }
                    }
                    catch( System.NullReferenceException )
                    {
                    }
                    catch( System.ArgumentException )
                    {
                    }
                    catch( Exception )
                    {
                    }
                });
                thread.Start();

            }
            catch( System.Net.Http.HttpRequestException ex )
            {
                string errorMsg = ex.Message.Replace("Request failed with Status Code: BadRequest", "");
                errorMsg = errorMsg.Replace("Request failed", "");

                BadRequestError? badRequestError = JsonSerializer.Deserialize<BadRequestError>(errorMsg);
            }
            catch( System.ArgumentException )
            {
            }
            catch( Exception )
            {
            }

        }

        protected virtual string BuildQuery(string? target)
        {
            sbCoaching.AppendLine($"JIRA Product:{jiraProject}");
            sbCoaching.AppendLine($"Product Name: \"{productName.Trim()}\"");

            if( target != null )
                sbCoaching.AppendLine($"{targetPrepend}\"{target.Trim()}\"");

            sbCoaching.AppendLine("");

            if( this.AICoaching != null )
                sbCoaching.AppendLine(this.AICoaching.IssueInstructions);

            // add any QATests or Subtasks if instructed
            AddAdditionalInstructions();

            string result = sbCoaching.ToString();

            if( result.Contains(NUMBEROFISSUES) )
                result = result.Replace(NUMBEROFISSUES, maxStories.ToString());

            return result;
        }

        protected virtual void AddAdditionalInstructions()
        {
            if( this.AICoaching != null )
            {
                if(
                    ( AICoaching.QATestInstructions != null )
                    ||
                    ( AICoaching.SubTaskInstructions != null )
                    )
                {
                    sbCoaching.AppendLine($"Beginning of {ADDITIONALINSTRUCTIONS}");

                    if( AddQATests )
                        sbCoaching.AppendLine(AICoaching.QATestInstructions);
                    else
                        sbCoaching.AppendLine(QATESTSTRUCTIONS);

                    sbCoaching.AppendLine("");

                    if( AddSubTasks )
                        sbCoaching.AppendLine(AICoaching.SubTaskInstructions);
                    else
                        sbCoaching.AppendLine(NOSUBTASKINSTRUCTIONS);

                    sbCoaching.AppendLine($"End of {ADDITIONALINSTRUCTIONS}");

                }
            }
        }

        private void LookupCompleted(Result result)
        {
            //IList<string>? answers = gfsGeminiClientHost.Answers;
            //if( answers == null )
            //{
            //    //Completed?.Invoke(-1, null);//|| answers.Count == 0
            //    OnCompleted(null);
            //    return;
            //}

            //string answer = answers.First();

            //Completed?.Invoke(userStoryKey, answer);
            OnCompleted(result);

        }

        protected virtual void OnCompleted(Result result)
        {
            IssueGeneratorBaseArgs args = new(result);
            Completed?.Invoke(args);
        }
    }

    public class IssueGeneratorBaseInputArgs
    {
        public Settings.AICoaching? AICoaching { get; internal set; }
        public string? Key { get; internal set; }
        public string? JiraProject { get; internal set; }
        public string? Target { get; internal set; }
        public string? ProductName { get; internal set; }
        public bool AddQATests { get; internal set; }
        public bool AddSubTasks { get; internal set; }
        public int MaxStories { get; internal set; }

    }



}