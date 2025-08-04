public static class InputHelper
{
    public static string? PromptForInput(string prompt)
    {
        Console.Write(prompt);

        string? input = Console.ReadLine();

        if (input?.Trim().ToLower() == "exit")
        {
            return null;
        }
        return input;
    }
}