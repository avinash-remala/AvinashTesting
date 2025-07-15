using AvinashTesting.Utilities;

if (args.Length > 0 && args[0] == "generate-report")
{
    SimpleHtmlGenerator.GenerateFromTrx();
}
else
{
    Console.WriteLine("Usage: dotnet run -- generate-report");
}