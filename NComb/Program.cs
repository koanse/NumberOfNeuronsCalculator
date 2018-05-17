using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NComb
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
    public class Comb
    {
        public static double LogCEval(double m, double n)
        {
            if (m >= n)
                return m;
            if (m == 0)
                return 0;
            double x = m / n;
            double H = -x * Math.Log(x, 2) - (1 - x) * Math.Log(1 - x, 2);
            return n * H;
        }
        public static double LogK(double N, double N0, double[,] NL, double[] PV, double[] d, out double[,] mK)
        {
            double[] L = new double[NL.GetLength(0)];
            double sum;
            for (int i = 0; i < NL.GetLength(0); i++)
            {
                sum = 0;
                for (int j = 0; j < NL.GetLength(1); j++)
                    sum += NL[i, j];
                L[i] = sum;
            }
            sum = 0;
            for (int i = 0; i < L.Length; i++)
                sum += L[i];
            double[] n = new double[L.Length];
            double[] v = new double[PV.Length];
            for (int i = 0; i < n.Length; i++)
            {
                n[i] = L[i] / sum * N;
                v[i] = PV[i] * n[i];
            }
            double[,] nn = new double[NL.GetLength(0), NL.GetLength(1)];
            for (int i = 0; i < NL.GetLength(0); i++)
            {
                sum = 0;
                for (int j = 0; j < NL.GetLength(1); j++)
                    sum += NL[i, j];
                for (int j = 0; j < nn.GetLength(1); j++)
                    nn[i, j] = NL[i, j] / sum * n[i];
            }

            double K = 0;
            mK = new double[nn.GetLength(0), nn.GetLength(1)];
            for (int i = 0; i < nn.GetLength(0); i++)
            {
                for (int j = 0; j < nn.GetLength(1); j++)
                {
                    double x = 0, lognn;
                    if (nn[i, j] == 0)
                        lognn = 0;
                    else
                    {
                        lognn = Math.Log(nn[i, j], 2);
                        if (i > 0)
                            x = lognn + LogCEval(d[j], n[i - 1] + v[i - 1]);
                        else
                            x = lognn + LogCEval(d[j], N0);
                    }
                    K += x;
                    mK[i, j] = x;
                }
            }
            return K;
        }
    }
}