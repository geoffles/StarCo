using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.ViewModels
{
    public class ColonyItemViewModel : NotifyPropertyChanged
    {
        public string Label { get; set; }
        public string SpriteUri { get; set; }
        public string Detail { get; set; }
        public string Tokens { get; set; }

        private int spriteXOffset;
        public int SpriteXOffset
        {
            get { return spriteXOffset; }
            set
            {
                spriteXOffset = value;
                FirePropertyChanged(() => SpriteXOffset);
            }
        }
    }
}
