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
                case "gold": return new Inventory("Gold", "gold", 1, storage);
                case "coal": return new Inventory("Coal", "coal", 1, storage);
                case "iron": return new Inventory("Iron", "iron", 1, storage);
                case "stone": return new Inventory("Stone", "stone", 1, storage);
                default: throw new ArgumentException("No inventory '" + inventoryType + "'");
            }
        }
    }
}
