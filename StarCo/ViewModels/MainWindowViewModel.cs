using StarCo.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StarCo.ViewModels
{
    public class MainWindowViewModel : NotifyPropertyChanged
    {
        private ColonyController ColonyController;

        public MainWindowViewModel(ColonyController colonyController)
        {
            ColonyController = colonyController;
            Categories = ColonyController.FindCategories();
            Tick = new CommandHandler(() => {
                ColonyController.Tick();
                UpdateView();
                new Persister().Save(ColonyController.Colony);
            });
        }

        private void UpdateView()
        {
            if (colonyItems != null)
            {
                var selectedIndex = ColonyItems.IndexOf(SelectedColonyItem);
                SelectedSubCategory = SelectedSubCategory;
                if (selectedIndex > -1) { SelectedColonyItem = ColonyItems[selectedIndex]; }
            }
            StorageSpace= ColonyController.GetStorageSpaceUsage();
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
                if (SelectedCategory != null)
                {
                    SubCategories = ColonyController.FindSubCategories(selectedCategory);
                }
                else
                {
                    SubCategories = null;
                }
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
                if (SelectedSubCategory != null)
                {
                    ColonyItems = ColonyController.FindItems(SelectedCategory, SelectedSubCategory);
                }
                else
                {
                    ColonyItems = null;
                }
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

        private int livingSpace;
        public int LivingSpace
        {
            get { return livingSpace; }
            set
            {
                livingSpace = value;
                FirePropertyChanged(() => LivingSpace);
            }
        }

        private int storageSpace;
        public int StorageSpace
        {
            get { return storageSpace; }
            set
            {
                storageSpace = value;
                FirePropertyChanged(() => StorageSpace);
            }
        }

        public ICommand Tick { get; private set; }
    }
}
