using StarCo.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo
{
    

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private CivilisationController ColonyController = new CivilisationController();

        
        private string selectedColonyItem;
        public string SelectedColonyItem
        {
            get { return selectedColonyItem; }
            set { 
                selectedColonyItem = value;
                OnPropertyChanged("ColonySubItems");
            }
        }

        private string selectedColonySubItem;
        public string SelectedColonySubItem
        {
            get { return selectedColonySubItem; }
            set
            {
                selectedColonySubItem = value;
                ListItems = ColonyController.FindItemsByResource(SelectedColonyItem, selectedColonySubItem);
                OnPropertyChanged("ListItems");
            }
        }

        public IList<string> ColonyItems { get { return ColonyController.FindResourceList().Keys.ToList(); } }

        public IList<string> ColonySubItems { get { return SelectedColonyItem == null ? null : ColonyController.FindResourceList()[SelectedColonyItem]; } }

        public IList<ItemViewViewModel> ListItems { get; set; }

        private ItemViewViewModel selectedListItem;
        public ItemViewViewModel SelectedListItem 
        {
            get { return selectedListItem; }
            set 
            {
                selectedListItem = value;
                OnPropertyChanged("SelectedListItem");
                OnPropertyChanged("SelectedTitle");
                value.OnPropertyChanged("Detail");
                value.OnPropertyChanged("Title");
                value.OnPropertyChanged("Tokens");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
