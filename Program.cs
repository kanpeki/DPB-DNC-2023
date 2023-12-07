namespace DelenDotNetChallenge2023;

internal class Program
{
    static void Main(string[] args)
    {
        var assignmentLetter = "c"; // Change this to attempt other solutions

        var assignmentFile = $"assignment_{assignmentLetter}.in";
        var outputFile = $"output_{assignmentLetter}.out";

        var assignment = Assignment.Parse(assignmentFile);

        List<SolutionLine> solutionLines = new();

        /* 
         * Your code should be here 
         * Create SolutionLines from the assignment  
         * The `assignment` variable contains all the input data
         */

        if (assignment != null)
        {
            var clientManagerScores = new List<ClientManagerScores>();
            foreach (var manager in assignment.ClientManagers)
            {
                foreach (var client in assignment.Clients)
                {
                    var yearlyScore = GetYearlyClientScore(manager, client);
                    var totalScore = GetTotalClientScore(assignment.YearsToSimulate, client.StartYear, yearlyScore);
                    clientManagerScores.Add(new ClientManagerScores
                    {
                        Client = client,
                        Manager = manager,
                        YearlyScore = yearlyScore,
                        TotalScore = totalScore
                    });
                }
            }

            var managerClients = new Dictionary<ClientManager, List<Client>>();
            foreach (var manager in assignment.ClientManagers)
            {
                managerClients.Add(manager, new List<Client>());
            }

            var matchedClientCount = 0;
            while (matchedClientCount < assignment.Clients.Count() && clientManagerScores.Any())
            {
                clientManagerScores = clientManagerScores.OrderByDescending(s => s.TotalScore).ToList();
                var topScore = clientManagerScores.FirstOrDefault();
                if (topScore != null)
                {
                    var clientManagers = clientManagerScores
                        .Where(s => s.Client.Name.Equals(topScore.Client.Name))
                        .OrderByDescending(s => s.TotalScore)
                        .Select(s => s.Manager);
                    foreach (var manager in clientManagers)
                    {
                        if (managerClients[manager].Count >= manager.ClientsPerYear)
                            continue;
                        managerClients[manager].Add(topScore.Client);
                        matchedClientCount++;
                        break;
                    }
                    clientManagerScores.RemoveAll(s => s.Client.Name == topScore.Client.Name);
                }
            };

            //foreach (var score in clientManagerScores)
            //{
            //    var clientManagers = clientManagerScores
            //        .Where(s => s.Client.Name.Equals(score.Client.Name))
            //        .OrderByDescending(s => s.TotalScore)
            //        .Select(s => s.Manager);

            //    foreach (var manager in clientManagers)
            //    {
            //        if (managerClients[manager].Count >= manager.ClientsPerYear)
            //            continue;
            //        managerClients[manager].Add(score.Client);
            //        break;
            //    }

            //    clientManagerScores.Remove(s => s.Client)
            //}

            //foreach (var manager in assignment.ClientManagers)
            //{
            //    Console.WriteLine($"{manager.Name} : {string.Join(",", managerClients[manager].Select(s => s.Name).ToList())}");
            //}

            for (var i = 0; i < assignment.YearsToSimulate; i++)
            {
                foreach (var manager in assignment.ClientManagers)
                {
                    solutionLines.Add(new SolutionLine
                    {
                        Year = i,
                        ClientManagerName = manager.Name,
                        ClientNames = managerClients[manager].Where(c => c.StartYear <= i).Select(c => c.Name).ToList()
                    });
                }
            }
        }

        Solution.CreateSolutionFile(solutionLines, outputFile);
    }

    static decimal GetTotalClientScore(int yearsToSimulate, int clientStartYear, decimal clientScore)
    {
        return (yearsToSimulate - clientStartYear) * clientScore;
    }

    static decimal GetYearlyClientScore(ClientManager manager, Client client)
    {
        var commonInterestCount = manager.Interests.Intersect(client.Interests).Count();
        return (decimal)(Math.Pow(commonInterestCount / client.Interests.Count(), 2) * client.WealthClass);
    }

    public class ClientManagerScores
    {
        public Client Client { get; set; }
        public ClientManager Manager { get; set; }
        public decimal YearlyScore { get; set; }
        public decimal TotalScore { get; set; }
    }
}
