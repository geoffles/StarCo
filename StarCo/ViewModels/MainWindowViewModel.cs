using StarCo.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.ViewModels
{
    public class MainWindowViewModel : NotifyPropertyChanged
    {
        private ColonyController ColonyController;

        public MainWindowViewModel(ColonyController colonyController)
        {
            ColonyController = colonyController;

            Categories = ColonyController.FindCategories();
        }

        private IList<ListItem> categories;
        public IList<ListItem> Categories
        {
            get { return categories; }
            set
            {
                categories = value;
                FirePropertyChanged(() => Categories);
            }
        }

        private ListItem selectedCategory;
        public ListItem SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                SubCategories = ColonyController.FindSubCategories(selectedCategory);
                FirePropertyChanged(() => SelectedCategory);
            }
        }

        private IList<ListItem> subCategories;
        public IList<ListItem> SubCategories
        {
            get { return subCategories; }
            set 
            {
                subCategories = value;
                FirePropertyChanged(() => SubCategories);
            }
        }

        private ListItem selectedSubCategory;
        public ListItem SelectedSubCategory
        {
            get { return selectedSubCategory; }
            set
            {
                selectedSubCategory = value;
                ColonyItems = ColonyController.FindItems(SelectedCategory, SelectedSubCategory);
                FirePropertyChanged(() => ColonyItems);
                FirePropertyChanged(() => SelectedSubCategory);
            }
        }

        private IList<ColonyItemViewModel> colonyItems;
        public IList<ColonyItemViewModel> ColonyItems
        {
            get { return colonyItems; }
            set
            {
                colonyItems = value;
                FirePropertyChanged(() => ColonyItems);
            }
        }

        private ColonyItemViewModel selectedColonyItem;
        public ColonyItemViewModel SelectedColonyItem
        {
            get { return selectedColonyItem; }
            set
            {
                selectedColonyItem = value;
                FirePropertyChanged(() => SelectedColonyItem);
            }
        }
    }
}
