using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonalChain
{
    public class PChain
    {
        public enum PValuetype { pNull, pNumbers, pChains};

        public virtual PValuetype valueType()
        {
            return PValuetype.pNull;
        }

        public virtual int length()
        {
            return 0;
        }

        public virtual bool sortMinToMax()
        {
            return false;
        }

        public virtual bool sortMaxToMin()
        {
            return false;
        }
    }
}
