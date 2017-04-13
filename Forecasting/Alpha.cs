using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting
{
    public static class Alpha
    {
        public static double CalculateAlpha(double[] x)
        {
            var sum = 0.0;
            for (int i = 0; i < 12; i++)
            {
                sum = sum + x[i];
            }

            return sum / 12;
        }
    }
}
