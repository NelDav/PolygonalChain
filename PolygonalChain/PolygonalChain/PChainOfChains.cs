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

        public bool addElement(PElement element)
        {
            if (element.y != null)
            {
                entrys.Add(element);
                return true;
            }
            return false;
        }

        public bool removeElement(PElement element)
        {
            if (element.y != null)
            {
                entrys.Remove(element);
                return true;
            }
            return false;
        }

        public bool removeElementAt(int index)
        {
            if (index > -1 && index < entrys.Count)
            {
                entrys.RemoveAt(index);
                return true;
            }
            return false;
        }

        public List<PElement> getElemtns()
        {
            return entrys;
        }

        public PElement getElement(int index)
        {
            if (index > -1 && index < entrys.Count)
            {
                return entrys[index];
            }
            else return null;
        }

        public override bool sortMinToMax()
        {
            bool gothrought = false;

            for (int j = 1; j < entrys.Count - 1 && !gothrought; j++)
            {
                gothrought = true;

                for (int i = 0; i < entrys.Count - j; i++)
                {
                    if (entrys[i].x > entrys[i + 1].x)
                    {
                        PElement temp = entrys[i];
                        entrys[i] = entrys[i + 1];
                        entrys[i + 1] = temp;

                        gothrought = false;
                    }
                }
            }
            return true;
        }

        public override bool sortMaxToMin()
        {
            bool gothrought = false;

            for (int j = 0; j < entrys.Count - 1 && !gothrought; j++)
            {
                gothrought = true;

                for (int i = 0; i < entrys.Count - j; i++)
                {
                    if (entrys[i].x < entrys[i + 1].x)
                    {
                        PElement temp = entrys[i];
                        entrys[i] = entrys[i + 1];
                        entrys[i + 1] = temp;

                        gothrought = false;
                    }
                }
            }
            return true;
        }
    }

    public class PElement
    {
        public double x;
        public PChain y;

        public PElement(double xPos, PChain yPos)
        {
            x = xPos;
            y = yPos;
        }

        public PElement(double xPos)
        {
            x = xPos;
            y = null;
        }

        public PElement(PChain yPos)
        {
            x = 0;
            y = yPos;
        }

        public PElement()
        {
            x = 0;
            y = null;
        }
    }
}
