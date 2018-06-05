using System;
using System.Collections.Generic;
using System.Text;

namespace ChMeth.Functions
{
    class ReverseFunction
    {
        private decimal EPS;
        private decimal[] f; //Start f array from tabulating class
        private decimal[] z; 
        private decimal[] zIteration;
        private decimal[] F; //Created array for store some func(f)
        private decimal[] g;
        static int[] k_it_new;
        private int n;
        private decimal h;
        public ReverseFunction(double[] f, double[] x, int n, double h, double eps = 0.00001)
        {
            this.n = n;
            this.h = (decimal)h;
            this.f = new decimal[n];
            this.zIteration = new decimal[n];
            for (int i = 0; i < n; i++)
            {
                this.f[i] = (decimal)f[i];
                this.zIteration[i] = (decimal)x[i];
                if (x[i] >= 3.3)
                {
                    this.n = i;
                    break;
                }
            }
            this.EPS = (decimal)eps;
            g = new decimal[n];
            F = new decimal[n];
            z = new decimal[n];
            k_it_new = new int[n];
            OnInit(); //Set F array
        }

        private void OnInit()
        {
            FuncF();
            for (int i = 0; i < n; i++)
            {
                z[i] = Iterations(zIteration[i], F[i], i);
                Console.WriteLine(z[i]);
            }
            Console.WriteLine("Hui");
        }

        private void FuncF()
        {
            for (int i = 0; i < n; i++)
            {
                F[i] = f[0] + i * (f[n - 1] - f[0]) / (n - 1);
                Console.WriteLine(F[i]);
            }
            Console.WriteLine();
        }

        private decimal Iterations(decimal x, decimal F, int i)
        {
            decimal z0 = x;

            k_it_new[i] = 1;

            decimal r = (decimal)IntMath.Si((double)z0) - F;

            decimal zk = z0 - r / dSi(z0);
            //decimal zk = z0 - (r / dSi(z0) + r / dSi(z0 + r / dSi(z0))) / 2;

            while (Math.Abs(r) > EPS)
            {
                //r = FSNRV(z0) - F;
                r = (decimal)IntMath.Si((double)z0) - F;
                zk = z0 - r / dSi(z0);
                //zk = z0 - (r / dSi(z0) + r / dSi(z0 + r / dSi(z0))) / 2;
                z0 = zk;
                k_it_new[i]++;
            }

            return zk;
        }
        
        private decimal FSNRV(decimal x)
        {
            decimal Tr = .0M;
            decimal h = (x - zIteration[0]) / (n - 1);
            for (int i = 0; i < n; i++)
            {
                Tr += (decimal)(Math.Exp(-((double)(zIteration[0] + i * h) * (double)(zIteration[0] + i * h)) / 2) / Math.Sqrt(2 * Math.PI));
                //Tr += fSNRV(a + (i - 1) * h) + fSNRV(a + i * h);
            }

            return Tr;
        }

        public decimal dSi(decimal t)
        {
            if (t == 0)
                return (decimal)1.0;
            return (decimal)Math.Sin((double)t) / t; //(decimal)Math.Cos((double)t) / t - (decimal)Math.Sin((double)t) / (t * t);
        }
    }
}
