using Spectre.Console;

public enum MessageType
{
        Success,
        Error,
        Warning
}

public static class Printer
{
        public static void PrintMessage(string message, MessageType type)
        {
                switch (type)
                {
                        case MessageType.Success:
                                AnsiConsole.MarkupLine($"[green]✅ {message}[/]");
                                break;
                        case MessageType.Error:
                                AnsiConsole.MarkupLine($"[red]❌ {message}[/]");
                                break;
                        case MessageType.Warning:
                                AnsiConsole.MarkupLine($"[yellow]⚠️ {message}[/]");
                                break;
                }
        }
}