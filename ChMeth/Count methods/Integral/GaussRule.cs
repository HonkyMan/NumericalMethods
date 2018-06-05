using ChMeth.Functions;
using System;

namespace ChMeth.Count_methods.Integral
{
    static public class GaussRule
    {
        static double Sum { get; set; }
        static double SumSecond { get; set; }
        static double H { get; set; }
        const double eps = 0.0001;

        static public double InitGauss(double x1, double x2, ref int Node)
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
                    Sum += (H / 2) * (IntMath.dSi(maskX1 - H + H / 2 * (1 - 1 / Math.Sqrt(3))) +
                                        IntMath.dSi(maskX1 - H + H / 2 * (1 + 1 / Math.Sqrt(3))));
                }

                maskX1 = x1;
                i *= 2;
                H = (x2 - maskX1) / i;
                while (maskX1 != x2 && maskX1 < x2)
                {
                    maskX1 += H;
                    SumSecond += (H / 2) * (IntMath.dSi(maskX1 - H + H / 2 * (1 - 1 / Math.Sqrt(3))) +
                                        IntMath.dSi(maskX1 - H + H / 2 * (1 + 1 / Math.Sqrt(3))));
                }
                if (Math.Abs(Sum - SumSecond) < eps && Sum != 0 && SumSecond != 0)
                {
                    Node = i;
                    return Sum;
                }
            }
        }
        static public double InitGauss(double x1, double x2)
        {
            double maskX1;
            x2 = Math.Abs(x2);
            for (int i = 2; ;)
            {
                Sum = 0;
                SumSecond = 0;

                maskX1 = x1;
                H = (x2 - maskX1) / i;
                while (maskX1 != x2 && maskX1 < x2)
                {
                    maskX1 += H;
                    Sum += (H / 2) * (IntMath.dSi(maskX1 - H + H / 2 * (1 - 1 / Math.Sqrt(3))) +
                                        IntMath.dSi(maskX1 - H + H / 2 * (1 + 1 / Math.Sqrt(3))));
                }

                maskX1 = x1;
                i *= 2;
                H = (x2 - maskX1) / i;
                while (maskX1 != x2 && maskX1 < x2)
                {
                    maskX1 += H;
                    SumSecond += (H / 2) * (IntMath.dSi(maskX1 - H + H / 2 * (1 - 1 / Math.Sqrt(3))) +
                                        IntMath.dSi(maskX1 - H + H / 2 * (1 + 1 / Math.Sqrt(3))));
                }
                if (Math.Abs(Sum - SumSecond) < eps && Sum != 0 && SumSecond != 0)
                {
                    return Sum;
                }
            }
        }
    }
}
