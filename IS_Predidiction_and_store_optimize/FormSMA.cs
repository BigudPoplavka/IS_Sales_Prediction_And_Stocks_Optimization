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
using IS_Predidiction_and_store_optimize.PredictionsMethods;

namespace IS_Predidiction_and_store_optimize
{
    public partial class FormSMA : Form
    {
        private string _seriesSales = "Продажи";
        private string _seriesPredict = "Прогноз при T = ";
        private string _errAdditionLegend = "Ошибка!!! График уже существует либо произошла непредвиденная ошибка";

        private List<double> test4 = new List<double>()
            {
                15, 40, 40, 30, 5, 30, 15, 50, 5, 34, 20, 15, 40, 30, 15, 30, 20
            };

        private Stack<Color> _seriesColors;

        private List<SaleDataRow> _inputValues;
        private List<double> _inputYValues;
        private List<string> _inputXValues;
        private List<(double, string)> _chartData;

        private PredictionMethod _predictionMethod;

        protected Chart chart;

        public FormSMA(List<SaleDataRow> values, PredictionMethod predictionMethod)
        {
            InitializeComponent();

            _inputValues = values;
            _predictionMethod = predictionMethod;

            _seriesColors = new Stack<Color>();
            _seriesColors.Push(Color.FromArgb(245, 167, 51));
            _seriesColors.Push(Color.FromArgb(77, 227, 76));
            _seriesColors.Push(Color.FromArgb(228, 241, 36));
            _seriesColors.Push(Color.FromArgb(36, 63, 240));
            _seriesColors.Push(Color.FromArgb(240, 36, 223));

            InitInputsData();
            InitChart();
        }

        #region Загрузка данных

        private void InitInputsData()
        {
            _chartData = new List<(double, string)>();
            _inputYValues = new List<double>();
            _inputXValues = new List<string>();

            foreach (SaleDataRow dataRow in _inputValues)
            {
                _chartData.Add((dataRow.sales, dataRow.month));
                _inputYValues.Add(dataRow.sales);
                _inputXValues.Add(dataRow.month);
            }
        }

        #endregion

        #region Инициализация UI и компонентов

        private void GetPredictionChartData(Chart chart, int T)
        {
            var title = _seriesPredict + T.ToString();

            try
            {
                chart.Legends.Add(title);
            }
            catch
            {
                MessageBox.Show(_errAdditionLegend);
                return;
            }

            List<double> predicts = _predictionMethod.PredictNextValues(_inputYValues, T);

            if (predicts == null)
            {
                return;
            }

            chart.Legends[title].ForeColor = _seriesColors.Pop();
            chart.Series.Add(title);
            chart.Series[title].ChartType = SeriesChartType.Line;
            chart.Series[title].BorderWidth = 3;

            // 95-100

            for (int i = T; i < _chartData.Count; i++)
            {
                _inputXValues.Add(_chartData[i].Item2);
            }

            for(int i = 0; i < T; i++)
            {
                chart.Series[title].Points.AddY(0);
            }

            foreach (double value in predicts)
            {
                chart.Series[title].Points.AddY(value);
            }
        }

        private void InitChart()
        {
            chart = new Chart();
            chart.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;

            ChartArea salesChart = new ChartArea(_seriesSales);

            chart.ChartAreas.Add(salesChart);
            chart.Series.Add(_seriesSales);
            chart.Series[_seriesSales].ChartType = SeriesChartType.Line;
            chart.Series[_seriesSales].BorderWidth = 3;

            chart.Legends.Add(_seriesSales);

            foreach ((double, string) pair in _chartData)
            {
                chart.Series[_seriesSales].Points.AddXY(pair.Item2, pair.Item1);
            }

            GetPredictionChartData(chart, 2);

            tableLayoutPanel1.Controls.Add(chart, 1, 0);

            chart.Show();
        }

        private void FormSMA_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region UI обработчики
        /*
         * UI ---> Кнопки
         */

        // Добавить 
        private void button1_Click(object sender, EventArgs e)
        {
            if(_seriesColors.Count != 0)
            {
                GetPredictionChartData(chart, (int)numericUpDown1.Value);
            }
        }

        #endregion
    }
}
