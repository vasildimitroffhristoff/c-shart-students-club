using System.Text.Json;

public static class DataFileHelper
{
    private const string FilePath = "students.json";

    public static void Save(StudentData data)
    {
        var json = JsonSerializer.Serialize(data);
        File.WriteAllText(FilePath, json);

    }

    public static StudentData Load()
    {
        if (!File.Exists(FilePath))
        {
            return new StudentData();
        }

        var json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<StudentData>(json) ?? new StudentData();
    }
}