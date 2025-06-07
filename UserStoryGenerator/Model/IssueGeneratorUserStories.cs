namespace UserStoryGenerator.Model
{
    public class IssueGeneratorUserStories : IssueGeneratorBase
    {
        public IssueGeneratorUserStories(IssueGeneratorBaseInputArgs args) : base(args)
        {
            targetPrepend = "Product Description: ";
        }
    }

    public class IssueGeneratorBaseArgs(string answer)
    {
        public string Answer { get; } = answer;
    }

}