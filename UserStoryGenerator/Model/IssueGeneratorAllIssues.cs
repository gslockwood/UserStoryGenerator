namespace UserStoryGenerator.Model
{
    public class IssueGeneratorAllIssues : IssueGeneratorBase
    {
        public IssueGeneratorAllIssues(string key, string jiraProject, string productName, long userStoryKey, string target, bool addQATests, bool addSubTasks, Settings.AICoaching? aiCoaching)
            : base(key, jiraProject, productName, target, addQATests, addSubTasks, aiCoaching)
        {
            targetPrepend = "User Story: ";
            //string query = BuildQuery(target.Trim());
            //gfsGeminiClientHost.Query = query.Replace(Environment.NewLine, " ").Trim();
        }
    }

    public class IssueGeneratorBaseArgsEx(string answer, int counter, long userStoryKey) : IssueGeneratorBaseArgs(answer)
    {
        public int Counter { get; } = counter;
        public long UserStoryKey { get; } = userStoryKey;
    }

}