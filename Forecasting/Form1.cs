using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Forecasting
{
    public partial class Form1 : Form
    {
        private double[] _demand;
        private double[] _ses;

        public Form1(double[] demand, double[] ses)
        {
            _demand = demand;
            _ses = ses;
            InitializeComponent();
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < _demand.Length; i++)
            {
                chart1.Series["Normal Data"].Points.AddXY(i, _demand[i]);
                chart1.Series["SES"].Points.AddXY(i, _ses[i]);
            }

            for (int j = 0; j < 10; j++)
            {
                chart1.Series["SES"].Points.AddXY(_demand.Length + j, _ses[_demand.Length - 1]);
            }

            chart1.Series["Normal Data"].ChartType = SeriesChartType.FastLine;
            chart1.Series["Normal Data"].Color = Color.Orange;

            chart1.Series["SES"].ChartType = SeriesChartType.FastLine;
            chart1.Series["SES"].Color = Color.Tomato;
        }
    }
}
