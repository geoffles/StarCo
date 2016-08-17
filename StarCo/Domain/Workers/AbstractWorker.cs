using StarCo.Domain.Factories;
using StarCo.Domain.State;
using StarCo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain.Workers
{
    public class AbstractWorker : DynamicProducerBase, IWorker
    {
        [DataMember]
        private AbstractWorkerState state;

        public AbstractWorker(AbstractWorkerState state, int maxproduction): base(maxproduction)
        {
            this.state = state;
        }

        public Colony Colony { get { return state.Colony; } }
        public IList<string> ProductionOptions { get { return state.ProductionOptions; } }

        public void SetProduction(string key)
        {
            CurrentProduction = key;
        }

        public bool ValidateProductionRequirements(string key)
        {
            var prerequisites = ObjectFactory.ProductionLookup().GetPrerequisitesFor(CurrentProduction);

            return prerequisites.All(p => p.Check(Colony));
        }

        protected override bool CheckRequirements()
        {
            var spaceRequirement = ObjectFactory.ProductionLookup().GetProductionSpaceFor(CurrentProduction);

            return spaceRequirement.CheckSpace(Colony) && ValidateProductionRequirements(CurrentProduction);
        }

        protected override void AllocateProduction()
        {
            var spaceRequirement = ObjectFactory.ProductionLookup().GetProductionSpaceFor(CurrentProduction);
            spaceRequirement.ConsumeSpace(Colony);

            var production = ObjectFactory.ImprovementFactory().BuildImprovement(CurrentProduction);
            production.Link(Colony);

            CurrentProduction = null;
        }

        protected override void ConsumePrerequisites()
        {
            var prerequisites = ObjectFactory.ProductionLookup().GetPrerequisitesFor(CurrentProduction);

            prerequisites.All(p =>
            {
                p.Consume(Colony);
                return true;
            });
        }

        public ColonyItemViewModel ToColonyItemViewModel()
        {
            return new AbstractWorkerItemTaskViewModel(this, ProductionOptions);
        }

        public string ResourceKey
        {
            get { return state.ResourceKey; }
        }

        public string SubCategoryKey
        {
            get { return state.SubCategoryKey;  }
        }

        public void Tick()
        {
            base.DoProduction();
        }
    }
}
