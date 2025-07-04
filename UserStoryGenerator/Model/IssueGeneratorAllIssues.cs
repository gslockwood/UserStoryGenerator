using static UserStoryGenerator.Model.GFSGeminiClientHost;

namespace UserStoryGenerator.Model
{
    public class IssueGeneratorAllIssues : IssueGeneratorBase
    {
        public IssueGeneratorAllIssues(IssueGeneratorBaseInputArgs args) : base(args)
        {
            targetPrepend = "User Story: ";

            if( args.Target == null ) throw new NullReferenceException(nameof(args.Target));
            string query = BuildQuery(args.Target.Trim());

            //Utilities.Logger.Info(query);

            gfsGeminiClientHost.MaxOutputTokens = 500000;

            gfsGeminiClientHost.Query = query.Replace(Environment.NewLine, " ").Trim();

        }
    }

    public class IssueGeneratorBaseArgsEx(Result result, int counter, long userStoryKey) : IssueGeneratorBaseArgs(result)
    {
        public int Counter { get; } = counter;
        public long UserStoryKey { get; } = userStoryKey;
    }

}