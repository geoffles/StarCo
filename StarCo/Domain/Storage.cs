using StarCo.Domain.Improvements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain
{
    [DataContract(IsReference=true)]
    public class Storage
    {
        [DataMember]
        public IList<StorageContainer> Containers { get; private set; }
        [DataMember]
        public IList<Habitat> Habitats { get; private set; }
        [DataMember]
        public long TotalStorageSpace { get; private set; }
        [DataMember]
        public long TotalLivingSpace { get; private set; }

        [DataMember]
        public long Available { get; private set; }

        public Storage()
        {
            Containers = new List<StorageContainer>();
            Habitats = new List<Habitat>();
        }

        public void AddContainer(StorageContainer container)
        {
            Containers.Add(container);

            TotalStorageSpace = Containers.Sum(p => p.Size);
            Available += container.Size;
        }

        public void AddHabitat(Habitat habitat)
        {
            Habitats.Add(habitat);

            TotalLivingSpace = Habitats.Sum(p => p.Size);
        }

        public void Free(long quantity)
        {
            Available += quantity;
        }

        public bool Store(long quantity)
        {
            if (Available >= quantity)
            {
                Available -= quantity;
                return true;
            }
            return false;
        }
    }
}
