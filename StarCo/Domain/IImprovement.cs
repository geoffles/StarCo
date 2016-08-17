using StarCo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain
{

    public interface IImprovement
    {
        string ResourceKey { get; }
        string SubCategoryKey { get; }

        ColonyItemViewModel ToColonyItemViewModel();

        void Link(Colony colony);
        void Tick(Colony colony);
    }
}
