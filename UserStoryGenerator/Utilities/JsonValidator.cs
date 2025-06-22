//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace UserStoryGenerator.Utilities
{
    public static class JsonValidator
    {
        /// <summary>
        /// Determines whether the specified string is a valid JSON string.
        /// </summary>
        /// <param name="jsonString">The string to validate.</param>
        /// <returns>
        ///   <c>true</c> if the string is a valid JSON string; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidJson(string jsonString)
        {
            // Check for null or empty string to avoid unnecessary processing
            if( string.IsNullOrWhiteSpace(jsonString) )
            {
                return false;
            }

            try
            {
                // Use Utf8JsonReader to parse the JSON string.
                // This is an efficient way to validate JSON without fully deserializing it
                // into an object model if only validation is needed.
                // JsonDocument.Parse also works, but Utf8JsonReader is generally more performant
                // for simple validation scenarios as it avoids building an in-memory DOM.
                var reader = new Utf8JsonReader(System.Text.Encoding.UTF8.GetBytes(jsonString));
                while( reader.Read() )
                {
                    // Continue reading until the end of the JSON string or an error occurs.
                    // The Read() method will throw a JsonException if the JSON is malformed.
                }
                return true; // If no exception is thrown, the JSON is valid.
            }
            catch( JsonException )
            {
                // A JsonException indicates that the string is not valid JSON.
                return false;
            }
            catch( Exception )
            {
                // Catch any other unexpected exceptions during parsing, though JsonException is most common.
                return false;
            }
        }

        /*
        // Alternative implementation using JsonDocument.Parse:
        public static bool IsValidJsonAlternative(string jsonString)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return false;
            }

            try
            {
                // Attempt to parse the JSON string into a JsonDocument.
                // If parsing fails, JsonDocument.Parse will throw a JsonException.
                using (JsonDocument doc = JsonDocument.Parse(jsonString))
                {
                    // If parsing is successful, the JSON is considered valid.
                    // No need to do anything with 'doc' if only validation is required.
                    return true;
                }
            }
            catch (JsonException)
            {
                // Catch the specific exception for invalid JSON.
                return false;
            }
            catch (Exception)
            {
                // Catch any other potential exceptions.
                return false;
            }
        }
        */
    }
    /*
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
*/
}
