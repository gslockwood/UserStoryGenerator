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
        //public string Answer { get; } = answer;
        public Result Result { get; } = result;
    }

}