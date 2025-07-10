namespace UserStoryGenerator.Model
{
    public partial class TreeSerialization
    {
        public class IssueResults
        {
            public string? JiraProject { get; set; }
            public string? ProductDescription { get; set; }
            public List<string>? UserStoryList { get; set; }
            public List<IssueData.Issue>? Issues { get; set; }
            public string? ProductOrFeature { get; set; }
            public string? EpicNameOrKey { get; set; }

        }

    }

}
