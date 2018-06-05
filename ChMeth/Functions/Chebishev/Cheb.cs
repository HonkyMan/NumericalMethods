using System;
using System.Collections.Generic;
using System.Text;

namespace ChMeth
{
    static class Cheb
    {
        static private double[] x { get; set; }
        static private double x1 { get; set; }
        static private double x2 { get; set; }

        static public double[] getNodes(double[] arr, double start, double end)
        {
            x = arr;
            x1 = start;
            x2 = end;

            for (int i = 0; i < x.Length; i++)
            {
                x[i] = 0.5 * ((start + end) + (end - start) * Math.Cos(Math.PI * (2 * i - 1) / (2 * x.Length)));
            }

            return x;
        }
    }
}
