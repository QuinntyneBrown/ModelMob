using System.Collections.Generic;

namespace ModelMob.Core.Entities
{
    public class Model: BaseEntity
    {
        public int ModelId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public ICollection<DigitalAsset> DigitalAssets { get; set; }        
    }
}
