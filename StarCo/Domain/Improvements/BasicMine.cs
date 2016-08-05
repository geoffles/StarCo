using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain.Improvements
{
    public class BasicMine : SimpleProducerBase, IImprovement
    {
        private BasicMine(IList<string> productTypes) : base(
            productTypes: productTypes, 
            productionThreshold: 3, 
            productionAmount: 1,
            maxProductionCounter: 10)
        {}

        public static BasicMine Gold()
        {
            return new BasicMine(new List<string> { "gold" });
        }
    }

}
