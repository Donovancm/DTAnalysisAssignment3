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
        private double[] _sesSmoothing;
        private double _sesAlpha;
        private double _sesError;
        private double[] _desForecast;
        private double[] _desSmoothing;
        private double _desAlpha;
        private double _desBeta;
        private double _desError;

        public Form1(double[] demand, double[] sesSmoothing, double sesBestAlpha, double sesBestError, double[] desForecast, double[] desSmoothing, double desBestAlpha, double desBestBeta, double desBestError)
        {
            _demand = demand;
            _sesSmoothing = sesSmoothing;
            _sesAlpha = sesBestAlpha;
            _sesError = sesBestError;
            _desForecast = desForecast;
            _desSmoothing = desSmoothing;
            _desAlpha = desBestAlpha;
            _desBeta = desBestBeta;
            _desError = desBestError;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < _demand.Length; i++)
            {
                chart1.Series["Normal Data"].Points.AddXY(i, _demand[i]);
                chart1.Series["SES"].Points.AddXY(i, _sesSmoothing[i]);
            }

            for (int k = 2; k < _demand.Length; k++)
            {
                chart1.Series["DES"].Points.AddXY(k, _desForecast[k]);
            }

            //Extension
            for (int j = 0; j < 12; j++)
            {
                chart1.Series["SES"].Points.AddXY(_demand.Length + j, _sesSmoothing[_demand.Length - 1]);
                chart1.Series["DES"].Points.AddXY(_demand.Length + j, _desSmoothing[_desSmoothing.Length - 1] + j * (_desSmoothing[_desSmoothing.Length - 1] - _desForecast[_demand.Length - 1]));
            }

            chart1.Series["Normal Data"].ChartType = SeriesChartType.FastLine;
            chart1.Series["Normal Data"].Color = Color.Red;
            chart1.Series["SES"].ChartType = SeriesChartType.FastLine;
            chart1.Series["SES"].Color = Color.Blue;
            chart1.Series["DES"].ChartType = SeriesChartType.FastLine;
            chart1.Series["DES"].Color = Color.Green;

            textBox1.AppendText("Best Alpha for SES: " + _sesAlpha + Environment.NewLine);
            textBox1.AppendText("Best Error for SES: " + _sesError + Environment.NewLine + Environment.NewLine);
            textBox1.AppendText("Best Alpha for DES: " + _desAlpha + Environment.NewLine);
            textBox1.AppendText("Best Beta for DES: " + _desBeta + Environment.NewLine);
            textBox1.AppendText("Best Error for DES: " + _desError);

        }
    }
}
