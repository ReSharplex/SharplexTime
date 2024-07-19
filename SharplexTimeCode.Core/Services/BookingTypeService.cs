using SharplexTimeCode.Core.Models;
using SharplexTimeCode.Core.Repositories;

namespace SharplexTimeCode.Core.Services;

public class BookingTypeService(IBookingTypeRepository bookingTypeRepository) : IBookingTypeService
{
    public void AddBookingType(BookingType bookingType)
    {
        bookingTypeRepository.AddBookingType(bookingType);
    }

    public IList<BookingType> GetBookingTypes()
    {
        return bookingTypeRepository.GetBookingTypes();
    }
}

public interface IBookingTypeService
{
    public void AddBookingType(BookingType bookingType);

    public IList<BookingType> GetBookingTypes();
}