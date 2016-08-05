using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain.Improvements
{
    public class BasicMine : IImprovement
    {
        private int productionCounter;

        private BasicMine(IList<string> productTypes)
        {
            productionCounter = 0;
            ProductTypes = productTypes;
        }

        public static BasicMine Gold()
        {
            return new BasicMine(new List<string> { "gold" });
        }

        public IList<string> ProductTypes { get; private set; }

        public void Tick(Colony colony)
        {
            productionCounter++;
            if (productionCounter < 3)
            {
                return;
            }

            productionCounter = 0;

            foreach (var product in ProductTypes)
            {
                var inventory = colony.GetInventory(product);

                inventory.Add(1);
            }

        }
    }

}
