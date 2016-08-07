using StarCo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain.Improvements
{
    public class Habitat : IImprovement
    {
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
            throw new NotImplementedException();
        }
    }
}
