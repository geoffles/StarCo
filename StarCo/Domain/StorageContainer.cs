using StarCo.Domain.Factories;
using StarCo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain
{
    [DataContract]
    public class StorageContainer : IImprovement
    {
        [DataMember]
        public string Label { get; private set; }
        [DataMember]
        public string SpriteUri { get; private set; }
        [DataMember]
        public long Size { get; private set; }
        [DataMember]
        public string ResourceKey { get; private set; }

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

        private StorageContainer(int size, string key, string label, string assetName)
        {
            Size = size;
            Label = label;
            SpriteUri = ObjectFactory.AssetName(assetName);
        }

        public static StorageContainer Small()
        {
            return new StorageContainer(80, "smallstorage", "Small Storage", "SmallStorage");
        }

        public static StorageContainer Medium()
        {
            return new StorageContainer(320, "mediumstorage", "Medium Storage", "MediumStorage");
        }

        public static StorageContainer Large()
        {
            return new StorageContainer(1280, "largestorage", "Large Storage", "LargeStorage");
        }

        public void Tick(Colony colony)
        {
            throw new NotImplementedException();
        }

        public void Link(Colony colony)
        {
            colony.Storage.AddContainer(this);
        }
    }
}
