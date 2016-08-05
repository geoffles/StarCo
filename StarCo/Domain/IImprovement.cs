using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCo.Domain
{
    public interface IImprovement
    {
        void Tick(Colony colony);
    }
}
