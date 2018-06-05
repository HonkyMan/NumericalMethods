using System;
using System.Collections.Generic;
using System.Text;
using ChMeth.Functions;

namespace ChMeth.Count_methods.Integral
{
    static public class SimpRule
    {
        static double Sum { get; set; }
        static double SumSecond { get; set; }
        static double H { get; set; }
        const double eps = 0.0001;

        static public double InitSimp(double x1, double x2, ref int Node)
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
                    Sum += (H / 6) * (IntMath.dSi(maskX1 - H) + 4 * IntMath.dSi((2 * maskX1 - H) / 2) + IntMath.dSi(maskX1));
                }

                maskX1 = x1;
                i *= 2;
                H = (x2 - maskX1) / i;
                while (maskX1 != x2 && maskX1 < x2)
                {
                    maskX1 += H;
                    SumSecond += (H / 6) * (IntMath.dSi(maskX1 - H) + 4 * IntMath.dSi((2 * maskX1 - H) / 2) + IntMath.dSi(maskX1));
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
