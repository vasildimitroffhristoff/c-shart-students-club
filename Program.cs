using Spectre.Console;
using Spectre.Console.Cli;

/** 
TODO : 
    - beautify the with Spectre.Console
    - store and load data from file
    - split code into multiple files
*/

class Program
{
    public enum MessageType
    {
        Success,
        Error,
        Warning
    }


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

    static void Main(string[] args)
    {
        var data = DataFileHelper.Load();
        Console.WriteLine(data);
        var studentNames = data.studentNames;
        var studentIds = data.studentIds;
        var studentGrades = data.studentGrades;

        bool isRunning = true;


        string[] menu = {
            "1. Add student",
            "2. View all students",
            "3. Search by NAME or ID",
            "4. Show top 3 students ",
            "5. Delete student by ID",
            "6. Exit"
        };


        while (isRunning)
        {
            Console.WriteLine("--- Student diary program ---");

            foreach (var menuItem in menu)
            {
                Console.WriteLine(menuItem);
            }

            Console.WriteLine("-- Select an option (1-4) to proceed... ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    // Name
                    Console.WriteLine("Add student name: ");
                    string? name = Console.ReadLine();

                    while (string.IsNullOrWhiteSpace(name))
                    {
                        PrintMessage("Name cannot be empty. Please try again.", MessageType.Error);
                        Console.WriteLine("Add student name: ");

                        name = Console.ReadLine();
                    }

                    int id;
                    while (true)
                    {
                        Console.Write("Add student ID: ");
                        string? idInput = Console.ReadLine();

                        if (!int.TryParse(idInput, out id) || id <= 0)
                        {
                            PrintMessage("Invalid ID. Please enter a positive number.", MessageType.Error);
                            continue;
                        }

                        if (studentIds.Contains(id))
                        {
                            PrintMessage("ID already exists. Please try again.", MessageType.Error);
                            continue;
                        }

                        break;
                    }

                    // Grades
                    Console.WriteLine("Enter grades separate by space");
                    string? gradesInput = Console.ReadLine();

                    while (string.IsNullOrEmpty(gradesInput))
                    {
                        PrintMessage("Grades cannot be empty. Please try again.", MessageType.Error);
                        Console.WriteLine("Enter grades separate by space");
                        gradesInput = Console.ReadLine();
                    }

                    string[] gradeStrings = gradesInput?.Split(" ") ?? [];

                    Console.WriteLine($"{gradeStrings}");
                    List<int> grades = [];


                    foreach (string grade in gradeStrings)
                    {
                        grades.Add(int.Parse(grade));
                    }

                    studentNames.Add(name);
                    studentIds.Add(id);
                    studentGrades.Add(grades);

                    DataFileHelper.Save(new StudentData{
                        studentNames = studentNames,
                        studentGrades = studentGrades,
                        studentIds = studentIds
                    });

                    PrintMessage("Student added successfully!", MessageType.Success);

                    break;
                case "2":
                    for (int i = 0; i < studentNames.Count; i++)
                    {
                        var avg = studentGrades[i].Count > 0 ? studentGrades[i].Average() : 0;
                        PrintMessage($"Name : {studentNames[i]}, ID: {studentIds[i]}, Grades: {string.Join(", ", studentGrades[i])}, Avg. grade : {avg}", MessageType.Success);
                    }
                    break;

                case "3":
                    Console.WriteLine("Enter student name or ID: ");

                    var searchInput = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(searchInput))
                    {
                        PrintMessage("Incorrect input", MessageType.Error);
                        continue;
                    }

                    if (int.TryParse(searchInput, out int idNumber))
                    {
                        bool found = false;

                        for (int i = 0; i < studentIds.Count; i++)
                        {
                            var currentId = studentIds[i];
                            if (currentId == idNumber)
                            {
                                PrintMessage($"Found student : Name: {studentNames[i]}, id: {studentIds[i]}", MessageType.Success);
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            PrintMessage("Student not found.", MessageType.Error);
                        }

                    }
                    else
                    {
                        bool found = false;

                        for (int i = 0; i < studentNames.Count; i++)
                        {
                            var currentName = studentNames[i];
                            if (currentName.Equals(searchInput, StringComparison.CurrentCultureIgnoreCase))
                            {
                                PrintMessage($"Found student : Name: {currentName}, id: {studentIds[i]}", MessageType.Success);
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            PrintMessage("Student not found.", MessageType.Error);
                        }
                    }

                    break;
                case "4":
                    List<double> averages = [];

                    for (int i = 0; i < studentGrades.Count; i++)
                    {
                        double avg = studentGrades[i].Count > 0 ? studentGrades[i].Average() : 0;
                        averages.Add(avg);
                    }

                    List<int> topIndexes = [];

                    for (int i = 0; i < studentGrades.Count; i++)
                    {
                        topIndexes.Add(i);
                    }

                    for (int i = 0; i < topIndexes.Count; i++)
                    {
                        for (int j = i + 1; j < topIndexes.Count; j++)
                        {
                            if (averages[topIndexes[i]] > averages[topIndexes[j]])
                            {
                                int temp = topIndexes[i];

                                topIndexes[i] = topIndexes[j];
                                topIndexes[j] = temp;
                            }

                        }
                    }


                    if (topIndexes.Count > 3)
                    {
                        PrintMessage("Top students: ", MessageType.Success);

                        int top3 = Math.Max(3, topIndexes.Count);

                        for (int i = 0; i < top3; i++)
                        {
                            int idx = topIndexes[i];

                            PrintMessage($"✅ {studentNames[idx]} (ID: {studentIds[idx]}) - Avg: {averages[idx]:F2}", MessageType.Success);

                        }
                    }
                    else
                    {
                        PrintMessage("Not enough students to show top 3.", MessageType.Error);
                    }

                    break;

                case "5":
                    Console.Write("Write student ID to remove: ");
                    string? deleteId = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(deleteId))
                    {
                        if (int.TryParse(deleteId, out int parsedId))
                        {
                            int indexToRemove = -1;

                            for (int i = 0; i < studentIds.Count; i++)
                            {
                                if (studentIds[i] == parsedId)
                                {
                                    indexToRemove = i;
                                    break;
                                }
                            }

                            if (indexToRemove != -1)
                            {
                                studentNames.RemoveAt(indexToRemove);
                                studentIds.RemoveAt(indexToRemove);
                                studentGrades.RemoveAt(indexToRemove);

                                PrintMessage("Student removed.", MessageType.Success);
                            }
                            else
                            {
                                PrintMessage("Student ID not found.", MessageType.Error);
                            }
                        }
                        else
                        {
                            PrintMessage("Invalid ID. Please enter a number.", MessageType.Error);
                        }
                    }
                    else
                    {
                        PrintMessage("You didn't enter anything.", MessageType.Error);
                    }

                    break;

                case "6":
                    isRunning = false;
                    Console.WriteLine("Exiting program...");
                    break;

                default:
                    PrintMessage("Invalid option. Please choose again.", MessageType.Error);
                    break;
            }
        }

    }

}