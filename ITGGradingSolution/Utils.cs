namespace ITGGradingSolution;
internal static class Utils
{
    public static void PrintBanner(string content)
    {
        Console.WriteLine("#############################################");
        Console.WriteLine($"############ {content} ############");
        Console.WriteLine("#############################################");
    }

    public static void PrintError(string message)
    {
        Console.Write("!!!!!!!!!!!!!!!!!!!!!!!");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(message);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!");
    }

    public static int SafelyConvertToInt(string x)
    {
        if (int.TryParse(x, out int result))
        {
            return result;
        }
        return int.MinValue;
    }
}
