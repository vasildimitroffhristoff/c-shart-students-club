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
            "5. Sort students (name or id)",
            "6. Delete student by ID",
            "7. Exit"

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

                    Console.WriteLine("Add student ID: ");
                    string? providedIdInput = Console.ReadLine();
                    int id = studentIds.Count > 0 ? studentIds.Max() + 1 : 0;

                    if (!string.IsNullOrWhiteSpace(providedIdInput))
                    {
                         id = int.Parse(providedIdInput);
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

                    Console.WriteLine("Student added successfully!");

                    break;
                case "2":
                    for (int i = 0; i < studentNames.Count; i++)
                    {
                        var avg = studentGrades[i].Count > 0 ? studentGrades[i].Average() : 0;
                        Console.WriteLine($"Name : {studentNames[i]}, ID: {studentIds[i]}, Grades: {string.Join(", ", studentGrades[i])}, Avg. grade : {avg}");
                    }
                    break;

                case "3":
                    Console.WriteLine("Enter student name or ID: ");

                    var searchInput = Console.ReadLine();

                    int idNumber;

                    bool isNumber = int.TryParse(searchInput, out idNumber);

                    if (isNumber)
                    {
                        bool found = false;

                        for (int i = 0; i < studentIds.Count; i++)
                        {
                            if (studentIds[i] == idNumber)
                            {
                                Console.WriteLine($"Found student : Name: {studentNames[i]}, id: {studentIds[i]}");
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            Console.WriteLine("Student not found.");
                        }

                    }
                    else
                    {
                        bool found = false;

                        for (int i = 0; i < studentNames.Count; i++)
                        {
                            if (string.IsNullOrWhiteSpace(searchInput))
                            {
                                Console.WriteLine("Incorrect input");
                                found = true;
                                break;
                            }
                            if (studentNames[i].ToLower() == searchInput.ToLower())
                            {
                                Console.WriteLine($"Found student : Name: {studentNames[i]}, id: {studentIds[i]}");
                                break;
                            }
                        }
                        
                        if (!found)
                        {
                            Console.WriteLine("Student not found.");
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

                    Console.WriteLine("Top students: ");
                     int top3 = Math.Max(3, topIndexes.Count);

                    for (int i = 0; i < top3; i++)
                    {
                        int idx = topIndexes[i];

                        Console.WriteLine($"{studentNames[idx]} (ID: {studentIds[idx]}) - Avg: {averages[idx]:F2}");

                     }

                    break;    
                case "5":
                    // Handle 
                    Console.WriteLine("Selected 3");
                    break;   
                case "6":
                    Console.WriteLine("Write student ID to remove: ");
                    var deleteId = Console.ReadLine();
                    var deleteIdIndex = !string.IsNullOrWhiteSpace(deleteId) ? studentIds.IndexOf(int.Parse(deleteId)) : -1;

                    if (deleteIdIndex > -1)
                    {
                        studentGrades.RemoveAt(deleteIdIndex);
                        studentNames.RemoveAt(deleteIdIndex);
                        studentIds.RemoveAt(deleteIdIndex);
                    }

                    break;         
                case "7":
                    isRunning = false;
                    Console.WriteLine("Exiting program...");
                    break;

                default:
                    Console.WriteLine("Invalid option. Please choose again.");
                    break;
            }
        }

    }
}