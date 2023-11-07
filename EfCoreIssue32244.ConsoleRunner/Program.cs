using EfCoreIssue32244;

using var test = new TestBase(Console.WriteLine, disableLoggingCache: false);
await test.Execute();
