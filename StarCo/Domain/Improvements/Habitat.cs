using StarCo.Domain.Factories;
using StarCo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain.Improvements
{
    [DataContract]
    public class Habitat : IImprovement
    {
        [DataMember]
        public Colony Colony { get; private set; }

        public Habitat()
        {

        }

        public string ResourceKey { get { return "habitat"; } }

        public void Tick(Colony colony)
        {
            //Right now we don't do anything
        }

        public ColonyItemViewModel ToColonyItemViewModel()
        {
            return new ColonyItemViewModel
            {
                Label = "Habitat",
                Detail = string.Format("Capacity: {0}", Size),
                SpriteUri = ObjectFactory.AssetName("Habitat"),
                Tokens = string.Empty
            };
        }

        public string SubCategoryKey
        {
            get { return this.GetType().Name.ToLower(); }
        }

        public long Size
        {
            get { return 5; }
        }

        public void Link(Colony colony)
        {
            Colony = colony;
            Colony.Storage.AddHabitat(this);
        }

        protected bool CheckProductionSpace()
        {
            return false;
            //throw new NotImplementedException();
        }

        protected void AllocateProduction()
        {
            //throw new NotImplementedException();
        }
    }
}
