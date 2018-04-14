namespace Core.Entities
{
    public class Client: Profile
    {
        public int ClientId { get; set; }           
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}
