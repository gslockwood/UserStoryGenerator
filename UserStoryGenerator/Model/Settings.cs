namespace UserStoryGenerator.Model
{
    public class Settings
    {
        internal static string? Template = "{\"Key\":\"google cloud gemini ai api key\",\"UserStoryCoaching\":{\"IssueInstructions\":\"Goal:\",\"QATestInstructions\":\"\",\"SubTaskInstructions\":\"\"},\"AllIssueCoaching\":{\"IssueInstructions\":\"Goal:\",\"QATestInstructions\":\"\",\"SubTaskInstructions\":\"\"},\"Projects\":[\"RED\",\"WHIT\",\"BLUE\"],\"JiraIssueTypes\":{\"Epic\":{\"IssueType\":\"Epic\",\"ForeColor\":\"DarkRed\",\"ImagePath\":\"Resources/EPIC.png\",\"Order\":0},\"Story\":{\"IssueType\":\"Story\",\"ForeColor\":\"DarkGreen\",\"ImagePath\":\"Resources/Story.png\",\"Order\":1},\"Task\":{\"IssueType\":\"Task\",\"ForeColor\":\"Navy\",\"ImagePath\":\"Resources/Task.png\",\"Order\":1},\"Test\":{\"IssueType\":\"Test\",\"ForeColor\":\"DarkGoldenrod\",\"ImagePath\":\"Resources/Test.png\",\"Order\":1},\"Bug\":{\"IssueType\":\"Bug\",\"ForeColor\":\"IndianRed\",\"ImagePath\":\"Resources/Bug.png\",\"Order\":1},\"Technical Debt\":{\"IssueType\":\"Technical Debt\",\"ForeColor\":\"IndianRed\",\"ImagePath\":\"Resources\\\\technical debt.jpg\",\"Order\":1},\"Sub-task\":{\"IssueType\":\"Sub-task\",\"ForeColor\":\"DarkSalmon\",\"ImagePath\":\"Resources/Sub-task.png\",\"Order\":2}},\"FundamentalInstructions\":\"Definitions and Rules:\",\"GeminiModel\":\"Gemini20Flash001\"}";

        public string? Key { get; set; }

        public AICoaching? UserStoryCoaching { get; set; }
        public AICoaching? AllIssueCoaching { get; set; }
        public List<string>? Projects { get; set; }
        //public List<string>? JiraIssueTypes { get; set; }
        public Dictionary<string, JiraIssueType>? JiraIssueTypes { get; set; }

        public string? FundamentalInstructions { get; set; }

        //[JsonIgnore]
        public string? GeminiModel { get; set; }

        public class AICoaching
        {
            public string? IssueInstructions { get; set; }
            public string? QATestInstructions { get; set; }
            public string? SubTaskInstructions { get; set; }
        }

        public class JiraIssueType
        {
            public readonly static string Epic = "Epic";
            public readonly static string Story = "Story";
            public readonly static string Sub_task = "Sub-task";
            public string? IssueType { get; set; }
            public string? ForeColor { get; set; }
            public string? ImagePath { get; set; }
            public int Order { get; set; }

        }

        public Settings()
        {
            JiraIssueTypes = [];
            JiraIssueTypes.Add(JiraIssueType.Epic, new JiraIssueType() { IssueType = JiraIssueType.Epic, Order = 0 });
            JiraIssueTypes.Add("Sub-task", new JiraIssueType() { IssueType = JiraIssueType.Sub_task, Order = 2 });
        }
    }

}