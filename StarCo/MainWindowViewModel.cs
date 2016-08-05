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
        private IDictionary<string, IList<string>> ResourceList = new Dictionary<string, IList<string>>
        {
            {"Storage", new List<string>{"x", "y", "z"}},
            {"Inventory", new List<string>{"A", "B", "C"}},
            {"Improvements", new List<string>{"Foo", "Bar", "Baz"}}
        };


        private string selectedColonyItem;
        public string SelectedColonyItem
        {
            get { return selectedColonyItem; }
            set { 
                selectedColonyItem = value;
                OnPropertyChanged("ColonySubItems");
            }
        }

        public IList<string> ColonyItems { get { return ResourceList.Keys.ToList(); } }

        public IList<string> ColonySubItems { get { return SelectedColonyItem == null? null : ResourceList[SelectedColonyItem]; } }

        public IList<ItemViewViewModel> ListItems
        {
            get
            {
                return new List<ItemViewViewModel> 
                {
                    new ItemViewViewModel{Title = "Mine", Detail="Gold", Tokens="oooo"},
                    new ItemViewViewModel{Title = "Mine", Detail="Iron", Tokens="oo" },
                    new ItemViewViewModel{Title = "Mine", Detail="Silver", Tokens="oo"},
                    new ItemViewViewModel{Title = "Mine", Detail="Iron", Tokens="oo" },
                    new ItemViewViewModel{Title = "Mine", Detail="Iron", Tokens="oo" },
                    new ItemViewViewModel{Title = "Mine", Detail="Iron", Tokens="oo" },
                    new ItemViewViewModel{Title = "Mine", Detail="Iron", Tokens="oo" },
                    new ItemViewViewModel{Title = "Mine", Detail="Iron", Tokens="oo" },
                    new ItemViewViewModel{Title = "Mine", Detail="Iron", Tokens="oo" },
                    new ItemViewViewModel{Title = "Mine", Detail="Iron", Tokens="oo" },

                };
            }
        }

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


        public string SelectedTitle { get { return SelectedListItem == null ? null : SelectedListItem.Title; } }
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
