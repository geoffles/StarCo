using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain.Factories
{
    public class LabelLookup
    {
        public LabelLookup()
        {
            CategoryLabels = new Dictionary<string, string>
            {
                {"storage", "Storage"},
                {"inventory", "Inventory"},
                {"improvements", "Improvements"},
                
            };
        }

        public IDictionary<string, string> CategoryLabels { get; private set; }
    }
}
