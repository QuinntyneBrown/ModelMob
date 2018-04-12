namespace ModelMob.Core.Entities
{
    public class BaseEntity
    {
        public int BaseEntityId { get; set; }           
        public virtual Tenant Tenant { get; set; }
    }
}
