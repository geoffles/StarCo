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

        public string ResourceKey { get { return "habitat"; } }

        public void Tick(Colony colony)
        {
            throw new NotImplementedException();
        }

        public ColonyItemViewModel ToColonyItemViewModel()
        {
            throw new NotImplementedException();
        }

        public string SubCategoryKey
        {
            get { return this.GetType().Name.ToLower(); }
        }

        public void Link(Colony colony)
        {
            Colony = colony;
            //TODO: This should be encapsulated.
            Colony.Storage.Habitats.Add(this);
        }
    }
}
