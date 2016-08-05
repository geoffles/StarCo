using StarCo.Domain.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain
{
    public abstract class DynamicProducerBase
    {
        public string CurrentProduction { get; protected set; }
        public int ProductionCounter { get; private set; }
        public int MaxProductionCounter { get; private set; }

        protected DynamicProducerBase(int maxProductionCounter)
        {
            MaxProductionCounter = maxProductionCounter;
        }

        protected void DoProduction()
        {
            if (ProductionCounter < MaxProductionCounter)
            {
                ProductionCounter++;
            }

            if (!String.IsNullOrEmpty(CurrentProduction))
            {
                DoProductionInternal();
            }
        }

        private void DoProductionInternal()
        {

            int productionThreshold = ObjectFactory.ProductionLookup().GetProductionTimeFor(CurrentProduction);
            if (ProductionCounter >= productionThreshold)
            {
                if (CheckProductionSpace())
                {
                    AllocateProduction();
                    ProductionCounter -= productionThreshold;
                }
            }
        }

        protected abstract bool CheckProductionSpace();

        protected abstract void AllocateProduction();
    }
}
