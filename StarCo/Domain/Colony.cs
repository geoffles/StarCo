using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarCo.Domain.Factories;
using System.Runtime.Serialization;

namespace StarCo.Domain
{
    [DataContract(IsReference=true)]
    public class Colony
    {
        [DataMember]
        public IList<IImprovement> Improvements { get; private set; }
        [DataMember]
        public IList<IWorker> Workers { get; set; } 
        [DataMember]
        public IDictionary<string, Inventory> Inventory { get; private set; }
        [DataMember]
        public Storage Storage { get; private set; }

        public Colony()
        {
            Storage = new Storage();
            Improvements = new List<IImprovement>();
            Workers = new List<IWorker>();
            Inventory = new Dictionary<string, Inventory>();
        }

        public Inventory GetInventory(string inventoryType)
        {
            if (Inventory.ContainsKey(inventoryType))
            {
                return Inventory[inventoryType];
            }

            var newInventory = ObjectFactory.InventoryFactory().CreateInventory(inventoryType, Storage);

            Inventory[inventoryType] = newInventory;

            return newInventory;
        }

        public void AddImprovement(IImprovement improvement)
        {
            this.Improvements.Add(improvement);
            improvement.Link(this);
        }

        public void AddWorker(IWorker worker)
        {
            this.Workers.Add(worker);
        }

        public void Tick()
        {
            foreach(var worker in Workers)
            {
                worker.Tick();
            }

            foreach(var improvement in Improvements)
            {
                improvement.Tick(this);
            }
        }
    }
}
