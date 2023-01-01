using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Text.Json;
using System.Linq;

namespace IS_Predidiction_and_store_optimize
{
    public partial class AddCategory : Form
    {
        private List<TextBox> _inputTextBoxes;
        private List<string> _savedSubCategories;

        private string _emptyNamesErr = "ОШИБКА!!! Название категории или подкатегорий пустые";
        private string _emptySubnameErr = "ОШИБКА!!! Не вписана ни одна подкатегория";
        private string _categotyExistsErr = "ОШИБКА!!! Такая категория уже существует";

        private bool _isAddedACtegoryEquDefaults;

        public event Action<Category, bool> onCategoryAdded = delegate { };

        public AddCategory(Form1 parent)
        {
            InitializeComponent();

            SetInputsList();
        }

        #region Инициализация UI и компонентов

        /*
         * Инициализация и обновление UI
         */

        private void SetInputsList()
        {
            _inputTextBoxes = new List<TextBox>()
            {
                textBox2, textBox3, textBox4, textBox5,
                textBox6, textBox7, textBox8, textBox9
            };

            _savedSubCategories = new List<string>();
        }

        #endregion

        #region Валидация

        /*
         * Валидация
         */

        private bool IsValidSubCategories()
        {
            if (_inputTextBoxes.TrueForAll(input => string.IsNullOrWhiteSpace(input.Text)))
            {
                MessageBox.Show(_emptySubnameErr);
                return false;
            }
            return true;
        }

        #endregion

        #region Сохранение и загрузка

        /*
         * Сохранение и загрузка 
         */

        private void SaveNewCategory(Category category)
        {
            _isAddedACtegoryEquDefaults = StaticDefaultData.defaultCategories.Any(x => x.CategoryName == category.CategoryName);
            var newDirPath = StaticDefaultData.categoriesSavePath + $"\\{category.CategoryName}";
            var newCategoryPath = newDirPath + $"\\SubCategories.dat";

            StaticDefaultData.savedCategories.Add(newDirPath, newCategoryPath);

            if (Directory.Exists(category.CategoryName) || _isAddedACtegoryEquDefaults)
            {
                MessageBox.Show(_categotyExistsErr);
                return;
            }

            Directory.CreateDirectory(newDirPath);
            File.Create(newCategoryPath).Close();

            using (StreamWriter writer = new StreamWriter(newCategoryPath))
            {
                foreach (SubCategory subCategory in category.SubCategories)
                {
                    writer.WriteLine(subCategory.CategoryName);
                }
            }
        }

        #endregion

        #region UI и обработчики

        /*
         * UI ---> Кнопки
         */

        // Добавить еще подкатегории
        private void button2_Click(object sender, EventArgs e)
        {
            if (!IsValidSubCategories())
            {
                return;
            }

            foreach (TextBox textBox in _inputTextBoxes)
            {
                if(!string.IsNullOrWhiteSpace(textBox.Text))
                {
                    _savedSubCategories.Add(textBox.Text);
                    textBox.Text = string.Empty;
                }
            }
        }

        // Добавить категорию
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || !IsValidSubCategories())
            {
                MessageBox.Show(_emptyNamesErr);
                return;
            }

            foreach (TextBox textBox in _inputTextBoxes)
            {
                if (!string.IsNullOrWhiteSpace(textBox.Text))
                {
                    _savedSubCategories.Add(textBox.Text);
                }
            }

            Category newCategory = new Category(textBox1.Text, true);
            newCategory.SetSubcategoriesList(_savedSubCategories);

            SaveNewCategory(newCategory);

            onCategoryAdded(newCategory, true);

            Close();
        }

        #endregion

        private void AddCategory_Load(object sender, EventArgs e)
        {

        }
    }
}
