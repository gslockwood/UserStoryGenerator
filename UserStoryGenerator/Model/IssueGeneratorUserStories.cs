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

            //Logger.Info(query);

            gfsGeminiClientHost.MaxOutputTokens = 2 * 4096;
            gfsGeminiClientHost.Temperature = 0.7f;
            gfsGeminiClientHost.TopP = 0.9f;
            gfsGeminiClientHost.TopK = 80;

            gfsGeminiClientHost.Query = query.Replace(Environment.NewLine, " ").Trim();

            //gfsGeminiClientHost.EstimateTextTokens();

        }
    }

    public class IssueGeneratorBaseArgs(Result result)
    {
        public Result Result { get; } = result;
        public List<IssueData.Issue>? Issues { get; internal set; }


    }

}