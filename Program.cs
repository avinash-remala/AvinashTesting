using AvinashTesting.Utilities;
using System;

namespace AvinashTesting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }

            switch (args[0].ToLower())
            {
                case "generate-report":
                case "report":
                    SimpleHtmlGenerator.GenerateFromTrx();
                    break;
                    
                case "run-tests":
                case "test":
                    RunTests();
                    break;
                    
                case "help":
                case "--help":
                case "-h":
                    ShowHelp();
                    break;
                    
                default:
                    Console.WriteLine($"❌ Unknown command: {args[0]}");
                    ShowHelp();
                    break;
            }
        }

        private static void ShowHelp()
        {
            Console.WriteLine("🧪 Avinash Testing Framework");
            Console.WriteLine("============================");
            Console.WriteLine();
            Console.WriteLine("Available commands:");
            Console.WriteLine("  dotnet run -- report          Generate HTML report from latest TRX");
            Console.WriteLine("  dotnet run -- test            Run all tests and generate report");
            Console.WriteLine("  dotnet run -- help            Show this help message");
            Console.WriteLine();
            Console.WriteLine("Direct commands:");
            Console.WriteLine("  dotnet test                   Run tests only");
            Console.WriteLine("  dotnet build                  Build project");
        }

        private static void RunTests()
        {
            Console.WriteLine("🧪 Running all tests...");
            // This would need Process.Start to run dotnet test
            Console.WriteLine("💡 Use: dotnet test --logger trx --results-directory ./TestResults");
            Console.WriteLine("💡 Then: dotnet run -- report");
        }
    }
}