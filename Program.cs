using Spectre.Console;
using Spectre.Console.Cli;

/** 
TODO : 
- add validation for existing student id
- when mistake continue from where it was instead of going back to the main menu
- add validation for grades
- store and load data from file
- add validation for empty grades
*/

class Program
{
    static void Main(string[] args)
    {
        var studentNames = new List<string>();
        var studentIds = new List<int>();
        var studentGrades = new List<List<int>>();

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
                    Console.WriteLine("Add student name: ");
                    string? name = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine("❌ Name cannot be empty. Please try again.");
                        continue;
                    }

                    Console.WriteLine("Add student ID: ");

                    if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
                    {
                        Console.WriteLine("❌ Invalid ID. Please enter a positive number.");
                        continue;
                    }

                    Console.WriteLine("Enter grades separate by space");
                    string[] gradeStrings = Console.ReadLine()?.Split(" ") ?? [];
                    List<int> grades = [];


                    foreach (string grade in gradeStrings)
                    {
                        grades.Add(int.Parse(grade));
                    }

                    studentNames.Add(name);
                    studentIds.Add(id);
                    studentGrades.Add(grades);

                    Console.WriteLine("✅ Student added successfully!");

                    break;
                case "2":
                    for (int i = 0; i < studentNames.Count; i++)
                    {
                        var avg = studentGrades[i].Count > 0 ? studentGrades[i].Average() : 0;
                        Console.WriteLine($"✅ Name : {studentNames[i]}, ID: {studentIds[i]}, Grades: {string.Join(", ", studentGrades[i])}, Avg. grade : {avg}");
                    }
                    break;

                case "3":
                    Console.WriteLine("Enter student name or ID: ");

                    var searchInput = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(searchInput))
                    {
                        Console.WriteLine("❌ Incorrect input");
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
                                Console.WriteLine($"✅ Found student : Name: {studentNames[i]}, id: {studentIds[i]}");
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            Console.WriteLine("❌ Student not found.");
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
                                Console.WriteLine($"✅ Found student : Name: {currentName}, id: {studentIds[i]}");
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            Console.WriteLine("❌ Student not found.");
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
                        Console.WriteLine("Top students: ");

                        int top3 = Math.Max(3, topIndexes.Count);

                        for (int i = 0; i < top3; i++)
                        {
                            int idx = topIndexes[i];

                            Console.WriteLine($"✅ {studentNames[idx]} (ID: {studentIds[idx]}) - Avg: {averages[idx]:F2}");

                        }
                    }
                    else
                    {
                        Console.WriteLine("❌ Not enough students to show top 3.");
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

                                Console.WriteLine("✅ Student removed.");
                            }
                            else
                            {
                                Console.WriteLine("❌ Student ID not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("❌ Invalid ID. Please enter a number.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("❌ You didn't enter anything.");
                    }

                    break;

                case "6":
                    isRunning = false;
                    Console.WriteLine("Exiting program...");
                    break;

                default:
                    Console.WriteLine("❌ Invalid option. Please choose again.");
                    break;
            }
        }

    }
}