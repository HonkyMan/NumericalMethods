using System;
using System.Collections.Generic;
using System.Text;

namespace ChMeth
{
    public class MyList
    {
        static public double[] X { get; set; }
        static public double[] Y { get; set; }
        static public int n { get; set; }

        List<keyValue> kv = new List<keyValue> { };

        public MyList(double[] x, double[] y, int length)
        {
            X = x;
            Y = y;
            n = length;
        }

        public double[] getX()
        {
            return X;
        }
        public double[] getY()
        {
            return Y;
        }
        
        public void init()
        {
            for (int i = 0; i < n; i++)
            {
                kv.Add(new keyValue{kvx = X[i], kvy = Y[i]});                
            }
        }
    }
}
