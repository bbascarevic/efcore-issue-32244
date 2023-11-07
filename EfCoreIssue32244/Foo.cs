using Microsoft.EntityFrameworkCore;

namespace EfCoreIssue32244;

public class Foo
{
    public int Id { get; }
    public string Name { get; private set; }

    private Foo(int id, string name)
    {
        Id = id;
        Name = name;
    }

    internal static void Configure(ModelBuilder modelBuilder)
    {
        var e = modelBuilder.Entity<Foo>();

        e.HasKey(f => f.Id);
        e.HasAlternateKey(f => f.Name);

        e.Property(f => f.Id).IsRequired().ValueGeneratedNever();
        e.Property(f => f.Name).IsRequired();
    }

    public class Service
    {
        private readonly DbContext _dbContext;

        public Service(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Foo> Create(int id, string name, CancellationToken cancellationToken = default)
        {
            if (_dbContext.ChangeTracker.HasChanges())
                throw new InvalidOperationException("DbContext is dirty");

            var foo = new Foo(id, name);
            await _dbContext.AddAsync(foo, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return foo;
        }
    }

    [Obsolete("Required by EF Core. Use the parameterized constructor instead.", true)]
#pragma warning disable CS8618
    private Foo() { }
#pragma warning restore CS8618
}
