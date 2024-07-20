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
    
    public void CreateSampleBookingTypes()
    {
        List<string> types = new List<string>
        {
            "Programming",
            "Study",
            "Social Media",
            "Streaming Services",
            "Gaming",
            "Administrative Tasks",
            "Leisure Activities",
            "Work",
            "Schoolwork",
            "Break"
        };
        
        foreach (var type in types)
        {
            var bookingType = new BookingType()
            {
                Title = type
            };
            bookingTypeRepository.AddBookingType(bookingType);
        }
    }
}

public interface IBookingTypeService
{
    public void AddBookingType(BookingType bookingType);

    public IList<BookingType> GetBookingTypes();
    
    public void CreateSampleBookingTypes();
}