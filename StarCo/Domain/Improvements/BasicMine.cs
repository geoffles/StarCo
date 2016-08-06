using StarCo.Domain.Factories;
using StarCo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain.Improvements
{
    public class BasicMine : SimpleProducerBase, IImprovement
    {
        private BasicMine(IList<string> productTypes) : base(
            productTypes: productTypes, 
            productionThreshold: 3, 
            productionAmount: 1,
            maxProductionCounter: 10)
        {}

        public static BasicMine Gold()
        {
            return new BasicMine(new List<string> { "gold" });
        }

        public ColonyItemViewModel ToColonyItemViewModel()
        {
            return new ColonyItemViewModel
            {
                Label = this.Label,
                Detail = this.Detail,
                SpriteUri = this.SpriteUri,
                Tokens = this.Tokens
            };
        }

        public string SubCategoryKey
        {
            get { return this.GetType().Name.ToLower(); }
        }

        public string Label
        {
            get { return "Basic Mine"; }
        }

        public string Detail
        {
            get { return string.Join(",", ProductTypes); }
        }

        public string Tokens
        {
            get { return new string(Enumerable.Repeat<char>('o', productionCounter).ToArray()); }
        }

        public string SpriteUri
        {
            get { return ObjectFactory.AssetName("BasicMine"); }
        }

        public void Tick(Colony colony)
        {
            base.DoTick(colony);
        }
    }

}
