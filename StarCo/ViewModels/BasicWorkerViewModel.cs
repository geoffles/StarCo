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
        public BasicWorkerItemTaskViewModel(BasicWorker recieveUpdatesFrom)
        {
            recieveUpdatesFrom.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "CurrentProduction")
                {
                    Detail = recieveUpdatesFrom.CurrentProduction;
                }
                if (e.PropertyName == "ProductionCounter")
                {
                    Tokens = new string(Enumerable.Repeat<char>('o', recieveUpdatesFrom.ProductionCounter).ToArray());
                }
            };

            ProductionOptions = new List<ListItem>
            {
                new ListItem("smallstorage", "Storage"),
                new ListItem("basicmine", "Mine")
            };
        }

        public ICommand ChangeProduction { get; set; }

        public ListItem SelectedProductionOption { get; set; }
        public List<ListItem> ProductionOptions { get; set; }
        
    }

}
