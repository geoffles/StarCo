﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarCo.Domain.Factories;

namespace StarCo.Domain
{
    public class Colony
    {
        public IList<IImprovement> Improvements { get; private set; }
        public IDictionary<string, Inventory> Inventory { get; private set; }
        public Storage Storage { get; private set; }

        public Inventory GetInventory(string inventoryType)
        {
            if (Inventory.ContainsKey(inventoryType))
            {
                return Inventory[inventoryType];
            }

            var newInventory = Factory.InventoryFactory().CreateInventory(inventoryType, Storage);

            Inventory[inventoryType] = newInventory;

            return newInventory;
        }
    }


}
