namespace ModelMob.Core.Entities
{
    public class Address
    {
        public int AddressId { get; set; }           
		public string StreetAddress { get; set; }
        public string City { get; set; }
        public string ProvinceState { get; set; }
        public string PostalZipCode { get; set; }
        public string Country { get; set; }
    }
}
