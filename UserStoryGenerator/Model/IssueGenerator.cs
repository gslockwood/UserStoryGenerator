using System.Text;
using System.Text.Json;
using static UserStoryGenerator.Model.GFSGeminiClientHost;

namespace UserStoryGenerator.Model
{
    public class IssueGeneratorBase
    {
        public delegate void CompletedEventHandler(IssueGeneratorBaseArgs args);
        public event CompletedEventHandler? Completed;
        public delegate void ErrorEventHandler();
        public event ErrorEventHandler? Error;

        protected readonly string jiraProject;
        protected readonly string productName;

        public Settings.AICoaching? AICoaching { get; }


        protected const string ADDITIONALINSTRUCTIONS = "Additional Instructions";
        protected const string QATESTSTRUCTIONS = "Under no circumstances generate 'Test' issues";
        protected const string NOSUBTASKINSTRUCTIONS = "Under no circumstances generate 'Sub-task' issues";

        protected bool AddQATests { get; }
        protected bool AddSubTasks { get; }

        protected StringBuilder sbCoaching = new();

        protected String targetPrepend = "";

        protected readonly UserStoryGenerator.Model.GFSGeminiClientHost gfsGeminiClientHost;

        public IssueGeneratorBase(string key, string jiraProject, string productName, string target, bool addQATests, bool addSubTasks, Settings.AICoaching? aiCoaching)
        {
            this.jiraProject = jiraProject ?? throw new ArgumentNullException(nameof(IssueGeneratorBase.jiraProject));
            this.productName = productName ?? throw new ArgumentNullException(nameof(IssueGeneratorBase.productName));

            this.AICoaching = aiCoaching;
            this.AddQATests = addQATests;
            this.AddSubTasks = addSubTasks;

            gfsGeminiClientHost = new(key, AIType.GenerativeAI);
            gfsGeminiClientHost.LookupCompleted += LookupCompleted;

            string query = BuildQuery(target.Trim());
            gfsGeminiClientHost.Query = query.Replace(Environment.NewLine, " ").Trim();

        }

        //public async Task RequestAnswer()
        public void RequestAnswer()
        {
            try
            {
                Thread thread = new(async () =>
                {
                    await gfsGeminiClientHost.RequestAnswer();
                });
                thread.Start();

                //await gfsGeminiClientHost.RequestAnswer();
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


            return sbCoaching.ToString();
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

        private void LookupCompleted()
        {
            IList<string>? answers = gfsGeminiClientHost.Answers;
            if( answers == null )
            {
                //Completed?.Invoke(-1, null);//|| answers.Count == 0
                OnCompleted(null);
                return;
            }
            string answer = answers.First();
            //Completed?.Invoke(userStoryKey, answer);
            OnCompleted(answer);

        }

        protected virtual void OnCompleted(string? answer)
        {
            if( answer == null )
                Error?.Invoke();

            else
            {
                IssueGeneratorBaseArgs args = new(answer);
                Completed?.Invoke(args);
            }
        }
    }

}