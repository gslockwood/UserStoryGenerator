using System.Text.Json;
using UserStoryGenerator.Utilities;
using UserStoryGenerator.View;
using static UserStoryGenerator.Model.GFSGeminiClientHost;
using static UserStoryGenerator.Model.IssueData;

namespace UserStoryGenerator.Model
{
    public class Model
    {
        public readonly static string DEFAULTKEY = "google cloud gemini ai api key";

        readonly JsonSerializerOptions options = new() { WriteIndented = true };
        public Settings Settings { get; private set; }

        public void SetSettings(Settings settings) { Settings = settings; }

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
                if( !File.Exists(@$".\Data\Settings.json") )//throw new FileNotFoundException($"{SettingsFileName} is not found");
                    json = Settings.Template;

                else
                    json = File.ReadAllText(@$".\Data\Settings.json");

                //Logger.Info(json);

                // testing
                //json = UserStoryGenerator.Properties.Resources.Settings;

                if( json == null ) throw new NullReferenceException(nameof(json));

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
                if( Settings.JiraIssueTypes == null ) throw new NullReferenceException(nameof(Settings.JiraIssueTypes));
                if( Settings.JiraIssueTypes.Count != 2 ) throw new Exception(nameof(Settings.JiraIssueTypes));

                if( temp != null )
                {
                    // remove this eventually
                    if( temp.JiraIssueTypes != null )
                    {
                        foreach( var item in temp.JiraIssueTypes.Values )
                        {
                            if( item.ImagePath == null ) continue;
                            if( !item.ImagePath.Contains("Data", StringComparison.OrdinalIgnoreCase) )
                            {
                                item.ImagePath = item.ImagePath.Replace("Resources", "Data/Resources");
                            }
                        }
                    }

                    Settings = temp;
                    //
                }


                if( Settings.JiraIssueTypes == null ) throw new NullReferenceException(nameof(Settings.JiraIssueTypes));

                IEnumerable<KeyValuePair<string, Settings.JiraIssueType>> any = Settings.JiraIssueTypes.Where(type => type.Value.Order == 2);
                if( !any.Any() ) throw new NullReferenceException("subTaskIssueType is missing");
                if( any.Count() > 1 ) throw new NullReferenceException("more than 1 subTaskIssueType");


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
                //ProductDescription
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

            if( Settings.JiraIssueTypes == null ) return false;

            string? epicIssueType = null;
            IEnumerable<KeyValuePair<string, Settings.JiraIssueType>> any = Settings.JiraIssueTypes.Where(type => type.Value.Order == 0);
            if( any.Any() )
            {
                KeyValuePair<string, Settings.JiraIssueType> toIssue = any.First();
                epicIssueType = toIssue.Value.IssueType;
            }

            any = Settings.JiraIssueTypes.Where(type => type.Value.Order == 2);
            if( any.Any() )
            {
                KeyValuePair<string, Settings.JiraIssueType> toIssue = any.First();
                if( toIssue.Value != null && toIssue.Value.IssueType != null )
                {
                    string subTaskIssueType = toIssue.Value.IssueType;

                    string csv = Converter.ToCSV(epicText, epicIssueType, subTaskIssueType, userStoryResults);
                    File.WriteAllText(fullFilePath, csv);
                    return true;
                }
            }

            throw new NullReferenceException("subTaskIssueType is missing");

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
            //Utilities.Logger.Info(result);
            File.WriteAllText(fullFilePath, result);
        }

