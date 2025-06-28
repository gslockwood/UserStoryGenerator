using System.Text;
using System.Text.Json;
using static UserStoryGenerator.Model.GFSGeminiClientHost;
using static UserStoryGenerator.Model.Settings;

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
        protected const string QATESTSTRUCTIONS = "OVERRIDE earlier instruction: Under no circumstances generate 'Test' issues. \n";
        protected const string NOSUBTASKINSTRUCTIONS = "OVERRIDE earlier instruction: Under no circumstances generate 'Sub-task' issues. \n";
        protected const string NODESCRIPTIONSINSTRUCTIONS = "OVERRIDE earlier instruction: Do not, under any circumstances, create description text for the \"Description\" field of any \"Standard\" or \"Sub-task\" Issue. Set the \"Description\" field to null. \n";
        protected const string NUMBEROFISSUES = "NUMBEROFISSUES";


        protected readonly string jiraProject;
        protected readonly string productName;
        protected readonly Settings.AICoaching? AICoaching;
        protected readonly bool AddQATests;
        protected readonly bool AddSubTasks;
        protected readonly bool AddDescriptions;
        protected readonly int maxStories;
        private readonly Dictionary<string, Settings.JiraIssueType>? jiraIssueTypes;
        private readonly string? fundamentalInstructions;
        protected readonly StringBuilder sbCoaching = new();
        protected String targetPrepend = "";

        protected readonly UserStoryGenerator.Model.GFSGeminiClientHost gfsGeminiClientHost;

        public IssueGeneratorBase(IssueGeneratorBaseInputArgs args)
        {
            string key = args.Key ?? throw new NullReferenceException(nameof(args.Key));
            string target = args.Target ?? throw new NullReferenceException(nameof(args.Target));
            if( args.Settings == null ) throw new NullReferenceException(nameof(args.Settings));

            this.jiraProject = args.JiraProject ?? throw new NullReferenceException(nameof(args.JiraProject));
            this.productName = args.ProductName ?? throw new NullReferenceException(nameof(args.ProductName));

            this.AICoaching = args.AICoaching;
            this.AddQATests = args.AddQATests;
            this.AddSubTasks = args.AddSubTasks;
            this.AddDescriptions = args.AddDescriptions;
            this.maxStories = args.MaxStories;
            this.jiraIssueTypes = args.Settings.JiraIssueTypes;// args.JiraIssueTypes;
            this.fundamentalInstructions = args.Settings.FundamentalInstructions;//.FundamentalInstructions;

            gfsGeminiClientHost = new(key, AIType.GenerativeAI, args.Settings.GeminiModel);
            gfsGeminiClientHost.LookupCompleted += LookupCompleted;

        }

        public void RequestAnswer()
        {
            try
            {
                _ = gfsGeminiClientHost.RequestAnswer();
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
            catch( System.ArgumentException )
            {
            }
            catch( Exception )
            {
            }

        }
        private void LookupCompleted(Result result)
        {
            IssueGeneratorBaseArgs args = new(result);
            Completed?.Invoke(args);

            //OnCompleted(result);
        }

        //protected virtual void OnCompleted(Result result)
        //{
        //    IssueGeneratorBaseArgs args = new(result);
        //    Completed?.Invoke(args);
        //}

        protected virtual string BuildQuery(string? target)
        {
            sbCoaching.AppendLine($"JIRA Product:{jiraProject}");
            sbCoaching.AppendLine($"Product Name: \"{productName.Trim()}\"");

            if( target != null )
                sbCoaching.AppendLine($"{targetPrepend}\"{target.Trim()}\"");

            sbCoaching.AppendLine("");

            if( this.AICoaching != null && AICoaching.IssueInstructions != null && jiraIssueTypes != null )
                sbCoaching.AppendLine(this.AICoaching.IssueInstructions);

            if( fundamentalInstructions != null )
            {
                sbCoaching.AppendLine("Beginning of Fundamental Instructions");
                string tempFundamentalInstructions = fundamentalInstructions;

                if( tempFundamentalInstructions.Contains("ISSUETYPEDEFINITIONS") )////\"Story\",\"Task\",\"Test\",\"Bug\"
                {
                    StringBuilder issueTypeDefinitions = new();

                    if( jiraIssueTypes != null )
                    {
                        foreach( Settings.JiraIssueType? jiraIssue in jiraIssueTypes.Values )
                        {
                            if( jiraIssue.Order == 1 )
                                issueTypeDefinitions.Append($"\"{jiraIssue.IssueType}\"" + ",");
                        }
                    }

                    tempFundamentalInstructions = tempFundamentalInstructions.Replace("ISSUETYPEDEFINITIONS", issueTypeDefinitions.ToString().TrimEnd(','));
                    //
                }

                sbCoaching.AppendLine(tempFundamentalInstructions);
                sbCoaching.AppendLine("Ending of Fundamental Instructions");
                sbCoaching.AppendLine("");
            }


            // add any QATests or Subtasks if instructed
            AddAdditionalInstructions();

            string result = sbCoaching.ToString();

            if( result.Contains(NUMBEROFISSUES) )
                result = result.Replace(NUMBEROFISSUES, maxStories.ToString());

            //UserStoryGenerator.Utilities.Logger.Info(result);

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

                    if( !AddDescriptions )
                        sbCoaching.AppendLine(NODESCRIPTIONSINSTRUCTIONS);

                    sbCoaching.AppendLine($"End of {ADDITIONALINSTRUCTIONS}");

                }
            }
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
        public Settings? Settings { get; internal set; }
        public bool AddDescriptions { get; internal set; }
    }



}