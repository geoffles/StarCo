using StarCo.Domain.Factories;
using StarCo.Domain.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StarCo.ViewModels
{
    public class BasicWorkerItemTaskViewModel : ColonyItemViewModel
    {
        public class ProductionOptionItem : NotifyPropertyChanged
        {
            public string Key { get; set; }
            public string Label { get; set; }
            public ICommand Click { get; set; }
            public BasicWorker Target {get; private set;}
            private bool meetsRequirements;
            public bool MeetsRequirements
            {
                get { return meetsRequirements; }
                set
                {
                    meetsRequirements = value;
                    FirePropertyChanged(() => MeetsRequirements);
                }
            }

            public ProductionOptionItem(string key, string label, BasicWorker target)
            {
                Key = key; 
                Label = label;
                Target = target;
                MeetsRequirements = true;
                Click = new CommandHandler<ProductionOptionItem>(vm => vm.Apply());
            }

            public void Apply()
            {
                Target.SetProduction(Key);
            }
        }

        public BasicWorkerItemTaskViewModel(BasicWorker recieveUpdatesFrom, IList<string> productionOptions)
        {
            recieveUpdatesFrom.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == PropertyName(() => recieveUpdatesFrom.CurrentProduction))
                {
                    Detail = recieveUpdatesFrom.CurrentProduction;
                }
                if (e.PropertyName == PropertyName(() => recieveUpdatesFrom.ProductionCounter))
                {
                    Tokens = new string(Enumerable.Repeat<char>('o', recieveUpdatesFrom.ProductionCounter).ToArray());
                }
            };

            var lookup = ObjectFactory.ProductionLookup();
            productionOptions.Select(key => new List<ProductionOptionItem>
                {
                    new ProductionOptionItem(key, lookup.GetLabelFor(key), recieveUpdatesFrom)
                });

            ProductionOptions = new List<ProductionOptionItem>
            {
                new ProductionOptionItem("smallstorage", "Storage", recieveUpdatesFrom),
                new ProductionOptionItem("basicmine", "Mine", recieveUpdatesFrom),
                new ProductionOptionItem("basicquarry", "Quarry", recieveUpdatesFrom),
                new ProductionOptionItem(null, "None", recieveUpdatesFrom)
            };
        }

        public ProductionOptionItem SelectedProductionOption { get; set; }
        public List<ProductionOptionItem> ProductionOptions { get; set; }
        
    }

}
