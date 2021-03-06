﻿using System;
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
            double lowestError = int.MaxValue;
            double bestAlpha = int.MaxValue;
            double bestBeta = int.MaxValue;
            Tuple<double[], double[]> response;
            double[] des;

            for (double i = 0.01; i <= 1; i += 0.01)
            {
                for (double j = 0.01; j <= 1; j += 0.01)
                {
                    response = DES(i, j, demand, Alpha);
                    des = response.Item1;
                    //Console.WriteLine(j);
                    var squaredError = SquaredError(des, demand);
                    //Console.WriteLine(squaredError);
                    if (squaredError < lowestError)
                    {
                        lowestError = squaredError;
                        bestAlpha = Math.Round(i, 3);
                        bestBeta = Math.Round(j, 3);
                    }
                }
            }
            response = DES(bestAlpha, bestBeta, demand, Alpha);

            return new Tuple<double[], double[], double, double, double>(response.Item1, response.Item2, bestAlpha, bestBeta, lowestError);
        }

        private static Tuple<double[], double[]> DES(double alpha, double beta, double[] x, Func<double[], double> init)
        {
            double[] s = new double[x.Length];
            double[] b = new double[x.Length];
            double[] f = new double[x.Length + 1];
            
            s[1] = x[1];
            b[1] = x[1] - x[0];
            f[2] = s[1] + b[1];

            for (int t = 2; t < x.Length; t++)
            {
                s[t] = alpha * x[t] + (1 - alpha) * (s[t - 1] + b[t - 1]);
                b[t] = beta * (s[t] - s[t - 1]) + (1 - beta) * b[t - 1];

                f[t + 1] = s[t] + b[t];
            }
            
            return new Tuple<double[], double[]>(f, s);
        }
        private static double SquaredError(double[] des, double[] demand)
        {
            var squaredDistance = 0.0;

            for (int k = 2; k < demand.Length; k++)
            {
                squaredDistance += Math.Pow(des[k] - demand[k], 2);
            }

            var squardedDistanceAverage = squaredDistance / (demand.Length - 2);
            var squaredError = Math.Sqrt(squardedDistanceAverage);

            return squaredError;
        }

        private static double Alpha(double[] x)
        {
            double sum = 0.0;
            for (int i = 0; i < 12; i++)
            {
                sum += x[i];
            }

            return sum / 12;
        }
    }
}
