using Core.Entities;

namespace Entities.Concrete
{
    public class Owner:IEntity
    {
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
    }
}