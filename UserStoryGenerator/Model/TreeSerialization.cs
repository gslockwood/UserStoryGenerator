using UserStoryGenerator.View;

namespace UserStoryGenerator.Model
{
    public partial class TreeSerialization
    {
        public static List<Issue> ConvertTreeNodeExListToIssueList(List<TreeNode> treeNodes)
        {
            List<Issue> serializableIssues = [];
            foreach( TreeNode node in treeNodes )
                serializableIssues = ConvertTreeNodesToJsonStructure(treeNodes);

            return serializableIssues;
        }
        public static List<Issue> ConvertTreeNodesToJsonStructure(List<TreeNode> treeNodes)
        {
            List<Issue> serializableIssues = [];
            //foreach( TriStateTreeView.TreeNodeEx node in treeNodes.Cast<TriStateTreeView.TreeNodeEx>() )
            foreach( TreeNode treeNode in treeNodes )
            {
                TriStateTreeView.TreeNodeEx node = (TriStateTreeView.TreeNodeEx)treeNode;

                Issue issue = Utilities.IssueDataBaseExtensions.CreateIssueFromTreeNodeEx(node);

                serializableIssues.Add(issue);

                if( node.Nodes.Count > 0 )
                {
                    foreach( TriStateTreeView.TreeNodeEx treeNodeEx in node.Nodes )
                    {
                        if( treeNodeEx.Text.Equals(TriStateTreeView.TreeNodeExSubTasks.NodeName) )// collection node with this text
                        {
                            foreach( TriStateTreeView.TreeNodeEx subTaskNode in treeNodeEx.Nodes )
                            {
                                SubTask subIssue = Utilities.SubTaskExtensions.CreateSubTaskIssueFromTreeNodeEx(subTaskNode);
                                issue.Subtasks ??= [];
                                issue.Subtasks.Add(subIssue);
                            }

                        }
                        else if( treeNodeEx.Text.Equals(TriStateTreeView.TreeNodeExLinkedIssues.NodeName) )// collection node with this text
                            issue.LinkedIssues = ConvertTreeNodesToJsonStructure(treeNodeEx.Nodes.Cast<TreeNode>().ToList());
                    }

                }

            }

            return serializableIssues;
            //
        }

        public static List<Issue> ConvertTreeNodesToJsonStructure2(List<TreeNode> treeNodes)
        {
            List<Issue> serializableIssues = [];
            foreach( TreeNode node in treeNodes )
            {
                View.TriStateTreeView.TreeNodeEx treeNodeEx = (View.TriStateTreeView.TreeNodeEx)node;

                Issue issue = Utilities.IssueDataBaseExtensions.CreateIssueFromTreeNodeEx(treeNodeEx);

                // Convert TreeNodeCollection to List<TreeNode> before recursive call
                if( node.Nodes.Count > 0 )
                    issue.LinkedIssues = ConvertTreeNodesToJsonStructure2(node.Nodes.Cast<TreeNode>().ToList());

                serializableIssues.Add(issue);

            }

            return serializableIssues;
            //
        }

    }

}
