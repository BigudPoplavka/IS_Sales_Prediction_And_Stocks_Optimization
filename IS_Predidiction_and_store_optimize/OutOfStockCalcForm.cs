using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IS_Predidiction_and_store_optimize.MetricsCalculators;
using System.Windows.Forms.DataVisualization.Charting;

namespace IS_Predidiction_and_store_optimize
{
    public partial class OutOfStockCalcForm : Form
    {
        private OutOfStockCalculator _outOfStockCalculator;

        private Chart _chart;

        private List<Color> _seriesColors;
        private List<Color> _pointsColors;

        private string _errInputs = "Ошибка! Неправильный или пустой ввод";
        private string _chartSKU = "Остальное кол-во";
        private string _chartOther = "Кол-во с нулевыми запасами";
        private string _title = "Сотношение";

        private int _SKUPositions;
        private int _allPositions;

        private double _result;

        public OutOfStockCalcForm()
        {
            InitializeComponent();

            _outOfStockCalculator = new OutOfStockCalculator();
            _chart = new Chart();

            _seriesColors = new List<Color>();
            _seriesColors.Add(Color.FromArgb(228, 65, 36));
            _seriesColors.Add(Color.FromArgb(36, 63, 240));

            _pointsColors = new List<Color>();
            _pointsColors.Add(Color.FromArgb(13, 237, 142));
            _pointsColors.Add(Color.FromArgb(232, 31, 121));

            SetFormData();
            InitChart();
        }

        #region Инициализация UI и компонентов

        private void OutOfStockCalcForm_Load(object sender, EventArgs e)
        {

        }

        private void SetFormData()
        {
            label2.Text = _outOfStockCalculator.CalcDescription.description;
            label3.Text = _outOfStockCalculator.CalcDescription.methodParams[0];
            label4.Text = _outOfStockCalculator.CalcDescription.methodParams[1];
        }

        private void InitChart()
        {
            _chart.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            _chart.ChartAreas.Add(_title);
            _chart.Series.Add(_title);
            _chart.Series[0].Points.Clear();
            _chart.Series[0].ChartType = SeriesChartType.Pie;

            _chart.Legends.Add(_chartSKU);
            _chart.Legends.Add(_chartOther);
            
            for(int i = 0; i < _seriesColors.Count; i++)
            {
                _chart.Legends[i].ForeColor = _seriesColors[i];
            }

            tableLayoutPanel1.Controls.Add(_chart, 0, 2);
        }

        private void SetResult()
        {
            label6.Text = _result.ToString();

            Dictionary<string, int> tags = new Dictionary<string, int>();

            tags.Add(_SKUPositions.ToString(), _SKUPositions);
            tags.Add((_allPositions - _SKUPositions).ToString(), _allPositions - _SKUPositions);

            _chart.Series[0].Points.Clear();

            foreach (string tagname in tags.Keys)
            {
                _chart.Series[0].Points.AddXY(tagname, tags[tagname]);
            }

            _chart.Series[0].Points[0].Color = _pointsColors[0];
            _chart.Series[0].Points[1].Color = _pointsColors[1];

            _chart.Show();
        }

        #endregion

        #region Валидация

        private bool IsValidInputs()
        {
            if(String.IsNullOrWhiteSpace(maskedTextBox1.Text) || String.IsNullOrWhiteSpace(maskedTextBox2.Text))
            {
                MessageBox.Show(_errInputs);
                return false;
            }

            try
            {
                _SKUPositions = int.Parse(maskedTextBox2.Text);
                _allPositions = int.Parse(maskedTextBox1.Text);
            }
            catch
            {
                MessageBox.Show(_errInputs);
                return false;
            }

            return true;
        }

        #endregion

        #region UI обработчики
        /*
         * UI ---> Кнопки
         */

        // Рассчитать
        private void button1_Click(object sender, EventArgs e)
        {
            if(IsValidInputs())
            {
                _result = _outOfStockCalculator.CalculateOutOfStock(_SKUPositions, _allPositions);
                SetResult();
            }  
        }

        #endregion
    }
}
