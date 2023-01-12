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

        private string _formTitleStart = "Модели Шрайбфедера ";

        public PredictionMethod PredictionMethod { get => _schreibfederModel; }

        public Chart Chart { set => _chart = value; }

        public ModelForm(PredictionMethod model)
        {
            InitializeComponent();

            _schreibfederModel = model;

            SetFormValues();
        }

        # region Инициализация UI и компонентов
        private void ModelForm_Load(object sender, EventArgs e) { }

        public void InitChart(Chart chart)
        {
            _chart = chart;

            //GetPredictionChartData(chart);


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

        #endregion

        private void ModelForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
