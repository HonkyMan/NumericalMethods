using System;
using System.Collections.Generic;
using System.Text;

namespace ChMeth.Cout_methods.Integral
{
    static class RectRule
    {
        static double Sum { get; set; }
        static double SumSecond { get; set; }
        static double H { get; set; }
        static double C { get; set; }
        const double eps = 0.0001;

        static public double InitLeft(double x1, double x2, ref int Node)
        {
            double maskX1;

            for (int i = 2; ; )
            {
                Sum = 0;
                SumSecond = 0;

                maskX1 = x1;
                H = (x2 - maskX1) / i;
                while (maskX1 != x2 && maskX1 < x2)
                {
                    Sum += H * ChMeth.Functions.IntMath.dSi(maskX1);
                    maskX1 += H;
                }

                maskX1 = x1;
                i *= 2;
                H = (x2 - maskX1) / i;
                while (maskX1 != x2 && maskX1 < x2)
                {
                    SumSecond += H * ChMeth.Functions.IntMath.dSi(maskX1);
                    maskX1 += H;
                }
                if (Math.Abs(Sum - SumSecond) < eps && Sum != 0 && SumSecond != 0)
                {
                    Node = i;
                    return Sum;
                }
            }
        }
        static public double InitRight(double x1, double x2, ref int Node)
        {
            double maskX1;

            for (int i = 2; ;)
            {
                Sum = 0;
                SumSecond = 0;

                maskX1 = x1;
                H = (x2 - maskX1) / i;
                while (maskX1 != x2 && maskX1 < x2)
                {
                    maskX1 += H;
                    Sum += H * ChMeth.Functions.IntMath.dSi(maskX1);
                }

                maskX1 = x1;
                i *= 2;
                H = (x2 - maskX1) / i;
                while (maskX1 != x2 && maskX1 < x2)
                {
                    maskX1 += H;
                    SumSecond += H * ChMeth.Functions.IntMath.dSi(maskX1);
                }
                if (Math.Abs(Sum - SumSecond) < eps && Sum != 0 && SumSecond != 0)
                {
                    Node = i;
                    return Sum;
                }
            }
        }

        static public double InitCenter(double x1, double x2, ref int Node)
        {
            double maskX1;

            for (int i = 2; ;)
            {
                Sum = 0;
                SumSecond = 0;

                maskX1 = x1;
                H = (x2 - maskX1) / i;
                while (maskX1 != x2 && maskX1 < x2)
                {
                    maskX1 += H;
                    Sum += H * ChMeth.Functions.IntMath.dSi((2 * maskX1 - H) / 2);
                }

                maskX1 = x1;
                i *= 2;
                H = (x2 - maskX1) / i;
                while (maskX1 != x2 && maskX1 < x2)
                {
                    maskX1 += H;
                    SumSecond += H * ChMeth.Functions.IntMath.dSi((2 * maskX1 - H) / 2);
                }
                if (Math.Abs(Sum - SumSecond) < eps && Sum != 0 && SumSecond != 0)
                {
                    Node = i;
                    return Sum;
                }
            }
        }
    }
}
