using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain.Factories
{
    public abstract class ProductionSpace
    {
        public class Storage : ProductionSpace
        {
            public Storage(int amount)
                : base(amount)
            {
            }

            public override bool CheckSpace(Colony colony)
            {
                return colony.Storage.Available > Amount;
            }

            public override void ConsumeSpace(Colony colony)
            {
                colony.Storage.Store(Amount);
            }

            public override void FreeSpace(Colony colony)
            {
                colony.Storage.Free(Amount);
            }
        }

        public class Living : ProductionSpace
        {
            public Living(int amount)
                : base(amount)
            {
            }

            public override bool CheckSpace(Colony colony)
            {
                return true;
            }

            public override void ConsumeSpace(Colony colony)
            {
                //nothing yet
            }

            public override void FreeSpace(Colony colony)
            {
                colony.Storage.Free(Amount);
            }
        }

        public class Improvement : ProductionSpace
        {
            public Improvement(int amount)
                : base(amount)
            {
            }

            public override bool CheckSpace(Colony colony)
            {
                return true;
            }

            public override void ConsumeSpace(Colony colony)
            {
                //nothing yet
            }

            public override void FreeSpace(Colony colony)
            {
                colony.Storage.Free(Amount);
            }
        }

        protected ProductionSpace(int amount)
        {
            Amount = amount;
        }

        public int Amount { get; private set; }

        public abstract bool CheckSpace(Colony colony);
        public abstract void ConsumeSpace(Colony colony);
        public abstract void FreeSpace(Colony colony);
    }
}
