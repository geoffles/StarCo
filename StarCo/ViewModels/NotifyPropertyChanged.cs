using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.ViewModels
{
    [DataContract]
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void FirePropertyChanged<T>(Expression<Func<T>> expression)
        {
            var body = (MemberExpression)expression.Body;
            FirePropertyChanged(body.Member.Name);
        }

        public void FirePropertyChanged(string property)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
