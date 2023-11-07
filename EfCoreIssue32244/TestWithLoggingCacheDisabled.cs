using Xunit;
using Xunit.Abstractions;

namespace EfCoreIssue32244;

[Collection("DoNotRunInParallel")]
public class TestWithLoggingCacheDisabled : TestBase
{
    public TestWithLoggingCacheDisabled(ITestOutputHelper testOutputHelper) : base(testOutputHelper.WriteLine, disableLoggingCache: true) { }

    [Fact]
    public async Task Test()
    {
        await Execute();
    }
}
