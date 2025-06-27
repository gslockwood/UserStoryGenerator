namespace UserStoryGenerator.Utilities
{
    public class Logger
    {
        internal static void Info(string message)
        {
            string dateTime = DateTime.Now.ToString("G");
#if DEBUG
            System.Diagnostics.Debug.WriteLine($"{dateTime}:{message}");
#else
            System.Console.WriteLine($"{dateTime}:{message}");
#endif
        }
        internal static void Info(object message)
        {
            if( message == null ) return;

            string dateTime = DateTime.Now.ToString("G");
#if DEBUG
            System.Diagnostics.Debug.WriteLine($"{dateTime}:{message.ToString()}");
#else
            System.Console.WriteLine($"{dateTime}:{message.ToString()}");
#endif
        }
    }

}

