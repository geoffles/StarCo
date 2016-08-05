using StarCo.Domain.Factories;
using StarCo.Domain.Improvements;
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
    }
}
