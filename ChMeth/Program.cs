using System;
using System.Collections.Generic;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace ChMeth 
{
    //public class Account
    //{
    //    public int ID { get; set; }
    //    public double Balance { get; set; }
    //}

    class Program
    {
        private static string writePath = @"..\Task_1_result.txt";
        //static private Excel.Application excelapp; //Для работы с Excel (Построение графиков)
        private static string text = string.Empty;
        static void Main(string[] args)
        {
            #region Variables
            int n; //Длина исходного массива
            int nn; //Новая длина массива для Логранжа
            double[] x; // Начальный массив Иксов, которые мы задаем с помощью формулы x[i] = x1 + i * h (x1 - начальная точка, i - счетчик, h - шаг)
            double[] f; //Результаты f[i] - которые мы получим, Табулируя функцию

            double[] nf; 
            double[] nx; //Массив иКсов длины в 2 раза больше чем n и шаг h - в 2 раза меньше (т.е. длина задается формулой (int){[x2 - x1] / [h / 2]})

            double[] chx; //Массив иксов - Чебышева
            double[] chf; // Массив результатов от ряда Чебышева (при табуляции)
            double[] chfL; //Это кароч массив для вычисления логранжа для ряда Чебышева

            double[] tf; //Результаты выполнения полинома лагранжа
            //double[] tx;

            double x1, x2, h; // x1 - start line sigment, x2 - end line sigment
            decimal eps; //Нубл... и так понятно
            #endregion

            #region Excel
            //// Создаём экземпляр нашего приложения
            //Excel.Application excelApp = new Excel.Application();
            //// Создаём экземпляр рабочий книги Excel
            //Excel.Workbook workBook;
            //// Создаём экземпляр листа Excel
            //Excel.Worksheet workSheet;

            //workBook = excelApp.Workbooks.Add();
            //workSheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);
            //for (int k = 1; k <= 10; k++)
            //{
            //    workSheet.Cells[1, k] = k;
            //}
            //excelApp.Visible = true;
            //excelApp.UserControl = true;
            #endregion

            #region set x1, x2, eps, step
            Console.Write("Enter Start Point: ");
            x1 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter End Point: ");
            x2 = Convert.ToDouble(Console.ReadLine());

            eps = Pow(10, -10);
            Console.Write("Epselent equals to:  " + eps + " \n");

            Console.Write("Enter count of nodes: ");
            n = Convert.ToInt32(Console.ReadLine()) + 1;

            text = "Enter Start Point: " + x1.ToString() + " \nEnter End Point: " + x2.ToString() + 
                " \nEpselent equals to: " + eps.ToString() + " \nEnter count of nodes: " + n.ToString() + "\n\n";

            //n = (int)((x2 - x1) / h);
            f = new double[n];
            x = new double[n];

            h = (x2 - x1) / (n - 1);
            nn = 2 * n - 1;
            nf = new double[nn];
            nx = new double[nn];

            chx = new double[nn];
            chx = ChMeth.Cheb.getNodes(chx, x1, x2);

            chf = new double[nn];
            chfL = new double[nn];

            tf = new double[nn];

            #endregion

            #region first tabulating small array
            Console.WriteLine("\nТабуляция начальныхзначений...\n");
            Tabulation tab = new Tabulation(x1, x2, eps, n);
            tab.Count(ref x, ref f);

            text += "\nBegin to Tabulating first array \n";
            Console.WriteLine("\nBegin to Tabulating first array \n");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(f[i]);
                text += "x[" + i + "] = " + x[i] + "; f[" + i + "] = " + f[i] + "; \n";
            }
            text += "End proccess. \n";
            FileOutPut(text);
            #endregion

            #region tabulating big array
            Console.WriteLine("Табуляция расширинного массива...\n");
            int j = 0;
            for (int i = 0; i < n-1; i++)
            {
                nx[j] = x[i];
                nx[++j] = x[i] + h / 2;
                j++;
            }
            nx[j] = nx[--j] + h / 2;

            tab = new Tabulation(x1, x2, eps, 2 * n - 1);
            tab.Count(nx, ref nf);

            text += "\nBegin to Tabulating second (BIG) array \n";
            for (int i = 0; i < nn; i++)
            {
                text += "nx[" + i + "] = " + nx[i] + "; nf[" + i + "] = " + nf[i] + "; \n";
            }
            text += "End peoccess. \n";
            FileOutPut(text);
            #endregion

            #region Set new array via Logranzh method
            Logranzh log = new Logranzh(ref x, ref f, n);
            for (int i = 0; i < nn; i++)
            {
                tf[i] = log.Polinomial(nx[i]);
            }

            text += "\nBegin to create Logranzh polinom in array \n";
            for (int i = 0; i < nn; i++)
            {
                text += "nx[" + i + "] = " + nx[i] + "; tf[" + i + "] = " + tf[i] + "; \n";
            }
            text += "End peoccess. \n";
            #endregion

            #region 1st Difference between

            text += "\nDifference between: \n";
            for (int i = 0; i < nn; i++)
            {
                text += Convert.ToString((decimal)(nf[i] - tf[i])) + "\n";
            }
            text += "End; \n";

            text += "\n***The biggest eps = " + MaxEps(nf, tf, nn).ToString() + "***\n";

            #endregion

            #region tabulating chebishova array

            tab = new Tabulation(x1, x2, eps, 2 * n - 1);
            tab.Count(chx, ref chf);

            text += "\nBegin to Tabulating third Cheb array \n";
            for (int i = 0; i < nn; i++)
            {
                text += "nx[" + i + "] = " + chx[i] + "; nf[" + i + "] = " + chf[i] + "; \n";
            }
            text += "End peoccess. \n";
            FileOutPut(text);

            #endregion

            #region Set new array via Chebishova ryad

            for (int i = 0; i < nn; i++)
            {
                chfL[i] = log.Polinomial(chx[i]);
            }
            text += "\nBegin to create Chebishov polinom in array \n";
            for (int i = 0; i < nn; i++)
            {
                text += "chx[" + i + "] = " + chx[i] + "; chf[" + i + "] = " + chfL[i] + "; \n";
            }
            text += "End peoccess. \n";

            #endregion

            #region 2nd difference between

            text += "\nDifference between: \n";
            for (int i = 0; i < nn; i++)
            {
                text += Convert.ToString((decimal)(chf[i] - chfL[i])) + "\n";
            }
            text += "End; \n";

            text += "\n***The biggest eps = " + MaxEps(chf, chfL, nn).ToString() + "***\n";

            #endregion

            #region 3rd difference between difference
            Console.WriteLine("Looking for difference between old differences...");
            Console.WriteLine(MaxEps(chf, chfL, nn));
            if (MaxEps(chf, chfL, nn) > MaxEps(nf, tf, nn))
                text += "\n ***The Chebishev row is very very good*** \n" + (decimal)(MaxEps(chf, chfL, nn) - MaxEps(nf, tf, nn));
            else
                text += "\n ***The Chebishev row is very very bad*** \n" + (decimal)(MaxEps(chf, chfL, nn) - MaxEps(nf, tf, nn));
            #endregion

            #region Squares counting

            text += "\nSquares counting\n ";
            Console.WriteLine("Count integral square...");
            int[] Node = new int[x.Length];
            Console.WriteLine("\nТрап прямоугольники\n");
            for (int i = 1; i < x.Length; i++)
            {
                Console.WriteLine(ChMeth.Count_methods.Integral.TrapRule.InitTrap(x1, x[i], ref Node[i]));
            }
            for (int i = 0; i < x.Length; i++)
            {
                Console.WriteLine(Node[i]);
            }


            Console.WriteLine("\nСимпсон прямоугольники\n");
            for (int i = 1; i < x.Length; i++)
            {
                Console.WriteLine(ChMeth.Count_methods.Integral.SimpRule.InitSimp(x1, x[i], ref Node[i]));
            }
            for (int i = 0; i < x.Length; i++)
            {
                Console.WriteLine(Node[i]);
            }


            Console.WriteLine("\nГаусс прямоугольники\n");
            for (int i = 1; i < x.Length; i++)
            {
                Console.WriteLine(ChMeth.Count_methods.Integral.GaussRule.InitGauss(x1, x[i], ref Node[i]));
            }
            for (int i = 0; i < x.Length; i++)
            {
                Console.WriteLine(Node[i]);
            }
            //for (int i = 1; i < x.Length; i++)
            //{
            //    text += "Left:\n";
            //    text += " x[" + i + "] = " + x[i] + "\n S = " + (decimal)ChMeth.Cout_methods.Integral.RectRule.InitLeft(x1, x[i], ref Node) + "\n";
            //    text += " Node = " + Node + "\n";
            //    text += "Right:\n";
            //    text += " x[" + i + "] = " + x[i] + "\n S = " + ChMeth.Cout_methods.Integral.RectRule.InitRight(x1, x[i], ref Node) + "\n";
            //    text += " Node = " + Node + "\n";
            //    text += "Center:\n";
            //    text += " x[" + i + "] = " + x[i] + "\n S = " + ChMeth.Cout_methods.Integral.RectRule.InitCenter(x1, x[i], ref Node) + "\n";
            //    text += " Node = " + Node + "\n";

            //    text += "Trap:\n";
            //    text += " x[" + i + "] = " + x[i] + "\n S = " + ChMeth.Count_methods.Integral.TrapRule.InitTrap(x1, x[i], ref Node) + "\n";
            //    text += " Node = " + Node + "\n";
            //    text += "Simpson:\n";
            //    text += " x[" + i + "] = " + x[i] + "\n S = " + ChMeth.Count_methods.Integral.SimpRule.InitSimp(x1, x[i], ref Node) + "\n";
            //    text += " Node = " + Node + "\n";
            //    text += "Gauss:\n";
            //    text += " x[" + i + "] = " + x[i] + "\n S = " + ChMeth.Count_methods.Integral.GaussRule.InitGauss(x1, x[i], ref Node) + "\n";
            //    text += " Node = " + Node + "\n";
            //}
            #endregion

            FileOutPut(text);

            ChMeth.Functions.ReverseFunction a = new Functions.ReverseFunction(f, x, n, h);

            Console.WriteLine("\nYou can check result of your program in path: " + writePath);

            Console.ReadLine();
        }

        private static decimal Pow(int variable, int degree)
        {
            decimal p = 1;
            for (int i = 0; i < -degree; i++)
            {
                p /= variable;
            }
            return p;
        }

        private static decimal MaxEps(double[] f, double[] nf, int n)
        {
            Console.WriteLine("Looking for difference for eps...");
            text += " \n";
            decimal eps = 0;
            for(int i = 0; i < n; i++)
            {
                if (Math.Abs(f[i] - nf[i]) > (double)eps) eps = (decimal)Math.Abs(f[i] - nf[i]);
                text += Convert.ToString(f[i] - nf[i]);
            }
            text += "\n\nThe biggest eps equals to: " + eps.ToString() + " \n";
            return eps;
        }

        private static void FileOutPut(string text)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
    }
}