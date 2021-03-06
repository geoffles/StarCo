﻿using StarCo.Domain.Improvements;
using StarCo.Domain.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain.Factories
{
    public class ImprovementFactory
    {
        public IImprovement BuildImprovement(string resourceName)
        {
            switch (resourceName)
            {
                case "smallstorage": return StorageContainer.Small(); 
                case "mediumstorage": return StorageContainer.Medium();
                case "largestorage": return StorageContainer.Large();
                case "basicmine": return BasicMine.Gold();
                case "basicquarry": return BasicQuarry.Stone();               
                default: throw new ArgumentException("No resource '" + resourceName + "'");
            }
        }
    }
}
