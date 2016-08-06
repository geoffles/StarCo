using StarCo.Domain.Improvements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain
{
    public class Storage//: IImprovement
    {
        public IList<StorageContainer> Containers { get; private set; }
        public IList<Habitat> Habitats { get; private set; }
        public long Size { get; private set; }
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
        }

        public void Free(long quantity)
        {
            Available += quantity;
        }

        public bool Consume(long quantity)
        {
            if (Available >= quantity)
            {
                Available = -quantity;
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
