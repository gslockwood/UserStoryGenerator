using UserStoryGenerator.Model;
using UserStoryGenerator.View;
using static UserStoryGenerator.View.TriStateTreeView;

namespace UserStoryGenerator.Utilities
{
    public static class DraggableNodeDataExtensions
    {
        internal static void ZeroSet(this DraggableNodeData draggableNodeData)
        {
            draggableNodeData.Product = string.Empty;
            draggableNodeData.Summary = string.Empty;
            draggableNodeData.IssueType = string.Empty;
            draggableNodeData.Description = string.Empty;
            draggableNodeData.Key = -1;
            draggableNodeData.StoryPoints = 0;
            draggableNodeData.OriginalEstimate = 0.0f;

        }

        internal static void SetFromTreeNodeEx(this DraggableNodeData draggableNodeData, TriStateTreeView.TreeNodeEx node)
        {
            draggableNodeData.Product = node.Product;
            draggableNodeData.Summary = node.Summary;
            draggableNodeData.IssueType = node.IssueType;
            draggableNodeData.Description = node.Description;
            draggableNodeData.Key = node.Key;
            draggableNodeData.StoryPoints = node.StoryPoints;
            draggableNodeData.OriginalEstimate = node.OriginalEstimate;
        }
    }
    public static class TreeNodeExExtensions
    {
        public static void SetTreeNodeEx(this TriStateTreeView.TreeNodeEx treeNodeEx, IssueDataBase issue)
        {
            treeNodeEx.Product = issue.Product?.Trim();
            treeNodeEx.Summary = issue.Summary?.Trim();
            treeNodeEx.IssueType = issue.IssueType?.Trim();
            treeNodeEx.Description = issue.Description?.Trim();
            treeNodeEx.Key = issue.Key;
            treeNodeEx.StoryPoints = issue.StoryPoints;
            treeNodeEx.OriginalEstimate = issue.OriginalEstimate;

            treeNodeEx.Text = issue.Summary;

        }

        internal static void SetTreeNodeEx2(TreeNodeEx treeNodeEx, DraggableNodeData draggableNodeData)
        {
            treeNodeEx.Product = draggableNodeData.Product?.Trim();
            treeNodeEx.Summary = draggableNodeData.Summary?.Trim();
            treeNodeEx.IssueType = draggableNodeData.IssueType?.Trim();
            treeNodeEx.Description = draggableNodeData.Description?.Trim();
            treeNodeEx.Key = draggableNodeData.Key;
            treeNodeEx.StoryPoints = draggableNodeData.StoryPoints;
            treeNodeEx.OriginalEstimate = draggableNodeData.OriginalEstimate;

            treeNodeEx.Text = draggableNodeData.Text;

            if( !string.IsNullOrEmpty(draggableNodeData.Description) )
            {
                TreeNode treeNode = new(draggableNodeData.Description)
                {
                    ImageIndex = Utilities.IssueUtilities.GetImageIndex("Circle")
                };

                treeNodeEx.Nodes.Add(treeNode);
            }

        }
        internal static void SetTreeNodeExFromTreeNodeEx(TreeNodeEx source, TreeNodeEx destination)
        {
            destination.Product = source.Product?.Trim();
            destination.Summary = source.Summary?.Trim();
            destination.IssueType = source.IssueType?.Trim();
            destination.Description = source.Description?.Trim();
            destination.Key = source.Key;
            destination.StoryPoints = source.StoryPoints;
            destination.OriginalEstimate = source.OriginalEstimate;

            destination.Text = source.Summary;
        }
    }

    public static class IssueDataBaseExtensions
    {
        internal static IssueData.Issue CreateIssueFromTreeNodeEx(TriStateTreeView.TreeNodeEx node)
        {
            IssueData.Issue issue = new()
            {
                Product = node.Product,
                Summary = node.Summary,
                IssueType = node.IssueType,
                Description = node.Description,
                StoryPoints = node.StoryPoints,
                OriginalEstimate = node.OriginalEstimate,
                // should Key be set here too?
                Key = node.Key,
            };

            return issue;
        }
    }
    public static class SubTaskExtensions
    {
        internal static IssueData.SubTask CreateSubTaskIssueFromTreeNodeEx(TriStateTreeView.TreeNodeEx node)
        {
            IssueData.SubTask issue = new()
            {
                Product = node.Product,
                Summary = node.Summary,
                IssueType = node.IssueType,
                Description = node.Description,
                StoryPoints = node.StoryPoints,
                OriginalEstimate = node.OriginalEstimate,
                // should Key be set here too?
                Key = node.Key,
            };

            return issue;
        }
    }
}
