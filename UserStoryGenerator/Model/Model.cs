using System.Text.Json;
using UserStoryGenerator.Model;
using UserStoryGenerator.Utilities;

namespace UserStoryGenerator.Model
{
    public class Model
    {
        readonly JsonSerializerOptions options = new() { WriteIndented = true };
        public Settings? Settings { get; }


        public Model()
        {
            string json = File.ReadAllText(@$".\Settings.json");
            Settings = JsonSerializer.Deserialize<Settings>(json);
            //File.WriteAllText(@$".\Settings.json", JsonSerializer.Serialize(Settings, options));

            if( false )//false true
            {
                UserStoryGenerator.Tests.RegexValidation.EpicNames();

                //UserStoryGenerator.Tests.IssueResults.BlankEpicName();
                UserStoryGenerator.Tests.IssueResults.DescriptiveEpicName();
                //UserStoryGenerator.Tests.IssueResults.ExistingEpicName();
            }
        }


        //internal void SaveDataToFile0(string answer)
        //{
        //    UserStoryGenerator.Utilities.Logger.Info(answer);
        //    File.WriteAllText(@$".\UserStores.txt", answer);
        //}

        internal void SaveDataToFile(string? epicText, List<string> storyList, List<TreeNode> checkedHierarchy)
        {
            //asdf:
            List<IssueData.Issue> serializableIssues = TreeSerialization.ConvertTreeNodesToJsonStructure(checkedHierarchy);
            //string result00 = JsonSerializer.Serialize(serializableIssues);

            TreeSerialization.IssueResults userStoryResults = new()
            {
                IssueList = storyList,
                HierarchyIssueList = serializableIssues
            };

            string result = JsonSerializer.Serialize(userStoryResults, options);

            UserStoryGenerator.Utilities.Logger.Info(result);
            File.WriteAllText(@$".\UserStores.txt", result);


            // csv
            try
            {
                //userStoryResults = null;  testing

                string csv = Converter.ToCSV(epicText, userStoryResults);
                Logger.Info(csv);
                File.WriteAllText("./Issues.csv", csv);
            }
            catch( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }

            //goto asdf;
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
        public IList<Issue>? Issues { get; set; }

        public class Issue : IssueDataBase
        {
            public List<Issue>? LinkedIssues { get; set; }

        }
    }

