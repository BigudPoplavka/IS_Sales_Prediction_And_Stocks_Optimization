using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IS_Predidiction_and_store_optimize.MetricsCalculators
{
    public partial class AvailableStockLevelForm : Form
    {
        private AvailableStocksLevelCalculator _availableStocksLevelCalculator;

        private List<string> _twoParamsMetrics;

        private delegate double TwoParamsFunc(int paramOne, int paramTwo);

        private Dictionary<string, (TwoParamsFunc, ASLCalcDescription)> _funcDict;

        private string _twoParamsFuncSelected;
        private string _errNoSelect = "Ошибка! Не выбрана функция";
        private string _errInputs = "Ошибка! Неправильный или пустой ввод";

        private int _paramOne;
        private int _paramTwo;

        private double _indexParam1;
        private double _indexParam2;
        private double _indexParam3;
        private double _indexParam4;
        private double _resultOne;
        private double _resultTwo;
        private double _resultThree;

        private List<double> _parsedDatedValues;

        private List<MaskedTextBox> _maskedTextBoxes;

        public AvailableStockLevelForm()
        {
            InitializeComponent();

            _availableStocksLevelCalculator = new AvailableStocksLevelCalculator();

            _parsedDatedValues = new List<double>();

            _maskedTextBoxes = new List<MaskedTextBox>()
            {
                maskedTextBox1, maskedTextBox2, maskedTextBox5, maskedTextBox6
            };

            _funcDict = new Dictionary<string, (TwoParamsFunc, ASLCalcDescription)>() { };

            _funcDict.Add(_availableStocksLevelCalculator.descrMidLevel.title,
                (_availableStocksLevelCalculator.CalculateMidLevel, _availableStocksLevelCalculator.descrMidLevel));

            _funcDict.Add(_availableStocksLevelCalculator.descrAvailabilityInDay.title,
             (_availableStocksLevelCalculator.CalculateStockAvailabilityInDay, _availableStocksLevelCalculator.descrAvailabilityInDay));

            _funcDict.Add(_availableStocksLevelCalculator.descrMidDeficiteToMidStocks.title,
                (_availableStocksLevelCalculator.CalculateMidDeficiteToMidStocks, _availableStocksLevelCalculator.descrMidDeficiteToMidStocks));

            _funcDict.Add(_availableStocksLevelCalculator.descrStockRentabl.title,
                (_availableStocksLevelCalculator.CalculateStocksRentabl, _availableStocksLevelCalculator.descrStockRentabl));

            InitFormsData();
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        #region Валидация

        private bool IsValidInputs(int formIndex)
        {
            switch(formIndex)
            {
                case 1:

                    if(_twoParamsFuncSelected == null)
                    {
                        MessageBox.Show(_errNoSelect);
                        return false;
                    }

                    if(String.IsNullOrWhiteSpace(maskedTextBox3.Text) || String.IsNullOrWhiteSpace(maskedTextBox4.Text))
                    {
                        MessageBox.Show(_errInputs);
                        return false;
                    }

                    _paramOne = int.Parse(maskedTextBox3.Text);
                    _paramTwo = int.Parse(maskedTextBox4.Text);

                    return true;
                case 2:

                    if(String.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        MessageBox.Show(_errInputs);
                        return false;
                    }

                    string[] data = textBox1.Text.Split(new char[] { '\n', ' ', ',' });
                    _parsedDatedValues.Clear();

                    try
                    {
                        foreach (string value in data)
                        {
                            if (value == "")
                            {
                                continue;
                            }
                            _parsedDatedValues.Add(Double.Parse(value.Replace('\r', ' ').Trim()));
                        }

                        return true;
                    }
                    catch
                    {
                        MessageBox.Show(_errInputs);
                        return false;
                    }

                case 3:

                    if(_maskedTextBoxes.Any(x => String.IsNullOrWhiteSpace(x.Text)))
                    {
                        MessageBox.Show(_errInputs);
                        return false;
                    }

                    try
                    {
                        _indexParam1 = Double.Parse(maskedTextBox1.Text);
                        _indexParam2 = Double.Parse(maskedTextBox2.Text);
                        _indexParam3 = Double.Parse(maskedTextBox5.Text);
                        _indexParam4 = Double.Parse(maskedTextBox6.Text);
                    }
                    catch
                    {
                        MessageBox.Show(_errInputs);
                        return false;
                    }

                    return true;
            }

            return false;
        }

        #endregion

        #region Инициализация UI и компонентов

        private void InitFormsData()
        {
            foreach(string aSLCalcDescription in _funcDict.Keys)
            {
                comboBox1.Items.Add(aSLCalcDescription);
            }
        }

        private void SetResultData(int formIndex)
        {

        }

        #endregion

        #region UI обработчики
        /*
         * UI ---> Кнопки
         */

        // Рассчитать (Функции с двумя параметрами)
        private void button2_Click(object sender, EventArgs e)
        {
            if(IsValidInputs(1))
            {
                _resultOne = _funcDict[_twoParamsFuncSelected].Item1.Invoke(_paramOne, _paramTwo);
                label9.Text = _resultOne.ToString();
            }
        }

        // Рассчитать (средний запас по формуле средней хронологической)
        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValidInputs(2))
            {
                _resultTwo = _availableStocksLevelCalculator.CalculateMidChronological(_parsedDatedValues);
                label4.Text = _resultTwo.ToString();
            }
        }

        // Рассчитать (Индекс доходности запасов)
        private void button3_Click(object sender, EventArgs e)
        {
            if (IsValidInputs(3))
            {
                _resultThree = _availableStocksLevelCalculator.CalculateStonksIndex(_indexParam1, _indexParam2, _indexParam3, _indexParam4);
                label15.Text = _resultThree.ToString();
            }
        }

        #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _twoParamsFuncSelected = comboBox1.SelectedItem.ToString();

            label7.Text = _funcDict[_twoParamsFuncSelected].Item2.title;
            label8.Text = _funcDict[_twoParamsFuncSelected].Item2.paramsDesription[0];
            label6.Text = _funcDict[_twoParamsFuncSelected].Item2.paramsDesription[1];
        }
    }
}
