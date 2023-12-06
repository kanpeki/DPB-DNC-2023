using System.Text.Json;

namespace DelenDotNetChallenge2023;

public class Assignment
{
    public int YearsToSimulate { get; set; }
    public List<ClientManager> ClientManagers { get; set; } = default!;
    public List<Client> Clients { get; set; } = default!;

    public static Assignment Parse(string fileName)
    {
        var jsonString = File.ReadAllText(fileName);
        var assignment = JsonSerializer.Deserialize<Assignment>(jsonString);

        return assignment ?? throw new Exception("Something went wrong parsing the assignment");
    }
}