    public class Settings
    {
        public string? Key { get; set; }
        public string? AITraining { get; set; }

    }

}

namespace UserStoryGenerator.Tests
{
    public class RegexValidation
    {
        public static void EpicNames()
        {
            Logger.Info($"\"PROJ-123\" is valid: {Utilities.InputValidator.RegexValidation("PROJ-123")}");    // True (4 capitals)
            Logger.Info($"\"ABC-456\" is valid: {Utilities.InputValidator.RegexValidation("ABC-456")}");      // True (3 capitals)
            Logger.Info($"\"XYZ-789\" is valid: {Utilities.InputValidator.RegexValidation("XYZ-789")}");      // True (3 capitals)
            Logger.Info($"\"ABCD-000\" is valid: {Utilities.InputValidator.RegexValidation("ABCD-000")}");    // True (4 capitals)

            Logger.Info($"\"AB-123\" is valid: {Utilities.InputValidator.RegexValidation("AB-123")}");        // False (2 capitals - too few)
            Logger.Info($"\"ABCDE-123\" is valid: {Utilities.InputValidator.RegexValidation("ABCDE-123")}");  // False (5 capitals - too many)
            Logger.Info($"\"Proj-123\" is valid: {Utilities.InputValidator.RegexValidation("Proj-123")}");    // False (lowercase 'P')
            Logger.Info($"\"PROJ-ABC\" is valid: {Utilities.InputValidator.RegexValidation("PROJ-ABC")}");  // False (letters instead of digits)
            Logger.Info($"\"PROJ-1234\" is valid: {Utilities.InputValidator.RegexValidation("PROJ-1234")}");  // False (too many digits)

            Logger.Info($"\"PROJ-12a4\" is valid: {Utilities.InputValidator.RegexValidation("PROJ-1234")}");  // False (too many digits)
        }

    }
    public class IssueResults
    {
        private static readonly string TESTShortList = "{\"IssueList\":[\"As a user, I want to browse products, so that I can find items I'm interested in purchasing.\",\"Implement product catalog API endpoint.\",\"Test for Task: Implement product catalog API endpoint.\",\"Develop product listing UI component with filtering and sorting.\",\"Test for Task: Develop product listing UI component with filtering and sorting.\",\"Create 'Products' database table with columns for 'ProductID', 'Name', 'Description', 'Price', 'ImageURL', and 'Category'.\",\"Test for Task: Create 'Products' database table with columns for 'ProductID', 'Name', 'Description', 'Price', 'ImageURL', and 'Category'.\",\"Implement backend logic to retrieve and display products based on search criteria.\",\"Test for Task: Implement backend logic to retrieve and display products based on search criteria.\"],\"HierarchyIssueList\":[{\"LinkedIssues\":[{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"Test for Task: Implement product catalog API endpoint.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":526694963}],\"Summary\":\"Implement product catalog API endpoint.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":1327218384},{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"Test for Task: Develop product listing UI component with filtering and sorting.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":3352021728}],\"Summary\":\"Develop product listing UI component with filtering and sorting.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":2325641820},{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"Test for Task: Create 'Products' database table with columns for 'ProductID', 'Name', 'Description', 'Price', 'ImageURL', and 'Category'.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":1029456690}],\"Summary\":\"Create 'Products' database table with columns for 'ProductID', 'Name', 'Description', 'Price', 'ImageURL', and 'Category'.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":4238352220},{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"Test for Task: Implement backend logic to retrieve and display products based on search criteria.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":1591733032}],\"Summary\":\"Implement backend logic to retrieve and display products based on search criteria.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":1063598885}],\"Summary\":\"As a user, I want to browse products, so that I can find items I'm interested in purchasing.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"Key\":2901872620}]}";
        private static readonly string TESTShortListWithQATasksAndTDDSubtasks = "{\"IssueList\":[\"As a user, I want to browse products, so that I can find items I want to purchase.\",\"Implement product browsing functionality in the frontend using React components and Redux for state management.\",\"QA Test for Task: Implement product browsing functionality in the frontend using React components and Redux for state management.\",\"Write unit tests for React components.\",\"Write integration tests for Redux state management.\",\"Create a REST API endpoint for fetching products with pagination and filtering.\",\"QA Test for Task: Create a REST API endpoint for fetching products with pagination and filtering.\",\"Write unit tests for API endpoint logic.\",\"Write integration tests for API endpoint with database.\",\"Design and implement a database schema for products including fields like name, description, price, and image URL.\",\"QA Test for Task: Design and implement a database schema for products including fields like name, description, price, and image URL.\",\"Write unit tests for database models.\",\"Write migration tests for database schema.\"],\"HierarchyIssueList\":[{\"LinkedIssues\":[{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Implement product browsing functionality in the frontend using React components and Redux for state management.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":2397963421},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for React components.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":2264463641},{\"LinkedIssues\":null,\"Summary\":\"Write integration tests for Redux state management.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":4046763539}],\"Summary\":\"Implement product browsing functionality in the frontend using React components and Redux for state management.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":2934481181},{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Create a REST API endpoint for fetching products with pagination and filtering.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":1306419728},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for API endpoint logic.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":2088241835},{\"LinkedIssues\":null,\"Summary\":\"Write integration tests for API endpoint with database.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":1763767470}],\"Summary\":\"Create a REST API endpoint for fetching products with pagination and filtering.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":3217282416},{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Design and implement a database schema for products including fields like name, description, price, and image URL.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":643087680},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for database models.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":162931845},{\"LinkedIssues\":null,\"Summary\":\"Write migration tests for database schema.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":1226697586}],\"Summary\":\"Design and implement a database schema for products including fields like name, description, price, and image URL.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":1502057260}],\"Summary\":\"As a user, I want to browse products, so that I can find items I want to purchase.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"Key\":2028911900}]}";

        internal static void BlankEpicName()
        {
            TreeSerialization.IssueResults? userStoryResults = JsonSerializer.Deserialize<TreeSerialization.IssueResults>(TESTShortList);
            Generate("BlankEpicName", null, userStoryResults);
        }

        internal static void DescriptiveEpicName()
        {
            TreeSerialization.IssueResults? userStoryResults = JsonSerializer.Deserialize<TreeSerialization.IssueResults>(TESTShortList);
            Generate("DescriptiveEpicName", "A very descriptive epic summary", userStoryResults);
        }

        internal static void ExistingEpicName()
        {
            TreeSerialization.IssueResults? userStoryResults = JsonSerializer.Deserialize<TreeSerialization.IssueResults>(TESTShortList);
            Generate("ExistingEpicName", "GSL-999", userStoryResults);
        }
        internal static void Generate(string testName, string? epicName, TreeSerialization.IssueResults? userStoryResults)
        {
            string csv = Converter.ToCSV(epicName, userStoryResults);

            Logger.Info(csv);
            File.WriteAllText($"./{testName} Issues.csv", csv);

        }
    }

}