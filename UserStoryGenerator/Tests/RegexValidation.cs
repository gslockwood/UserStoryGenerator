using UserStoryGenerator.Utilities;

namespace UserStoryGenerator.Tests
{
    public class RegexValidation
    {
        public static void EpicNames()
        {
            Logger.Info($"\"PROJ-123\" is valid: {Utilities.InputValidator.RegexValidation("PROJ-123")}");    // True (4 capitals)
            Logger.Info($"\"ABC-456\" is valid: {Utilities.InputValidator.RegexValidation("ABC-456")}");      // True (3 capitals)
            Logger.Info($"\"XYZ-789\" is valid: {Utilities.InputValidator.RegexValidation("XYZ-789")}");      // True (3 capitals)
            Logger.Info($"\"ABCD-000\" is valid: {Utilities.InputValidator.RegexValidation("ABCD-000")}");    // True (4 capitals)

            Logger.Info($"\"AB-123\" is valid: {Utilities.InputValidator.RegexValidation("AB-123")}");        // False (2 capitals - too few)
            Logger.Info($"\"ABCDE-123\" is valid: {Utilities.InputValidator.RegexValidation("ABCDE-123")}");  // False (5 capitals - too many)
            Logger.Info($"\"Proj-123\" is valid: {Utilities.InputValidator.RegexValidation("Proj-123")}");    // False (lowercase 'P')
            Logger.Info($"\"PROJ-ABC\" is valid: {Utilities.InputValidator.RegexValidation("PROJ-ABC")}");  // False (letters instead of digits)
            Logger.Info($"\"PROJ-1234\" is valid: {Utilities.InputValidator.RegexValidation("PROJ-1234")}");  // False (too many digits)

            Logger.Info($"\"PROJ-12a4\" is valid: {Utilities.InputValidator.RegexValidation("PROJ-1234")}");  // False (too many digits)
        }

    }

}