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

            if( draggableNodeData is DraggableNodeDataTask task )
            {
            }

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

            //if( node is TreeNodeExTask task )
            //{
            ////( (DraggableNodeDataTask)draggableNodeData ).Component = task.Component;
            //fuck:
            //    DraggableNodeDataTask draggableNodeDataTask = draggableNodeData as DraggableNodeDataTask;
            //    if( draggableNodeDataTask != null )
            //        draggableNodeDataTask.Component = task.Component;

            //    //goto fuck;
            //}

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
            treeNodeEx.StoryPoints = ( issue.StoryPoints != null ) ? issue.StoryPoints.Value : 0;
            treeNodeEx.OriginalEstimate = ( issue.OriginalEstimate != null ) ? issue.OriginalEstimate.Value : 0;

            treeNodeEx.Text = issue.Summary;

            treeNodeEx.ToolTipText = treeNodeEx.IssueType;
            treeNodeEx.ImageIndex = Utilities.IssueUtilities.GetImageIndex(treeNodeEx.IssueType);

            treeNodeEx.Name = issue.Key.ToString();

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
            if( node is TriStateTreeView.TreeNodeExTask )
            {
                IssueData.TaskIssue task = new()
                {
                    Product = node.Product,
                    Summary = node.Summary,
                    IssueType = node.IssueType,
                    Description = node.Description,
                    StoryPoints = node.StoryPoints,
                    OriginalEstimate = node.OriginalEstimate,
                    // should Key be set here too?
                    Key = node.Key,

                    Component = ( (TriStateTreeView.TreeNodeExTask)node ).Component,
                };

                return task;

            }

            if( node is TriStateTreeView.TreeNodeExTest )
            {
            }

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
