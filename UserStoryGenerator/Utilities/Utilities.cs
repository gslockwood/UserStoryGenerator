using System.Text.RegularExpressions;

namespace UserStoryGenerator.Utilities
{
    public class InputValidator
    {
        public static bool RegexValidation(string inputText)
        {
            string pattern = @"^[A-Z]{3,4}-\d{3}$";
            return Regex.IsMatch(inputText, pattern);
        }
        public static bool RegexContainsValidation(string inputText)
        {
            string pattern = @"[a-zA-Z]{3,4}-\d";
            return Regex.Match(inputText, pattern).Success;
        }
    }

}
