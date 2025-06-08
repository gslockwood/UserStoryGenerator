using System.Text.Json;
using UserStoryGenerator.View;
using static UserStoryGenerator.Model.IssueData;

namespace UserStoryGenerator.Model
{
    public class Model
    {
        private const string V = "google cloud gemini ai api key";
        public static string DEFAULTKEY = V;

        readonly JsonSerializerOptions options = new() { WriteIndented = true };
        public Settings Settings { get; private set; }

        public void SetSettings(Settings settings) { Settings = settings; }

        public string SettingsFileName { get; private set; } = @$".\Settings.json";

        public Model()
        {

            try
            {
                string? json = null;
                if( !File.Exists(SettingsFileName) )//throw new FileNotFoundException($"{SettingsFileName} is not found");
                    json = UserStoryGenerator.Properties.Resources.Settings;
                else
                    json = File.ReadAllText(SettingsFileName);

                // testing
                //json = UserStoryGenerator.Properties.Resources.Settings;

                Settings? temp = JsonSerializer.Deserialize<Settings>(json);
                /*
                temp.UserStoryCoaching = new Settings.AICoaching();
                string coachingAI = File.ReadAllText(@".\AI Coaching For User Stories.json");
                temp.UserStoryCoaching.IssueInstructions = coachingAI;
                temp.UserStoryCoaching.QATestInstructions = "Every Story shall include a set of \"Integrated Test\" \"Test\" LinkedIssues with associated \"Sub-task\" issues as seemed fit unless instructed not to create subtask issues.\r\nThe \"Integrated Test \"Test\" Issues Summary field shall be prepended with \"Integrated Test: \". ";
                temp.UserStoryCoaching.SubTaskInstructions = "Every Story shall include a set of Documentation \"Sub-task\" issues as seemed fit that are placed in the \"Subtasks\" collection. ";

                temp.AllIssueCoaching = new Settings.AICoaching();
                temp.AllIssueCoaching.IssueInstructions = "Goal: \r\nUsing the \"Fundamental Instructions\" given below, please analyze the provided \"User Story\" and use the provided \"JIRA Product\" below and create a rich, robust, thorough collection of Jira Issues from \"User Story\". \r\n\r\nIf \"Additional Instructions\" are present, incorporate them into the \"Fundamental Instructions\". \r\n\r\nPer the provided \"User Story\", generate dozens of \"Story\", \"Task\", and unless expressedly told otherwise, \"Test\" issues, and unless expressedly told otherwise, generate, associated ALL Sub-tasks as defined by the \"Instructions\".  \r\n\r\nIMPORTANT: Don't include the provided \"User Story\" in the response.\r\n\r\nThoroughly break down the complete technical implementation of the story into a long list of technical Tasks covering all software layes thoroughly.  \r\n\r\nThe technical software layers included: Frontend, Backend, API, Database, Services layers. \r\n\r\n\r\nCoaching:\r\nBe creative, read between the lines to imagine robust plentiful 'inbetween' functionalities. \r\nGenerate new issues freely and generously as instructed below in the \"Fundamental Instructions\" and the \"Additional Instructions\". \r\n\r\n\r\nBeginning of \"Fundamental Instructions\":\r\n\r\nResults Format: \r\nThe \"Issues\" collection schema is: \"Issues\" : [] \r\nThe Jira Issue schema is {\"Summary\",\"IssueType\",\"Product\",\"Subtasks\": [],\"LinkedIssues\": []} \r\nThe \"IssueType\" are: \"Story\",\"Task\",\"Test\",\"Bug\" \r\nThe \"Sub-task\" schema is: {\"Summary\",\"IssueType\",\"Product\" } \r\n\r\n\r\nDefiitions and Rules:\r\nThe Issue's \"Product\" field shall be defined by the provided \"JIRA Product\" text unchanged. \r\n\r\nNever use the the provided \\\"JIRA Product\\\" in any issue Summary field.\r\n\r\nUser stories are directly linked to delivering user or business value.   User stories define the \"what\" (the user's need) and \"why\" while technical tasks detail the business value \"why\" (the implementation details needed to fulfill that need). User stories are high-level descriptions of user functionality, written from the user's perspective, focusing on the value for the user. Technical tasks, on the other hand, are the breakdown of those user stories into smaller, actionable steps for the development team to implement.\r\n\r\nIMPORTANT: Therefore: Stories should receive numerous \"Task\" issues that are placed in the Story's \"LinkedIssues\" collection. \r\n\r\nOverall structure: the heirarchy issue is:  Story /  x Tasks / y Tests.  Stories have many Tasks and Tasks have many Tests. \r\n\r\nIt is rare but possible that Tasks are at the Story level.\r\n\r\nEvery Story should have many, multiple Tasks linked to it regarding the technical implementaion of the user story in the software layers;\r\n\r\nStories may have Tests in the Story's \"LinkedIssues\" collection as well but most Test issues are attached the Tasks. \r\n\r\n\r\n\"Sub-task\" issues:\r\nSub-task \"IssueType\" field shall be: \"Sub-task\"\r\nSub-task issues shall be placed in their Parent issues's \"Subtasks\" collection.\r\n\r\n\"Story\" issues:\r\nSome \"Story\" issues can receive several \"Sub-tasks\" issues created and placed in the Story's \"Sub-tasks\" collection. \r\n\r\n\"Task\" issues:\r\nTask Issue \"IssueType\" field shall be: \"Task\". \r\nIf directed to do so, every \"Task\" should receive several Sub-task issues created and placed in the Task's \"Sub-tasks\" collection. \r\n\r\nTest issues:\r\nTest Issue Summary field shall begin with: \"QA Test\". \r\nTest Issue \"IssueType\" field shall be: \"Test\". \r\n\r\nEnd of \"Fundamental Instructions\" \r\n\r\n";
                temp.AllIssueCoaching.QATestInstructions = "Every generated Task shall include a set Manual \"Test\" issues and a set of \"Automated Resting\" \"Test\" issues as seemed fit.\r\n\r\nEvery generated Story shall include a set of \"Integrated Test\" \"Test\" issues and associated \"Sub-task\" issues as seemed fit.\r\n\r\nUnless directed otherwise, Every \"Task\" should receive several \"Test\" issues created and placed in the Task's \"LinkedIssues\" collection. \r\n";
                temp.AllIssueCoaching.SubTaskInstructions = "Every Story issues shall have Documentation sub-tasks.\r\n\r\nEvery \"Frontend Task\" shall receive at least one or many \"Implement UI Control x\" Sub-task issue for each UI control in the implementation. Each is placed in the Task's \"Sub-tasks\" collection. \r\nEvery \"Database Task\" shall receive a \"Implement CRUD method: x\" Sub-task issue where x is of the list: {create, read, update, and delete} in the implementation. Each is placed in the Task's \"Sub-tasks\" collection.  There shall be a corresponding TDD test Sub-task issue for each. \r\n\r\nEvery \"API Task\" shall receive a \"Implement HTTP method: x\" Sub-task issue where x is of the list: {GET,POST,PUT,PATCH,DELETE} in the implementation. Each is placed in the Task's \"Sub-tasks\" collection. There shall be a corresponding TDD test Sub-task issue for each. \r\n\r\nThere shall be \"Backend\" Tasks take cover relevant Database queries and Service usage functionality. There shall be a corresponding TDD test Sub-task issues for each. \r\n\r\n\r\nTest Issue Sub-tasks: \r\nIf directed to include Test issues, there shall be subtasks for all and any \"Test Automation\" as required by the Test Issue's Parent Task. \r\nIf directed to include Test issues, there shall be subtasks for all and any \"Integration Tests\" as required by the Test Issue's Parent Task. \r\nIf directed to include Test issues, there shall be subtasks for all and any Manual Workflow Validation Tests\" as required by the Test Issue's Parent Task. ";
                */


                Settings = new Settings();
                if( temp != null )
                    Settings = temp;

                //this.SaveSettings(); //testing only

                //Tests();

            }
            catch( Exception )
            {
                throw;
            }

        }

