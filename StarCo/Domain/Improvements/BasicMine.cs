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
    public class BasicMine : SimpleProducerBase, IImprovement
    {
        [DataMember]
        public Colony Colony { get; private set; }

        public string ResourceKey { get { return "basicmine"; } }

        private BasicMine(string productType) : base(
            productType: productType, 
            productionThreshold: 3, 
            productionAmount: 1)
        {}

        public static BasicMine Gold()
        {
            return new BasicMine("gold");
        }

        public ColonyItemViewModel ToColonyItemViewModel()
        {
            return new ColonyItemViewModel
            {
                Label = "Basic Mine",
                Detail = string.Join(",", ProductType),
                SpriteUri = ObjectFactory.AssetName("BasicMine"),
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
            colony.AddImprovement(this);
        }

        public void Tick(Colony colony)
        {
            base.DoProduction();
        }

        protected override bool CheckProductionSpace()
        {
            var productionSpace = ObjectFactory.ProductionLookup().GetProductionSpaceFor(ProductType);
            return productionSpace.CheckSpace(Colony);
        }

        protected override void AllocateProduction()
        {
            Colony.GetInventory(ProductType).Add(ProductionAmount);
        }
    }
}
