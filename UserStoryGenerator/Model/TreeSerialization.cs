namespace UserStoryGenerator.Model
{
    public class TreeSerialization
    {
        public static List<IssueData.Issue> ConvertTreeNodesToJsonStructure(List<TreeNode> treeNodes)
        {
            List<IssueData.Issue> serializableIssues = [];
            foreach( TreeNode node in treeNodes )
            {
                View.TriStateTreeView.TreeNodeEx treeNodeEx = (View.TriStateTreeView.TreeNodeEx)node;
                IssueData.Issue issue = new()
                {
                    Summary = treeNodeEx.Summary,
                    IssueType = treeNodeEx.IssueType,
                    Product = treeNodeEx.Product
                };
                if( node.Nodes.Count > 0 )
                {
                    // Convert TreeNodeCollection to List<TreeNode> before recursive call
                    issue.LinkedIssues = ConvertTreeNodesToJsonStructure(node.Nodes.Cast<TreeNode>().ToList());
                }

                serializableIssues.Add(issue);

            }

            return serializableIssues;
            //
        }

        public class IssueResults
        {
            public List<string>? IssueList { get; set; }
            public List<IssueData.Issue>? HierarchyIssueList { get; set; }
        }
    }

}
