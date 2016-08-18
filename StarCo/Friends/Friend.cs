using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Friends
{
    public abstract class Friend
    {
        public object Target { get; private set; }

        protected Friend(object target)
        {
            Target = target;
        }

        protected void Invoke(params object[] args)
        {
            var proxyFrame = new StackFrame(1);
            var callerframe = new StackFrame(2);
            
            var proxyInfo = proxyFrame.GetMethod();            

            string methodName = proxyFrame.GetMethod().Name;
            var callingType = callerframe.GetMethod().DeclaringType;

            var targetType = Target.GetType();

            var friendsList = targetType
                .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(method => method.Name == methodName)
                .Where(method => method
                    .GetCustomAttributes(typeof(FriendAttribute), false)
                    .OfType<FriendAttribute>()
                    .Any(r => r
                        .Friends
                        .Any(s => s
                            .IsAssignableFrom(callingType))
                    )
                )
                .ToList();

            if (!friendsList.Any())
            {
                var message = string.Format("The calling method is friendly but has no friends");

                throw new InvalidOperationException(message);            
            }

            var targetMethod = friendsList.First();

            targetMethod.Invoke(Target, args);
        }
    }
}
