using System.Collections.Generic;

namespace ModelMob.Core.Entities
{
    public class Model: Profile
    {
        public int ModelId { get; set; }
        public ICollection<DigitalAsset> DigitalAssets { get; set; }        
    }
}
