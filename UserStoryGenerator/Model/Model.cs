using System.Text.Json;
using UserStoryGenerator.View;
using static UserStoryGenerator.Model.IssueData;

namespace UserStoryGenerator.Model
{
    public class Model
    {
        public readonly static string DEFAULTKEY = "google cloud gemini ai api key";

        readonly JsonSerializerOptions options = new() { WriteIndented = true };
        public Settings Settings { get; private set; }

        public void SetSettings(Settings settings) { Settings = settings; }

        //public string SettingsFileName { get; private set; } = @$".\Settings.json";

        public delegate void CompletedUserStoryEventHandler(IssueGeneratorBaseArgs answer);
        public event CompletedUserStoryEventHandler? UserStoryGeneratorCompleted;
        public delegate void CompletedEventHandler(IssueGeneratorBaseArgsEx args);
        public event CompletedEventHandler? IssueGeneratorCompleted;

        public event IssueGeneratorBase.CompletedInErrorEventHandler? CompletedInError;


        public Model()
        {
            try
            {
                string? json = null;
                if( !File.Exists(@$".\Settings.json") )//throw new FileNotFoundException($"{SettingsFileName} is not found");
                    json = UserStoryGenerator.Properties.Resources.Settings;
                else
                    json = File.ReadAllText(@$".\Settings.json");

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
#if DEBUG
        private static void Tests()
        {
            UserStoryGenerator.Tests.RegexValidation.EpicNames();

            //UserStoryGenerator.Tests.IssueResults.BlankEpicName();
            UserStoryGenerator.Tests.IssueResults.DescriptiveEpicName();
            //UserStoryGenerator.Tests.IssueResults.ExistingEpicName();
        }
#endif
        //public void SaveSettings()
        //{
        //    File.WriteAllText(SettingsFileName, JsonSerializer.Serialize(Settings, options));
        //}

        TreeSerialization.IssueResults userStoryResults = new();

        internal void SaveDataToFile(string epicText, List<string> storyList, List<TreeNode> checkedHierarchy)
        {
            List<IssueData.Issue> serializableIssues = TreeSerialization.Convert(checkedHierarchy);

            userStoryResults = new TreeSerialization.IssueResults()
            {
                UserStoryList = storyList,
                Issues = serializableIssues
            };

            try
            {
                string result = JsonSerializer.Serialize(userStoryResults);//options

                SaveUserStoryResultsToJson(@$".\Generated Jira Issues.json", result);

                //userStoryResults = null;  testing

                //string csv = Converter.ToCSV(epicText, userStoryResults);
                //Logger.Info(csv);
                //File.WriteAllText("./Issues.csv", csv);

                SaveUserStoryResultsToCSV("./Jira Import.csv", epicText);

            }
            catch( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }

            //goto asdf;
        }

        internal List<string>? GetStorySummaries()
        {
            return userStoryResults.UserStoryList;
        }

        public bool SaveUserStoryResultsToCSV(string fullFilePath, string epicText)
        {
            if( userStoryResults.UserStoryList == null || userStoryResults.UserStoryList.Count == 0 ) return false;
            string csv = Converter.ToCSV(epicText, userStoryResults);
            File.WriteAllText(fullFilePath, csv);
            return true;
        }
        public bool SaveUserStoryResultsAsJson(string fullFilePath, string? productDescription)
        {
            if( userStoryResults.UserStoryList == null || userStoryResults.UserStoryList.Count == 0 ) return false;
            userStoryResults.ProductDescription = productDescription;
            string result = JsonSerializer.Serialize(userStoryResults, options);
            SaveUserStoryResultsToJson(fullFilePath, result);
            return true;
        }
        private static void SaveUserStoryResultsToJson(string fullFilePath, string result)
        {
            //UserStoryGenerator.Utilities.Logger.Info(result);
            File.WriteAllText(fullFilePath, result);
        }

        private static List<Issue>? ProcessIssues(string json)
        {
            try
            {
                //Logger.Info(json);
                IssueData? issueData = JsonSerializer.Deserialize<IssueData>(json);
                if( issueData == null ) throw new NullReferenceException(nameof(issueData));
                return issueData.Issues;
            }
            catch( Exception )
            {
                throw;
            }
        }

        internal async Task ProduceUserStories(string jiraProject, string productName, string target, bool addQATests, bool addSubTasks, int maxStories)
        {
            if( Settings == null ) throw new NullReferenceException(nameof(Settings));
            if( Settings.Key == null ) throw new NullReferenceException(nameof(Settings.Key));

            IssueGeneratorBaseInputArgs issueGeneratorBaseInputArgs = new()
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

            IssueGeneratorUserStories? issueGenerator = new(issueGeneratorBaseInputArgs);
            issueGenerator.Completed += (args) =>
            {
                try
                {
                    GFSGeminiClientHost.Result result = args.Result;
                    if( result.Answer == null ) throw new NullReferenceException(nameof(result.Answer));

                    List<IssueData.Issue>? issues = ProcessIssues(result.Answer);
                    if( issues != null )
                    {
                        args.Issues = issues;
                    }

                }
                catch( Exception )
                {
                    //throw ex;
                }
                finally
                {
                    UserStoryGeneratorCompleted?.Invoke(args);
                }

            };

            issueGenerator.Error += (error) =>
            {
                CompletedInError?.Invoke(error);
            };

            await Task.Delay(0);// this because RequestAnswer isn't really async
            issueGenerator.RequestAnswer();
            //
        }

        internal async Task ProcessStoryList(string productName, bool addQATests, bool addSubTasks, int maxStories, List<StoryPackage> list)//async Task 
        {
            if( Settings == null ) throw new NullReferenceException(nameof(Settings));
            if( Settings.Key == null ) throw new NullReferenceException(nameof(Settings.Key));

            int counter = list.Count;

            foreach( StoryPackage storyPackage in list )
            {
                if( storyPackage.JiraProduct == null ) continue;

                IssueGeneratorBaseInputArgs issueGeneratorBaseInputArgs = new()
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

                IssueGeneratorUserStories? issueGenerator = new(issueGeneratorBaseInputArgs);
                issueGenerator.Completed += (args) =>
                {
                    //Logger.Info($"ProcessStoryList: Received{index++}");
                    //Utilities.Logger.Info($"Model:GfsGeminiClientHost_LookupCompleted: {counter}");
                    IssueGeneratorBaseArgsEx issueGeneratorBaseArgsEx = new(args.Result, --counter, storyPackage.Key);

                    GFSGeminiClientHost.Result result = args.Result;
                    try
                    {
                        if( result.Answer == null ) throw new NullReferenceException(nameof(result.Answer));

                        List<IssueData.Issue>? issues = ProcessIssues(result.Answer);
                        if( issues != null )
                        {
                            issueGeneratorBaseArgsEx.Issues = issues;
                        }

                    }
                    catch( Exception )
                    {
                        //throw ex;
                    }
                    finally
                    {
                        IssueGeneratorCompleted?.Invoke(issueGeneratorBaseArgsEx);
                    }
                };

                await Task.Delay(0);// this because RequestAnswer isn't really async
                try
                {
                    issueGenerator.RequestAnswer();
                }
                catch( Exception )
                {
                    throw;
                }
            }

        }

        internal string? CreateUserStories(string? json = null)
        {
            const string testJson = "{\"Issues\":[{\"IssueType\":\"Story\",\"Key\":2136038463,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":285410868,\"LinkedIssues\":[],\"Product\":\"RED Product:online store\",\"Subtasks\":null,\"Summary\":\"Integrated Test: Verify that users can successfully browse products with proper filtering and search functionalities.\"}],\"Product\":\"RED Product:online store\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3409469161,\"Product\":\"RED Product:online store\",\"Summary\":\"Create documentation for browsing functionality, including search and filter options.\"}],\"Summary\":\"As a customer, I want to browse products on the online store, so that I can find the items I am interested in purchasing.\"},{\"IssueType\":\"Story\",\"Key\":648900350,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":1116981987,\"LinkedIssues\":[],\"Product\":\"RED Product:online store\",\"Subtasks\":null,\"Summary\":\"Integrated Test: Verify that users can successfully add products to the shopping cart and that the cart updates correctly.\"}],\"Product\":\"RED Product:online store\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":4170968389,\"Product\":\"RED Product:online store\",\"Summary\":\"Document the steps for adding products to the shopping cart.\"}],\"Summary\":\"As a customer, I want to add products to a shopping cart, so that I can keep track of the items I want to purchase.\"},{\"IssueType\":\"Story\",\"Key\":1410043155,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":3520425854,\"LinkedIssues\":[],\"Product\":\"RED Product:online store\",\"Subtasks\":null,\"Summary\":\"Integrated Test: Verify that the online purchase process is secure and that transactions are processed correctly.\"}],\"Product\":\"RED Product:online store\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3496540348,\"Product\":\"RED Product:online store\",\"Summary\":\"Create documentation outlining the security measures for online purchases.\"}],\"Summary\":\"As a customer, I want to complete secure online purchases, so that I can buy the products I want with confidence.\"},{\"IssueType\":\"Story\",\"Key\":4167297321,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":1234728070,\"LinkedIssues\":[],\"Product\":\"RED Product:online store\",\"Subtasks\":null,\"Summary\":\"Integrated Test: Verify that the checkout process is streamlined and user-friendly.\"}],\"Product\":\"RED Product:online store\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2203977675,\"Product\":\"RED Product:online store\",\"Summary\":\"Document the steps in the streamlined checkout process.\"}],\"Summary\":\"As a customer, I want a streamlined checkout process, so that I can quickly and easily complete my purchase.\"},{\"IssueType\":\"Story\",\"Key\":74832464,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":2472500171,\"LinkedIssues\":[],\"Product\":\"RED Product:online store\",\"Subtasks\":null,\"Summary\":\"Integrated Test: Verify that diverse payment options are available and function correctly.\"}],\"Product\":\"RED Product:online store\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":13989294,\"Product\":\"RED Product:online store\",\"Summary\":\"Document the available payment options and how to use them.\"}],\"Summary\":\"As a customer, I want diverse payment options, so that I can choose the method that is most convenient for me.\"},{\"IssueType\":\"Story\",\"Key\":697506967,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":4194623129,\"LinkedIssues\":[],\"Product\":\"RED Product:online store\",\"Subtasks\":null,\"Summary\":\"Integrated Test: Verify that order tracking is robust and provides accurate status updates.\"}],\"Product\":\"RED Product:online store\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2878317142,\"Product\":\"RED Product:online store\",\"Summary\":\"Document how to use the order tracking feature.\"}],\"Summary\":\"As a customer, I want robust order tracking capabilities, so that I can monitor the status of my order.\"},{\"IssueType\":\"Story\",\"Key\":3043734179,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":1603618522,\"LinkedIssues\":[],\"Product\":\"RED Product:online store\",\"Subtasks\":null,\"Summary\":\"Integrated Test: Verify that the 'Customer' table is created with the specified columns and data types.\"}],\"Product\":\"RED Product:online store\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":643123150,\"Product\":\"RED Product:online store\",\"Summary\":\"Create documentation for the 'Customer' database table schema.\"}],\"Summary\":\"As a administrator, I want to create a 'Customer' database table with 'Name' and 'Phone number' columns, so that I can store customer information.\"},{\"IssueType\":\"Story\",\"Key\":1492005358,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":2051881399,\"LinkedIssues\":[],\"Product\":\"RED Product:online store\",\"Subtasks\":null,\"Summary\":\"Integrated Test: Verify that the online store is fully responsive and functions correctly on various mobile devices.\"}],\"Product\":\"RED Product:online store\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2254127475,\"Product\":\"RED Product:online store\",\"Summary\":\"Document the mobile-first design approach and implementation details.\"}],\"Summary\":\"As a developer, I want to implement a mobile-first design, so that the online store is accessible and user-friendly on mobile devices.\"},{\"IssueType\":\"Story\",\"Key\":2880553539,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":1151543679,\"LinkedIssues\":[],\"Product\":\"RED Product:online store\",\"Subtasks\":null,\"Summary\":\"Integrated Test: Verify that the online store meets accessibility standards (e.g., WCAG).\"}],\"Product\":\"RED Product:online store\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2720722552,\"Product\":\"RED Product:online store\",\"Summary\":\"Document the accessibility features and guidelines.\"}],\"Summary\":\"As a user, I want the platform to be accessible, so that users with disabilities can use the online store effectively.\"},{\"IssueType\":\"Story\",\"Key\":251193665,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":1070729156,\"LinkedIssues\":[],\"Product\":\"RED Product:online store\",\"Subtasks\":null,\"Summary\":\"Integrated Test: Verify that the online store is highly performant and loads quickly.\"}],\"Product\":\"RED Product:online store\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2438113419,\"Product\":\"RED Product:online store\",\"Summary\":\"Document performance optimization techniques.\"}],\"Summary\":\"As a user, I want the platform to be highly performant, so that I can browse and purchase products quickly and efficiently.\"},{\"IssueType\":\"Story\",\"Key\":2413098779,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":3700965913,\"LinkedIssues\":[],\"Product\":\"RED Product:online store\",\"Subtasks\":null,\"Summary\":\"Integrated Test: Verify that returning customers can save and reuse their shipping and payment information securely.\"}],\"Product\":\"RED Product:online store\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":125991027,\"Product\":\"RED Product:online store\",\"Summary\":\"Document how saved information is stored and used.\"}],\"Summary\":\"As a returning customer, I want to be able to save my shipping and payment information, so that I can quickly complete future purchases.\"},{\"IssueType\":\"Story\",\"Key\":1261221386,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":3987856855,\"LinkedIssues\":[],\"Product\":\"RED Product:online store\",\"Subtasks\":null,\"Summary\":\"Integrated Test: Verify that product information can be easily managed and updated by administrators.\"}],\"Product\":\"RED Product:online store\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3726686527,\"Product\":\"RED Product:online store\",\"Summary\":\"Document the process for managing product information.\"}],\"Summary\":\"As a store administrator, I want to be able to easily manage and update product information, so that customers always see accurate details.\"}],\"ProductDescription\":\"Implement a mobile-first, fully responsive e-commerce platform for our online store, enabling users to browse products, add them to a shopping cart, and complete secure online purchases. The platform should offer a streamlined checkout process, diverse payment options, and robust order tracking capabilities. We need to ensure that the platform is user-friendly, accessible, and highly performant, with a focus on a seamless user experience across all devices.  Please include this task: Create a Database table called \\\\\\\"Customer\\\\\\\".   Add the \\\\\\\"Name\\\\\\\" and \\\\\\\"Phone number\\\\\\\" and other relevant columns to the \\\\\\\"Customer\\\\\\\" table.\",\"UserStoryList\":[\"As a customer, I want to browse products on the online store, so that I can find the items I am interested in purchasing.\",\"As a customer, I want to add products to a shopping cart, so that I can keep track of the items I want to purchase.\",\"As a customer, I want to complete secure online purchases, so that I can buy the products I want with confidence.\",\"As a customer, I want a streamlined checkout process, so that I can quickly and easily complete my purchase.\",\"As a customer, I want diverse payment options, so that I can choose the method that is most convenient for me.\",\"As a customer, I want robust order tracking capabilities, so that I can monitor the status of my order.\",\"As a administrator, I want to create a 'Customer' database table with 'Name' and 'Phone number' columns, so that I can store customer information.\",\"As a developer, I want to implement a mobile-first design, so that the online store is accessible and user-friendly on mobile devices.\",\"As a user, I want the platform to be accessible, so that users with disabilities can use the online store effectively.\",\"As a user, I want the platform to be highly performant, so that I can browse and purchase products quickly and efficiently.\",\"As a returning customer, I want to be able to save my shipping and payment information, so that I can quickly complete future purchases.\",\"As a store administrator, I want to be able to easily manage and update product information, so that customers always see accurate details.\"]}";
            json ??= testJson;

            GFSGeminiClientHost.Result result = new(-1, json);
            IssueGeneratorBaseArgs args = new(result);
            try
            {
                TreeSerialization.IssueResults? issueResults =
                    JsonSerializer.Deserialize<TreeSerialization.IssueResults>(json);
                if( issueResults == null ) return null;

                List<IssueData.Issue>? issues = issueResults.Issues;
                if( issues != null )
                {
                    args.Issues = issues;
                    UserStoryGeneratorCompleted?.Invoke(args);
                }

                return issueResults.ProductDescription;

            }
            catch( Exception )
            {
                throw;
            }

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