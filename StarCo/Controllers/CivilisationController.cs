using StarCo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Controllers
{
    public class CivilisationController
    {
        public IDictionary<string, IList<string>> FindResourceList()
        {
            return new Dictionary<string, IList<string>>
            {
                {"Storage", new List<string>{"Storage", "Habitat"}},
                {"Inventory", new List<string>{"Gold", "Iron", "Copper"}},
                {"Improvements", new List<string>{"BasicMine", "BasicFactory"}}
            };
        }

        public IList<ItemViewViewModel> FindItemsByResource(string resourceType, string resourceName)
        {
            switch(resourceType)
            {
                case "Storage": return FindStorage(resourceName);
                case "Inventory": return FindInventory(resourceName);
                case "Improvements": return FindImprovements(resourceName);
                default:return null;
            }
        }

        private IList<ItemViewViewModel> FindStorage(string resourceName)
        {
            if (resourceName == "Storage")
            { 
                return new List<ItemViewViewModel>
                {
                    new ItemViewViewModel{Title = "SmallStorage", Detail="100% Full" },
                    new ItemViewViewModel{Title = "SmallStorage", Detail="24% Full" }
                };
            }
            else
            {
                return new List<ItemViewViewModel>
                {
                    new ItemViewViewModel{Title = "Habitat", Detail = "Small", Tokens = "ooo" }
                };
            }
        }

        private IList<ItemViewViewModel> FindImprovements(string improvementType)
        {
            if (improvementType == "BasicMine")
            {
                return new List<ItemViewViewModel>
                {
                    new ItemViewViewModel {Title = "BasicMine", Detail="Gold", Tokens="o"},
                    new ItemViewViewModel {Title = "BasicMine", Detail="Iron", Tokens="oo"},
                    new ItemViewViewModel {Title = "BasicMine", Detail="Gold", Tokens=""},
                    new ItemViewViewModel {Title = "BasicMine", Detail="Copper", Tokens="o"}
                };
            }

            if (improvementType == "BasicFactory")
            {
                return new List<ItemViewViewModel>
                {
                    new ItemViewViewModel {Title = "BasicFactory", Detail="Luxuries", Tokens="oooo"},
                    new ItemViewViewModel {Title = "BasicFactory", Detail="Electronics", Tokens="oo"}
                };
            }

            return null;
        }

        private IList<ItemViewViewModel> FindInventory(string inventoryType)
        {
            return new List<ItemViewViewModel>
            {
                new ItemViewViewModel {Title=inventoryType, Detail=(Math.Abs(inventoryType.GetHashCode()%30)).ToString(), Tokens=""}
            };
        }
    }
}
