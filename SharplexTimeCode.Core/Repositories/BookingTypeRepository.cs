using SharplexTimeCode.Core.Data;
using SharplexTimeCode.Core.Models;

namespace SharplexTimeCode.Core.Repositories;

public class BookingTypeRepository : IBookingTypeRepository
{
    public void AddBookingType(BookingType bookingType)
    {
        using var context = new AppDbContext();
        context.BookingTypes.Add(bookingType);
        context.SaveChanges();
    }

    public IList<BookingType> GetBookingTypes()
    {
        using var context = new AppDbContext();
        return context.BookingTypes.ToList();
    }
}

public interface IBookingTypeRepository
{
    public void AddBookingType(BookingType bookingType);
    
    public IList<BookingType> GetBookingTypes();
}