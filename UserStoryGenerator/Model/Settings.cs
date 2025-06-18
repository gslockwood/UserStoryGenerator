namespace UserStoryGenerator.Model
{
    public class Settings
    {
        public string? Key { get; set; }

        public AICoaching? UserStoryCoaching { get; set; }
        public AICoaching? AllIssueCoaching { get; set; }
        public List<string>? Projects { get; set; }
        //public List<string>? JiraIssueTypes { get; set; }
        public Dictionary<string, JiraIssue>? JiraIssueTypes { get; set; }

        public class AICoaching
        {
            public string? IssueInstructions { get; set; }
            public string? QATestInstructions { get; set; }
            public string? SubTaskInstructions { get; set; }
        }

        public class JiraIssue
        {
            public string? IssueType { get; set; }
            public string? ForeColor { get; set; }
            public string? ImagePath { get; set; }
            public int Order { get; set; }

        }

        public Settings()
        {
            JiraIssueTypes = [];
            JiraIssueTypes.Add("Epic", new JiraIssue() { IssueType = "Epic", Order = 0 });
            JiraIssueTypes.Add("Sub-task", new JiraIssue() { IssueType = "Sub-task", Order = 2 });
        }
    }

}