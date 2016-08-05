using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain.Factories
{
    public class ObjectFactory
    {
        public static InventoryFactory InventoryFactory()
        {
            return new InventoryFactory();
        }

        public static ProductionLookup ProductionLookup()
        {
            return new ProductionLookup();
        }

        public static ImprovementFactory ImprovementFactory()
        {
            return new ImprovementFactory();
        }
    }
}
