using StarCo.Domain.Factories;
using StarCo.Domain.State;
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
        private DynamicProducerBaseState state;

        protected DynamicProducerBase(int maxProductionCounter)
        {
            state = new DynamicProducerBaseState();
            MaxProductionCounter = maxProductionCounter;
        }

        public string CurrentProduction
        {
            get { return state.CurrentProduction; }
            set
            {
                state.CurrentProduction = value;
                FirePropertyChanged(() => CurrentProduction);
            }
        }

        public int ProductionCounter
        {
            get { return state.ProductionCounter; }
            set
            {
                state.ProductionCounter = value;
                FirePropertyChanged(() => ProductionCounter);
            }
        }

        public int MaxProductionCounter 
        {
            get { return state.MaxProductionCounter; }
            private set { state.MaxProductionCounter = value; } 
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
                if (CheckRequirements())
                {
                    ConsumePrerequisites();
                    AllocateProduction();
                    ProductionCounter -= productionThreshold;
                }
            }
        }

        protected abstract bool CheckRequirements();
        protected abstract void AllocateProduction();
        protected abstract void ConsumePrerequisites();
    }
}
