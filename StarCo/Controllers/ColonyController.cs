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
                new ListItem ( label: "Improvements", key: "improvements" )
            };
        }

        public IList<ListItem> FindSubCategories(ListItem category)
        {
            switch(category.Key)
            {
                case "storage": return StorageCategories();
                case "inventory": return Inventories();
                case "improvements": return Improvements();
                default: throw new NotImplementedException();
            };
        }

        private IList<ListItem> Improvements()
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

        private IList<ListItem> Inventories()
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
                default: throw new NotImplementedException();
            };
        }

        private IList<ColonyItemViewModel> Improvement(ListItem subcategory)
        {
            var improvements = Colony
                .Improvements
                .Where(p => p.GetType().Name.ToLower() == subcategory.Key)
                .Select(p => new ColonyItemViewModel
                {
                    Detail = "Basic Mine",
                    Tokens = "ooo",
                    SpriteUri = "/StarCo;component/Assets/BasicMine.png"
                }).ToList();

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
                return Colony.Storage.Containers.Select(p => new ColonyItemViewModel
                    {
                        Detail = "Small Storage",
                        Tokens = "ooo",
                        SpriteUri = "/StarCo;component/Assets/SmallStorage.png"
                    }).ToList();
            }

            return null;
        }
    }
}
