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

        public string SettingsFileName { get; private set; } = @$".\Settings.json";

        public delegate void CompletedUserStoryEventHandler(IssueGeneratorBaseArgs answer);
        public event CompletedUserStoryEventHandler? UserStoryGeneratorCompleted;
        public delegate void CompletedEventHandler(IssueGeneratorBaseArgsEx args);
        public event CompletedEventHandler? IssueGeneratorCompleted;


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

        private static void Tests()
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
        public bool SaveUserStoryResultsToJson(string fullFilePath)
        {
            if( userStoryResults.UserStoryList == null || userStoryResults.UserStoryList.Count == 0 ) return false;
            string result = JsonSerializer.Serialize(userStoryResults, options);
            SaveUserStoryResultsToJson(fullFilePath, result);
            return true;
        }
        private static void SaveUserStoryResultsToJson(string fullFilePath, string result)
        {
            //UserStoryGenerator.Utilities.Logger.Info(result);
            File.WriteAllText(fullFilePath, result);
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

                    IssueData? data = JsonSerializer.Deserialize<IssueData>(result.Answer);
                    if( data == null ) throw new NullReferenceException(nameof(data));
                    List<IssueData.Issue>? issues = data.Issues;
                    if( issues != null )
                    {
                        args.Issues = issues;
                        UserStoryGeneratorCompleted?.Invoke(args);
                    }

                }
                catch( Exception )
                {
                    //throw ex;
                }

                //UserStoryGeneratorCompleted?.Invoke(args);

            };
            //issueGenerator.Error += () =>
            //{
            //};

            await Task.Delay(0);// this because RequestAnswer isn't really async
            issueGenerator.RequestAnswer();
        }



        internal async Task ProcessStoryList(string productName, bool addQATests, bool addSubTasks, int maxStories, List<StoryPackage> list)//async Task 
        {
            if( Settings == null ) throw new NullReferenceException(nameof(Settings));
            if( Settings.Key == null ) throw new NullReferenceException(nameof(Settings.Key));

            int counter = list.Count;

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

                    GFSGeminiClientHost.Result result = args.Result;
                    try
                    {
                        if( result.Answer == null ) throw new NullReferenceException(nameof(result.Answer));

                        IssueData? data = JsonSerializer.Deserialize<IssueData>(result.Answer);
                        if( data == null ) throw new NullReferenceException(nameof(data));
                        List<IssueData.Issue>? issues = data.Issues;
                        if( issues != null )
                        {
                            issueGeneratorBaseArgsEx.Issues = issues;
                            //IssueGeneratorCompleted?.Invoke(issueGeneratorBaseArgsEx);
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


                    //IssueGeneratorCompleted?.Invoke(issueGeneratorBaseArgsEx);
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

        internal void CreateUserStories(string? json = null)
        {
            const string testJson = "{\"Issues\":[{\"IssueType\":\"Story\",\"Key\":3232055690,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":3976707388,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Integrated Test: Verify that product browsing is fully functional and responsive on various mobile devices.\"},{\"IssueType\":\"Task\",\"Key\":3519554780,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":2691601439,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2604813636,\"Product\":\"RED\",\"Summary\":\"Test Automation: Create automated tests for layout on different screen sizes\"},{\"IssueType\":\"Sub-task\",\"Key\":2007887043,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Integrate with device farm for cross-device testing\"},{\"IssueType\":\"Sub-task\",\"Key\":3597500886,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Manually test product grid display on iOS and Android devices\"}],\"Summary\":\"QA Test: Verify product grid responsiveness on various mobile devices\"},{\"IssueType\":\"Test\",\"Key\":2432324526,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":1807291504,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate filter selection and result validation\"},{\"IssueType\":\"Sub-task\",\"Key\":2138929949,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Verify filter integration with backend API\"},{\"IssueType\":\"Sub-task\",\"Key\":3315271110,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Test filter options and their impact on product display\"}],\"Summary\":\"QA Test: Verify filter functionality on mobile\"},{\"IssueType\":\"Test\",\"Key\":3204096851,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3933706854,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate sort option selection and result verification\"},{\"IssueType\":\"Sub-task\",\"Key\":4102501445,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Verify sort integration with backend API\"},{\"IssueType\":\"Sub-task\",\"Key\":1847766664,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Test different sort orders and their impact on product display\"}],\"Summary\":\"QA Test: Verify sort options on mobile\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2938779869,\"Product\":\"RED\",\"Summary\":\"Implement UI Control: Product grid\"},{\"IssueType\":\"Sub-task\",\"Key\":4104982404,\"Product\":\"RED\",\"Summary\":\"Implement UI Control: Filter menu\"},{\"IssueType\":\"Sub-task\",\"Key\":587506377,\"Product\":\"RED\",\"Summary\":\"Implement UI Control: Sort options\"}],\"Summary\":\"Implement responsive layout for product browsing\"},{\"IssueType\":\"Task\",\"Key\":484621982,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":3189872586,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3722024667,\"Product\":\"RED\",\"Summary\":\"Stress test: Category API\"},{\"IssueType\":\"Sub-task\",\"Key\":3524661166,\"Product\":\"RED\",\"Summary\":\"Load test: Category API\"},{\"IssueType\":\"Sub-task\",\"Key\":3918148508,\"Product\":\"RED\",\"Summary\":\"Load balancing test: Category API\"},{\"IssueType\":\"Sub-task\",\"Key\":369122353,\"Product\":\"RED\",\"Summary\":\"Latency test: Category API\"},{\"IssueType\":\"Sub-task\",\"Key\":1530448137,\"Product\":\"RED\",\"Summary\":\"Scalability test: Category API\"},{\"IssueType\":\"Sub-task\",\"Key\":856098653,\"Product\":\"RED\",\"Summary\":\"Endurance test: Category API\"},{\"IssueType\":\"Sub-task\",\"Key\":2751369948,\"Product\":\"RED\",\"Summary\":\"Security test: Category API\"}],\"Summary\":\"QA Test: API performance and security\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":1556222387,\"Product\":\"RED\",\"Summary\":\"Implement HTTP method: GET\"},{\"IssueType\":\"Sub-task\",\"Key\":3606976774,\"Product\":\"RED\",\"Summary\":\"Implement TDD test: GET category products\"}],\"Summary\":\"Implement product category browsing API\"},{\"IssueType\":\"Task\",\"Key\":2720958150,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":3258764476,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":648460084,\"Product\":\"RED\",\"Summary\":\"Stress test: Search API\"},{\"IssueType\":\"Sub-task\",\"Key\":3445899186,\"Product\":\"RED\",\"Summary\":\"Load test: Search API\"},{\"IssueType\":\"Sub-task\",\"Key\":208724582,\"Product\":\"RED\",\"Summary\":\"Load balancing test: Search API\"},{\"IssueType\":\"Sub-task\",\"Key\":2141597990,\"Product\":\"RED\",\"Summary\":\"Latency test: Search API\"},{\"IssueType\":\"Sub-task\",\"Key\":1462842744,\"Product\":\"RED\",\"Summary\":\"Scalability test: Search API\"},{\"IssueType\":\"Sub-task\",\"Key\":3010883301,\"Product\":\"RED\",\"Summary\":\"Endurance test: Search API\"},{\"IssueType\":\"Sub-task\",\"Key\":3011629277,\"Product\":\"RED\",\"Summary\":\"Security test: Search API\"}],\"Summary\":\"QA Test: Product search API performance and security\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2278602008,\"Product\":\"RED\",\"Summary\":\"Implement HTTP method: GET\"},{\"IssueType\":\"Sub-task\",\"Key\":1543658572,\"Product\":\"RED\",\"Summary\":\"Implement TDD test: GET search results\"}],\"Summary\":\"Implement product search API\"},{\"IssueType\":\"Task\",\"Key\":3044429637,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":1198564542,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2917723724,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate query performance test\"},{\"IssueType\":\"Sub-task\",\"Key\":2024525972,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Verify DB integration\"},{\"IssueType\":\"Sub-task\",\"Key\":449583299,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Manually test query execution time\"}],\"Summary\":\"QA Test: Validate database query performance\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2856587816,\"Product\":\"RED\",\"Summary\":\"Implement CRUD method: read\"},{\"IssueType\":\"Sub-task\",\"Key\":2419879181,\"Product\":\"RED\",\"Summary\":\"Implement TDD test: Read products from DB\"}],\"Summary\":\"Implement database query for product listing\"},{\"IssueType\":\"Task\",\"Key\":3805632403,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":3600550010,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":438448344,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate service integration tests\"},{\"IssueType\":\"Sub-task\",\"Key\":4026893759,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Verify service integration with other services\"},{\"IssueType\":\"Sub-task\",\"Key\":3760675873,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Validate data flow between service and DB\"}],\"Summary\":\"QA Test: Service integration with database\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":1824662455,\"Product\":\"RED\",\"Summary\":\"Implement TDD test: Fetch products and apply filters\"}],\"Summary\":\"Implement product listing service\"},{\"IssueType\":\"Task\",\"Key\":2719710539,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":3263285411,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":871156300,\"Product\":\"RED\",\"Summary\":\"Test Automation: Create automated tests for image loading times\"},{\"IssueType\":\"Sub-task\",\"Key\":1468022552,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Integrate with CDN for image delivery\"},{\"IssueType\":\"Sub-task\",\"Key\":3274287887,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Manually test image display and loading on different mobile devices\"}],\"Summary\":\"QA Test: Image loading performance on mobile\"}],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Optimize images for mobile devices\"},{\"IssueType\":\"Task\",\"Key\":1531868992,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":1450376784,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":199515189,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate CSS responsiveness tests using Selenium\"},{\"IssueType\":\"Sub-task\",\"Key\":2867447858,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Verify CSS integration with UI components\"},{\"IssueType\":\"Sub-task\",\"Key\":43924473,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Validate CSS rendering on different mobile browsers\"}],\"Summary\":\"QA Test: CSS responsiveness on mobile\"}],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement mobile-first CSS\"},{\"IssueType\":\"Task\",\"Key\":1544482175,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":3430735242,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":1651648338,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate touch navigation tests\"},{\"IssueType\":\"Sub-task\",\"Key\":2903216017,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Verify touch integration with navigation components\"},{\"IssueType\":\"Sub-task\",\"Key\":3656611073,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Test touch navigation usability on mobile devices\"}],\"Summary\":\"QA Test: Touch navigation usability\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":1493695564,\"Product\":\"RED\",\"Summary\":\"Implement UI Control: Hamburger menu\"},{\"IssueType\":\"Sub-task\",\"Key\":4228221281,\"Product\":\"RED\",\"Summary\":\"Implement UI Control: Swipe gestures\"}],\"Summary\":\"Implement touch-friendly navigation\"},{\"IssueType\":\"Task\",\"Key\":2581778365,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":2708717469,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":1721043986,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate website speed tests on mobile networks\"},{\"IssueType\":\"Sub-task\",\"Key\":3385346886,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Verify CDN integration\"},{\"IssueType\":\"Sub-task\",\"Key\":1649992735,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Manually test website loading speed on 3G/4G networks\"}],\"Summary\":\"QA Test: Website loading speed on 3G/4G\"}],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Ensure website loads quickly on mobile networks\"},{\"IssueType\":\"Task\",\"Key\":641643254,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":3463012635,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":121214037,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate zoom functionality tests\"},{\"IssueType\":\"Sub-task\",\"Key\":1485386937,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Verify image resolution on zoom\"},{\"IssueType\":\"Sub-task\",\"Key\":2697771087,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Test zoom usability on mobile\"}],\"Summary\":\"QA Test: Zoom functionality on mobile\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":541224567,\"Product\":\"RED\",\"Summary\":\"Implement UI Control: Pinch to zoom\"}],\"Summary\":\"Implement product zoom functionality on mobile\"},{\"IssueType\":\"Task\",\"Key\":3522480408,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":202901402,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":4213927273,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate accessibility tests\"},{\"IssueType\":\"Sub-task\",\"Key\":724051527,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Verify accessibility integration with UI components\"},{\"IssueType\":\"Sub-task\",\"Key\":3451378459,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Validate accessibility with screen readers on mobile\"}],\"Summary\":\"QA Test: Accessibility for screen readers on mobile\"}],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Ensure accessibility on mobile devices\"},{\"IssueType\":\"Task\",\"Key\":3861276179,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":3567206098,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2259678430,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate search bar usability tests\"},{\"IssueType\":\"Sub-task\",\"Key\":2981373719,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Verify search integration with API\"},{\"IssueType\":\"Sub-task\",\"Key\":201465253,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Test search bar usability on mobile devices\"}],\"Summary\":\"QA Test: Mobile search bar usability\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":1357811506,\"Product\":\"RED\",\"Summary\":\"Implement UI Control: Autocomplete suggestions\"}],\"Summary\":\"Implement mobile-friendly search bar\"},{\"IssueType\":\"Task\",\"Key\":277149978,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":2080280131,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3318327687,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate product detail page tests\"},{\"IssueType\":\"Sub-task\",\"Key\":1014340329,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Verify integration with backend data\"},{\"IssueType\":\"Sub-task\",\"Key\":2125626903,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Test product detail page usability on mobile devices\"}],\"Summary\":\"QA Test: Mobile product detail page usability\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2256605036,\"Product\":\"RED\",\"Summary\":\"Implement UI Control: Image carousel\"},{\"IssueType\":\"Sub-task\",\"Key\":3925278133,\"Product\":\"RED\",\"Summary\":\"Implement UI Control: Product description\"},{\"IssueType\":\"Sub-task\",\"Key\":4109421662,\"Product\":\"RED\",\"Summary\":\"Implement UI Control: Add to cart button\"}],\"Summary\":\"Implement product detail page for mobile\"},{\"IssueType\":\"Task\",\"Key\":2282660691,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":3676813235,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":931817516,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate security vulnerability scans\"},{\"IssueType\":\"Sub-task\",\"Key\":2491531190,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Verify SSL certificate\"},{\"IssueType\":\"Sub-task\",\"Key\":4127671112,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Perform penetration testing on mobile\"}],\"Summary\":\"QA Test: Security testing on mobile\"}],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement secure browsing on mobile\"},{\"IssueType\":\"Task\",\"Key\":1682010049,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":2340128237,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3417076519,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate encryption tests\"},{\"IssueType\":\"Sub-task\",\"Key\":2513015614,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Verify data encryption in transit and at rest\"},{\"IssueType\":\"Sub-task\",\"Key\":2726957725,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Validate encryption implementation on mobile\"}],\"Summary\":\"QA Test: Encryption testing on mobile\"}],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement data encryption for mobile\"},{\"IssueType\":\"Task\",\"Key\":357896504,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":3425960254,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":1913974826,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate session management tests\"},{\"IssueType\":\"Sub-task\",\"Key\":963425559,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Verify session persistence and timeout\"},{\"IssueType\":\"Sub-task\",\"Key\":3296026545,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Validate session management on mobile\"}],\"Summary\":\"QA Test: Session management testing on mobile\"}],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement session management on mobile\"},{\"IssueType\":\"Task\",\"Key\":2918405845,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":890721942,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":1394919734,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate checkout process tests\"},{\"IssueType\":\"Sub-task\",\"Key\":2613113756,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Verify integration with payment gateway\"},{\"IssueType\":\"Sub-task\",\"Key\":703646189,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Test checkout usability on mobile devices\"}],\"Summary\":\"QA Test: Mobile checkout process usability\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":90787312,\"Product\":\"RED\",\"Summary\":\"Implement UI Control: Guest checkout option\"},{\"IssueType\":\"Sub-task\",\"Key\":989767545,\"Product\":\"RED\",\"Summary\":\"Implement UI Control: Auto-fill form fields\"},{\"IssueType\":\"Sub-task\",\"Key\":2844861922,\"Product\":\"RED\",\"Summary\":\"Implement UI Control: Progress indicator\"}],\"Summary\":\"Implement mobile-friendly checkout process\"},{\"IssueType\":\"Task\",\"Key\":1240502366,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":841981744,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2040489221,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate payment gateway tests\"},{\"IssueType\":\"Sub-task\",\"Key\":4106218903,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Verify integration with different payment gateways\"},{\"IssueType\":\"Sub-task\",\"Key\":3041810769,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Test payment options on mobile\"}],\"Summary\":\"QA Test: Payment gateway integration on mobile\"}],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Support various payment options on mobile\"},{\"IssueType\":\"Task\",\"Key\":2428810273,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":2732510310,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":1200238156,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate GDPR compliance tests\"},{\"IssueType\":\"Sub-task\",\"Key\":1479725878,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Verify data handling practices\"},{\"IssueType\":\"Sub-task\",\"Key\":2735976670,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Validate GDPR compliance on mobile\"}],\"Summary\":\"QA Test: GDPR compliance on mobile\"}],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Ensure compliance with data protection regulations\"},{\"IssueType\":\"Task\",\"Key\":1769187116,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":51031797,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3450087755,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate social media integration tests\"},{\"IssueType\":\"Sub-task\",\"Key\":2218532328,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Verify social media sharing functionality\"},{\"IssueType\":\"Sub-task\",\"Key\":706398971,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Test social media integration on mobile\"}],\"Summary\":\"QA Test: Social media sharing on mobile\"}],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Integrate with social media platforms\"},{\"IssueType\":\"Task\",\"Key\":3974825586,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":1556549768,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2985595950,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate push notification tests\"},{\"IssueType\":\"Sub-task\",\"Key\":2790187455,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Verify push notification delivery\"},{\"IssueType\":\"Sub-task\",\"Key\":1397895745,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Validate push notification functionality on mobile\"}],\"Summary\":\"QA Test: Push notification functionality on mobile\"}],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement push notifications for sales and promotions\"},{\"IssueType\":\"Task\",\"Key\":1284474085,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":4281922587,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":1324544272,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate mobile SEO tests\"},{\"IssueType\":\"Sub-task\",\"Key\":3874739562,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Verify SEO integration with site content\"},{\"IssueType\":\"Sub-task\",\"Key\":2877755300,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Validate mobile SEO implementation\"}],\"Summary\":\"QA Test: Mobile SEO optimization\"}],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Optimize content for mobile SEO\"},{\"IssueType\":\"Task\",\"Key\":1771122270,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":2336436970,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":142566520,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate AR tests\"},{\"IssueType\":\"Sub-task\",\"Key\":3561342948,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Verify AR integration with product data\"},{\"IssueType\":\"Sub-task\",\"Key\":1795143567,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Validate AR implementation on mobile\"}],\"Summary\":\"QA Test: AR functionality on mobile\"}],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement Augmented Reality (AR) features for product preview\"},{\"IssueType\":\"Task\",\"Key\":1388313774,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":1854090370,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2062511948,\"Product\":\"RED\",\"Summary\":\"Security: Fuzz Testing\"},{\"IssueType\":\"Sub-task\",\"Key\":4059023081,\"Product\":\"RED\",\"Summary\":\"Security: Static Code Analysis\"},{\"IssueType\":\"Sub-task\",\"Key\":1217290310,\"Product\":\"RED\",\"Summary\":\"Security: Dynamic Analysis\"}],\"Summary\":\"QA Test: Security Audit\"}],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Mobile App Security Audit\"},{\"IssueType\":\"Task\",\"Key\":1508882114,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":3184931359,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2886471029,\"Product\":\"RED\",\"Summary\":\"Mobile Performance: Memory Usage Test\"},{\"IssueType\":\"Sub-task\",\"Key\":222680929,\"Product\":\"RED\",\"Summary\":\"Mobile Performance: CPU Usage Test\"},{\"IssueType\":\"Sub-task\",\"Key\":3880867226,\"Product\":\"RED\",\"Summary\":\"Mobile Performance: Battery Consumption Test\"}],\"Summary\":\"QA Test: Monitor Mobile App Performance\"}],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement Mobile App Performance Monitoring\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3394272592,\"Product\":\"RED\",\"Summary\":\"Document the steps for browsing products on the mobile platform.\"}],\"Summary\":\"As a user, I want to browse products on a mobile-first e-commerce platform, so that I can easily find and view items on my mobile device.\"},{\"IssueType\":\"Story\",\"Key\":1781617193,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":459772826,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Integrated Test: Verify that adding products to the shopping cart works correctly and the cart updates accurately.\"},{\"IssueType\":\"Task\",\"Key\":2438516008,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":4269195067,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":1359689085,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate 'Add to Cart' button functionality\"},{\"IssueType\":\"Sub-task\",\"Key\":2235560636,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Verify 'Add to Cart' adds product to cart\"},{\"IssueType\":\"Sub-task\",\"Key\":2972755127,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Manually test adding product to cart\"}],\"Summary\":\"QA Test: Verify 'Add to Cart' button functionality\"},{\"IssueType\":\"Test\",\"Key\":3884234979,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2633307969,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate item added to cart notification\"},{\"IssueType\":\"Sub-task\",\"Key\":2108845640,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Test notification displayed after item added\"},{\"IssueType\":\"Sub-task\",\"Key\":1426288748,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Manually test item added to cart notification\"}],\"Summary\":\"QA Test: Verify item added to cart notification\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":1438057410,\"Product\":\"RED\",\"Summary\":\"Implement UI Control: 'Add to Cart' button\"},{\"IssueType\":\"Sub-task\",\"Key\":308779980,\"Product\":\"RED\",\"Summary\":\"Implement UI Control: Display cart icon/link\"},{\"IssueType\":\"Sub-task\",\"Key\":181050678,\"Product\":\"RED\",\"Summary\":\"Implement UI Control: Display quantity field\"}],\"Summary\":\"Implement Frontend: Add Products to Cart UI\"},{\"IssueType\":\"Task\",\"Key\":1855353479,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":4220381120,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":758662260,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate API add to cart\"},{\"IssueType\":\"Sub-task\",\"Key\":279231904,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Test API add to cart\"},{\"IssueType\":\"Sub-task\",\"Key\":2727589252,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Test API add to cart\"},{\"IssueType\":\"Sub-task\",\"Key\":25769606,\"Product\":\"RED\",\"Summary\":\"Stress test: API - Add product to cart\"},{\"IssueType\":\"Sub-task\",\"Key\":1535504687,\"Product\":\"RED\",\"Summary\":\"Load test: API - Add product to cart\"},{\"IssueType\":\"Sub-task\",\"Key\":2435857091,\"Product\":\"RED\",\"Summary\":\"Load balancing test: API - Add product to cart\"},{\"IssueType\":\"Sub-task\",\"Key\":4065120829,\"Product\":\"RED\",\"Summary\":\"Latency test: API - Add product to cart\"},{\"IssueType\":\"Sub-task\",\"Key\":1645723136,\"Product\":\"RED\",\"Summary\":\"Scalability test: API - Add product to cart\"},{\"IssueType\":\"Sub-task\",\"Key\":3933031013,\"Product\":\"RED\",\"Summary\":\"Endurance test: API - Add product to cart\"},{\"IssueType\":\"Sub-task\",\"Key\":1896539748,\"Product\":\"RED\",\"Summary\":\"Security test: API - Add product to cart\"}],\"Summary\":\"QA Test: API - Add product to cart\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":4002709689,\"Product\":\"RED\",\"Summary\":\"Implement HTTP method: POST\"},{\"IssueType\":\"Sub-task\",\"Key\":4230600742,\"Product\":\"RED\",\"Summary\":\"TDD test: POST method implementation\"}],\"Summary\":\"Implement Backend: Create API endpoint for adding products to cart\"},{\"IssueType\":\"Task\",\"Key\":198799276,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":859717984,\"Product\":\"RED\",\"Summary\":\"Implement CRUD method: create\"},{\"IssueType\":\"Sub-task\",\"Key\":2661375932,\"Product\":\"RED\",\"Summary\":\"TDD test: create method implementation\"},{\"IssueType\":\"Sub-task\",\"Key\":1499015180,\"Product\":\"RED\",\"Summary\":\"Implement CRUD method: read\"},{\"IssueType\":\"Sub-task\",\"Key\":2840142291,\"Product\":\"RED\",\"Summary\":\"TDD test: read method implementation\"},{\"IssueType\":\"Sub-task\",\"Key\":2531820947,\"Product\":\"RED\",\"Summary\":\"Implement CRUD method: update\"},{\"IssueType\":\"Sub-task\",\"Key\":576405690,\"Product\":\"RED\",\"Summary\":\"TDD test: update method implementation\"},{\"IssueType\":\"Sub-task\",\"Key\":1772330274,\"Product\":\"RED\",\"Summary\":\"Implement CRUD method: delete\"},{\"IssueType\":\"Sub-task\",\"Key\":3341912681,\"Product\":\"RED\",\"Summary\":\"TDD test: delete method implementation\"}],\"Summary\":\"Implement Database: Create table to store cart items\"},{\"IssueType\":\"Task\",\"Key\":4173134353,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":1385739171,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3058383965,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate cart item persistence verification\"},{\"IssueType\":\"Sub-task\",\"Key\":84879147,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Test cart item persistence\"},{\"IssueType\":\"Sub-task\",\"Key\":1633969300,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Manually verify cart item persistence\"}],\"Summary\":\"QA Test: Verify cart item persistence\"}],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement Backend: Service to manage cart item persistence\"},{\"IssueType\":\"Task\",\"Key\":662124215,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":1979886132,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":4181538513,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate API retrieve cart contents\"},{\"IssueType\":\"Sub-task\",\"Key\":1168071997,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Test API retrieve cart contents\"},{\"IssueType\":\"Sub-task\",\"Key\":1399988924,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Manually test API retrieve cart contents\"},{\"IssueType\":\"Sub-task\",\"Key\":2840449640,\"Product\":\"RED\",\"Summary\":\"Stress test: API - Retrieve cart contents\"},{\"IssueType\":\"Sub-task\",\"Key\":4231247555,\"Product\":\"RED\",\"Summary\":\"Load test: API - Retrieve cart contents\"},{\"IssueType\":\"Sub-task\",\"Key\":8499248,\"Product\":\"RED\",\"Summary\":\"Load balancing test: API - Retrieve cart contents\"},{\"IssueType\":\"Sub-task\",\"Key\":3267485005,\"Product\":\"RED\",\"Summary\":\"Latency test: API - Retrieve cart contents\"},{\"IssueType\":\"Sub-task\",\"Key\":2652363588,\"Product\":\"RED\",\"Summary\":\"Scalability test: API - Retrieve cart contents\"},{\"IssueType\":\"Sub-task\",\"Key\":2035255814,\"Product\":\"RED\",\"Summary\":\"Endurance test: API - Retrieve cart contents\"},{\"IssueType\":\"Sub-task\",\"Key\":1041461573,\"Product\":\"RED\",\"Summary\":\"Security test: API - Retrieve cart contents\"}],\"Summary\":\"QA Test: API - Retrieve cart contents\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":4124861017,\"Product\":\"RED\",\"Summary\":\"Implement HTTP method: GET\"},{\"IssueType\":\"Sub-task\",\"Key\":1247877828,\"Product\":\"RED\",\"Summary\":\"TDD test: GET method implementation\"}],\"Summary\":\"Implement API: Retrieve cart contents\"},{\"IssueType\":\"Task\",\"Key\":812274365,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":1025513608,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3673812982,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate cart contents display verification\"},{\"IssueType\":\"Sub-task\",\"Key\":2982795854,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Test cart contents display after retrieval\"},{\"IssueType\":\"Sub-task\",\"Key\":3611382899,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Manually verify cart contents display\"}],\"Summary\":\"QA Test: Verify cart contents are displayed correctly\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":938966695,\"Product\":\"RED\",\"Summary\":\"Implement UI Control: Display product image in cart\"},{\"IssueType\":\"Sub-task\",\"Key\":2808949028,\"Product\":\"RED\",\"Summary\":\"Implement UI Control: Display product name in cart\"},{\"IssueType\":\"Sub-task\",\"Key\":616769988,\"Product\":\"RED\",\"Summary\":\"Implement UI Control: Display product price in cart\"},{\"IssueType\":\"Sub-task\",\"Key\":2573574067,\"Product\":\"RED\",\"Summary\":\"Implement UI Control: Display quantity of product in cart\"},{\"IssueType\":\"Sub-task\",\"Key\":366198922,\"Product\":\"RED\",\"Summary\":\"Implement UI Control: Display subtotal, tax, and total\"}],\"Summary\":\"Implement Frontend: Display cart contents\"},{\"IssueType\":\"Task\",\"Key\":3360590507,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":3163184282,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":688947632,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate API update quantity\"},{\"IssueType\":\"Sub-task\",\"Key\":2953929054,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Test API update quantity\"},{\"IssueType\":\"Sub-task\",\"Key\":1704620589,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Manually test API update quantity\"},{\"IssueType\":\"Sub-task\",\"Key\":979080404,\"Product\":\"RED\",\"Summary\":\"Stress test: API - Update product quantity\"},{\"IssueType\":\"Sub-task\",\"Key\":609547401,\"Product\":\"RED\",\"Summary\":\"Load test: API - Update product quantity\"},{\"IssueType\":\"Sub-task\",\"Key\":991385326,\"Product\":\"RED\",\"Summary\":\"Load balancing test: API - Update product quantity\"},{\"IssueType\":\"Sub-task\",\"Key\":1177940280,\"Product\":\"RED\",\"Summary\":\"Latency test: API - Update product quantity\"},{\"IssueType\":\"Sub-task\",\"Key\":1971627888,\"Product\":\"RED\",\"Summary\":\"Scalability test: API - Update product quantity\"},{\"IssueType\":\"Sub-task\",\"Key\":3327662415,\"Product\":\"RED\",\"Summary\":\"Endurance test: API - Update product quantity\"},{\"IssueType\":\"Sub-task\",\"Key\":1021514918,\"Product\":\"RED\",\"Summary\":\"Security test: API - Update product quantity\"}],\"Summary\":\"QA Test: API - Update product quantity\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":4004149972,\"Product\":\"RED\",\"Summary\":\"Implement HTTP method: PUT\"},{\"IssueType\":\"Sub-task\",\"Key\":2471276113,\"Product\":\"RED\",\"Summary\":\"TDD test: PUT method implementation\"}],\"Summary\":\"Implement API: Update product quantity in cart\"},{\"IssueType\":\"Task\",\"Key\":2158767314,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":96457174,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":726936048,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate product quantity update verification\"},{\"IssueType\":\"Sub-task\",\"Key\":2942877253,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Test quantity update and cart total\"},{\"IssueType\":\"Sub-task\",\"Key\":2305400984,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Manually verify quantity update\"}],\"Summary\":\"QA Test: Verify product quantity update functionality\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":1007360117,\"Product\":\"RED\",\"Summary\":\"Implement UI Control: Quantity input field\"},{\"IssueType\":\"Sub-task\",\"Key\":2504936679,\"Product\":\"RED\",\"Summary\":\"Implement UI Control: Update button\"}],\"Summary\":\"Implement Frontend: Update quantity in cart functionality\"},{\"IssueType\":\"Task\",\"Key\":1017209089,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":3917977569,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":4236531128,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate API remove from cart\"},{\"IssueType\":\"Sub-task\",\"Key\":2037008631,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Test API remove from cart\"},{\"IssueType\":\"Sub-task\",\"Key\":3427222343,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Manually test API remove from cart\"},{\"IssueType\":\"Sub-task\",\"Key\":143242997,\"Product\":\"RED\",\"Summary\":\"Stress test: API - Remove product from cart\"},{\"IssueType\":\"Sub-task\",\"Key\":22281419,\"Product\":\"RED\",\"Summary\":\"Load test: API - Remove product from cart\"},{\"IssueType\":\"Sub-task\",\"Key\":936228079,\"Product\":\"RED\",\"Summary\":\"Load balancing test: API - Remove product from cart\"},{\"IssueType\":\"Sub-task\",\"Key\":1714921490,\"Product\":\"RED\",\"Summary\":\"Latency test: API - Remove product from cart\"},{\"IssueType\":\"Sub-task\",\"Key\":2820951612,\"Product\":\"RED\",\"Summary\":\"Scalability test: API - Remove product from cart\"},{\"IssueType\":\"Sub-task\",\"Key\":3818948357,\"Product\":\"RED\",\"Summary\":\"Endurance test: API - Remove product from cart\"},{\"IssueType\":\"Sub-task\",\"Key\":1743012791,\"Product\":\"RED\",\"Summary\":\"Security test: API - Remove product from cart\"}],\"Summary\":\"QA Test: API - Remove product from cart\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3818588295,\"Product\":\"RED\",\"Summary\":\"Implement HTTP method: DELETE\"},{\"IssueType\":\"Sub-task\",\"Key\":3854071837,\"Product\":\"RED\",\"Summary\":\"TDD test: DELETE method implementation\"}],\"Summary\":\"Implement API: Remove product from cart\"},{\"IssueType\":\"Task\",\"Key\":1083004321,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":2907633477,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":331031969,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate remove from cart verification\"},{\"IssueType\":\"Sub-task\",\"Key\":3229756014,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Test remove from cart and cart total\"},{\"IssueType\":\"Sub-task\",\"Key\":2496940019,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Manually verify remove from cart\"}],\"Summary\":\"QA Test: Verify remove from cart functionality\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":1953736875,\"Product\":\"RED\",\"Summary\":\"Implement UI Control: Remove button/icon\"}],\"Summary\":\"Implement Frontend: Remove from cart functionality\"},{\"IssueType\":\"Task\",\"Key\":3388362231,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":2352089723,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":494599977,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate subtotal, tax, and total verification\"},{\"IssueType\":\"Sub-task\",\"Key\":1223342876,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Test calculations with different product quantities\"},{\"IssueType\":\"Sub-task\",\"Key\":4178030494,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Manually verify subtotal, tax, total\"}],\"Summary\":\"QA Test: Verify subtotal, tax, and total calculations\"}],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement Frontend: Display subtotal, tax, and total\"},{\"IssueType\":\"Task\",\"Key\":2371012881,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":3501700263,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":789882210,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate API cart total calculation\"},{\"IssueType\":\"Sub-task\",\"Key\":2813256911,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Test API cart total calculation with different items\"},{\"IssueType\":\"Sub-task\",\"Key\":355474434,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Manually test API cart total calculation\"}],\"Summary\":\"QA Test: API - Cart total calculation\"}],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement API: Calculate cart total\"},{\"IssueType\":\"Task\",\"Key\":2387504967,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":2325956087,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":156076750,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate database interaction verification\"},{\"IssueType\":\"Sub-task\",\"Key\":1359106335,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Test database interactions for different operations\"},{\"IssueType\":\"Sub-task\",\"Key\":1711843640,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Manually verify database interactions\"}],\"Summary\":\"QA Test: Verify database interactions for cart operations\"}],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement Backend: Handle database interactions for cart operations\"},{\"IssueType\":\"Task\",\"Key\":2592259653,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":1199399705,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2053187516,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate security tests for cart API\"},{\"IssueType\":\"Sub-task\",\"Key\":762358629,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Test security measures implemented\"},{\"IssueType\":\"Sub-task\",\"Key\":1776317210,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Manually verify security measures\"},{\"IssueType\":\"Sub-task\",\"Key\":1178659292,\"Product\":\"RED\",\"Summary\":\"Security test: API - Cart API\"}],\"Summary\":\"QA Test: Security - Cart API\"}],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement Security: Secure the cart API\"},{\"IssueType\":\"Task\",\"Key\":2812250475,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement Logging: Log cart related actions\"},{\"IssueType\":\"Task\",\"Key\":2084208626,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement Error Handling: Handle cart related errors gracefully\"},{\"IssueType\":\"Task\",\"Key\":1519123928,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":1058240876,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3647575467,\"Product\":\"RED\",\"Summary\":\"Test Automation: Automate product existence validation verification\"},{\"IssueType\":\"Sub-task\",\"Key\":3450196410,\"Product\":\"RED\",\"Summary\":\"Integration Tests: Test validation on API calls\"},{\"IssueType\":\"Sub-task\",\"Key\":4203393154,\"Product\":\"RED\",\"Summary\":\"Manual Workflow Validation Tests: Manually verify validation\"}],\"Summary\":\"QA Test: Verify product existence validation\"}],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement API: Validate product exists before adding to cart\"},{\"IssueType\":\"Task\",\"Key\":2491086844,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement Session Management: Track user's cart using session\"},{\"IssueType\":\"Task\",\"Key\":2083082269,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Database: Implement Cart Table Indexing\"},{\"IssueType\":\"Task\",\"Key\":2039637177,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement Frontend: Display empty cart message\"},{\"IssueType\":\"Task\",\"Key\":1586347248,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement Frontend: Persist cart data on page refresh\"},{\"IssueType\":\"Task\",\"Key\":554000014,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Database: Implement Data Normalization\"},{\"IssueType\":\"Task\",\"Key\":3504032079,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement Cart Abandonment Recovery Mechanism\"},{\"IssueType\":\"Task\",\"Key\":13037452,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement API: Promotional Code Application\"},{\"IssueType\":\"Task\",\"Key\":3913544398,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement Database: Optimize Cart Queries\"},{\"IssueType\":\"Task\",\"Key\":3652760643,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement API: Handle edge cases for extremely large carts\"},{\"IssueType\":\"Task\",\"Key\":1740009953,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement API: Structured Filtering, Sorting and Pagination\"},{\"IssueType\":\"Task\",\"Key\":547472666,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement API: Cart API Versioning\"},{\"IssueType\":\"Task\",\"Key\":3298984056,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement API: Async Cart Updates\"},{\"IssueType\":\"Task\",\"Key\":2859306544,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement Frontend: Multiple Payment Features\"},{\"IssueType\":\"Task\",\"Key\":1728623084,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement Frontend: Visual Clarity in Cart and Checkout\"},{\"IssueType\":\"Task\",\"Key\":3997774587,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement API: Integration with External Systems\"},{\"IssueType\":\"Task\",\"Key\":1020035733,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement API: Support for Cart-Level Coupons\"},{\"IssueType\":\"Task\",\"Key\":1246149832,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement Frontend: Cart Persistence Across Devices\"},{\"IssueType\":\"Task\",\"Key\":2369029912,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement Security: Protection Against SQL Injection\"},{\"IssueType\":\"Task\",\"Key\":103066451,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement Security: SSL Certificate\"},{\"IssueType\":\"Task\",\"Key\":2195763979,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement Security: Adherence to Privacy Regulations\"},{\"IssueType\":\"Task\",\"Key\":2895727102,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement API: Calculation of Estimated Shipping Costs and Taxes\"},{\"IssueType\":\"Task\",\"Key\":1674904074,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement API: Persistent Cart on Log-In\"},{\"IssueType\":\"Task\",\"Key\":1600984849,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement API: Support for Backorders\"},{\"IssueType\":\"Task\",\"Key\":3073713616,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement Frontend: Real-time Updates Using WebSockets\"},{\"IssueType\":\"Task\",\"Key\":779567271,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement Security: PCI DSS Compliance\"},{\"IssueType\":\"Task\",\"Key\":416983865,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement Frontend: Error Notifications\"},{\"IssueType\":\"Task\",\"Key\":1286162705,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement Security: Two-Factor Authentication\"},{\"IssueType\":\"Task\",\"Key\":2620529456,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Implement API: Rate Limiting\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3890413358,\"Product\":\"RED\",\"Summary\":\"Document the procedure for adding items to the shopping cart.\"}],\"Summary\":\"As a user, I want to add products to a shopping cart, so that I can keep track of the items I want to purchase.\"},{\"IssueType\":\"Story\",\"Key\":3723841851,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":1681819642,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":null,\"Summary\":\"Integrated Test: Ensure that online purchases are secure and encrypted to protect user data.\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3112193015,\"Product\":\"RED\",\"Summary\":\"Document the secure purchase process, highlighting security measures.\"}],\"Summary\":\"As a user, I want to complete secure online purchases, so that I can buy the items in my shopping cart with confidence.\"}],\"UserStoryList\":[\"As a user, I want to browse products on a mobile-first e-commerce platform, so that I can easily find and view items on my mobile device.\",\"As a user, I want to add products to a shopping cart, so that I can keep track of the items I want to purchase.\",\"As a user, I want to complete secure online purchases, so that I can buy the items in my shopping cart with confidence.\"]}";
            json ??= testJson;

            GFSGeminiClientHost.Result result = new(-1, json);
            IssueGeneratorBaseArgs args = new(result);
            try
            {
                TreeSerialization.IssueResults? issueResults =
                    JsonSerializer.Deserialize<TreeSerialization.IssueResults>(json);
                if( issueResults == null ) return;

                List<IssueData.Issue>? issues = issueResults.Issues;
                if( issues != null )
                {
                    args.Issues = issues;
                    UserStoryGeneratorCompleted?.Invoke(args);
                }

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