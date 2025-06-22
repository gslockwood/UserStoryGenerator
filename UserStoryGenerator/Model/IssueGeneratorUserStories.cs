using static UserStoryGenerator.Model.GFSGeminiClientHost;

namespace UserStoryGenerator.Model
{
    public class IssueGeneratorUserStories : IssueGeneratorBase
    {
        public IssueGeneratorUserStories(IssueGeneratorBaseInputArgs args) : base(args)
        {
            targetPrepend = "Product Description: ";

            if( args.Target == null ) throw new NullReferenceException(nameof(args.Target));
            string query = BuildQuery(args.Target.Trim());
            gfsGeminiClientHost.Query = query.Replace(Environment.NewLine, " ").Trim();

        }
    }

    public class IssueGeneratorBaseArgs(Result result)
    {
        public Result Result { get; } = result;
        public List<IssueData.Issue>? Issues { get; internal set; }


    }

}