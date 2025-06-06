namespace UserStoryGenerator.Model
{
    //public interface ISettings
    //{
    //    string? Key { get; set; }

    //    string? GeneralAITraining { get; set; }
    //    string? QATestsAITraining { get; set; }
    //    string? SubTaskAITraining { get; set; }
    //    List<string>? Projects { get; set; }
    //}
    public class Settings //: ISettings
    {
        public string? Key { get; set; }

        //public string? GeneralAITraining { get; set; }
        //public string? QATestsAITraining { get; set; }
        //public string? SubTaskAITraining { get; set; }
        public AICoaching? UserStoryCoaching { get; set; }
        public AICoaching? AllIssueCoaching { get; set; }
        public List<string>? Projects { get; set; }
        public List<string>? JiraIssueTypes { get; set; }

        public class AICoaching
        {
            public string? IssueInstructions { get; set; }
            public string? QATestInstructions { get; set; }
            public string? SubTaskInstructions { get; set; }
        }
    }

}