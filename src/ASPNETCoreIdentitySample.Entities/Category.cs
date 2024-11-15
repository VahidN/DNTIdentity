using ASPNETCoreIdentitySample.Entities.AuditableEntity;

namespace ASPNETCoreIdentitySample.Entities;

public class Category : IAuditableEntity
{
    public Category() => Products = [];

    public int Id { get; set; }

    public string Name { get; set; }

    public string Title { get; set; }

    public virtual ICollection<Product> Products { get; set; }
}