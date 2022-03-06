//simple class to handle debug.log included colours and arrays
static class Logger
{
    public static void LogLine(string logMessage)
    {
        Console.WriteLine(logMessage);
    }

    public static void LogLine(string logMessage, ConsoleColor consoleColor)
    {
        Console.ForegroundColor = consoleColor;
        Console.WriteLine(logMessage);
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void LogLines(string[] logMessages)
    {
        for (int i = 0; i < logMessages.Length; i++)
        {
            Console.WriteLine(logMessages[i]);
        }
    }

    public static void LogLines(string[] logMessages, ConsoleColor consoleColor)
    {
        Console.ForegroundColor = consoleColor;
        for (int i = 0; i < logMessages.Length; i++)
        {
            Console.WriteLine(logMessages[i]);
        }
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void LogLines(string[] logMessages, ConsoleColor[] consoleColor)
    {
        for (int i = 0; i < logMessages.Length; i++)
        {
            Console.ForegroundColor = consoleColor[i];
            Console.WriteLine(logMessages[i]);
        }
        Console.ForegroundColor = ConsoleColor.White;
    }
}
