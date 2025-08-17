using System.Reflection;
using UserStoryGenerator.Model;
using UserStoryGenerator.View;
using static UserStoryGenerator.View.TriStateTreeView;

namespace UserStoryGenerator.Utilities
{
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
        internal static Issue CreateIssueFromTreeNodeEx(TriStateTreeView.TreeNodeEx node)
        {
            Type issueType;
            if( node is TriStateTreeView.TreeNodeExTask )
            {
                issueType = typeof(TaskIssue);
            }
            else if( node is TriStateTreeView.TreeNodeExTest )
            {
                // Add other cases here as needed, mapping TreeNodeExTest to its corresponding Issue type.
                issueType = typeof(Issue); // Placeholder for now
            }
            else
            {
                issueType = typeof(Issue);
            }

            Issue issue = (Issue)Activator.CreateInstance(issueType)!;

            PropertyInfo[] sourceProperties = typeof(TriStateTreeView.TreeNodeEx).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach( PropertyInfo sourceProp in sourceProperties )
            {
                // Find the corresponding property in the target Issue object.
                PropertyInfo? targetProp = issueType.GetProperty(sourceProp.Name, BindingFlags.Public | BindingFlags.Instance);

                // Check if the property exists and is writable.
                if( targetProp != null && targetProp.CanWrite )
                {
                    // Get the value from the source object.
                    object? value = sourceProp.GetValue(node);

                    // Set the value on the target object.
                    targetProp.SetValue(issue, value);
                }
            }

            if( node is TriStateTreeView.TreeNodeExTask taskNode && issue is TaskIssue taskIssue )
            {
                taskIssue.Component = taskNode.Component;
            }

            return issue;
            /*
            Logger.Info("");

            if( node is TriStateTreeView.TreeNodeExTask task1 )
            {
                TaskIssue task = new()
                {
                    Product = node.Product,
                    Summary = node.Summary,
                    IssueType = node.IssueType,
                    Description = node.Description,
                    StoryPoints = node.StoryPoints,
                    OriginalEstimate = node.OriginalEstimate,
                    // should Key be set here too?
                    Key = node.Key,

                    Component = task1.Component,
                };

                return task;

            }

            if( node is TriStateTreeView.TreeNodeExTest )
            {
            }

            Issue issue = new()
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
            */
        }
    }
    public static class SubTaskExtensions
    {
        internal static SubTask CreateSubTaskIssueFromTreeNodeEx(TriStateTreeView.TreeNodeEx node)
        {
            SubTask issue = new()
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
