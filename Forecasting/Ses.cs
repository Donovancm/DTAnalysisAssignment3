﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting
{
    public static class Ses
    {
        public static Tuple<double[],double[], double, double> ExecuteAlgorithm(double[] demand)
        {
            double lowestError = int.MaxValue;
            double bestAlpha = int.MaxValue;
            double[] ses = new double[0];

            for (double i = 0.01; i <= 1; i += 0.01)
            {
                ses = SES(i, demand, Alpha);
                //Console.WriteLine(i);
                var squaredError = SquaredError(ses, demand);
                //Console.WriteLine(squaredError);
                if (squaredError < lowestError)
                {
                    lowestError = squaredError;
                    bestAlpha = Math.Round(i, 3);
                }
            }

            ses = SES(bestAlpha, demand, Alpha);

            return new Tuple<double[], double[], double, double>(demand, ses, bestAlpha, lowestError);
        }

        private static double[] SES(double alpha, double[] x, Func<double[], double> init)
        {
            double[] s = new double[x.Length];
            s[0] = init(x);

            for (int t = 1; t < x.Length; t++)
            {
                s[t] = alpha * x[t - 1] + (1 - alpha) * s[t - 1];
            }

            return s;
        }

        private static double SquaredError(double[] ses, double[] demand)
        {
            var squaredDistance = 0.0;

            for (int i = 0; i < demand.Length; i++)
            {
                squaredDistance += Math.Pow(ses[i] - demand[i], 2);
            }
            var squaredDistanceAverage = squaredDistance / (demand.Length - 1);
            var squaredError = Math.Sqrt(squaredDistanceAverage);
            
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
