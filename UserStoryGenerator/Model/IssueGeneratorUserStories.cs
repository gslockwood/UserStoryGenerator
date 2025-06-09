using static UserStoryGenerator.Model.GFSGeminiClientHost;

namespace UserStoryGenerator.Model
{
    public class IssueGeneratorUserStories : IssueGeneratorBase
    {
        public IssueGeneratorUserStories(IssueGeneratorBaseInputArgs args) : base(args)
        {
            targetPrepend = "Product Description: ";
        }
    }

    public class IssueGeneratorBaseArgs(Result result)
    {
        public Result Result { get; } = result;
        public List<IssueData.Issue>? Issues { get; internal set; }


    }

}