using StarCo.Domain;
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
    public class AbstractWorkerItemTaskViewModel : ColonyItemViewModel
    {
        public class ProductionOptionItem : NotifyPropertyChanged
        {
            public string Key { get; set; }
            public string Label { get; set; }
            public ICommand Click { get; set; }
            public AbstractWorker Target {get; private set;}
            
            private bool meetsRequirements;
            public bool MeetsRequirements
            {
                get { return Target.ValidateProductionRequirements(Key); }
                set
                {
                    meetsRequirements = value;
                    FirePropertyChanged(() => MeetsRequirements);
                }
            }

            public ProductionOptionItem(string key, string label, AbstractWorker target)
            {
                Key = key; 
                Label = label;
                Target = target;
                Click = new CommandHandler<ProductionOptionItem>(vm => vm.Apply());
            }

            public void Apply()
            {
                Target.SetProduction(Key);
            }
        }

        private string MakeTokens(int count)
        {
            return new string(Enumerable.Repeat<char>('o', count).ToArray());
        }

        public AbstractWorkerItemTaskViewModel(AbstractWorker recieveUpdatesFrom, IEnumerable<string> productionOptions)
        {
            recieveUpdatesFrom.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == PropertyName(() => recieveUpdatesFrom.CurrentProduction))
                {
                    Detail = recieveUpdatesFrom.CurrentProduction;
                }
                if (e.PropertyName == PropertyName(() => recieveUpdatesFrom.ProductionCounter))
                {
                    Tokens = MakeTokens(recieveUpdatesFrom.ProductionCounter);
                }
            };

            var lookup = ObjectFactory.ProductionLookup();
            ProductionOptions = productionOptions
                .Select(key => new ProductionOptionItem(key, lookup.GetLabelFor(key), recieveUpdatesFrom))
                .Concat(new[] { new ProductionOptionItem(null, "None", recieveUpdatesFrom) })
                .ToList();

            Label = lookup.GetLabelFor(recieveUpdatesFrom.ResourceKey);
            Detail = recieveUpdatesFrom.CurrentProduction;
            SpriteUri = ObjectFactory.AssetName(lookup.GetGlyphKeyFor(recieveUpdatesFrom.ResourceKey));
            Tokens = MakeTokens(recieveUpdatesFrom.ProductionCounter);
        }

        public ProductionOptionItem SelectedProductionOption { get; set; }
        public List<ProductionOptionItem> ProductionOptions { get; set; }
        
    }

}
