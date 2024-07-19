namespace SharplexTimeCode.Core.Models;

public class Booking
{
    public Guid Id { get; set; }
    
    public int BookingTypeId { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
    
    public BookingStatus Status { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedBy { get; set; }
}