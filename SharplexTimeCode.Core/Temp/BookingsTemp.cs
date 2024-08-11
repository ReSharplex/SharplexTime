using SharplexTimeCode.Core.Models;

namespace SharplexTimeCode.Core.Temp;

public class BookingsTemp : IBookingsTemp
{
    private List<Booking> Bookings { get; } = [];

    public ResponseResult StartBookingPerButton(DateTime startDate, int bookingTypeId)
    {
        var lastBooking = Bookings.LastOrDefault();

        if (lastBooking is not null && lastBooking.EndTime is null)
        {
            return ResponseResult.Failure("Booking already in progress, please end it before starting a new one");
        }

        var booking = new Booking(startDate, bookingTypeId);
        
        Bookings.Add(booking);
        
        return ResponseResult.Success();
    }

    public ResponseResult PauseBookingPerButton(DateTime startDate)
    {
        var lastBooking = Bookings.LastOrDefault();

        switch (lastBooking)
        {
            case { BookingTypeId: 162, EndTime: null }:
                return ResponseResult.Failure("Booking already in progress, please end it before starting a new one");
            case { EndTime: null }:
                lastBooking.EndTime = startDate;
                break;
        }
        
        var booking = new Booking(startDate, 162);
        
        Bookings.Add(booking);

        return ResponseResult.Success();
    }
    
    public ResponseResult EndBookingPerButton()
    {
        var lastBooking = Bookings.LastOrDefault();
        if (lastBooking is null || lastBooking.EndTime is not null)
        {
            return ResponseResult.Failure("No booking in progress");
        }
        
        lastBooking.EndTime = DateTime.Now;
        
        return ResponseResult.Success();
    }
}

public interface IBookingsTemp
{
    ResponseResult StartBookingPerButton(DateTime startDate, int bookingTypeId);
    ResponseResult PauseBookingPerButton(DateTime startDate);
    ResponseResult EndBookingPerButton();
}