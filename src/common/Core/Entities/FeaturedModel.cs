using System;

namespace Core.Entities
{
    public class FeaturedModel
    {
        public int FeaturedModelId { get; set; }           
		public int ModelId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
