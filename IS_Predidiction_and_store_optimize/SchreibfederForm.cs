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

namespace IS_Predidiction_and_store_optimize
{
    public partial class SchreibfederForm : Form
    {
        public SchreibfederForm()
        {
            InitializeComponent();
        }

        #region UI обработчики
        /*
         * UI ---> Кнопки
         */

        // Средняя взвешенная
        private void button2_Click(object sender, EventArgs e)
        {
            ModelForm MidWeightedForm = new ModelForm(new MidWeighted());
            MidWeightedForm.Show();
        }

        // Средняя взвешенная с усиленным влиянием последнего месяца
        private void button1_Click(object sender, EventArgs e)
        {
            ModelForm mwLastMonth = new ModelForm(new MidWeightedLastMonthForced());
            mwLastMonth.Show();
        }

        // Средняя взвешенная для монотонного зарактера спроса
        private void button3_Click(object sender, EventArgs e)
        {
            ModelForm mwLastMonth = new ModelForm(new MidWeightedMonotonus());
            mwLastMonth.Show();
        }

        // Средняя взвешенная сезонная
        private void button4_Click(object sender, EventArgs e)
        {
            ModelForm mwSeasonal = new ModelForm(new MidWeightedSeasonal());
            mwSeasonal.Show();
        }

        // Простая сезонная средняя
        private void button5_Click(object sender, EventArgs e)
        {
            ModelForm simpleSeasonalMid = new ModelForm(new SimpleSeasonalMid());
            simpleSeasonalMid.Show();
        }

        #endregion
    }
}
