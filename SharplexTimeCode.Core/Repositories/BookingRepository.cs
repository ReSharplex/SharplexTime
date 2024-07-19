using SharplexTimeCode.Core.Data;
using SharplexTimeCode.Core.Models;

namespace SharplexTimeCode.Core.Repositories;

public class BookingRepository : IBookingRepository
{
    public void AddBooking(Booking booking)
    {
        using var context = new AppDbContext();
        context.Add(booking);
        context.SaveChanges();
    }

    public IList<Booking> GetBookings()
    {
        using var context = new AppDbContext();
        return context.Bookings.ToList();
    }
}

public interface IBookingRepository
{
    public void AddBooking(Booking booking);
    
    public IList<Booking> GetBookings();
}