        private void Tests()
        {
            UserStoryGenerator.Tests.RegexValidation.EpicNames();

            //UserStoryGenerator.Tests.IssueResults.BlankEpicName();
            UserStoryGenerator.Tests.IssueResults.DescriptiveEpicName();
            //UserStoryGenerator.Tests.IssueResults.ExistingEpicName();
        }

        public void SaveSettings()
        {
            File.WriteAllText(SettingsFileName, JsonSerializer.Serialize(Settings, options));
        }

        TreeSerialization.IssueResults userStoryResults = new TreeSerialization.IssueResults();

        internal void SaveDataToFile(string epicText, List<string> storyList, List<TreeNode> checkedHierarchy)
        {
            List<IssueData.Issue> serializableIssues = TreeSerialization.Convert(checkedHierarchy);

            userStoryResults = new TreeSerialization.IssueResults()
            {
                IssueList = storyList,
                HierarchyIssueList = serializableIssues
            };



            string result = JsonSerializer.Serialize(userStoryResults);//options

            SaveUserStoryResultsToJson(result, @$".\UserStores.json");


            // csv
            try
            {
                //userStoryResults = null;  testing


                //string csv = Converter.ToCSV(epicText, userStoryResults);
                //Logger.Info(csv);
                //File.WriteAllText("./Issues.csv", csv);

                SaveUserStoryResultsToCSV("./Issues.csv", epicText);

            }
            catch( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }

            //goto asdf;
        }

