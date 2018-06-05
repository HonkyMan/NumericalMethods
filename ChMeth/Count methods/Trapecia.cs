using System;
using System.Collections.Generic;
using System.Text;

namespace ChMeth
{
    class Trapecia
    {
        private const double eps = 0.0001;
        private double X1 { get;  set; }

        public Trapecia(double x1)
        {
            this.X1 = x1;
        }

        public double Count(double x, int n)
        {
            double h = (double)(x - X1) / n;
            double s = h * (1 + Math.Sin(h) / h) / 2, t;
            for (int i = 1; i <= n - 1; i++)
            {
                t = (Math.Sin(X1 + i * h) / (X1 + i * h) + Math.Sin(X1 + (i + 1) * h) / (X1 + (i + 1) * h)) / 2;
                s += t * h;
            };
            return s;
        }
    }
}
