namespace ModelMob.Core.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }           
		public int JobId { get; set; }
        public int ClientId { get; set; }
        public int ModelId { get; set; }
    }
}