        internal List<string>? GetStories()
        {
            //if( userStoryResults.IssueList == null || userStoryResults.IssueList.Count == 0 ) 
            //    throw new NullReferenceException(nameof(userStoryResults.IssueList));

            return userStoryResults.IssueList;
        }

        public bool SaveUserStoryResultsToCSV(string fullFilePath, string epicText)
        {
            if( userStoryResults.IssueList == null || userStoryResults.IssueList.Count == 0 ) return false;
            string csv = Converter.ToCSV(epicText, userStoryResults);
            File.WriteAllText("./Issues.csv", csv);
            return true;
        }
        public bool SaveUserStoryResultsToJson(string fullFilePath)
        {
            if( userStoryResults.IssueList == null || userStoryResults.IssueList.Count == 0 ) return false;
            string result = JsonSerializer.Serialize(userStoryResults, options);
            SaveUserStoryResultsToJson(result, fullFilePath);
            return true;
        }
        private void SaveUserStoryResultsToJson(string result, string fullFilePath)
        {
            //UserStoryGenerator.Utilities.Logger.Info(result);
            File.WriteAllText(fullFilePath, result);
        }

        public delegate void CompletedUserStoryEventHandler(IssueGeneratorBaseArgs answer);
        public event CompletedUserStoryEventHandler? UserStoryGeneratorCompleted;

        internal async Task ProduceUserStories(string jiraProject, string productName, string target, bool addQATests, bool addSubTasks, int maxStories)
        {
            if( Settings == null ) throw new NullReferenceException(nameof(Settings));
            if( Settings.Key == null ) throw new NullReferenceException(nameof(Settings.Key));

            IssueGeneratorBaseInputArgs args = new()
            {
                Key = Settings.Key,
                JiraProject = jiraProject,
                ProductName = productName,
                Target = target,
                AddQATests = addQATests,
                AddSubTasks = addSubTasks,
                AICoaching = Settings.UserStoryCoaching,
                MaxStories = maxStories
            };

            IssueGeneratorUserStories? issueGenerator = new(args);
            issueGenerator.Completed += (args) =>
            {
                UserStoryGeneratorCompleted?.Invoke(args);
            };
            issueGenerator.Error += () =>
            {
            };

            await Task.Delay(0);// this because RequestAnswer isn't really async
            issueGenerator.RequestAnswer();
        }


        public delegate void CompletedEventHandler(IssueGeneratorBaseArgsEx args);
        public event CompletedEventHandler? IssueGeneratorCompleted;


        //IssueGenerator? issueGenerator = null;
        internal async Task ProcessSingleStory(long userStoryKey, string jiraProject, string productName, string userStoryText, bool addQATests, bool addSubTasks, int maxStories)
        {
            if( Settings == null ) throw new NullReferenceException(nameof(Settings));
            if( Settings.Key == null ) throw new NullReferenceException(nameof(Settings.Key));
            if( jiraProject == null ) throw new NullReferenceException(nameof(jiraProject));
            if( productName == null ) throw new NullReferenceException(nameof(productName));

            IssueGeneratorBaseInputArgs args = new()
            {
                Key = Settings.Key,
                JiraProject = jiraProject,
                ProductName = productName,
                Target = userStoryText,
                AddQATests = addQATests,
                AddSubTasks = addSubTasks,
                AICoaching = Settings.AllIssueCoaching,
                MaxStories = maxStories
            };

            IssueGeneratorUserStories? issueGenerator = new(args);
            issueGenerator.Completed += (args) =>
            {
                GFSGeminiClientHost.Result result = args.Result;
                IssueGeneratorBaseArgsEx issueGeneratorBaseArgsEx = new(result, 0, userStoryKey);
                IssueGeneratorCompleted?.Invoke(issueGeneratorBaseArgsEx);
                issueGenerator = null;
            };
            issueGenerator.Error += () =>
            {
            };

            await Task.Delay(0);// this because RequestAnswer isn't really async
            issueGenerator.RequestAnswer();

        }

