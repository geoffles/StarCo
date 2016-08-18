using StarCo.Domain.Factories;
using StarCo.Domain.Improvements;
using StarCo.Domain.State;
using StarCo.Friends;
using StarCo.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StarCo.Domain.Workers
{
    [DataContract]
    public class BasicWorker : DynamicProducerBase, IWorker
    {        
        [DataMember]
        private BasicWorkerState state;

        [DataMember]
        public IList<string> Produces { get; private set; }

        public string ResourceKey { get { return "basicworker"; } }

        public BasicWorker(Colony colony)
            : base(10)
        {
            state = new BasicWorkerState();
            Colony = colony;
            Produces = new List<string>
            {
                "basicmine",
                "basicquarry",
                "smallstorage"
            };
        }

        public Colony Colony
        {
            get { return state.Colony; }
            private set { state.Colony = value; }
        }

        public void Tick()
        {
            base.DoProduction();
        }

        public void Link(Colony colony)
        {
            Colony = colony;
            //Colony.AddWorker(this);
            //new WorkerColonyFriendImpl(colony).AddWorker(this);
        }

        protected override bool CheckRequirements()
        {
            return true;
        }

        protected override void AllocateProduction()
        {
            var production = ObjectFactory.ImprovementFactory().BuildImprovement(base.CurrentProduction);
            
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
            var result = new BasicWorkerItemTaskViewModel(this, new List<string>
                    {
                        "smallstorage",
                        "basicmine", 
                        "basicquarry"
                    })
            {
                Label = "Basic Worker",
                Detail = base.CurrentProduction,
                SpriteUri = ObjectFactory.AssetName("BasicWorker"),
                Tokens = new string(Enumerable.Repeat<char>('o', base.ProductionCounter).ToArray())
            };
            return result;
        }

        public void SetProduction(string production)
        {
            CurrentProduction = production;
        }

        public string SubCategoryKey
        {
            get { return "basicworker"; }
        }
    }
}
