using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IS_Predidiction_and_store_optimize
{
    public partial class Import : Form
    {
        private string _errInputs = "Ошибка!!! Поля пусты или заполнены не верно";

        private string _dataX;
        private string _dataY;

        private List<double> _parsedDataY;
        private List<string> _parsedDataX;

        public Import()
        {
            InitializeComponent();

            _parsedDataY = new List<double>();
            _parsedDataX = new List<string>();
        }

        #region UI обработчики
        /*
         * UI ---> Кнопки
         */

        // Добавить 
        private void button1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show(_errInputs);
                return;
            }

            _dataX = textBox1.Text;
            _dataY = textBox2.Text;

            IsValidData();
        }

        #endregion

        #region Валидация

        private bool IsValidData()
        {
            string[] dataY = _dataY.Split(new char[] { '\n', ' ', ',' });
            string[] dataX = _dataX.Split(new char[] { '\n', ' ', ',' });

            try
            {
                foreach(string value in dataY)
                {
                    if (value == "")
                    {
                        continue;
                    }
                    _parsedDataY.Add(Double.Parse(value.Replace('\r', ' ').Trim()));
                }

                foreach (string value in dataX)
                {
                    if (value == "")
                    {
                        continue;
                    }
                    _parsedDataX.Add(value.Replace('\r', ' ').Trim());
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
    }
}
