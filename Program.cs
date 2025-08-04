class Program
{
    static void Main(string[] args)
    {
        var data = DataFileHelper.Load();
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
                    StudentManager.AddStudent(studentNames, studentIds, studentGrades);
                    break;
                case "2":
                    StudentManager.ListStudents(studentNames, studentIds, studentGrades);
                    break;

                case "3":
                    StudentManager.SearchStudent(studentNames, studentIds, studentGrades);
                    break;

                case "4":
                    StudentManager.DisplayTopStudents(studentNames, studentIds, studentGrades);
                    break;

                case "5":
                    StudentManager.RemoveStudent(studentNames, studentIds, studentGrades);
                    break;

                case "6":
                    isRunning = false;
                    Console.WriteLine("Exiting program...");
                    break;

                default:
                    Printer.PrintMessage("Invalid option. Please choose again.", MessageType.Error);
                    break;
            }
        }

    }




}