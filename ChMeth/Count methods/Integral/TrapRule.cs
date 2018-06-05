using System;
using System.Collections.Generic;
using System.Text;
using ChMeth.Functions;

namespace ChMeth.Count_methods.Integral
{
    static public class TrapRule
    {
        static double Sum { get; set; }
        static double SumSecond { get; set; }
        static double H { get; set; }
        const double eps = 0.0001;

        static public double InitTrap(double x1, double x2, ref int Node)
        {
            double z;

            for (int i = 2; ;)
            {
                Sum = 0;
                SumSecond = 0;

                z = x1;
                H = (x2 - z) / i;
                while (z != x2 && z < x2)
                {
                    z += H;
                    Sum += H * (IntMath.dSi(z) + IntMath.dSi(z - H)) / 2;
                }

                z = x1;
                i *= 2;
                H = (x2 - z) / i;
                while (z != x2 && z < x2)
                {
                    z += H;
                    SumSecond += H * (IntMath.dSi(z) + IntMath.dSi(z - H)) / 2;
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
