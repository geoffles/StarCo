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
            };

            ProductionOptions = new List<string>
            {
                "Storage",
                "Mine"
            };
        }

        public ICommand ChangeProduction { get; set; }

        public string SelectedProductionOption { get; set; }
        public List<string> ProductionOptions { get; set; }
        
    }

}
