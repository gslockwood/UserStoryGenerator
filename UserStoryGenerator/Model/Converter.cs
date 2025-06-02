using System.Text;

namespace UserStoryGenerator.Model
{
    internal class Converter
    {
        internal static string ToCSV(string? epicText, TreeSerialization.IssueResults? userStoryResults)
        {
            //return "";

            if( userStoryResults == null ) throw new NullReferenceException(nameof(userStoryResults));
            if( userStoryResults.HierarchyIssueList == null ) throw new NullReferenceException(nameof(userStoryResults.HierarchyIssueList));
            if( userStoryResults.HierarchyIssueList.Count == 0 ) throw new ArgumentNullException(nameof(userStoryResults.HierarchyIssueList));

            //string headerLine = "Project Key,ID,Parent ID,Summary,Issue Type,Description,Status, Reporter,Assignee";
            string headerLine = "Project Key,ID,Parent ID,Link,Summary,Issue Type,Status";

            StringBuilder sbFile = new();
            sbFile.AppendLine(headerLine.TrimEnd(','));


            //////////  Epic  //////////

            string epicKey;
            //if( epicText != null )
            if( !string.IsNullOrEmpty(epicText) )
            {
                // is either a proper reference to a persumably existing Epic in Jira
                // or a text string to be used as a summary for a new epic issue

                bool isValid = Utilities.InputValidator.RegexValidation(epicText);
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
                        IssueType = "EPIC",
                        Product = userStoryResults.HierarchyIssueList[0].Product,
                        Key = epicKey0,
                    };

                    string epicLine = CreateLine(epicIssue, "", "");
                    sbFile.AppendLine(epicLine);

                }
            }
            else
                epicKey = "";


            //////////  Story Level  //////////
            Recursive(userStoryResults.HierarchyIssueList, epicKey, -1, sbFile);

            return sbFile.ToString();
            //
        }

        private static void Recursive(List<IssueData.Issue> hierarchyIssueList, string epicKey, long issueKey, StringBuilder sbFile)
        {
            foreach( IssueData.Issue issue in hierarchyIssueList )
            {
                string issueKeyStr;
                if( issueKey == -1 )
                    issueKeyStr = "";
                else
                    issueKeyStr = issueKey.ToString();

                string line = CreateLine(issue, epicKey, issueKeyStr);
                sbFile.AppendLine(line);
                if( issue.LinkedIssues != null )
                    Recursive(issue.LinkedIssues, epicKey, issue.Key, sbFile);
            }
        }

        private static string CreateLine(IssueData.Issue issue, string parentID, string linedToId)
        {
            StringBuilder sbLine = new();

            if( issue.Product != null )
                sbLine.Append(issue.Product.ToString().Trim() + ",");
            else
                sbLine.Append(',');

            // ID
            long issueKey = issue.Key;
            sbLine.Append(issueKey.ToString() + ",");

            // Parent ID
            if( issue.IssueType != null && issue.IssueType.Equals("Sub-task") )
                sbLine.Append(linedToId + ",");
            else
                sbLine.Append(parentID + ",");

            // Linked To ID
            if( issue.IssueType != null && issue.IssueType.Equals("Sub-task") )
                sbLine.Append(',');
            else
                sbLine.Append(linedToId + ",");

            // Summary
            if( issue.Summary != null )
            {
                string temp = issue.Summary.ToString().Trim();

                //temp = "He said, \"Special item with double quotes\" then left.";   testing only

                if( temp.Contains(',') )
                {
                    temp = temp.Replace("\"", "\"\"");
                    temp = $"\"{temp}\"";
                }

                sbLine.Append(temp + ",");
            }
            else
                sbLine.Append(',');

            // IssueType
            if( issue.IssueType != null )
                sbLine.Append(issue.IssueType.ToString().Trim() + ",");
            else
                sbLine.Append(',');

            // Status
            sbLine.Append("OPEN");

            return sbLine.ToString().TrimEnd(',').Trim();
            //
        }
    }
}