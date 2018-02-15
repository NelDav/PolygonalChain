using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonalChain
{
    public class PChainOfChains : PChain
    {
        List<PElement> entrys = new List<PElement>();

        public override PValuetype valueType()
        {
            return PValuetype.pChains;
        }

        public override int length()
        {
            return entrys.Count;
        }

        public List<PElement> getEntrys()
    }

    public class PElement
    {
        double x;
        PChain y;
    }
}
