using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain.State
{
    [DataContract]
    public class DynamicProducerBaseState
    {
        [DataMember]
        public string CurrentProduction { get; set; }

        [DataMember]
        public int ProductionCounter { get; set; }

        [DataMember]
        public int MaxProductionCounter { get; set; }
    }
}
