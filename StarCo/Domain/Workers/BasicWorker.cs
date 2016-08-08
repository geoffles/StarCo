using StarCo.Domain.Factories;
using StarCo.Domain.Improvements;
using StarCo.Domain.State;
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

        public BasicWorker(Colony colony)
            : base(10)
        {
            state = new BasicWorkerState();
            Colony = colony;
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
            else if (production is Habitat)
            {
                throw new NotImplementedException();
            }
            else 
            {
                this.Colony.AddImprovement(production);
            }
            CurrentProduction = null;
        }

        public ColonyItemViewModel ToColonyItemViewModel()
        {
            var result = new BasicWorkerItemTaskViewModel(this)
            {
                Label = "Basic Worker",
                Detail = base.CurrentProduction,
                SpriteUri = ObjectFactory.AssetName("BasicWorker"),
                Tokens = new string(Enumerable.Repeat<char>('o', base.ProductionCounter).ToArray()),
                //ChangeProduction = new CommandHandler<BasicWorkerItemTaskViewModel>(vm => SetProduction(vm.SelectedProductionOption.Key))
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
