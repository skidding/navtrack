using System.Collections.Generic;

namespace Navtrack.DataAccess.Model
{
    public class Asset : IEntity
    {
        public Asset()
        {
            Locations = new HashSet<Location>();
            Users = new HashSet<UserAsset>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public Device Device { get; set; }
        public int DeviceId { get; set; }
        public ICollection<Location> Locations { get; set; }
        public ICollection<UserAsset> Users { get; set; }
    }
}