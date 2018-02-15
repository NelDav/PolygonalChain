using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonalChain
{
    public class PChainOfNumbers : PChain
    {
        public List<PPoint> entrys = new List<PPoint>();

        public override PValuetype valueType()
        {
            return PValuetype.pNumbers;
        }

        public override int length()
        {
            return entrys.Count;
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
                        PPoint temp = entrys[i];
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
                        PPoint temp = entrys[i];
                        entrys[i] = entrys[i + 1];
                        entrys[i + 1] = temp;

                        gothrought = false;
                    }
                }
            }
            return true;
        }

        public PPoint getValue(double xPos)
        {
            double differenceXPos_Point1;
            PPoint Point1 = new PPoint();
            PPoint Point2 = new PPoint();

            sortMinToMax();

            //Seraching the Point that is nearest to the searched point
            int index = 0;
            do
            {
                Point1 = entrys[index];
                differenceXPos_Point1 = xPos - Point1.x;
                index++;

                if (index >= entrys.Count)
                    break;
            } while (Math.Abs(xPos - entrys[index].x) < Math.Abs(differenceXPos_Point1));

            //Find the point on the other side of the searched point and save at Point2
            if (xPos >= Point1.x) //differenceXPos_Point1 is positive or 0
            {
                if (index + 1 < entrys.Count)
                    Point2 = entrys[index + 1];
                else if (index - 1 >= 0)    //If there is no point on the other side of the searched point take the next point on the other side of point1
                    Point2 = entrys[index - 1];
                else    //If there is only one point in entrys return this point.
                    return Point1;
            }
            else
            {
                if (index - 1 >= 0)
                    Point2 = entrys[index - 1];
                else if (index + 1 < entrys.Count)  //If there is no point on the other side of the searched point take the next point on the other side of point1
                    Point2 = entrys[index + 1];
                else    //If there is only one point in entrys return this point.
                    return Point1;
            }

            PPoint differencePoint1_Point2 = new PPoint(Point1.x - Point2.x, Point1.y - Point2.y);

            return new PPoint(xPos, (differencePoint1_Point2.y / differencePoint1_Point2.x) * differenceXPos_Point1);
        }
    }

    public class PPoint
    {
        public double x, y;

        public PPoint(double xPos, double yPos)
        {
            x = xPos;
            y = yPos;
        }

        public PPoint()
        {
            x = 0;
            y = 0;
        }
    }
}
