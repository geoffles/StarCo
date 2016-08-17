using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain.State
{
    [DataContract]
    public class AbstractWorkerState
    {
        [DataMember]
        public Colony Colony { get; set; }

        [DataMember]
        public string Sprite { get; set; }

        [DataMember]
        public IList<string> ProductionOptions { get; set; }

        [DataMember]
        public string ResourceKey { get; set; }

        [DataMember]
        public string SubCategoryKey { get; set; }
    }
}
