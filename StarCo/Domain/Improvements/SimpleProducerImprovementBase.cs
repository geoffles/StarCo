using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain.Improvements
{
    public class SimpleProducerBase : IImprovement
    {
        protected int productionCounter;
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

        public virtual void Tick(Colony colony)
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
        }
    }
}
