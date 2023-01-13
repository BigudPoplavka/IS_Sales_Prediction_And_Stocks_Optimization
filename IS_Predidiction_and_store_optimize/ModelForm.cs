using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IS_Predidiction_and_store_optimize.PredictionsMethods.SchreibfederSchemes;
using IS_Predidiction_and_store_optimize.PredictionsMethods;
using System.Windows.Forms.DataVisualization.Charting;

namespace IS_Predidiction_and_store_optimize
{
    public partial class ModelForm : Form
    {
        private PredictionMethod _schreibfederModel;

        private Chart _chart;

        private const int _MIN_TARGET_DAYS = 7;
        private const int _MAX_TARGET_DAYS = 31;
        private int _targetMonthDays;
        private int _predicted;

        private string _seriesSales = "Продажи";
        private string _title = "Прогноз";
        private string _formTitleStart = "Модели Шрайбфедера ";
        private string _errInputs = "Ошибка!!! Поля пусты или заполнены не верно";
        private string _errInputsLen = "Ошибка!!! Неравное количество параметров";
        private string _shcemeSituation = "Сезонная взвешенная средняя. Схема 1";
        private string _dataDays;

        private List<int> _parsedDataDays;
        private List<double> _parsedDataY;
        private List<string> _parsedDataX;
        private List<string> _monthList;

        public SchreibfederForm parentForm;

        public PredictionMethod PredictionMethod { get => _schreibfederModel; }

        public Chart Chart { set => _chart = value; }

        public ModelForm(PredictionMethod model, SchreibfederForm parent)
        {
            InitializeComponent();

            _schreibfederModel = model;

            parentForm = parent;

            _parsedDataDays = new List<int>();

            _monthList = new List<string>()
            {
                "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
            };

            SetFormValues();
        }

        # region Инициализация UI и компонентов
        private void ModelForm_Load(object sender, EventArgs e) { }

        public void InitChart(Chart chart, List<double> inputsY, List<string> inputsX)
        {
            _chart = chart;

            _parsedDataY = inputsY;
            _parsedDataX = inputsX;

            tableLayoutPanel1.Controls.Add(_chart, 1, 1);

            chart.Show();
        }

        private void SetFormValues()
        {
            _formTitleStart += _schreibfederModel.MenuText;
            Text = _formTitleStart;
            label1.Text = _schreibfederModel.MenuText;
            label2.Text = _schreibfederModel.Description;
        }

      
        private List<(double, int)> GetMonthDaysTuples()
        {
            List<(double, int)> tuplesList = new List<(double, int)>();

            try
            {
                for(int i = 0; i < _parsedDataY.Count; i++)
                {
                    tuplesList.Add((_parsedDataY[i], _parsedDataDays[i]));
                }
            }
            catch
            {
                MessageBox.Show(_errInputsLen);
                return null;
            }

            return tuplesList;
        }

        private string GetPredictedMonth(string lastMonth)
        {
            if(_schreibfederModel.MenuText == _shcemeSituation)
            {
                if(lastMonth == _monthList[0])
                {
                    return _monthList[_monthList.Count - 2];
                }

                if (lastMonth == _monthList[1])
                {
                    return _monthList[_monthList.Count - 1];
                }

                return _monthList[_monthList.IndexOf(lastMonth) - 2];
            }

            if(lastMonth == _monthList.Last())
            {
                return _monthList.First();
            }

            return _monthList[_monthList.IndexOf(lastMonth) + 1];
        }

        private void GetPredictionChartData(Chart chart)
        {
            chart.Legends.Add(_title);
            chart.Legends[_title].ForeColor = Color.FromArgb(245, 167, 51);
            chart.Series.Add(_title);
            chart.Series[_title].ChartType = SeriesChartType.Line;
            chart.Series[_title].BorderWidth = 3;

            var dataTuples = GetMonthDaysTuples();

            _predicted = _schreibfederModel.PredictNextMonthValues(dataTuples, _targetMonthDays, 2);

            if(_predicted == -1)
            {
                MessageBox.Show(_errInputsLen);
                return;
            }

            for (int i = 0; i < _parsedDataY.Count - 1; i++)
            {
                chart.Series[_title].Points.AddXY(_parsedDataX[i], _parsedDataY[i]);
            }

            chart.Series[_title].Points.AddXY(_parsedDataX[_parsedDataY.Count - 1], _parsedDataY.Last());

            label8.Text += GetPredictedMonth(_parsedDataX.Last());
            label9.Text = _predicted.ToString();

            chart.Series[_title].Points.AddXY(GetPredictedMonth(_parsedDataX.Last()), _predicted);

            chart.Show();
        }

        #endregion

        #region Валидация

        private bool IsValidData()
        {
            string[] dataDays = _dataDays.Split(new char[] { '\n', ' ', ',' });

            if(_targetMonthDays < _MIN_TARGET_DAYS || _targetMonthDays > _MAX_TARGET_DAYS)
            {
                return false;
            }

            try
            {
                foreach (string value in dataDays)
                {
                    if (value == "")
                    {
                        continue;
                    }
                    _parsedDataDays.Add(int.Parse(value.Replace('\r', ' ').Trim()));
                }

                if(_chart.Series[_seriesSales].Points.Count != _parsedDataDays.Count)
                {
                    return false;
                }

                return true;
            }
            catch
            {
                MessageBox.Show(_errInputs);
                return false;
            }
        }

        #endregion

        private void ModelForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            parentForm.Close();
        }

        #region UI обработчики
        /*
         * UI ---> Кнопки
         */

        // Рассчитать
        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrWhiteSpace(maskedTextBox1.Text))
            {
                MessageBox.Show(_errInputs);
                return;
            }

            _dataDays = textBox1.Text;
            _targetMonthDays = int.Parse(maskedTextBox1.Text);

            if (IsValidData())
            {
                GetPredictionChartData(_chart);
            }
        }

        #endregion
    }
}
