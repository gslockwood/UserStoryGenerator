namespace UserStoryGenerator.Utilities
{
    public class Logger
    {
        internal static void Info(string message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(message);
#else
            System.Console.WriteLine(message);
#endif
        }
        internal static void Info(object message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(message);
#else
            System.Console.WriteLine(message);
#endif
        }
    }

}

