using StarCo.Friends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain.Improvements
{
    [DataContract]
    public abstract class ImprovementBase : SimpleProducerBase
    {
        protected class ColonyFriend : Friend
        {
            public ColonyFriend(Colony colony) : base(colony)
            {
            }

            public void AddImprovement(IImprovement improvement)
            {
                Invoke(improvement);
            }
        }

        protected ImprovementBase(string productType, int productionThreshold, int productionAmount)
            :base(productType, productionThreshold, productionAmount)
        {
        }
    }
}
