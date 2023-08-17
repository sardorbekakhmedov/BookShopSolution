namespace BookShop.Domain.Shared;

public class BaseEntity<T> where T: struct
{
    public T Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
}

public class BaseEntity : BaseEntity<Guid>
{ }
