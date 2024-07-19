using Microsoft.EntityFrameworkCore;
using SharplexTimeCode.Core.Models;

namespace SharplexTimeCode.Core.Data;

public class AppDbContext : DbContext
{
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BookingType> BookingTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=sharplexTime.db");
        }
    }
}