using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain
{
    public class StorageContainer : IImprovement
    {
        public long Size { get; private set; }

        private StorageContainer(int size)
        {
            Size = size;
        }

        public static StorageContainer Small()
        {
            return new StorageContainer(80);
        }

        public static StorageContainer Medium()
        {
            return new StorageContainer(320);
        }

        public static StorageContainer Large()
        {
            return new StorageContainer(1280);
        }

        public void Tick(Colony colony)
        {
            throw new NotImplementedException();
        }
    }
}
