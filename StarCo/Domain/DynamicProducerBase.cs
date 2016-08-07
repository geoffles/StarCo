using StarCo.Domain.Factories;
using StarCo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain
{
    [DataContract]
    public abstract class DynamicProducerBase : NotifyPropertyChanged
    {
        [DataMember]
        private string currentProduction;
        public string CurrentProduction
        {
            get { return currentProduction; }
            set
            {
                currentProduction = value;
                FirePropertyChanged(() => CurrentProduction);
            }
        }

        [DataMember]
        private int productionCounter;
        public int ProductionCounter
        {
            get { return productionCounter; }
            set
            {
                productionCounter = value;
                FirePropertyChanged(() => ProductionCounter);
            }
        }

        [DataMember]
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
