namespace UserStoryGenerator.Model
{
    public class IssueGeneratorUserStories : IssueGeneratorBase
    {
        public IssueGeneratorUserStories(string key, string jiraProject, string productName, string target, bool addQATests, bool addSubTasks, Settings.AICoaching? aiCoaching)
            : base(key, jiraProject, productName, target, addQATests, addSubTasks, aiCoaching)
        {
            targetPrepend = "Product Description: ";
            //string query = BuildQuery(target.Trim());
            //gfsGeminiClientHost.Query = query.Replace(Environment.NewLine, " ").Trim();
        }
    }

    public class IssueGeneratorBaseArgs(string answer)
    {
        public string Answer { get; } = answer;
    }


}