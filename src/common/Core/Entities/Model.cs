using System.Collections.Generic;

namespace Core.Entities
{
    public class Model: Profile
    {
        public int ModelId { get; set; }
        public string Gender { get; set; }
        public ICollection<DigitalAsset> DigitalAssets { get; set; }        
    }
}
