using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UserStoryGenerator.Utilities
{
    public class JsonValidator
    {
        /// <summary>
        /// Verifies if the given string is a valid JSON.
        /// </summary>
        /// <param name="jsonString">The string to validate.</param>
        /// <returns>True if the string is valid JSON; otherwise, false.</returns>
        public static bool IsValidJson(string jsonString)
        {
            // Check if the input string is null or empty, as empty strings are not valid JSON.
            if( string.IsNullOrWhiteSpace(jsonString) )
            {
                return false;
            }

            try
            {
                // Attempt to parse the string as a JSON token.
                // JToken.Parse() will throw an exception if the string is not valid JSON.
                JToken.Parse(jsonString);
                return true; // If parsing succeeds, it's valid JSON.
            }
            catch( JsonReaderException )
            {
                // This exception is typically thrown when the string is malformed JSON.
                // Console.WriteLine($"Invalid JSON format: {ex.Message}"); // For debugging
                return false;
            }
            catch( Exception )
            {
                // Catch any other potential exceptions during parsing.
                // This could include ArgumentNullException if null was somehow passed to Parse,
                // though we already check for null/whitespace.
                // Console.WriteLine($"An unexpected error occurred: {ex.Message}"); // For debugging
                return false;
            }
        }
    }
}
