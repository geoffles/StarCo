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

        private string tokens;
        public string Tokens
        {
            get { return tokens; }
            set
            {
                tokens = value;
                FirePropertyChanged(() => Tokens);
            }
        }

        public string Label { get; set; }
        public string SpriteUri { get; set; }
        
        

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
