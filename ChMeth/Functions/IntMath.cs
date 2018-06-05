using System;
using System.Collections.Generic;
using System.Text;

namespace ChMeth.Functions
{
    static public class IntMath
    {
        static public double dSi(double t)
        {
            if(t != 0)
                return Math.Sin(t) / t;
            return 1;
        }
        static public double Si(double x, decimal eps = 0.000001M)
        {
            double s = x;
            //if (x >= 66) return 1.5697957048;
            //if (x <= 66) return -1.5697957048;
            double k = x;
            int i = 0;
            while ((decimal)Math.Abs(k) > eps)
            {
                k *= -((x * x * (2 * i + 1)) / ((2 * i + 2) * (2 * i + 3) * (2 * i + 3)));
                s += k;
                ++i;
            }
            return s;
        }
    }
}
