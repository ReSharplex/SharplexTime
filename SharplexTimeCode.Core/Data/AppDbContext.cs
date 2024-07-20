using Microsoft.EntityFrameworkCore;
using SharplexTimeCode.Core.Models;

namespace SharplexTimeCode.Core.Data;

public class AppDbContext : DbContext
{
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BookingType> BookingTypes { get; set; }

    public string DbPath { get; init; }
    
    public AppDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "sharplexTime.db");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
}