using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Friends
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class FriendAttribute : Attribute
    {
        public FriendAttribute(params Type[] friends)
        {
            Friends = friends;
        }

        public Type[] Friends { get; private set; }
    }
}
