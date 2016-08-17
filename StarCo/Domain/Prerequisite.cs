using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain
{
    public abstract class Prerequisite
    {
        public Prerequisite(string key, int amount, bool consume)
        {
            Key = key;
            Amount = amount;
            Consumes = consume;
        }

        public string Key { get; private set; }
        public int Amount {get; private set;}
        public bool Consumes { get; private set; }

        public abstract bool Check(Colony colony);
        public abstract void Consume(Colony colony);
    }

    public class InventoryPrerequisite : Prerequisite
    {
        public InventoryPrerequisite(string key, int amount, bool consume = true)
            : base(key, amount, consume)
        {
        }

        public override bool Check(Colony colony)
        {
            var inv = colony.GetInventory(Key);

            return inv != null && inv.Quantity >= Amount;
        }

        public override void Consume(Colony colony)
        {
            if (Consumes)
            {
                var inventory = colony.GetInventory(Key);
                inventory.Consume(Amount);
            }
        }
    }

    public class ImprovementPrerequisite : Prerequisite
    {
        public ImprovementPrerequisite(string key, int amount, bool consume = true)
            : base(key, amount, consume)
        {
        }

        public override bool Check(Colony colony)
        {
            var count = colony.Improvements.Count(p=> p.ResourceKey == Key);

            return count >= Amount;
        }

        public override void Consume(Colony colony)
        {
            if (Consumes)
            {
                var improvement = colony.Improvements.First(p => p.ResourceKey == Key);
                colony.Improvements.Remove(improvement);
            }
        }
    }
}
