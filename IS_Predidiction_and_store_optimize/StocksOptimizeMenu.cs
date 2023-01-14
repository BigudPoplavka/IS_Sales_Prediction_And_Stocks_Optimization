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

namespace IS_Predidiction_and_store_optimize
{
    public partial class StocksOptimizeMenu : Form
    {
        private AvailableStocksLevelCalculator _availableStocksLevelCalculator;
        private TurnoveralCalculator _turnoveralCalculator;

        public StocksOptimizeMenu()
        {
            InitializeComponent();

            _availableStocksLevelCalculator = new AvailableStocksLevelCalculator();
            _turnoveralCalculator = new TurnoveralCalculator();
        }

        #region Инициализация UI и компонентов

        private void StocksOptimizeMenu_Load(object sender, EventArgs e)
        {
            
        }

        #endregion

        #region UI обработчики
        /*
         * UI ---> Кнопки
         */
        // Уровень наличия запасов / учет нехватки 
        private void button1_Click(object sender, EventArgs e)
        {
            OutOfStockCalcForm form = new OutOfStockCalcForm();
            form.Show();
        }

        // Отсутствие излишков запасов
        private void button2_Click(object sender, EventArgs e)
        {
            TurnoveralCalcForm form = new TurnoveralCalcForm();
            form.Show();
        }

        // Формулы средних запасов
        private void button3_Click(object sender, EventArgs e)
        {
            AvailableStockLevelForm form = new AvailableStockLevelForm();
            form.Show();
        }

        #endregion

        private void StocksOptimizeMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            StartupMenu.instance.Show();
        }
    }
}
