using System.ComponentModel.DataAnnotations;

namespace SharplexTimeCode.Core.Models;

public class BookingType
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; } = null!;
}