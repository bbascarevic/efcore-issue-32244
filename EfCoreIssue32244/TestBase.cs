using Microsoft.EntityFrameworkCore;

namespace EfCoreIssue32244;

public class TestBase : IDisposable
{
    private DbContext DbContext { get; }
    private Foo.Service Service { get; }

    public TestBase(Action<string> writerDelegate, bool disableLoggingCache)
    {
        DbContext = new(writerDelegate: writerDelegate, disableLoggingCache: disableLoggingCache);
        DbContext.Database.EnsureCreated();
        Service = new(DbContext);
    }

    public async Task Execute()
    {
        await Service.Create(1, "Foo");

        await LogSomeCommand("Before");

        DbContext.ChangeTracker.Clear();
        try
        {
            // Commenting this line makes logging work independently of other things.
            await Service.Create(1, "Whatever");
        }
        catch (Exception)
        {
            // Expected to throw
        }

        // Uncommenting this line makes logging work independently of other things.
        // await Task.Delay(1001);

        await LogSomeCommand("After");
    }

    private async Task LogSomeCommand(string tag)
    {
        await DbContext.Foos.Select(f => new { Id = f.Id, Name = f.Name, Tag = tag }).ToListAsync();
    }

    public void Dispose()
    {
        DbContext.Database.EnsureDeleted();
    }
}
