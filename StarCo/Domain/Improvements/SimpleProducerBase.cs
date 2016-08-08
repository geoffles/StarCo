using StarCo.Domain.Factories;
using StarCo.Domain.State;
using StarCo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain.Improvements
{
    [DataContract]
    public abstract class SimpleProducerBase : NotifyPropertyChanged
    {
        [DataMember]
        private SimpleProducerBaseState state;

        protected SimpleProducerBase(string productType, int productionThreshold, int productionAmount)
        {
            state = new SimpleProducerBaseState();
            ProductionThreshold = productionThreshold;
            ProductType = productType;
            ProductionAmount = productionAmount;
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

        public int ProductionAmount 
        { 
            get { return state.ProductionAmount; } 
            private set { state.ProductionAmount = value; } 
        }

        protected int ProductionThreshold
        { 
            get { return state.ProductionThreshold; }
            private set { state.ProductionThreshold = value; }
        }

        public string ProductType 
        {
            get { return state.ProductType; }
            private set { state.ProductType = value; } 
        }

        protected abstract bool CheckProductionSpace();

        protected abstract void AllocateProduction();

        protected void DoProduction()
        {
            ProductionCounter++;

            if (!String.IsNullOrEmpty(ProductType))
            {
                DoProductionInternal();
            }
        }

        private void DoProductionInternal()
        {
            int productionThreshold = ObjectFactory.ProductionLookup().GetProductionTimeFor(ProductType);
            if (ProductionCounter >= productionThreshold)
            {
                if (CheckProductionSpace())
                {
                    AllocateProduction();
                    ProductionCounter -= productionThreshold;
                }
            }
        }
    }
}
