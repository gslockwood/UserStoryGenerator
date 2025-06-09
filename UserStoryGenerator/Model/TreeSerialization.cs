using UserStoryGenerator.View;

namespace UserStoryGenerator.Model
{
    public class TreeSerialization
    {
        public static List<IssueData.Issue> Convert(List<TreeNode> treeNodes)
        {
            List<IssueData.Issue> serializableIssues = [];
            foreach( TreeNode node in treeNodes )
            {
                //if( node.Text.Equals("LinkedIssues") ) continue;

                serializableIssues = ConvertTreeNodesToJsonStructure(treeNodes);

            }

            return serializableIssues;
        }
        public static List<IssueData.Issue> ConvertTreeNodesToJsonStructure(List<TreeNode> treeNodes)
        {
            List<IssueData.Issue> serializableIssues = [];
            foreach( TriStateTreeView.TreeNodeEx node in treeNodes.Cast<TriStateTreeView.TreeNodeEx>() )
            {
                IssueData.Issue issue = new()
                {
                    Summary = node.Summary,
                    IssueType = node.IssueType,
                    Product = node.Product
                };

                serializableIssues.Add(issue);

                if( node.Nodes.Count > 0 )
                {
                    foreach( TriStateTreeView.TreeNodeEx xxxx in node.Nodes )
                    {
                        if( xxxx.Text.Equals("Subtasks") )
                        {
                            foreach( TriStateTreeView.TreeNodeEx subTaskNode in xxxx.Nodes )
                            {
                                IssueData.SubTask subIssue = new()
                                {
                                    Summary = subTaskNode.Summary,
                                    IssueType = subTaskNode.IssueType,
                                    Product = subTaskNode.Product
                                };
                                issue.Subtasks ??= [];
                                issue.Subtasks.Add(subIssue);
                            }

                        }
                        else if( xxxx.Text.Equals("LinkedIssues") )
                        {
                            issue.LinkedIssues = ConvertTreeNodesToJsonStructure(xxxx.Nodes.Cast<TreeNode>().ToList());
                        }
                    }

                }

            }

            return serializableIssues;
            //
        }

        public static List<IssueData.Issue> ConvertTreeNodesToJsonStructure2(List<TreeNode> treeNodes)
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
                    issue.LinkedIssues = ConvertTreeNodesToJsonStructure2(node.Nodes.Cast<TreeNode>().ToList());
                }

                serializableIssues.Add(issue);

            }

            return serializableIssues;
            //
        }

        public class IssueResults
        {
            public List<string>? UserStoryList { get; set; }
            public List<IssueData.Issue>? Issues { get; set; }
        }
    }

}
