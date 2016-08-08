using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain.State
{
    [DataContract]
    public class BasicWorkerState
    {
        [DataMember]
        public Colony Colony { get; set; }
    }
}
