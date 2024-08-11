using SharplexTimeCode.Core.Repositories;

namespace SharplexTimeCode.Core.Services;

public class BookingService(IBookingRepository bookingRepository) : IBookingService
{
}

public interface IBookingService
{
}