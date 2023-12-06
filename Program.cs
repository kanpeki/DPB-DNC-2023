namespace DelenDotNetChallenge2023;

internal class Program
{
    static void Main(string[] args)
    {
        var assignmentLetter = "a"; // Change this to attempt other solutions

        var assignmentFile = $"assignment_{assignmentLetter}.in";
        var outputFile = $"output_{assignmentLetter}.out";

        var assignment = Assignment.Parse(assignmentFile);

        List<SolutionLine> solutionLines = new();

        /* 
         * Your code should be here 
         * Create SolutionLines from the assignment  
         * The `assignment` variable contains all the input data
         */

        Solution.CreateSolutionFile(solutionLines, outputFile);
    }
}
