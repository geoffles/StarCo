using StarCo.Domain.Factories;
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
        public int ProductionAmount { get; private set; }

        [DataMember]
        protected int ProductionThreshold { get; private set; }
        [DataMember]
        public string ProductType { get; private set; }

        protected SimpleProducerBase(string productType, int productionThreshold, int productionAmount)
        {
            ProductionThreshold = productionThreshold;
            ProductType = productType;
            ProductionAmount = productionAmount;
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

        /*protected int productionCounter;
        protected int productionThreshold;
        protected int productionAmount;
        protected int maxProductionCounter;

        protected SimpleProducerBase(IList<string> productTypes, int productionThreshold, int productionAmount, int maxProductionCounter)
        {
            this.productionThreshold = productionThreshold;
            this.productionAmount = productionAmount;
            productionCounter = 0;
            ProductTypes = productTypes;
        }

        public IList<string> ProductTypes { get; private set; }

        public virtual void DoTick(Colony colony)
        {
            productionCounter++;
            if (productionCounter < productionThreshold)
            {
                return;
            }

            CompleteProduction(colony);
        }

        protected virtual void CompleteProduction(Colony colony)
        {
            var storeResults = ProductTypes.Select(product =>
            {
                var inventory = colony.GetInventory(product);
                var amount = productionAmount;
                return new
                {
                    Product = product,
                    Amount = amount,
                    Completed = inventory.Add(amount)
                };
            })
            .ToList();

            if (storeResults.Any(result => result.Completed == false))
            {
                foreach(var product in storeResults.Where(result => result.Completed))
                {
                    var inventory = colony.GetInventory(product.Product);
                    inventory.Consume(product.Amount);
                }
            }
            else
            {
                productionCounter -= productionThreshold;
            }
        }*/        
    }
}
