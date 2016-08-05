using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain
{
    public class Inventory
    {
        public Inventory(string name, int size, Storage storage)
        {
            Name = name;
            Size = size;
            Storage = storage;
            Quantity = 0;
        }

        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public int Size { get; private set; }
        public Storage Storage { get; private set; }

        public bool Add(int quantity)
        {
            if (Storage.Consume(Size * quantity))
            {
                Quantity += quantity;
                return true;
            }

            return false;
        }

        public bool Consume(int quantity)
        {
            if (quantity <= Quantity)
            {
                Quantity = -quantity;
                Storage.Free(Size * quantity);
                return true;
            }
            return false;
        }
    }
}
