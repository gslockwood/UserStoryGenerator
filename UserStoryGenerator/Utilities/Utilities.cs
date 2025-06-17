using System.Text.RegularExpressions;

namespace UserStoryGenerator.Utilities
{
    public class InputValidator
    {
        public static bool RegexValidation(string inputText)
        {
            string pattern = @"^[A-Z]{3,4}-\d+$";
            return Regex.IsMatch(inputText, pattern);
        }
        public static bool RegexContainsValidation(string inputText)
        {
            string pattern = @"[a-zA-Z]{3,4}-\d";
            return Regex.Match(inputText, pattern).Success;
        }
    }

    public class IssueUtilities
    {
        public static List<Model.IssueData.SubTask> GetAllSubTasks(List<Model.IssueData.Issue> issues)
        {
            List<Model.IssueData.SubTask> list = [];
            if( issues != null )
            {
                foreach( var issue in issues )
                {
                    if( issue.Subtasks != null )
                        list.AddRange(issue.Subtasks);

                    if( issue.LinkedIssues != null )
                        list.AddRange(GetAllSubTasks(issue.LinkedIssues));

                }
            }
            return list;
        }
    }

}