        //public List<StoryPackage> successList = [];


        internal async Task ProcessStoryList(string productName, bool addQATests, bool addSubTasks, int maxStories, List<StoryPackage> list)//async Task 
        {
            if( Settings == null ) throw new NullReferenceException(nameof(Settings));
            if( Settings.Key == null ) throw new NullReferenceException(nameof(Settings.Key));

            int counter = list.Count;
            int index0 = 0;
            int index = 0;

            //successList = [];

            foreach( StoryPackage storyPackage in list )
            {
                if( storyPackage.JiraProduct == null ) continue;

                IssueGeneratorBaseInputArgs args = new()
                {
                    Key = Settings.Key,
                    JiraProject = storyPackage.JiraProduct,
                    ProductName = productName,
                    Target = storyPackage.UserStoryText,
                    AddQATests = addQATests,
                    AddSubTasks = addSubTasks,
                    AICoaching = Settings.AllIssueCoaching,
                    MaxStories = maxStories
                };

                IssueGeneratorUserStories? issueGenerator = new(args);
                issueGenerator.Completed += (args) =>
                {
                    //Logger.Info($"ProcessStoryList: Received{index++}");
                    //Utilities.Logger.Info($"Model:GfsGeminiClientHost_LookupCompleted: {counter}");
                    IssueGeneratorBaseArgsEx issueGeneratorBaseArgsEx = new(args.Result, --counter, storyPackage.Key);
                    IssueGeneratorCompleted?.Invoke(issueGeneratorBaseArgsEx);
                };
                issueGenerator.Error += () =>
                {
                };

                //Logger.Info($"ProcessStoryList:issueGenerator Requesting Answer: {index0++}");


                await Task.Delay(0);// this because RequestAnswer isn't really async
                issueGenerator.RequestAnswer();

            }

            //Logger.Info($"ProcessStoryList:Started:complete");

        }


    }

    public class StoryPackage
    {
        public long Key { get; }

        public string UserStoryText { get; }
        public string? JiraProduct { get; }

        public StoryPackage(TriStateTreeView.TreeNodeEx treeNodeEx)
        {
            Key = treeNodeEx.Key;
            JiraProduct = treeNodeEx.Product;
            UserStoryText = treeNodeEx.Text;
        }

        public StoryPackage(long userStoryKey, string project, string userStoryText)
        {
            Key = userStoryKey;
            JiraProduct = project;
            UserStoryText = userStoryText;
        }
    }

    public interface IIssue
    {
        string? Product { get; set; }
        string? Summary { get; set; }
        string? IssueType { get; set; }
        long Key { get; set; }
    }

    public class IssueDataBase : IIssue
    {
        public string? Summary { get; set; }
        public string? IssueType { get; set; }
        public string? Product { get; set; }
        public long Key { get; set; } = new Random().Next() * ( uint.MaxValue / int.MaxValue ) + (uint)new Random().Next(0, 2) * ( uint.MaxValue % int.MaxValue );
    }

    public class IssueData
    {
        public List<Issue>? Issues { get; set; }

        public class Issue : IssueDataBase
        {
            public List<SubTask>? Subtasks { get; set; }
            public List<Issue>? LinkedIssues { get; set; }

        }
        public class SubTask : IssueDataBase { }
    }

    public static class IssueExtensions
    {
        // This method recursively flattens the hierarchy
        public static IEnumerable<Issue> FlattenStandardIssues(this IEnumerable<Issue> source)
        {
            foreach( var issue in source )
            {
                yield return issue; // Yield the current issue
                if( issue.LinkedIssues != null )
                {
                    // Recursively yield all children
                    foreach( var child in issue.LinkedIssues.FlattenStandardIssues() )
                    {
                        yield return child;
                    }
                }
            }
        }

    }

    public class JiraIssueTypes
    {
        public const String STORY = "Story";
        public const String TASK = "Task";
        public const String TEST = "Test";
        public const String BUG = "Bug";
        public const String EPIC = "Epic";
        public const String SUBTASK = "Sub-task";
    }

}