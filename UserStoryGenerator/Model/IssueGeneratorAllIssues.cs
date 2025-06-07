using static UserStoryGenerator.Model.GFSGeminiClientHost;

namespace UserStoryGenerator.Model
{
    public class IssueGeneratorAllIssues : IssueGeneratorBase
    {
        public IssueGeneratorAllIssues(IssueGeneratorBaseInputArgs args) : base(args)
        {
            targetPrepend = "User Story: ";
        }
    }

    public class IssueGeneratorBaseArgsEx(Result result, int counter, long userStoryKey) : IssueGeneratorBaseArgs(result)
    {
        public int Counter { get; } = counter;
        public long UserStoryKey { get; } = userStoryKey;
    }

}