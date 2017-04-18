using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting
{
    public static class Des
    {
        public static Tuple<double[], double[], double, double, double> ExecuteAlgorithm(double[] demand)
        {
            double lowestError = -1;
            double bestAlpha = -1;
            double bestBeta = -1;
            Tuple<double[], double[]> response;
            double[] des;

            for (double i = 0.1; i <= 1; i += 0.1)
            {
                for (double j = 0.1; j <= 1; j += 0.1)
                {
                    response = DES(i, j, demand, Alpha.CalculateAlpha);
                    des = response.Item1;
                    Console.WriteLine(j);
                    var squaredError = SquaredError(des, demand);
                    Console.WriteLine(squaredError);
                    if (lowestError < 0 || squaredError < lowestError)
                    {
                        lowestError = squaredError;
                        bestAlpha = Math.Round(i, 3);
                        bestBeta = Math.Round(j, 3);
                    }
                }
            }
            response = DES(bestAlpha, bestBeta, demand, Alpha.CalculateAlpha);

            return new Tuple<double[], double[], double, double, double>(response.Item1, response.Item2, bestAlpha, bestBeta, lowestError);
        }

        private static Tuple<double[], double[]> DES(double alpha, double beta, double[] x, Func<double[], double> init)
        {
            double[] s = new double[x.Length];
            double[] b = new double[x.Length];
            double[] forecast = new double[x.Length + 1];
            
            s[1] = x[1];
            b[1] = x[1] - x[0];
            forecast[2] = s[1] + b[1];

            for (int t = 2; t < x.Length; t++)
            {
                var smoothing = alpha * x[t] + (1 - alpha) * (s[t - 1] + b[t - 1]);
                s[t] = smoothing;
                var estimate = beta * (s[t] - s[t - 1]) + (1 - beta) * b[t - 1];
                b[t] = estimate;

                forecast[t + 1] = s[t] + b[t];
            }
            
            return new Tuple<double[], double[]>(forecast, s);
        }
        private static double SquaredError(double[] des, double[] demand)
        {
            var squaredDistance = 0.0;

            for (int k = 2; k < demand.Length; k++)
            {
                squaredDistance += Math.Pow(des[k] - demand[k], 2);
            }
            Console.WriteLine(squaredDistance);
            var squardedDistanceAverage = squaredDistance / (demand.Length - 2);
            var squaredError = Math.Sqrt(squardedDistanceAverage);
            //Console.WriteLine(squaredError);
            return squaredError;
        }
    }
}
