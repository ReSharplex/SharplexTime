using System;
using SharplexTimeCode.Core.Models;

namespace SharplexTimeCode.Models;

public class BookingUI
{
    public Guid Id { get; set; }
    
    public int BookingTypeId { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime? EndTime { get; set; }
    
    public BookingStatus Status { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}