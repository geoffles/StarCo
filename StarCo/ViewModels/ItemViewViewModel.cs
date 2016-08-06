using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.ViewModels
{
    public class ItemViewViewModel : NotifyPropertyChanged
    {
        public string Title { get; set; }
        public string Tokens { get; set; }
        public string Detail { get; set; }
    }
}
