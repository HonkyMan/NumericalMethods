using System;
using System.Collections.Generic;
using System.Text;

namespace ChMeth
{
    class Logranzh
    {
        private int n;
        private double[] x;
        private double[] f;

        public Logranzh(ref double[] x, ref double[] f, int n)
        {
            this.x = x;
            this.f = f;
            this.n = n;
        }

        public double Polinomial(double t)
        {
            double p, sum = .0;
            for (int i = 0; i < n; i++)
            {
                p = 1;
                for (int j = 0; j < n; j++)
                {
                    if (i == j) continue;
                    p *= (t - x[j]) / (x[i] - x[j]);
                }
                sum += p * f[i];
            }
            return sum;
        }
    }
}
