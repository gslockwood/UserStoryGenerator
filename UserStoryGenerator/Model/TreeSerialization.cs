using UserStoryGenerator.View;

namespace UserStoryGenerator.Model
{
    public partial class TreeSerialization
    {
        public static List<IssueData.Issue> Convert(List<TreeNode> treeNodes)
        {
            List<IssueData.Issue> serializableIssues = [];
            foreach( TreeNode node in treeNodes )
                serializableIssues = ConvertTreeNodesToJsonStructure(treeNodes);

            return serializableIssues;
        }
        public static List<IssueData.Issue> ConvertTreeNodesToJsonStructure(List<TreeNode> treeNodes)
        {
            List<IssueData.Issue> serializableIssues = [];
            foreach( TriStateTreeView.TreeNodeEx node in treeNodes.Cast<TriStateTreeView.TreeNodeEx>() )
            {
                //IssueData.Issue issue = new()
                //{
                //    Summary = node.Summary,
                //    IssueType = node.IssueType,
                //    Product = node.Product
                //};

                IssueData.Issue issue = Utilities.IssueDataBaseExtensions.CreateIssueFromTreeNodeEx(node);

                serializableIssues.Add(issue);

                if( node.Nodes.Count > 0 )
                {
                    foreach( TriStateTreeView.TreeNodeEx treeNodeEx in node.Nodes )
                    {
                        if( treeNodeEx.Text.Equals(TriStateTreeView.TreeNodeExSubTasks.NodeName) )// collection node with this text
                        {
                            foreach( TriStateTreeView.TreeNodeEx subTaskNode in treeNodeEx.Nodes )
                            {
                                IssueData.SubTask subIssue = Utilities.SubTaskExtensions.CreateSubTaskIssueFromTreeNodeEx(subTaskNode);
                                //IssueData.SubTask subIssue = new()
                                //{
                                //    Summary = subTaskNode.Summary,
                                //    IssueType = subTaskNode.IssueType,
                                //    Product = subTaskNode.Product
                                //};
                                issue.Subtasks ??= [];
                                issue.Subtasks.Add(subIssue);
                            }

                        }
                        else if( treeNodeEx.Text.Equals(TriStateTreeView.TreeNodeExLinkedIssues.NodeName) )// collection node with this text
                        {
                            issue.LinkedIssues = ConvertTreeNodesToJsonStructure(treeNodeEx.Nodes.Cast<TreeNode>().ToList());
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

                IssueData.Issue issue = Utilities.IssueDataBaseExtensions.CreateIssueFromTreeNodeEx(treeNodeEx);

                //IssueData.Issue issue = new()
                //{
                //    Summary = treeNodeEx.Summary,
                //    IssueType = treeNodeEx.IssueType,
                //    Product = treeNodeEx.Product
                //};
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

    }

}
