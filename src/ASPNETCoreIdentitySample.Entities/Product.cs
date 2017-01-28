using ASPNETCoreIdentitySample.Entities.AuditableEntity;

namespace ASPNETCoreIdentitySample.Entities
{
    public class Product : IAuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}