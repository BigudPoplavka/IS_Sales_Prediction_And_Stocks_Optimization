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
using IS_Predidiction_and_store_optimize.PredictionsMethods.SchreibfederSchemes;

namespace IS_Predidiction_and_store_optimize
{
    public partial class SchreibfederForm : Form
    {
        protected Chart chart;

        private List<double> _inputYValues;
        private List<string> _inputXValues;
        private List<(double, string)> _chartData;
        private List<SaleDataRow> _inputValues;

        private string _seriesSales = "Продажи";

        public SchreibfederForm(List<SaleDataRow> values)
        {
            InitializeComponent();

            _inputValues = values;

            InitInputsData();
            InitChart();
        }

        #region Инициализация UI и компонентов

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

            //GetPredictionChartData(chart);
        }

        #endregion

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

        private void GetPredictionChartData(Chart chart, PredictionMethod method)
        {

        }

        #region UI обработчики
        /*
         * UI ---> Кнопки
         */

        // Средняя взвешенная
        private void button2_Click(object sender, EventArgs e)
        {
            ModelForm MidWeightedForm = new ModelForm(new MidWeighted());
            MidWeightedForm.InitChart(chart);
            MidWeightedForm.ShowDialog();
        }

        // Средняя взвешенная с усиленным влиянием последнего месяца
        private void button1_Click(object sender, EventArgs e)
        {
            ModelForm mwLastMonth = new ModelForm(new MidWeightedLastMonthForced());
            mwLastMonth.InitChart(chart);
            mwLastMonth.ShowDialog();
        }

        // Средняя взвешенная для монотонного зарактера спроса
        private void button3_Click(object sender, EventArgs e)
        {
            ModelForm mwMonotonus = new ModelForm(new MidWeightedMonotonus());
            mwMonotonus.InitChart(chart);
            mwMonotonus.ShowDialog();
        }

        // Средняя взвешенная сезонная
        private void button4_Click(object sender, EventArgs e)
        {
            ModelForm mwSeasonal = new ModelForm(new MidWeightedSeasonal());
            mwSeasonal.InitChart(chart);
            mwSeasonal.ShowDialog();
        }

        // Простая сезонная средняя
        private void button5_Click(object sender, EventArgs e)
        {
            ModelForm simpleSeasonalMid = new ModelForm(new SimpleSeasonalMid());
            simpleSeasonalMid.InitChart(chart);
            simpleSeasonalMid.ShowDialog();
        }

        #endregion

        private void SchreibfederForm_Load(object sender, EventArgs e)
        {

        }
    }
}
