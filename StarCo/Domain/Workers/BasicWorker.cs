using StarCo.Domain.Factories;
using StarCo.Domain.Improvements;
using StarCo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain
{
    public class BasicWorker : DynamicProducerBase, IWorker
    {
        public Colony Colony { get; private set; }
        public BasicWorker(Colony colony) : base(8)
        {
            Colony = colony;
        }

        public void Tick()
        {
            base.DoProduction();
        }

        protected override bool CheckProductionSpace()
        {
            return true;
        }

        protected override void AllocateProduction()
        {
            var production = ObjectFactory.ImprovementFactory().BuildImprovement(base.CurrentProduction);
            if (production is StorageContainer)
            {
                Colony.Storage.AddContainer((StorageContainer)production);
            }
            if (production is Habitat)
            {
                throw new NotImplementedException();
            }

            this.Colony.AddImprovement(production);
        }

        public ColonyItemViewModel ToColonyItemViewModel()
        {
            return new ColonyItemViewModel
            {
                Label = "Basic Worker",
                Detail = base.CurrentProduction,
                SpriteUri = ObjectFactory.AssetName("BasicWorker"),
                Tokens = new string(Enumerable.Repeat<char>('o', base.ProductionCounter).ToArray())
            };
        }

        public string SubCategoryKey
        {
            get { return "basicworker"; }
        }
    }
}
