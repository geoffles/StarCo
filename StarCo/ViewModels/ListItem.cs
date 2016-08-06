using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.ViewModels
{
    public class ListItem
    {
        public ListItem(string key, string label)
        {
            Key = key;
            Label = label;
        }

        public string Key { get; set; }
        public string Label { get; set; }
    }

    public class ListItem<T>
    {
        public string Key { get; set; }
        public string Label { get; set; }
        public T Value { get; set; }
    }
}
