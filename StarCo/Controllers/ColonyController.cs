using StarCo.Domain;
using StarCo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Controllers
{
    public class ColonyController
    {
        public ColonyController(Colony colony)
        {
            Colony = colony;
        }

        public Colony Colony {get; private set;}

        public IList<ListItem> FindCategories()
        {
            return new List<ListItem>
            {
                new ListItem ( label: "Storage", key: "storage" ),
                new ListItem ( label: "Inventory", key: "inventory" ),
                new ListItem ( label: "Improvements", key: "improvements" ),
                new ListItem ( label: "Workers", key: "workers" )
            };
        }

        public IList<ListItem> FindSubCategories(ListItem category)
        {
            switch(category.Key)
            {
                case "storage": return StorageCategories();
                case "inventory": return InventoryCategories();
                case "improvements": return ImprovementCategories();
                case "workers": return WorkerCategories();
                default: throw new NotImplementedException();
            };
        }

        private IList<ListItem> WorkerCategories()
        {
            return Colony
                .Workers
                .GroupBy(p => p.SubCategoryKey)
                .Select(p => new ListItem(key:p.Key, label:p.Key))
                .ToList();
        }

        private IList<ListItem> ImprovementCategories()
        {
            return Colony
                .Improvements
                .Select(p => new ListItem
                (
                    key: p.GetType().Name.ToLower(),
                    label: p.GetType().Name
                ))
                .ToList();
        }

        private IList<ListItem> InventoryCategories()
        {
            return Colony.Inventory.Keys.Select(p => new ListItem
                (
                    key: p,
                    label: p
                ))
            .ToList();
        }

        private IList<ListItem> StorageCategories()
        {
            return new List<ListItem>
            {
                new ListItem(key:"storage", label: "Storage"),
                new ListItem(key:"habitat", label: "Habitat"),

            };
        }

        public IList<ColonyItemViewModel> FindItems(ListItem category, ListItem subcategory)
        {
            switch (category.Key)
            {
                case "storage": return Storage(subcategory);
                case "inventory": return Inventory(subcategory);
                case "improvements": return Improvement(subcategory);
                case "workers": return Workers(subcategory);
                default: throw new NotImplementedException();
            };
        }

        private IList<ColonyItemViewModel> Workers(ListItem subcategory)
        {
            var workers = Colony
                .Workers
                .GroupBy(p => p.GetType().Name)
                .Where(p => p.Key.ToLower() == subcategory.Key)
                .SelectMany(p => p)
                .Select(p => p.ToColonyItemViewModel())
                .ToList();

            return workers;
        }

        private IList<ColonyItemViewModel> Improvement(ListItem subcategory)
        {
            var improvements = Colony
                .Improvements
                .GroupBy(p => p.GetType().Name)
                .Where(p => p.Key.ToLower() == subcategory.Key)
                .SelectMany(p => p)
                .Select(p => p.ToColonyItemViewModel())
                .ToList();

            return improvements;
        }

        private IList<ColonyItemViewModel> Inventory(ListItem subcategory)
        {
            throw new NotImplementedException();
        }

        private IList<ColonyItemViewModel> Storage(ListItem subcategory)
        {
            if (subcategory.Key == "storage")
            {
                return Colony.Storage.Containers.Select(p => p.ToColonyItemViewModel()).ToList();
            }

            return null;
        }
    }
}
