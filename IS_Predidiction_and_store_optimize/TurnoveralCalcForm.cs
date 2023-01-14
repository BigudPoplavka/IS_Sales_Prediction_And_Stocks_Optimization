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

namespace IS_Predidiction_and_store_optimize.MetricsCalculators
{
    public partial class TurnoveralCalcForm : Form
    {
        private TurnoveralCalculator _turnoveralCalculator;

        private string _errInputs = "Ошибка! Неправильный или пустой ввод";
        private string _errInputsCnt = "Ошибка! Введено неверное число значений";
        private string _errMixedInputs = "Ошибка! Введите или одно среднее или значения для рассчета среднего";

        private int _allYearSales;
        private int _midMonthSales;

        private double _result;

        public TurnoveralCalcForm()
        {
            InitializeComponent();

            _turnoveralCalculator = new TurnoveralCalculator();

            SetFormData();
        }

        private void SetFormData()
        {
            label2.Text = _turnoveralCalculator.CalcDescription.description;
            label3.Text = _turnoveralCalculator.CalcDescription.methodParams[0];
            label4.Text = _turnoveralCalculator.CalcDescription.methodParams[1];
        }

        private void SetResult()
        {
            label6.Text = _result.ToString();
        }

        private void TurnoveralCalcForm_Load(object sender, EventArgs e)
        {

        }

        #region Валидация

        private bool IsValidInputs()
        {
            if(!String.IsNullOrWhiteSpace(textBox1.Text) && !String.IsNullOrWhiteSpace(maskedTextBox2.Text))
            {
                MessageBox.Show(_errMixedInputs);
                return false;
            }

            if(String.IsNullOrWhiteSpace(textBox1.Text))
            {
                if (String.IsNullOrWhiteSpace(maskedTextBox1.Text) || String.IsNullOrWhiteSpace(maskedTextBox2.Text))
                {
                    MessageBox.Show(_errInputs);
                    return false;
                }

                try
                {
                    _midMonthSales = int.Parse(maskedTextBox2.Text);
                    _allYearSales = int.Parse(maskedTextBox1.Text);
                }
                catch
                {
                    MessageBox.Show(_errInputs);
                    return false;
                }
            }
            else
            {
                if (String.IsNullOrWhiteSpace(maskedTextBox1.Text) || String.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show(_errInputs);
                    return false;
                }

                try
                {
                    string[] dataMid = textBox1.Text.Split(new char[] { '\n', ' ', ',' });
                    List<int> midValues = new List<int>();

                    foreach (string value in dataMid)
                    {
                        if (value == "")
                        {
                            continue;
                        }
                        midValues.Add(int.Parse(value.Replace('\r', ' ').Trim()));
                    }

                    if (midValues.Count != 3 || midValues.Count != 6)
                    {
                        MessageBox.Show(_errInputsCnt);
                        return false;
                    }

                    _midMonthSales = midValues.Sum() / midValues.Count;
                    _allYearSales = int.Parse(maskedTextBox1.Text);
                }
                catch
                {
                    MessageBox.Show(_errInputs);
                    return false;
                }
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
            if (IsValidInputs())
            {
                _result = _turnoveralCalculator.CalculateTurnoveral(_allYearSales, _midMonthSales);
                SetResult();
            }
        }

        #endregion
    }
}
