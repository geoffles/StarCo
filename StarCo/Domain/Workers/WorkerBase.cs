using StarCo.Friends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain.Workers
{
    [DataContract]
    public abstract class WorkerBase : DynamicProducerBase
    {
        protected class ColonyProxy : Friend
        {
            public ColonyProxy(Colony colony) : base(colony) { }

            public void AddWorker(IWorker worker)
            {
                Invoke(worker);
            }
        }      

        public WorkerBase(int maxProduction)
            : base(maxProduction)
        {
        }        
    }
}
