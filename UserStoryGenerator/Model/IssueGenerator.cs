using System.Net.Mime;
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

        protected const string ADDITIONAL_INSTRUCTIONS = "Additional Instructions";
        protected const string QATESTS_INSTRUCTIONS = "OVERRIDE earlier instruction: Under no circumstances generate 'Test' issues. \n";
        protected const string NO_SUBTASKS_INSTRUCTIONS = "OVERRIDE earlier instruction: Under no circumstances generate 'Sub-task' issues. \n";
        protected const string NO_DESCRIPTIONS_INSTRUCTIONS = "OVERRIDE earlier instruction: Do not, under any circumstances, create description text for the \"Description\" field of any \"Standard\" or \"Sub-task\" Issue. Set the \"Description\" field to null. \n";
        protected const string NO_ESTIMATES_INSTRUCTIONS = "OVERRIDE earlier instruction: Do not, under any circumstances, generate StoryPoint estimates for the \"StoryPoints\" field of any issue. Set the \"StoryPoints\" field to 0. \n";
        protected const string NUMBER_OF_ISSUES = "NUMBEROFISSUES";


        protected readonly string jiraProject;
        protected readonly string productName;
        protected readonly Settings.AICoaching? AICoaching;
        protected readonly bool AddQATests;
        protected readonly bool AddSubTasks;
        protected readonly bool AddDescriptions;
        protected readonly bool AddEstimates;
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
            if( args.Settings.GeminiModel == null ) throw new NullReferenceException(nameof(args.Settings.GeminiModel));
            if( args.Customization == null ) throw new NullReferenceException(nameof(args.Customization));

            this.jiraProject = args.JiraProject ?? throw new NullReferenceException(nameof(args.JiraProject));
            this.productName = args.ProductName ?? throw new NullReferenceException(nameof(args.ProductName));

            this.AICoaching = args.AICoaching;
            this.AddQATests = args.Customization.AddQATests;
            this.AddSubTasks = args.Customization.AddSubTasks;
            this.AddDescriptions = args.Customization.AddDescriptions;
            this.AddEstimates = args.Customization.AddEstimates;

            this.maxStories = args.MaxStories;
            this.jiraIssueTypes = args.Settings.JiraIssueTypes;// args.JiraIssueTypes;
            this.fundamentalInstructions = args.Settings.FundamentalInstructions;//.FundamentalInstructions;

            gfsGeminiClientHost = new(key, AIType.GenerativeAI, args.Settings.GeminiModel);

            if( args.Settings.GeminiModel.Contains("Gemma", StringComparison.CurrentCultureIgnoreCase) )
                gfsGeminiClientHost.ResponseMimeType = MediaTypeNames.Text.Plain;// "text/plain";

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

                if( tempFundamentalInstructions.Contains("ISSUETYPEDEFINITIONS") )
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

            if( result.Contains(NUMBER_OF_ISSUES) )
                result = result.Replace(NUMBER_OF_ISSUES, maxStories.ToString());

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
                    sbCoaching.AppendLine($"Beginning of {ADDITIONAL_INSTRUCTIONS}");

                    if( AddQATests )
                        sbCoaching.AppendLine(AICoaching.QATestInstructions);
                    else
                        sbCoaching.AppendLine(QATESTS_INSTRUCTIONS);

                    sbCoaching.AppendLine("");

                    if( AddSubTasks )
                        sbCoaching.AppendLine(AICoaching.SubTaskInstructions);
                    else
                        sbCoaching.AppendLine(NO_SUBTASKS_INSTRUCTIONS);

                    if( !AddDescriptions )
                        sbCoaching.AppendLine(NO_DESCRIPTIONS_INSTRUCTIONS);

                    if( !AddEstimates )
                        sbCoaching.AppendLine(NO_ESTIMATES_INSTRUCTIONS);

                    sbCoaching.AppendLine($"End of {ADDITIONAL_INSTRUCTIONS}");

                }
            }
        }

    }

    public class Customization
    {
        public bool AddQATests { get; internal set; }
        public bool AddSubTasks { get; internal set; }
        public bool AddDescriptions { get; internal set; }
        public bool AddEstimates { get; internal set; }
    }

    public class IssueGeneratorBaseInputArgs
    {
        public Settings.AICoaching? AICoaching { get; internal set; }
        public string? Key { get; internal set; }
        public string? JiraProject { get; internal set; }
        public string? Target { get; internal set; }
        public string? ProductName { get; internal set; }
        //public bool AddQATests { get; internal set; }
        //public bool AddSubTasks { get; internal set; }
        //public bool AddDescriptions { get; internal set; }

        public int MaxStories { get; internal set; }
        public Settings? Settings { get; internal set; }
        public Customization? Customization { get; internal set; }
    }



}