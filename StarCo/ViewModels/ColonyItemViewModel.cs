using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.ViewModels
{
    public class ColonyItemViewModel : NotifyPropertyChanged
    {
        private string detail;
        public string Detail
        {
            get { return detail; }
            set
            {
                detail = value;
                FirePropertyChanged(() => Detail);
            }
        }
        
        public string Label { get; set; }
        public string SpriteUri { get; set; }
        
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
