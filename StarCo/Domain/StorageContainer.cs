using StarCo.Domain.Factories;
using StarCo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain
{
    public class StorageContainer : IImprovement
    {

        public string Label { get; private set; }
        public string SpriteUri { get; private set; }
        public long Size { get; private set; }

        public ColonyItemViewModel ToColonyItemViewModel()
        {
            return new ColonyItemViewModel
            {
                Label = this.Label,
                Detail = string.Format("Capacity: {0}", Size),
                SpriteUri = this.SpriteUri,
                Tokens = string.Empty
            };
        }

        public string SubCategoryKey
        {
            get { return this.GetType().Name.ToLower(); }
        }

        private StorageContainer(int size, string label, string assetName)
        {
            Size = size;
            Label = label;
            SpriteUri = ObjectFactory.AssetName(assetName);
        }

        public static StorageContainer Small()
        {
            return new StorageContainer(80, "Small Storage", "SmallStorage");
        }

        public static StorageContainer Medium()
        {
            return new StorageContainer(320, "Medium Storage", "MediumStorage");
        }

        public static StorageContainer Large()
        {
            return new StorageContainer(1280, "Large Storage", "LargeStorage");
        }

        public void Tick(Colony colony)
        {
            throw new NotImplementedException();
        }
    }
}
