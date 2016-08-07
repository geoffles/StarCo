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
    public class Storage//: IImprovement
    {
        [DataMember]
        public IList<StorageContainer> Containers { get; private set; }
        //[DataMember]
        public IList<Habitat> Habitats { get; private set; }
        [DataMember]
        public long Size { get; private set; }
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

            Size = Containers.Sum(p => p.Size);
            Available += container.Size;
        }

        public void Free(long quantity)
        {
            Available += quantity;
        }

        public bool Store(long quantity)
        {
            if (Available >= quantity)
            {
                Available = Available - quantity;
                return true;
            }
            return false;
        }

        public void Tick(Colony colony)
        {
            throw new NotImplementedException();
        }
    }
}
