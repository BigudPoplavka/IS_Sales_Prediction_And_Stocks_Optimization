﻿using System;
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
    public partial class StartupMenu : Form
    {
        public static StartupMenu instance;
        public StartupMenu()
        {
            if (instance == null)
            {
                instance = this;
            }

            InitializeComponent();
        }


        #region UI обработчики
        /*
         * UI ---> Кнопки
         */

        // Вызов окна -> Предсказание спроса
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 predictionMenu = new Form1();
            predictionMenu.Show();
            Hide();
        }

        // Вызов окна -> Оптимизация запаслв
        private void button2_Click(object sender, EventArgs e)
        {
            StocksOptimizeMenu stocksOptimizeMenu = new StocksOptimizeMenu();
            stocksOptimizeMenu.Show();
            Hide();
        }

        #endregion

        #region Инициализация UI и компонентов

        /*
         * Инициализация UI
         */
        private void StartupMenu_Load(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
