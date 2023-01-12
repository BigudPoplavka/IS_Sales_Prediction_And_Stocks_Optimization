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

namespace IS_Predidiction_and_store_optimize
{
    public partial class ModelForm : Form
    {
        private PredictionMethod _schreibfederModel;

        private string _formTitleStart = "Модели Шрайбфедера ";

        public ModelForm(PredictionMethod model)
        {
            InitializeComponent();

            _schreibfederModel = model;

            SetFormValues();
        }

        # region Инициализация UI и компонентов
        private void ModelForm_Load(object sender, EventArgs e) { }

        private void SetFormValues()
        {
            _formTitleStart += _schreibfederModel.MenuText;
            Text = _formTitleStart;
            label1.Text = _schreibfederModel.MenuText;
            label2.Text = _schreibfederModel.Description;
        }

        # endregion
    }
}
