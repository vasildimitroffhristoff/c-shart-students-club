class Program
{
    static void Main(string[] args)
    {
        var studentNames = new List<string>();
        var studentIds = new List<int>();
        var studentGrades = new List<List<int>>();

        bool running = true;

        string[] menu = {
            "1. Add student",
            "2. View all students",
            "3. Search by NAME or ID",
            "4. Exit"
        };

        while (running)
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
                        Console.WriteLine($"Name : {studentNames[i]}, ID: {studentIds[i]}, Grades: {string.Join(", ", studentGrades[i])}, Avg. : {avg}");
                    }
                    break;

                case "3":
                    // Handle 
                    Console.WriteLine("Selected 3");
                    break;

                case "4":
                    running = false;
                    Console.WriteLine("Exiting program...");
                    break;

                default:
                    Console.WriteLine("Invalid option. Please choose again.");
                    break;
            }
        }

    }
}