using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain
{
    public class Storage
    {
        public IList<StorageContainer> Containers { get; private set; }
        public long Size { get; private set; }
        public long Available { get; private set; }

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
    }
}
