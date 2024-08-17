using System.ComponentModel.DataAnnotations;

namespace SharplexTimeCode.Core.Models;

public class Booking
{
    public Booking(DateTime startTime, int bookingTypeId)
    {
        StartTime = startTime;
        BookingTypeId = bookingTypeId;
    }

    [Key]
    public Guid Id { get; set; }
    
    public int BookingTypeId { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime? EndTime { get; set; }
    
    public BookingStatus Status { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}