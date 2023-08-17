using BookShop.Domain.Shared;

namespace BookShop.Domain.Entities;

public class Discount : BaseEntity
{
    public Guid? UserId { get; set; }
    public virtual User? User { get; set; }
    public Guid? PublisherId { get; set; }
    public virtual Publisher? Publisher { get; set; }
    
    public required Guid BookId { get; set; }
    public virtual Book Book { get; set; } = null!;
    public float Percentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}