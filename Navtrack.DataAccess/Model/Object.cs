using System.Collections.Generic;

namespace Navtrack.DataAccess.Model
{
    public class Object
    {
        public Object()
        {
            Locations = new HashSet<Location>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        
        public Device Device { get; set; }
        public int DeviceId { get; set; }
        public ICollection<Location> Locations { get; set; }
    }
}