        private static List<Issue>? ProcessIssues(string json)
        {
            try
            {
                //Logger.Info(json);
                IssueData? issueData = JsonSerializer.Deserialize<IssueData>(json);
                //if( issueData == null ) throw new NullReferenceException("The response could not be Deserialized.");
                return issueData == null ? throw new NullReferenceException("The response could not be Deserialized.") : issueData.Issues;
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
            if( Settings.JiraIssueTypes == null ) throw new NullReferenceException(nameof(Settings.JiraIssueTypes));

            IssueGeneratorBaseInputArgs issueGeneratorBaseInputArgs = new()
            {
                Key = Settings.Key,
                JiraProject = jiraProject,
                ProductName = productName,
                Target = target,
                AddQATests = addQATests,
                AddSubTasks = addSubTasks,
                MaxStories = maxStories,
                Settings = Settings,
                AICoaching = Settings.UserStoryCoaching,
            };

            IssueGeneratorUserStories? issueGenerator = new(issueGeneratorBaseInputArgs);
            issueGenerator.Completed += (args) =>
            {
                GFSGeminiClientHost.Result result = args.Result;
                try
                {
                    if( result == null ) throw new NullReferenceException("The Result came back empty (null).");
                    if( result.Answer == null ) throw new NullReferenceException("The Result came back empty.");

                    //Logger.Info(result.Answer);

                    ProcessResults(args, result);

                }
                catch( Exception ex )
                {
                    result.ErrorCode = -2;
                    Logger.Info(ex.Message);
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

            await Task.Delay(0);
            issueGenerator.RequestAnswer();
            //
        }

        internal async Task ProcessStoryList(string productName, bool addQATests, bool addSubTasks, int maxStories, List<StoryPackage> list)//async Task 
        {
            //    const string testAnswer = "{\"Issues\":[{\"Summary\":\"Implement product browsing functionality on the frontend\",\"IssueType\":\"Task\",\"Description\":\"Develop the frontend components and logic to display a list of products to the user. This includes fetching product data from the backend API, rendering the product list, and implementing pagination or infinite scrolling. The frontend should be responsive and provide a good user experience across different devices.\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"QA Test product browsing functionality on the frontend\",\"IssueType\":\"Test\",\"Description\":\"Perform thorough testing of the product browsing functionality on the frontend. This includes testing the display of product information, pagination, filtering, and sorting. Verify the responsiveness of the UI across different devices and browsers. Ensure that the frontend handles errors gracefully and provides informative error messages to the user.\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Test Automation: Frontend product browsing\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Integration Tests: Frontend product browsing with API\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Manual Workflow Validation Tests: Frontend product browsing\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null}]},{\"Summary\":\"QA Test product browsing functionality on the frontend - Edge Cases\",\"IssueType\":\"Test\",\"Description\":\"Perform thorough testing of the product browsing functionality on the frontend. This includes testing the display of product information, pagination, filtering, and sorting. Verify the responsiveness of the UI across different devices and browsers. Ensure that the frontend handles errors gracefully and provides informative error messages to the user.\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Test Automation: Frontend product browsing - Edge Cases\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Integration Tests: Frontend product browsing with API - Edge Cases\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Manual Workflow Validation Tests: Frontend product browsing - Edge Cases\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null}]}],\"Subtasks\":[{\"Summary\":\"Implement UI Control: Product List\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Implement UI Control: Pagination\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Implement UI Control: Filter\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"TDD: Unit test frontend product browsing\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null}]},{\"Summary\":\"Implement API endpoint for retrieving products\",\"IssueType\":\"Task\",\"Description\":\"Develop the backend API endpoint to retrieve a list of products from the database. This includes implementing the necessary data access logic, handling pagination and filtering parameters, and returning the product data in a JSON format. The API should be secured and handle authentication and authorization properly.\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"QA Test API endpoint for retrieving products\",\"IssueType\":\"Test\",\"Description\":\"Perform thorough testing of the API endpoint for retrieving products. This includes testing the retrieval of product data, pagination, filtering, and error handling. Verify the security of the API and ensure that it handles authentication and authorization properly. Test the API with different types of requests and data to ensure its robustness.\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Test Automation: API endpoint for retrieving products\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Integration Tests: API endpoint with database\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Manual Workflow Validation Tests: API endpoint for retrieving products\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null}]},{\"Summary\":\"QA Test API endpoint for retrieving products - Stress, load, load balancing, Latency, Scalability, Endurance, and Security\",\"IssueType\":\"Test\",\"Description\":\"Perform thorough testing of the API endpoint for retrieving products. This includes testing the retrieval of product data, pagination, filtering, and error handling. Verify the security of the API and ensure that it handles authentication and authorization properly. Test the API with different types of requests and data to ensure its robustness.\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Stress Test: API endpoint for retrieving products\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Load Test: API endpoint for retrieving products\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Load Balancing Test: API endpoint for retrieving products\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Latency Test: API endpoint for retrieving products\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Scalability Test: API endpoint for retrieving products\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Endurance Test: API endpoint for retrieving products\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Security Test: API endpoint for retrieving products\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null}]}],\"Subtasks\":[{\"Summary\":\"Implement HTTP method: GET\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"TDD: Unit test API endpoint for retrieving products\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null}]},{\"Summary\":\"Implement product data model in the database\",\"IssueType\":\"Task\",\"Description\":\"Define the schema for storing product information in the database. This includes defining the data types, constraints, and relationships between different tables. Ensure that the data model is optimized for performance and scalability.\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"QA Test product data model in the database\",\"IssueType\":\"Test\",\"Description\":\"Perform thorough testing of the product data model in the database. This includes testing the data types, constraints, and relationships between different tables. Verify the performance and scalability of the data model. Ensure that the database handles errors gracefully and provides informative error messages.\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Test Automation: Product data model in the database\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Integration Tests: Product data model with API\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Manual Workflow Validation Tests: Product data model in the database\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null}]}],\"Subtasks\":[{\"Summary\":\"Implement CRUD method: create\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Implement CRUD method: read\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Implement CRUD method: update\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Implement CRUD method: delete\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"TDD: Unit test product data model\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null}]},{\"Summary\":\"Implement product browsing service\",\"IssueType\":\"Task\",\"Description\":\"Develop a service to handle product browsing logic. This includes fetching products from the database, applying filters and sorting, and returning the results to the API layer. The service should be designed for scalability and performance.\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"QA Test product browsing service\",\"IssueType\":\"Test\",\"Description\":\"Perform thorough testing of the product browsing service. This includes testing the retrieval of product data, filtering, and sorting. Verify the performance and scalability of the service. Ensure that the service handles errors gracefully and provides informative error messages.\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Test Automation: Product browsing service\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Integration Tests: Product browsing service with API and database\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Manual Workflow Validation Tests: Product browsing service\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null}]}],\"Subtasks\":[{\"Summary\":\"TDD: Unit test product browsing service\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null}]},{\"Summary\":\"Implement filtering functionality for product browsing\",\"IssueType\":\"Task\",\"Description\":\"Develop the functionality to allow users to filter products based on various criteria such as price, category, and brand. This includes implementing the necessary UI controls on the frontend, updating the API endpoint to handle filtering parameters, and modifying the product browsing service to apply the filters.\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"QA Test filtering functionality for product browsing\",\"IssueType\":\"Test\",\"Description\":\"Perform thorough testing of the filtering functionality for product browsing. This includes testing the filtering of products based on various criteria such as price, category, and brand. Verify the accuracy of the filtering results and ensure that the UI controls are working correctly. Test the filtering functionality with different types of requests and data to ensure its robustness.\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Test Automation: Filtering functionality for product browsing\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Integration Tests: Filtering functionality with API, service, and database\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Manual Workflow Validation Tests: Filtering functionality for product browsing\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null}]}],\"Subtasks\":[{\"Summary\":\"Implement UI Control: Filter Price\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Implement UI Control: Filter Category\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Implement UI Control: Filter Brand\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"TDD: Unit test filtering functionality\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null}]},{\"Summary\":\"Implement sorting functionality for product browsing\",\"IssueType\":\"Task\",\"Description\":\"Develop the functionality to allow users to sort products based on various criteria such as price, popularity, and rating. This includes implementing the necessary UI controls on the frontend, updating the API endpoint to handle sorting parameters, and modifying the product browsing service to apply the sorting.\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"QA Test sorting functionality for product browsing\",\"IssueType\":\"Test\",\"Description\":\"Perform thorough testing of the sorting functionality for product browsing. This includes testing the sorting of products based on various criteria such as price, popularity, and rating. Verify the accuracy of the sorting results and ensure that the UI controls are working correctly. Test the sorting functionality with different types of requests and data to ensure its robustness.\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Test Automation: Sorting functionality for product browsing\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Integration Tests: Sorting functionality with API, service, and database\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Manual Workflow Validation Tests: Sorting functionality for product browsing\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null}]}],\"Subtasks\":[{\"Summary\":\"Implement UI Control: Sort Price\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Implement UI Control: Sort Popularity\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Implement UI Control: Sort Rating\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"TDD: Unit test sorting functionality\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null}]},{\"Summary\":\"Implement pagination for product browsing\",\"IssueType\":\"Task\",\"Description\":\"Develop the functionality to paginate the list of products displayed to the user. This includes implementing the necessary UI controls on the frontend, updating the API endpoint to handle pagination parameters, and modifying the product browsing service to retrieve products in batches.\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"QA Test pagination for product browsing\",\"IssueType\":\"Test\",\"Description\":\"Perform thorough testing of the pagination functionality for product browsing. This includes testing the display of products in batches, the navigation between pages, and the handling of edge cases such as empty pages. Verify the performance of the pagination functionality and ensure that it does not degrade the user experience.\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Test Automation: Pagination for product browsing\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Integration Tests: Pagination with API, service, and database\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Manual Workflow Validation Tests: Pagination for product browsing\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null}]}],\"Subtasks\":[{\"Summary\":\"Implement UI Control: Pagination Buttons\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"TDD: Unit test pagination functionality\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null}]},{\"Summary\":\"Implement error handling for product browsing\",\"IssueType\":\"Task\",\"Description\":\"Develop the error handling logic for the product browsing functionality. This includes handling errors in the frontend, API, and service layers. Ensure that errors are logged properly and that informative error messages are displayed to the user.\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"QA Test error handling for product browsing\",\"IssueType\":\"Test\",\"Description\":\"Perform thorough testing of the error handling logic for the product browsing functionality. This includes testing the handling of errors in the frontend, API, and service layers. Verify that errors are logged properly and that informative error messages are displayed to the user. Test the error handling functionality with different types of errors to ensure its robustness.\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Test Automation: Error handling for product browsing\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Integration Tests: Error handling with API, service, and database\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Manual Workflow Validation Tests: Error handling for product browsing\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null}]}],\"Subtasks\":[{\"Summary\":\"TDD: Unit test error handling\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null}]},{\"Summary\":\"Implement logging for product browsing\",\"IssueType\":\"Task\",\"Description\":\"Implement logging throughout the product browsing functionality to track user activity, errors, and performance metrics. This will help in debugging issues and monitoring the performance of the application.\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"QA Test logging for product browsing\",\"IssueType\":\"Test\",\"Description\":\"Perform thorough testing of the logging functionality for the product browsing functionality. Verify that user activity, errors, and performance metrics are being logged properly. Ensure that the logs are being stored in a secure location and that they can be accessed for debugging and monitoring purposes.\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Test Automation: Logging for product browsing\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Integration Tests: Logging with API, service, and database\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Manual Workflow Validation Tests: Logging for product browsing\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null}]}],\"Subtasks\":[{\"Summary\":\"TDD: Unit test logging functionality\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null}]},{\"Summary\":\"Implement security measures for product browsing\",\"IssueType\":\"Task\",\"Description\":\"Implement security measures to protect the product browsing functionality from unauthorized access and malicious attacks. This includes implementing authentication and authorization, validating user input, and protecting against common web vulnerabilities.\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"QA Test security measures for product browsing\",\"IssueType\":\"Test\",\"Description\":\"Perform thorough testing of the security measures for the product browsing functionality. This includes testing authentication and authorization, validating user input, and testing for common web vulnerabilities. Verify that the application is protected from unauthorized access and malicious attacks.\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Test Automation: Security measures for product browsing\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Integration Tests: Security measures with API, service, and database\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null},{\"Summary\":\"Manual Workflow Validation Tests: Security measures for product browsing\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null}]}],\"Subtasks\":[{\"Summary\":\"TDD: Unit test security measures\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\",\"Description\":null}]}]}";
            //again:

            //    GFSGeminiClientHost.Result result = new(-1, testAnswer);
            //    IssueGeneratorBaseArgs args = new(result);

            //    ProcessResults(args, result);

            //    goto again;


            if( Settings == null ) throw new NullReferenceException(nameof(Settings));
            if( Settings.Key == null ) throw new NullReferenceException(nameof(Settings.Key));
            if( Settings.JiraIssueTypes == null ) throw new NullReferenceException(nameof(Settings.JiraIssueTypes));

            int counter = list.Count;

            foreach( StoryPackage storyPackage in list )
            {
                if( storyPackage.JiraProduct == null ) continue;

                IssueGeneratorBaseInputArgs issueGeneratorBaseInputArgs = new()
                {
                    Key = this.Settings.Key,
                    JiraProject = storyPackage.JiraProduct,
                    ProductName = productName,
                    Target = storyPackage.UserStoryText,
                    AddQATests = addQATests,
                    AddSubTasks = addSubTasks,
                    AICoaching = this.Settings.AllIssueCoaching,
                    MaxStories = maxStories,
                    Settings = this.Settings,
                };

                IssueGeneratorAllIssues? issueGenerator = new(issueGeneratorBaseInputArgs);
                issueGenerator.Completed += (args) =>
                {
                    IssueGeneratorBaseArgsEx issueGeneratorBaseArgsEx = new(args.Result, --counter, storyPackage.Key);

                    GFSGeminiClientHost.Result result = args.Result;
                    try
                    {
                        if( result == null ) throw new NullReferenceException("The Result came back empty (null).");
                        if( result.Answer == null ) throw new NullReferenceException("The Result came back empty.");

                        ProcessResults(issueGeneratorBaseArgsEx, result);

                    }
                    catch( Exception ex )
                    {
                        result.ErrorCode = -2;
                        Logger.Info(ex.Message);
                    }
                    finally
                    {
                        IssueGeneratorCompleted?.Invoke(issueGeneratorBaseArgsEx);
                    }
                };

                await Task.Delay(250);
                try
                {
                    //Logger.Info($"{storyPackage.UserStoryText}");
                    issueGenerator.RequestAnswer();
                }
                catch( Exception )
                {
                    throw;
                }
            }

        }

        private static void ProcessResults(IssueGeneratorBaseArgs arg, Result result)
        {
            if( result.Answer == null )
            {
                result.ErrorCode = -2;
                throw new NullReferenceException("The Result Answer came back empty.");
            }
            Logger.Info(result.Answer);

            if( Utilities.JsonValidator.IsValidJson(result.Answer) )
            {
                arg.Issues = ProcessIssues(result.Answer);
                if( arg.Issues == null || arg.Issues.Count == 0 )
                    result.ErrorCode = -1;

                //else if( issueGeneratorBaseArgsEx.Issues[0] != null && issueGeneratorBaseArgsEx.Issues[0].IssueType.Equals("Story") )
                //{
                //    List<Issue>? iinkedIssues = issueGeneratorBaseArgsEx.Issues[0].LinkedIssues;
                //    issueGeneratorBaseArgsEx.Issues = iinkedIssues;
                //}

            }
            else
                result.ErrorCode = -99;
            //
        }

        internal string? CreateUserStories(string? json = null)
        {
            const string testJson = "{\"Issues\":[{\"Description\":\"As a user, I want to be able to easily browse the available products on the e-commerce platform. This includes being able to view product images, descriptions, and prices.  The browsing experience should be intuitive and allow me to quickly find the products I am looking for. Acceptance Criteria: Given I am on the product listing page. When I scroll through the products. Then I should see product images, descriptions, and prices. Given I use the search bar with a keyword. When I press enter. Then I should see a list of products related to my keyword.\",\"IssueType\":\"Story\",\"Key\":1027481699,\"LinkedIssues\":[{\"Description\":\"This test verifies that users can successfully browse products on the e-commerce platform. It checks the display of product information, the functionality of search filters, and the overall browsing experience. We must ensure that the images load properly. We must make sure there are no broken links.  We must make sure the product titles are correct. Acceptance Criteria: Given I am on the product listing page. When I scroll through the products. Then I should see product images, descriptions, and prices. Given I use the search bar with a keyword. When I press enter. Then I should see a list of products related to my keyword.\",\"IssueType\":\"Test\",\"Key\":2048383677,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":3471032040,\"Product\":\"RED\",\"Summary\":\"Create test cases for product browsing functionality.\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":3958085019,\"Product\":\"RED\",\"Summary\":\"Execute test cases for product browsing functionality.\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":2547377978,\"Product\":\"RED\",\"Summary\":\"Document test results for product browsing functionality.\"}],\"Summary\":\"Integrated Test: Verify product browsing functionality.\"}],\"Product\":\"RED\",\"Subtasks\":[{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":1161126207,\"Product\":\"RED\",\"Summary\":\"Document usability guidelines\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":1632585281,\"Product\":\"RED\",\"Summary\":\"UI/UX style guide\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":3731122695,\"Product\":\"RED\",\"Summary\":\"UI/UX wireframe mock-up\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":4021797797,\"Product\":\"RED\",\"Summary\":\"Document product browsing functionality\"}],\"Summary\":\"As a user, I want to browse products, so that I can find items I am interested in purchasing.\"},{\"Description\":\"As a user, I want to be able to add products to a shopping cart so that I can purchase multiple items in a single transaction. This includes being able to easily add items, adjust quantities, and remove items from the cart. Acceptance Criteria: Given I am viewing a product. When I click the 'Add to Cart' button. Then the product should be added to my shopping cart. Given I am in my shopping cart. When I change the quantity of a product. Then the total price should automatically update.\",\"IssueType\":\"Story\",\"Key\":3807573754,\"LinkedIssues\":[{\"Description\":\"This test verifies that users can successfully add products to their shopping cart. It checks the ability to add items, adjust quantities, and remove items. We must ensure that the cart updates in real time. We must make sure there are no memory leaks. We must make sure the shopping cart page loads quickly. Acceptance Criteria: Given I am viewing a product. When I click the 'Add to Cart' button. Then the product should be added to my shopping cart. Given I am in my shopping cart. When I change the quantity of a product. Then the total price should automatically update.\",\"IssueType\":\"Test\",\"Key\":4145719468,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":2918114295,\"Product\":\"RED\",\"Summary\":\"Create test cases for add to cart functionality.\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":2560698856,\"Product\":\"RED\",\"Summary\":\"Execute test cases for add to cart functionality.\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":1162782013,\"Product\":\"RED\",\"Summary\":\"Document test results for add to cart functionality.\"}],\"Summary\":\"Integrated Test: Verify add to cart functionality.\"}],\"Product\":\"RED\",\"Subtasks\":[{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":1896668326,\"Product\":\"RED\",\"Summary\":\"Document usability guidelines\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":3128679205,\"Product\":\"RED\",\"Summary\":\"UI/UX style guide\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":2517123900,\"Product\":\"RED\",\"Summary\":\"UI/UX wireframe mock-up\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":3939677649,\"Product\":\"RED\",\"Summary\":\"Document shopping cart functionality\"}],\"Summary\":\"As a user, I want to add products to a shopping cart, so that I can purchase multiple items at once.\"},{\"Description\":\"As a user, I want to be able to complete secure online purchases, so that my payment information is protected during the transaction.  This includes secure handling of credit card information and integration with trusted payment gateways. Acceptance Criteria: Given I am on the checkout page. When I enter my payment information. Then my information should be encrypted. Given I complete my purchase. When the transaction is successful. Then I should receive a confirmation email.\",\"IssueType\":\"Story\",\"Key\":628608329,\"LinkedIssues\":[{\"Description\":\"This test verifies that users can complete secure online purchases and that their payment information is protected. It checks the encryption of payment data and the integration with payment gateways.  We must verify there are no man in the middle attacks. We must simulate load testing.  We must verify there are no exploits such as SQL injection attempts. Acceptance Criteria: Given I am on the checkout page. When I enter my payment information. Then my information should be encrypted. Given I complete my purchase. When the transaction is successful. Then I should receive a confirmation email.\",\"IssueType\":\"Test\",\"Key\":502990550,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":2635326035,\"Product\":\"RED\",\"Summary\":\"Create test cases for secure online purchase functionality.\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":1041772634,\"Product\":\"RED\",\"Summary\":\"Execute test cases for secure online purchase functionality.\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":89848815,\"Product\":\"RED\",\"Summary\":\"Document test results for secure online purchase functionality.\"}],\"Summary\":\"Integrated Test: Verify secure online purchase functionality.\"}],\"Product\":\"RED\",\"Subtasks\":[{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":2104924168,\"Product\":\"RED\",\"Summary\":\"Document usability guidelines\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":409679266,\"Product\":\"RED\",\"Summary\":\"UI/UX style guide\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":3944261820,\"Product\":\"RED\",\"Summary\":\"UI/UX wireframe mock-up\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":439123968,\"Product\":\"RED\",\"Summary\":\"Document secure purchase functionality\"}],\"Summary\":\"As a user, I want to complete secure online purchases, so that my payment information is protected.\"},{\"Description\":\"As a user, I want a streamlined checkout process so that I can quickly and easily complete my purchase. This includes minimizing the number of steps required and providing clear instructions. Acceptance Criteria: Given I have items in my shopping cart. When I proceed to checkout. Then I should be able to complete my purchase in fewer than 5 steps. Given the checkout process has an error. When the error occurs. Then the error message must be clear and concise.\",\"IssueType\":\"Story\",\"Key\":4029808633,\"LinkedIssues\":[{\"Description\":\"This test verifies that the checkout process is streamlined and easy to use. It checks the number of steps required, the clarity of instructions, and the overall user experience.  We must check to see if Autofill is present.  We must check to see if there are redundant fields. We must check to make sure the process is mobile-first responsive. Acceptance Criteria: Given I have items in my shopping cart. When I proceed to checkout. Then I should be able to complete my purchase in fewer than 5 steps. Given the checkout process has an error. When the error occurs. Then the error message must be clear and concise.\",\"IssueType\":\"Test\",\"Key\":1771885889,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":1587527731,\"Product\":\"RED\",\"Summary\":\"Create test cases for streamlined checkout process.\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":3923708080,\"Product\":\"RED\",\"Summary\":\"Execute test cases for streamlined checkout process.\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":3051499381,\"Product\":\"RED\",\"Summary\":\"Document test results for streamlined checkout process.\"}],\"Summary\":\"Integrated Test: Verify streamlined checkout process.\"}],\"Product\":\"RED\",\"Subtasks\":[{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":432019189,\"Product\":\"RED\",\"Summary\":\"Document usability guidelines\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":2981680304,\"Product\":\"RED\",\"Summary\":\"UI/UX style guide\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":1840126983,\"Product\":\"RED\",\"Summary\":\"UI/UX wireframe mock-up\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":2880404119,\"Product\":\"RED\",\"Summary\":\"Document streamlined checkout process\"}],\"Summary\":\"As a user, I want a streamlined checkout process, so that I can quickly and easily complete my purchase.\"},{\"Description\":\"As a user, I want to be able to pay using my preferred method, so I expect diverse payment options.  This includes credit cards, debit cards, digital wallets, and other common payment methods. Acceptance Criteria: Given I am on the checkout page. When I reach the payment selection step. Then I should see options for credit card, debit card, and digital wallets. Given I select a digital wallet option. When I click to proceed. Then I should be redirected to the digital wallet's payment portal.\",\"IssueType\":\"Story\",\"Key\":279508119,\"LinkedIssues\":[{\"Description\":\"This test verifies that the platform offers diverse payment options. It checks the availability of credit cards, debit cards, digital wallets, and other payment methods.  We must ensure that all payment methods are functioning correctly.  We must ensure that there is proper logging. We must check the response times of the third party APIs. Acceptance Criteria: Given I am on the checkout page. When I reach the payment selection step. Then I should see options for credit card, debit card, and digital wallets. Given I select a digital wallet option. When I click to proceed. Then I should be redirected to the digital wallet's payment portal.\",\"IssueType\":\"Test\",\"Key\":1926819237,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":1456799658,\"Product\":\"RED\",\"Summary\":\"Create test cases for diverse payment options.\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":3726861945,\"Product\":\"RED\",\"Summary\":\"Execute test cases for diverse payment options.\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":3000499338,\"Product\":\"RED\",\"Summary\":\"Document test results for diverse payment options.\"}],\"Summary\":\"Integrated Test: Verify diverse payment options.\"}],\"Product\":\"RED\",\"Subtasks\":[{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":2988315958,\"Product\":\"RED\",\"Summary\":\"Document usability guidelines\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":1260021093,\"Product\":\"RED\",\"Summary\":\"UI/UX style guide\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":3030092753,\"Product\":\"RED\",\"Summary\":\"UI/UX wireframe mock-up\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":2573199200,\"Product\":\"RED\",\"Summary\":\"Document supported payment options\"}],\"Summary\":\"As a user, I want diverse payment options, so that I can pay using my preferred method.\"},{\"Description\":\"As a user, I want robust order tracking capabilities, so that I can monitor the status of my order from placement to delivery.  This includes real-time updates and notifications. Acceptance Criteria: Given I have placed an order. When I log in to my account. Then I should see a list of my orders and their current status. Given my order status changes. When the status changes. Then I should receive an email or SMS notification.\",\"IssueType\":\"Story\",\"Key\":2744541043,\"LinkedIssues\":[{\"Description\":\"This test verifies that users can successfully track their orders and receive real-time updates. It checks the accuracy of order status information and the delivery of notifications.  We must verify that the user gets status updates for all order stages.  We must verify the links work to the tracking website. We must verify the data is correct. Acceptance Criteria: Given I have placed an order. When I log in to my account. Then I should see a list of my orders and their current status. Given my order status changes. When the status changes. Then I should receive an email or SMS notification.\",\"IssueType\":\"Test\",\"Key\":1306241903,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":1147624844,\"Product\":\"RED\",\"Summary\":\"Create test cases for robust order tracking capabilities.\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":1346232294,\"Product\":\"RED\",\"Summary\":\"Execute test cases for robust order tracking capabilities.\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":930997139,\"Product\":\"RED\",\"Summary\":\"Document test results for robust order tracking capabilities.\"}],\"Summary\":\"Integrated Test: Verify robust order tracking capabilities.\"}],\"Product\":\"RED\",\"Subtasks\":[{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":56000847,\"Product\":\"RED\",\"Summary\":\"Document usability guidelines\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":3584166930,\"Product\":\"RED\",\"Summary\":\"UI/UX style guide\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":4250283551,\"Product\":\"RED\",\"Summary\":\"UI/UX wireframe mock-up\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":3530318902,\"Product\":\"RED\",\"Summary\":\"Document order tracking functionality\"}],\"Summary\":\"As a user, I want robust order tracking capabilities, so that I can monitor the status of my order.\"},{\"Description\":\"As a user, I want a user-friendly interface so that I can easily navigate and use the platform without confusion.  This includes clear navigation, intuitive design, and helpful tooltips. Acceptance Criteria: Given I am a new user. When I visit the platform for the first time. Then I should be able to easily find the search bar, product categories, and my account settings. Given I hover over an icon. When I hover over the icon. Then a tooltip should appear explaining the icon's function.\",\"IssueType\":\"Story\",\"Key\":1143702617,\"LinkedIssues\":[{\"Description\":\"This test verifies that the platform's interface is user-friendly and easy to navigate. It checks the clarity of navigation, the intuitiveness of design, and the helpfulness of tooltips. We must make sure the response times are fast.  We must make sure the UI is modern.  We must make sure the process is mobile-first responsive. Acceptance Criteria: Given I am a new user. When I visit the platform for the first time. Then I should be able to easily find the search bar, product categories, and my account settings. Given I hover over an icon. When I hover over the icon. Then a tooltip should appear explaining the icon's function.\",\"IssueType\":\"Test\",\"Key\":3390776199,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":3928807188,\"Product\":\"RED\",\"Summary\":\"Create test cases for user-friendly interface.\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":4136927629,\"Product\":\"RED\",\"Summary\":\"Execute test cases for user-friendly interface.\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":1052848531,\"Product\":\"RED\",\"Summary\":\"Document test results for user-friendly interface.\"}],\"Summary\":\"Integrated Test: Verify user-friendly interface.\"}],\"Product\":\"RED\",\"Subtasks\":[{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":1965707513,\"Product\":\"RED\",\"Summary\":\"Document usability guidelines\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":2541452836,\"Product\":\"RED\",\"Summary\":\"UI/UX style guide\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":1888883469,\"Product\":\"RED\",\"Summary\":\"UI/UX wireframe mock-up\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":2883979750,\"Product\":\"RED\",\"Summary\":\"Document UI/UX design\"}],\"Summary\":\"As a user, I want a user-friendly interface, so that I can easily navigate and use the platform.\"},{\"Description\":\"As a user, I want an accessible platform, so that users with disabilities can use the online store effectively.  This includes adherence to accessibility standards like WCAG. Acceptance Criteria: Given I am using a screen reader. When I navigate the platform. Then the screen reader should be able to read all content and controls. Given I am using keyboard navigation. When I navigate the platform. Then I should be able to access all interactive elements using the keyboard.\",\"IssueType\":\"Story\",\"Key\":4259455547,\"LinkedIssues\":[{\"Description\":\"This test verifies that the platform is accessible to users with disabilities. It checks adherence to accessibility standards like WCAG and the usability of assistive technologies.  We must verify that images have alt tags.  We must verify the color contrast is sufficient. We must verify the site is screen-reader compatible. Acceptance Criteria: Given I am using a screen reader. When I navigate the platform. Then the screen reader should be able to read all content and controls. Given I am using keyboard navigation. When I navigate the platform. Then I should be able to access all interactive elements using the keyboard.\",\"IssueType\":\"Test\",\"Key\":196380748,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":1146241466,\"Product\":\"RED\",\"Summary\":\"Create test cases for platform accessibility.\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":4046422887,\"Product\":\"RED\",\"Summary\":\"Execute test cases for platform accessibility.\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":3216208596,\"Product\":\"RED\",\"Summary\":\"Document test results for platform accessibility.\"}],\"Summary\":\"Integrated Test: Verify platform accessibility.\"}],\"Product\":\"RED\",\"Subtasks\":[{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":535016688,\"Product\":\"RED\",\"Summary\":\"Document usability guidelines\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":3248039835,\"Product\":\"RED\",\"Summary\":\"UI/UX style guide\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":2772913300,\"Product\":\"RED\",\"Summary\":\"UI/UX wireframe mock-up\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":153927487,\"Product\":\"RED\",\"Summary\":\"Document accessibility standards\"}],\"Summary\":\"As a user, I want an accessible platform, so that users with disabilities can use the online store.\"},{\"Description\":\"As a user, I want a highly performant platform, so that the site is responsive and loads quickly for all users.  This includes optimized code, efficient database queries, and caching mechanisms. Acceptance Criteria: Given I am browsing the platform. When I load a page. Then the page should load in under 3 seconds. Given I am adding a product to my cart. When I click 'Add to Cart'. Then the product should be added to my cart within 1 second.\",\"IssueType\":\"Story\",\"Key\":2023927821,\"LinkedIssues\":[{\"Description\":\"This test verifies that the platform is highly performant and responsive. It checks page load times, transaction speeds, and overall site responsiveness.  We must check the CPU usage.  We must check the memory usage. We must load test the website. Acceptance Criteria: Given I am browsing the platform. When I load a page. Then the page should load in under 3 seconds. Given I am adding a product to my cart. When I click 'Add to Cart'. Then the product should be added to my cart within 1 second.\",\"IssueType\":\"Test\",\"Key\":3527172001,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":4069625419,\"Product\":\"RED\",\"Summary\":\"Create test cases for platform performance.\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":1497611275,\"Product\":\"RED\",\"Summary\":\"Execute test cases for platform performance.\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":527862355,\"Product\":\"RED\",\"Summary\":\"Document test results for platform performance.\"}],\"Summary\":\"Integrated Test: Verify platform performance.\"}],\"Product\":\"RED\",\"Subtasks\":[{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":1671723276,\"Product\":\"RED\",\"Summary\":\"Document usability guidelines\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":317866469,\"Product\":\"RED\",\"Summary\":\"UI/UX style guide\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":4275531346,\"Product\":\"RED\",\"Summary\":\"UI/UX wireframe mock-up\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":1626147638,\"Product\":\"RED\",\"Summary\":\"Document performance optimization\"}],\"Summary\":\"As a user, I want a highly performant platform, so that the site is responsive and loads quickly.\"},{\"Description\":\"As a developer, I need to create a database table called 'Customer' to store customer-related information.  This table should include columns for 'Name', 'Phone number', and other relevant details. Acceptance Criteria: Given I execute the database creation script. When the script completes. Then a table named 'Customer' should exist in the database. Given the 'Customer' table exists. When I describe the table. Then the table should include columns for 'Name', 'Phone number', and other relevant details.\",\"IssueType\":\"Story\",\"Key\":3052121167,\"LinkedIssues\":[{\"Description\":\"This test verifies that the 'Customer' database table is successfully created with the required columns. It checks the existence of the table and the presence of the specified columns.  We must ensure that the table constraints are set properly.  We must make sure there are indexes on the table. We must make sure that the datatypes are correct. Acceptance Criteria: Given I execute the database creation script. When the script completes. Then a table named 'Customer' should exist in the database. Given the 'Customer' table exists. When I describe the table. Then the table should include columns for 'Name', 'Phone number', and other relevant details.\",\"IssueType\":\"Test\",\"Key\":3838962924,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":3206620091,\"Product\":\"RED\",\"Summary\":\"Create test cases for 'Customer' database table creation.\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":1830177063,\"Product\":\"RED\",\"Summary\":\"Execute test cases for 'Customer' database table creation.\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":2965970245,\"Product\":\"RED\",\"Summary\":\"Document test results for 'Customer' database table creation.\"}],\"Summary\":\"Integrated Test: Verify 'Customer' database table creation.\"}],\"Product\":\"RED\",\"Subtasks\":[{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":1218433995,\"Product\":\"RED\",\"Summary\":\"Document database schema\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":3878656485,\"Product\":\"RED\",\"Summary\":\"Document Customer table columns\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":2499004641,\"Product\":\"RED\",\"Summary\":\"Create ERD diagram\"}],\"Summary\":\"As a developer, I want to create a 'Customer' database table, so that customer data can be stored.\"},{\"Description\":\"As a user, I want a seamless user experience across all devices, so that the platform is easy to use whether I'm on a desktop, tablet, or smartphone. This includes responsive design and consistent functionality. Acceptance Criteria: Given I am accessing the platform on a desktop computer. When I resize the browser window. Then the layout should adapt to fit the window size. Given I am accessing the platform on a mobile device. When I navigate the site. Then the site should be fully functional and easy to use.\",\"IssueType\":\"Story\",\"Key\":3804250474,\"LinkedIssues\":[{\"Description\":\"This test verifies that the platform provides a seamless user experience across all devices. It checks the responsiveness of the design and the consistency of functionality.  We must ensure the website scales well. We must ensure the website loads quickly. We must ensure that UI elements do not overlap. Acceptance Criteria: Given I am accessing the platform on a desktop computer. When I resize the browser window. Then the layout should adapt to fit the window size. Given I am accessing the platform on a mobile device. When I navigate the site. Then the site should be fully functional and easy to use.\",\"IssueType\":\"Test\",\"Key\":1750479420,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":304459051,\"Product\":\"RED\",\"Summary\":\"Create test cases for seamless user experience across all devices.\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":3843028677,\"Product\":\"RED\",\"Summary\":\"Execute test cases for seamless user experience across all devices.\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":1629208679,\"Product\":\"RED\",\"Summary\":\"Document test results for seamless user experience across all devices.\"}],\"Summary\":\"Integrated Test: Verify seamless user experience across all devices.\"}],\"Product\":\"RED\",\"Subtasks\":[{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":1326453168,\"Product\":\"RED\",\"Summary\":\"Document usability guidelines\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":187669422,\"Product\":\"RED\",\"Summary\":\"UI/UX style guide\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":570396634,\"Product\":\"RED\",\"Summary\":\"UI/UX wireframe mock-up\"},{\"Description\":null,\"IssueType\":\"Sub-task\",\"Key\":3503555516,\"Product\":\"RED\",\"Summary\":\"Document support for mobile devices\"}],\"Summary\":\"As a user, I want a seamless user experience across all devices, so that the platform is easy to use on any device.\"}],\"ProductDescription\":\"Implement a mobile-first, fully responsive e-commerce platform for our online store, enabling users to browse products, add them to a shopping cart, and complete secure online purchases. The platform should offer a streamlined checkout process, diverse payment options, and robust order tracking capabilities. We need to ensure that the platform is user-friendly, accessible, and highly performant, with a focus on a seamless user experience across all devices.  Please include this task: Create a Database table called \\\\\\\"Customer\\\\\\\".   Add the \\\\\\\"Name\\\\\\\" and \\\\\\\"Phone number\\\\\\\" and other relevant columns to the \\\\\\\"Customer\\\\\\\" table.\",\"UserStoryList\":[\"As a user, I want to browse products, so that I can find items I am interested in purchasing.\",\"As a user, I want to add products to a shopping cart, so that I can purchase multiple items at once.\",\"As a user, I want to complete secure online purchases, so that my payment information is protected.\",\"As a user, I want a streamlined checkout process, so that I can quickly and easily complete my purchase.\",\"As a user, I want diverse payment options, so that I can pay using my preferred method.\",\"As a user, I want robust order tracking capabilities, so that I can monitor the status of my order.\",\"As a user, I want a user-friendly interface, so that I can easily navigate and use the platform.\",\"As a user, I want an accessible platform, so that users with disabilities can use the online store.\",\"As a user, I want a highly performant platform, so that the site is responsive and loads quickly.\",\"As a developer, I want to create a 'Customer' database table, so that customer data can be stored.\",\"As a user, I want a seamless user experience across all devices, so that the platform is easy to use on any device.\"]}";
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
        string? Description { get; set; }
        long Key { get; set; }
        //uint Order { get; set; }
    }

    public class IssueDataBase : IIssue
    {
        public string? Summary { get; set; }
        public string? IssueType { get; set; }
        public string? Description { get; set; }
        public string? Product { get; set; }
        public long Key { get; set; } = new Random().Next() * ( uint.MaxValue / int.MaxValue ) + (uint)new Random().Next(0, 2) * ( uint.MaxValue % int.MaxValue );

        //public uint Order { get; set; }
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

}