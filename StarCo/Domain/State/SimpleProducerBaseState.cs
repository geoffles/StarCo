using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain.State
{
    [DataContract]
    public class SimpleProducerBaseState
    {
        [DataMember]
        public string CurrentProduction { get; set; }

        [DataMember]
        public int ProductionCounter { get; set; }

        [DataMember]
        public int ProductionAmount { get; set; }

        [DataMember]
        public int ProductionThreshold { get; set; }

        [DataMember]
        public string ProductType { get; set; }
    }
}
