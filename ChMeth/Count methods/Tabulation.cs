using System;
using System.Collections.Generic;
using System.Text;

namespace ChMeth
{
    class Tabulation
    {
        private double x1;
        private double x2;
        private decimal eps;
        private double h;
        private int n;
        private double[] x;

        public Tabulation(double startPoint, double endPoint, decimal eps, int n)
        {
            this.x1 = startPoint;
            this.x2 = endPoint;
            this.eps = eps;
            this.n = n;
            this.h = (x2 - x1) / (n - 1);
            //this.h = step;
            //this.n = (int)((x2 - x1) / h);
        }
        
        private double Ryad(double x)
        {
            double s = x;
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

        internal void Count(double[] x, ref double[] f)
        {
            this.x = x;
            for (int i = 0; i < n; i++)
            {
                f[i] = Ryad(x[i]);
            }
        }

        internal void Count(ref double[] x, ref double[] f)
        {
            for (int i = 0; i < n; i++)
            {
                x[i] = x1 + i * h;
                f[i] = Ryad(x[i]);
            }
        }
    }
}
