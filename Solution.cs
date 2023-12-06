using System.Text.Json;

namespace DelenDotNetChallenge2023;

public class Solution
{
    public static void CreateSolutionFile(IEnumerable<SolutionLine> lines, string fileName)
    {
        var jsonString = JsonSerializer.Serialize(lines);
        File.WriteAllText(fileName, jsonString);
    }

    public static List<SolutionLine> Parse(string fileName)
    {
        var jsonString = File.ReadAllText(fileName);
        var solution = JsonSerializer.Deserialize<List<SolutionLine>>(jsonString);

        return solution ?? throw new Exception("Something went wrong parsing the solution file");
    }
}
