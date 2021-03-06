﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forecasting
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var data = CsvReader.ReadCsv();
            var SES = Ses.ExecuteAlgorithm(data);
            var DES = Des.ExecuteAlgorithm(data);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(SES.Item1, SES.Item2, SES.Item3, SES.Item4, DES.Item1,DES.Item2, DES.Item3, DES.Item4, DES.Item5));
        }
    }
}
