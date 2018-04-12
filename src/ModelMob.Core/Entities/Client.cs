namespace ModelMob.Core.Entities
{
    public class Client: BaseEntity
    {
        public int ClientId { get; set; }           
		public string Code { get; set; }
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}
