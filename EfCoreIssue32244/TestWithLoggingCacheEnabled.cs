using Xunit;
using Xunit.Abstractions;

namespace EfCoreIssue32244;

[Collection("DoNotRunInParallel")]
public class TestWithLoggingCacheEnabled : TestBase
{
    public TestWithLoggingCacheEnabled(ITestOutputHelper testOutputHelper) : base(testOutputHelper.WriteLine, disableLoggingCache: false) { }

    [Fact]
    public async Task Test()
    {
        await Execute();
    }
}
