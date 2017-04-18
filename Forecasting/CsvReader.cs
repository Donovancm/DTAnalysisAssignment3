using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting
{
    public static class CsvReader
    {

        public static double[] ReadCsv()
        {
            double[] demand;
            string fileLocation = ("../../Resources/ForeCastingSwordData.csv");

            StreamReader streamReader = new StreamReader(fileLocation);
            int columnSize = 0;
            while (streamReader.ReadLine() != null) columnSize++;
            
            demand = new double[columnSize];
            streamReader.Close();

            bool fileExists = File.Exists(fileLocation);

            if (!fileExists)
            {
                return null;
            }


            using (StreamReader streamreader = new StreamReader("../../Resources/ForeCastingSwordData.csv"))
            {
                string line;
                while ((line = streamreader.ReadLine()) != null)
                {
                    var fields = line.Split(',');
                    demand[Convert.ToInt32(fields[0]) - 1] = Convert.ToDouble(fields[1]);
                }
            }
            return demand;
        }
    }
}
