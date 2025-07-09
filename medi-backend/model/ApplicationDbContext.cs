using Microsoft.EntityFrameworkCore;
using Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Scan> Scans => Set<Scan>();
    public DbSet<Note> Notes => Set<Note>();
}