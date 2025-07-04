using System.Reflection;
using System.Text.RegularExpressions;
using UserStoryGenerator.Model;
using static UserStoryGenerator.Model.Settings;

namespace UserStoryGenerator.Utilities
{
    public class GeminiUtilities
    {
        public static string? GetGeminiModel(string geminiModelStr)
        {
            Type constantsType = typeof(Mscc.GenerativeAI.Model);

            // Get all public static fields from the class
            FieldInfo[] fields = constantsType.GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach( FieldInfo field in fields )
            {
                // Check if the field is a literal (i.e., a const) and is of type string
                if( field.IsLiteral && !field.IsInitOnly && field.FieldType == typeof(string) )
                {
                    if( field.Name.Equals(geminiModelStr) )
                    {
                        object? temp = field.GetRawConstantValue();
                        if( temp != null )
                            return (string)temp;
                    }
                }
            }

            return null;
        }
    }

    public class InputValidator
    {
        public static bool IsJiraKey(string inputText)
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
        private static ImageList? ImageList;
        private static Dictionary<string, JiraIssueType>? JiraIssueTypes;

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

        internal static int GetImageIndex(string issueType)
        {
            if( ImageList == null ) throw new NullReferenceException(nameof(ImageList));

            return ImageList.Images.IndexOfKey(issueType);
        }

        internal static void SetImageList(ImageList imageList)
        {
            ImageList = imageList;
        }

        internal static void SetJiraIssueTypes(Dictionary<string, JiraIssueType> jiraIssueTypes)
        {
            JiraIssueTypes = jiraIssueTypes;
        }

        internal static int GetSubTaskImageIndex()
        {
            if( ImageList == null ) throw new NullReferenceException(nameof(ImageList));
            if( JiraIssueTypes == null ) throw new NullReferenceException(nameof(JiraIssueTypes));
            //if( model.Settings == null ) throw new NullReferenceException(nameof(model.Settings));
            //if( model.Settings.JiraIssueTypes == null ) throw new NullReferenceException(nameof(model.Settings.JiraIssueTypes));

            IEnumerable<KeyValuePair<string, Settings.JiraIssueType>> any = JiraIssueTypes.Where(type => type.Value.Order == 2);
            if( !any.Any() ) throw new NullReferenceException("subTaskIssueType is missing");
            if( any.Count() > 1 ) throw new NullReferenceException("more than 1 subTaskIssueType");

            Settings.JiraIssueType first = any.First().Value;
            if( first.IssueType == null ) throw new NullReferenceException("first.IssueType");

            return ImageList.Images.IndexOfKey(first.IssueType);

        }

    }

}
