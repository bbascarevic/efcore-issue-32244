using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EfCoreIssue32244;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    private readonly bool _disableLoggingCache;
    private readonly Action<string> _writerDelegate;

    public DbSet<Foo> Foos { get; set; } = null!;

    public DbContext(bool disableLoggingCache = false, Action<string>? writerDelegate = null)
    {
        _disableLoggingCache = disableLoggingCache;
        _writerDelegate = writerDelegate ?? Console.WriteLine;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql("Host=localhost; Database=ef-issue-32244; Username=postgres; Password=pgpwd; Include Error Detail=true")
            .LogTo(_writerDelegate, LogLevel.Information)
            .EnableSensitiveDataLogging();

        if (_disableLoggingCache)
            optionsBuilder.ConfigureLoggingCacheTime(TimeSpan.Zero);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Foo.Configure(modelBuilder);
    }
}
