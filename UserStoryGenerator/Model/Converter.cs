using System.Text;
using UserStoryGenerator.Utilities;

namespace UserStoryGenerator.Model
{
    internal class Converter
    {
        public static long NO_EPIC_HAS_NO_PARENT = -1;
        public static string? SubTaskIssueType { get; private set; }

        internal static string ToCSV(string? epicText, string? epicIssueType, string subTaskIssueType, TreeSerialization.IssueResults? userStoryResults)
        {
            if( userStoryResults == null ) throw new NullReferenceException(nameof(userStoryResults));
            if( userStoryResults.Issues == null ) throw new NullReferenceException(nameof(userStoryResults.Issues));
            //if( userStoryResults.Issues.Count == 0 ) throw new ArgumentNullException(nameof(userStoryResults.Issues));

            SubTaskIssueType = subTaskIssueType;

            //string headerLine = "Project Key,ID,Parent ID,Summary,Issue Type,Description,Status, Reporter,Assignee";
            //string headerLine = "Project Key,ID,Parent ID,Link,Summary,Issue Type,Status";
            //string headerLine = "Project Key,ID,Parent ID,Link,Summary,Issue Type,Description,Status";
            string headerLine = "Project Key,ID,Parent ID,Link,Summary,Issue Type,Description,Status,StoryPoints,OriginalEstimate";

            StringBuilder sbFile = new();
            sbFile.AppendLine(headerLine.TrimEnd(','));


            //////////  Epic  //////////

            string epicKey;
            //if( epicText != null )
            if( !string.IsNullOrEmpty(epicText) )
            {
                // is either a proper reference to a persumably existing Epic in Jira
                // or a text string to be used as a summary for a new epic issue

                bool isValid = Utilities.InputValidator.IsJiraKey(epicText);
                if( isValid )
                    epicKey = epicText;

                else
                {
                    // create a line in the cvs to create a new Epic issue in Jira

                    long epicKey0 = new Random().Next() * ( uint.MaxValue / int.MaxValue ) + (uint)new Random().Next(0, 2) * ( uint.MaxValue % int.MaxValue );
                    epicKey = epicKey0.ToString();

                    IssueData.Issue epicIssue = new()
                    {
                        Summary = epicText,
                        IssueType = epicIssueType,// JiraIssueTypes.EPIC,
                        Product = userStoryResults.Issues[0].Product,
                        // do description for epics
                        Key = epicKey0,
                    };

                    string epicLine = CreateLine(epicIssue, "", "");
                    sbFile.AppendLine(epicLine);

                }
            }
            else
                epicKey = string.Empty;


            //////////  Story Level  //////////
            Recursive(userStoryResults.Issues, epicKey, -1, sbFile);

            return sbFile.ToString();
            //
        }


        private static void Recursive(List<IssueData.Issue> hierarchyIssueList, string epicKey, long issueKey, StringBuilder sbFile)
        {
            foreach( IssueData.Issue issue in hierarchyIssueList )
            {
                string ultimateParendID;
                // 
                if( issueKey == NO_EPIC_HAS_NO_PARENT )
                    ultimateParendID = "";
                else
                    ultimateParendID = issueKey.ToString();

                string line = CreateLine(issue, epicKey, ultimateParendID);
                sbFile.AppendLine(line);

                if( issue.Subtasks != null )
                {
                    foreach( IssueData.SubTask subTask in issue.Subtasks )
                    {
                        string subTaskLine = CreateSubTaskLine(subTask, issue.Key.ToString());
                        Logger.Info(subTaskLine);
                        sbFile.AppendLine(subTaskLine);
                    }
                }

                if( issue.LinkedIssues != null )
                    Recursive(issue.LinkedIssues, epicKey, issue.Key, sbFile);
            }
        }

        private static string CreateSubTaskLine(IssueData.SubTask subTask, string issueKeyStr)
        {
            StringBuilder sbLine = new();

            if( subTask.Product != null )
                sbLine.Append(subTask.Product.ToString().Trim() + ",");
            else
                sbLine.Append(',');

            // ID
            long issueKey = subTask.Key;
            sbLine.Append(issueKey.ToString() + ",");

            // Parent ID
            sbLine.Append(issueKeyStr + ",");

            // Linked To ID
            sbLine.Append(',');

            // Summary
            sbLine.Append(ProcessString(subTask.Summary));


            // IssueType
            sbLine.Append(subTask.IssueType + ",");


            // Description
            sbLine.Append(ProcessString(subTask.Description));


            // Status //OPEN
            sbLine.Append("TODO");
            sbLine.Append(',');

            //StoryPoints
            sbLine.Append(subTask.StoryPoints);
            sbLine.Append(',');

            //OriginalEstimate
            sbLine.Append(1 * subTask.OriginalEstimate);//3600


            return sbLine.ToString().TrimEnd(',').Trim();
        }

        private static string CreateLine(IssueData.Issue issue, string parentID, string linedToId)
        {
            StringBuilder sbLine = new();

            if( issue.Product != null )
                sbLine.Append(issue.Product.Trim() + ",");
            else
                sbLine.Append(',');

            // ID
            long issueKey = issue.Key;
            sbLine.Append(issueKey.ToString() + ",");

            //issue is IssueData.SubTask

            // Parent ID
            if( issue.IssueType != null && issue.IssueType.Equals(SubTaskIssueType) )//JiraIssueTypes.SUBTASK
                sbLine.Append(linedToId + ",");
            else
                sbLine.Append(parentID + ",");

            // Linked To ID
            if( issue.IssueType != null && issue.IssueType.Equals(SubTaskIssueType) )//JiraIssueTypes.SUBTASK
                sbLine.Append(',');
            else
                sbLine.Append(linedToId + ",");


            // Summary
            sbLine.Append(ProcessString(issue.Summary));


            // IssueType
            sbLine.Append(ProcessString(issue.IssueType));


            // Description
            sbLine.Append(ProcessString(issue.Description));

            // Status//OPEN
            sbLine.Append("TODO");
            sbLine.Append(',');


            //StoryPoints
            sbLine.Append(issue.StoryPoints);
            sbLine.Append(',');

            //OriginalEstimate
            sbLine.Append(1 * issue.OriginalEstimate);//3600


            return sbLine.ToString().TrimEnd(',').Trim();
            //
        }
        private static string? ProcessString(string? temp)//value
        {
            if( temp == null ) return ",";

            temp = temp.Trim();
            if( temp.Contains(',') || temp.Contains('"') || temp.Contains('\r') || temp.Contains('\n') )
            {
                temp = temp.Replace("\"", "\"\"");
                temp = $"\"{temp}\"";
            }

            return temp + ",";

        }

    }
}