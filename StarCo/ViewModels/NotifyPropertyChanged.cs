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

        public string PropertyName<T>(Expression<Func<T>> expression)
        {
            var body = (MemberExpression)expression.Body;
            return body.Member.Name;
        }

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

    public static class NotifyPropertyChangedExtension
    {
        //public static string PropertyName<T, R>(this T t, Expression<Func<R>> expression)
        //{
        //    var body = (MemberExpression)expression.Body;
        //    return body.Member.Name;
        //}

    }
}
