using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain.Factories
{
    public class InventoryFactory
    {
        public Inventory CreateInventory(string inventoryType, Storage storage)
        {
            switch (inventoryType.ToLower())
            {
                case "gold": return new Inventory("gold", 1, storage);
                case "coal": return new Inventory("coal", 1, storage);
                case "iron": return new Inventory("iron", 1, storage);
                case "limestone": return new Inventory("limestone", 1, storage);
                default: throw new ArgumentException("No inventory '" + inventoryType + "'");
            }
        }
    }
}
