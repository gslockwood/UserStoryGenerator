namespace UserStoryGenerator.Model
{
    public class IssueGeneratorAllIssues : IssueGeneratorBase
    {
        public IssueGeneratorAllIssues(IssueGeneratorBaseInputArgs args) : base(args)
        {
            targetPrepend = "User Story: ";
        }
    }

    public class IssueGeneratorBaseArgsEx(string answer, int counter, long userStoryKey) : IssueGeneratorBaseArgs(answer)
    {
        public int Counter { get; } = counter;
        public long UserStoryKey { get; } = userStoryKey;
    }

}