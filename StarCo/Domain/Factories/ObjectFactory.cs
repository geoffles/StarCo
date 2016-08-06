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

        public static string AssetName(string asset)
        {
            return string.Format("/StarCo;component/Assets/{0}.png", asset);
        }
    }
}
