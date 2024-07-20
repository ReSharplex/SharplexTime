using SharplexTimeCode.Core.Data;
using SharplexTimeCode.Core.Models;

namespace SharplexTimeCode.Core.Repositories;

public class BookingTypeRepository : IBookingTypeRepository
{
    public void AddBookingType(BookingType bookingType)
    {
        using var context = new AppDbContext();

        var type = context.BookingTypes.FirstOrDefault(a => bookingType.Title.Equals(a.Title));

        if (type == null)
        {
            context.BookingTypes.Add(bookingType);
            context.SaveChanges();   
        }
    }

    public void AddBookingType(IList<BookingType> bookingTypes)
    {
        using var context = new AppDbContext();
        context.BookingTypes.AddRange(bookingTypes);
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

    public void AddBookingType(IList<BookingType> bookingTypes);
    
    public IList<BookingType> GetBookingTypes();
}