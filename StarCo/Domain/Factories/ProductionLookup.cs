﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain.Factories
{
    public class ProductionLookup
    {
        private class Production
        {
            public int Time;
            public int Size;
        }

        private IDictionary<string, Production> ProductionTimes;
        public ProductionLookup()
        {
            ProductionTimes = new Dictionary<string, Production>
            {
                { "smallstorage", new Production{Time = 5, Size = 0 } },
                { "mediumstorage", new Production{Time = 10, Size = 0}  },
                { "largestorage", new Production{Time = 15, Size = 0}  }
            };
        }

        public int GetProductionTimeFor(string resourceName)
        {
            if (ProductionTimes.ContainsKey(resourceName))
            {
                return ProductionTimes[resourceName].Time;
            }
            throw new ArgumentException("No Resource '" + resourceName + "'");
        }
    }
}