using SharplexTimeCode.Core.Models;

namespace SharplexTimeCode.Core.Temp;

public class BookingsTemp : IBookingsTemp
{
    private List<Booking> Bookings { get; } = [];

    public (ResponseResult responseResult, int? bookingTypeId) StartBookingPerButton(DateTime startDate, int? bookingTypeId)
    {
        var lastBooking = Bookings.LastOrDefault();

        if (bookingTypeId is null)
        {
            return (ResponseResult.Failure("No booking type selected"), null);
        }

        if (lastBooking is not null && lastBooking.EndTime is null)
        {
        }

        switch (lastBooking)
        {
            case { EndTime: null, BookingTypeId: 162 }:
            {
                lastBooking.EndTime = startDate;

                if (Bookings.Count < 2)
                {
                    goto checkIncompleteBooking;
                }
                
                var preLastBooking = Bookings[^2];
                
                Bookings.Add(new Booking(startDate, preLastBooking.BookingTypeId));
                return (ResponseResult.Success(), preLastBooking.BookingTypeId);
            }
            case { EndTime: null }:
                checkIncompleteBooking:
                return (ResponseResult.Failure("Booking already in progress, please end it before starting a new one"), null);
        }

        var booking = new Booking(startDate, (int) bookingTypeId);
        
        Bookings.Add(booking);
        
        return (ResponseResult.Success(), null);
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
            case null:
                return ResponseResult.Failure("No booking in progress");
        }
        
        var booking = new Booking(startDate, 162);
        
        Bookings.Add(booking);

        return ResponseResult.Success();
    }
    
    public ResponseResult EndBookingPerButton()
    {
        var lastBooking = Bookings.LastOrDefault();

        switch (lastBooking)
        {
            case null:
            case { EndTime: not null }:
                return ResponseResult.Failure("No booking in progress");
        }
        
        lastBooking.EndTime = DateTime.Now;
        
        return ResponseResult.Success();
    }
}

public interface IBookingsTemp
{
    (ResponseResult responseResult, int? bookingTypeId) StartBookingPerButton(DateTime startDate, int? bookingTypeId);
    ResponseResult PauseBookingPerButton(DateTime startDate);
    ResponseResult EndBookingPerButton();
}