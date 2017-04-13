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
        private static readonly char[] Delimiters = { ';', ',' };
        private static double[] _demand;

        public static bool ReadCsv()
        {
            string fileLocation = ("../../Resources/ForeCastingSwordData.csv");

            StreamReader streamReader = new StreamReader(fileLocation);
            int columnSize = 0;
            while (streamReader.ReadLine() != null) columnSize++;
            
            _demand = new double[columnSize];
            streamReader.Close();

            bool fileExists = File.Exists(fileLocation);

            if (!fileExists)
            {
                Console.WriteLine("NOPE");
                return false;
            }
            Console.WriteLine("YES");


            using (StreamReader streamreader = new StreamReader("../../Resources/ForeCastingSwordData.csv"))
            {
                string line;
                while ((line = streamreader.ReadLine()) != null)
                {
                    var fields = line.Split(Delimiters[1]);
                    //If the fields read is not empty
                    if (!fields[0].Equals("")) 
                       _demand[Convert.ToInt32(fields[0]) - 1] = Convert.ToDouble(fields[1]);
                }
            }
            Console.WriteLine(_demand[36]);
            Console.ReadLine();
            return true;
        }
    }
}
