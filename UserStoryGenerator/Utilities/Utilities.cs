using System.Reflection;
using System.Text;
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

        public static List<Model.SubTask> GetAllSubTasks(List<Model.Issue> issues)
        {
            List<Model.SubTask> list = [];
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

        internal static int GetImageIndex(string? issueType)
        {
            if( ImageList == null ) throw new NullReferenceException(nameof(ImageList));
            if( issueType == null ) return -1;

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

        public static string FormatEstimateTime(float timeInSeconds)
        {
            // Ensure the time is non-negative
            if( timeInSeconds < 0 )
                timeInSeconds = 0;

            // Calculate total minutes from seconds
            int totalMinutes = (int)Math.Floor(timeInSeconds / 60);

            // Calculate minutes component (remainder after hours)
            int minutes = totalMinutes % 60;

            // Calculate total hours from total minutes
            int totalHours = totalMinutes / 60;

            // Calculate hours component (remainder after days)
            int hours = totalHours % 24;

            // Calculate days component
            int days = totalHours / 24;

            // Use StringBuilder for efficient string construction
            StringBuilder resultBuilder = new();

            // Conditionally append days if greater than 0
            if( days > 0 )
                resultBuilder.Append($"{days}d ");

            // Conditionally append hours if greater than 0
            if( hours > 0 )
                resultBuilder.Append($"{hours}h ");

            // Always append minutes
            resultBuilder.Append($"{minutes}m");

            // Return the final formatted string
            return resultBuilder.ToString();
        }
    }

}
