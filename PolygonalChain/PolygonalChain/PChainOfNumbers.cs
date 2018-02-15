using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonalChain
{
    public class PChainOfNumbers : PChain
    {
        List<PPoint> entrys = new List<PPoint>();

        public override PValuetype valueType()
        {
            return PValuetype.pNumbers;
        }

        public override int length()
        {
            return entrys.Count;
        }
    }

    public class PPoint
    {
        double x, y;

        public PPoint(double xPos, double yPos)
        {
            x = xPos;
            y = yPos;
        }
    }
}
