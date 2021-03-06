﻿using StarCo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain
{
    public interface IWorker
    {
        ColonyItemViewModel ToColonyItemViewModel();
        string ResourceKey { get; }
        string SubCategoryKey { get; }

        void Tick();
        void Link(Colony colony);
    }
}
