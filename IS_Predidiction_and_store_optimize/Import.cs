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
        private Form1 _form11;

        private string _errInputs = "Ошибка!!! Поля пусты или заполнены не верно";
        private string _errInputsLen = "Ошибка!!! Неравное количество параметров";

        private string _dataX;
        private string _dataY;

        private List<double> _parsedDataY;
        private List<string> _parsedDataX;

        public event Action onDataSaved = delegate { };

        public Import(Form1 form1)
        {
            InitializeComponent();

            _parsedDataY = new List<double>();
            _parsedDataX = new List<string>();

            _form11 = form1;
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

            if(IsValidData())
            {
                _form11.SetImportedData(GetDataFromImport());

                Close();
            }
        }

        #endregion

        #region Валидация

        private bool IsValidData()
        {
            string[] dataY = _dataY.Split(new char[] { '\n', ' ', ',' });
            string[] dataX = _dataX.Split(new char[] { '\n', ' ', ',' });

            if(dataX.Length != dataY.Length)
            {
                MessageBox.Show(_errInputsLen);
                return false;
            }

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

        public List<SaleDataRow> GetDataFromImport()
        {
            List<SaleDataRow> dataRows = new List<SaleDataRow>();

            for (int i = 0; i < _parsedDataY.Count; i++)
            {
                dataRows.Add(new SaleDataRow(_parsedDataY[i], _parsedDataX[i]));
            }

            return dataRows;
        }

        private void Import_FormClosing(object sender, FormClosingEventArgs e)
        {
            onDataSaved();
        }

        private void Import_Load(object sender, EventArgs e)
        {

        }
    }
}
