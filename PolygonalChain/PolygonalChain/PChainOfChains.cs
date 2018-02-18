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

        public bool addEntry(PElement element)
        {
            if (element.y != null)
            {
                bool foundElement = false;
                foreach(PElement entry in entrys)
                {
                    if (entry.x == element.x)
                        foundElement = true;
                }

                if(!foundElement)
                {
                    entrys.Add(element);
                    return true;
                }
            }
            return false;
        }

        public bool removeEntry(PElement element)
        {
            if (element.y != null)
            {
                return entrys.Remove(element);
            }
            return false;
        }

        public bool removeEntryAt(int index)
        {
            if (index > -1 && index < entrys.Count)
            {
                entrys.RemoveAt(index);
                return true;
            }
            return false;
        }

        public List<PElement> getEntrys()
        {
            return entrys;
        }

        public PElement getEntry(int index)
        {
            if (index > -1 && index < entrys.Count)
            {
                return entrys[index];
            }
            else return null;
        }

        public override List<double> getXCoordinates()
        {
            List<double> result = new List<double>();
            foreach (PElement entry in entrys)
                result.Add(entry.x);

            return result;
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

        public PElement getElement(double xCoordinate)
        {
            double differenceXCoordinate_element1;
            PElement element1 = new PElement();
            PElement element2 = new PElement();

            sortMinToMax();

            //Seraching the Point that is nearest to the searched point
            int index = 0;
            do
            {
                element1 = entrys[index];
                differenceXCoordinate_element1 = xCoordinate - element1.x;
                index++;

                if (index >= entrys.Count)
                    break;
            } while (Math.Abs(xCoordinate - entrys[index].x) < Math.Abs(differenceXCoordinate_element1));
            index--;

            //Find the point on the other side of the searched point and save at Point2
            if (xCoordinate == element1.x)
                return element1;
            else if (xCoordinate > element1.x) //differenceXPos_Point1 is positive or 0
            {
                if (index + 1 < entrys.Count)
                    element2 = entrys[index + 1];
                else if (index - 1 >= 0)    //If there is no point on the other side of the searched point take the next point on the other side of point1
                    element2 = entrys[index - 1];
                else    //If there is only one point in entrys return this point.
                    return element1;
            }
            else
            {
                if (index - 1 >= 0)
                    element2 = entrys[index - 1];
                else if (index + 1 < entrys.Count)  //If there is no point on the other side of the searched point take the next point on the other side of point1
                    element2 = entrys[index + 1];
                else    //If there is only one point in entrys return this point.
                    return element1;
            }

            return interpolateElements(element1, element2, xCoordinate, element1.x - element2.x, differenceXCoordinate_element1);
        }

        private PElement interpolateElements(PElement element1, PElement element2, double xRes, double xDiffe1_e2, double xDiffe1_xres)
        {
            //Create a List with all xCoordinates from element1 and element2
            List<double> xCoordinates = element1.y.getXCoordinates();

            foreach(double e2XCordiante in element2.y.getXCoordinates())
            {
                bool goThrough = false;
                foreach(double xcoordinate in xCoordinates)
                {
                    if (xcoordinate == e2XCordiante)
                        goThrough = true;
                }

                if (!goThrough)
                    xCoordinates.Add(e2XCordiante);
            }

            if (element1.y.valueType() == PValuetype.pChains && element2.y.valueType() == PValuetype.pChains)
            {
                PChainOfChains chainFromElement1 = (PChainOfChains)element1.y;
                PChainOfChains chainFromElement2 = (PChainOfChains)element2.y;
                PChainOfChains resultChainOfChains = new PChainOfChains();
                PElement resultElement;

                foreach (double position in xCoordinates)
                {
                    resultChainOfChains.addEntry(interpolateElements(chainFromElement1.getElement(position), chainFromElement2.getElement(position), position, xDiffe1_e2, xDiffe1_xres));
                }
                resultElement = new PElement(xRes, resultChainOfChains);
                return resultElement;
            }
            else if (element1.y.valueType() == PValuetype.pNumbers && element2.y.valueType() == PValuetype.pNumbers)
            {
                PChainOfNumbers chainFromElement1 = (PChainOfNumbers)element1.y;
                PChainOfNumbers chainFromElement2 = (PChainOfNumbers)element2.y;
                PChainOfNumbers resultChainOfNumbers = new PChainOfNumbers();
                PElement resultElement;

                foreach (double position in xCoordinates)
                {
                    resultChainOfNumbers.addPoint(new PPoint(position, chainFromElement1.getValue(position) + ((chainFromElement1.getValue(position) - chainFromElement2.getValue(position)) / xDiffe1_e2) * xDiffe1_xres));
                }

                resultElement = new PElement(xRes, resultChainOfNumbers);
                return resultElement;
            }
            else
                return null;
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
