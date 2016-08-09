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
    public class BasicQuarry : SimpleProducerBase, IImprovement
    {
        [DataMember]
        public Colony Colony { get; private set; }

        private BasicQuarry(string productType) : base(
            productType: productType, 
            productionThreshold: 3, 
            productionAmount: 1)
        {}


        public static BasicQuarry Stone()
        {
            return new BasicQuarry("stone");
        }

        public ColonyItemViewModel ToColonyItemViewModel()
        {
            return new ColonyItemViewModel
            {
                Label = "Basic Quarry",
                Detail = string.Join(",", ProductType),
                SpriteUri = ObjectFactory.AssetName("BasicQuarry"),
                Tokens = new string(Enumerable.Repeat<char>('o', ProductionCounter).ToArray())
            };
        }

        public string SubCategoryKey
        {
            get { return this.GetType().Name.ToLower(); }
        }

        public void Link(Colony colony)
        {
            Colony = colony;
        }

        public void Tick(Colony colony)
        {
            base.DoProduction();
        }

        protected override bool CheckProductionSpace()
        {
            return Colony.Storage.Available >= ProductionAmount * ObjectFactory.ProductionLookup().GetProductionSpaceFor(this.ProductType);
        }

        protected override void AllocateProduction()
        {
            Colony.GetInventory(ProductType).Add(ProductionAmount);
        }
    }
}
