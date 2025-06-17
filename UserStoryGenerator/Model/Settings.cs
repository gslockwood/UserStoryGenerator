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
    }

    //public class JiraIssueTypes1
    //{
    //    //public const String EPIC = "Epic";
    //    public const String STORY = "Story";
    //    public const String TASK = "Task";
    //    public const String TEST = "Test";
    //    public const String BUG = "Bug";
    //    //public const String SUBTASK = "Sub-task";
    //}

}