using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using IS_Predidiction_and_store_optimize.PredictionsMethods;
using IS_Predidiction_and_store_optimize.PredictionsMethods.SchreibfederSchemes;


namespace IS_Predidiction_and_store_optimize
{
    public enum Periods
    {
        Дни, Недели, Месяцы, Годы
    }

    public enum Month
    {
        Январь, Февраль, Март, Апрель, Май, Июнь, Июль, Август, Сентябрь, Откябрь, Ноябрь, Декабрь
    }

    public partial class Form1 : Form
    {
        public static Form1 instance;

        private List<SaleDataRow> _importedData;

        protected StaticDefaultData defaultData;

        private string _newCategAdded = "Новая категория успешно добавлена";
        private string _imortedDataSaved = "Данные успешно импортированы";
        private string _errInputs = "Ошибка!!! Поля пусты или заполнены не верно";
        private string _errDelEmpty = "Ошибка!!! Записи отсутствуют";
        private string _sureDelAll = "Уверены, что хотите очистить все?";
        private string _sureDelTitle = "Очистить все?";

        private PredictionMethod predictionMethod;

        public Form1()
        {
            InitializeComponent();

            if(instance == null)
            {
                instance = this;
            }

            List<(double, int)> test = new List<(double, int)>()
            {
                (256, 20),
                (228, 19),
                (171, 18)
            };

            List<(double, int)> test2 = new List<(double, int)>()
            {
                (166, 18),
                (152, 18),
                (160, 21),
                (106, 21),
                (178, 22)
            };

            List<double> test4 = new List<double>()
            {
                15, 40, 40, 30, 5, 30, 15, 50, 5, 34, 20, 15, 40, 30, 15, 30, 20
            };

            //schreibfederModel = new MidWeightedMonotonus();
            //predictionMethod = schreibfederModel;
            //predictionMethod.PredictNextMonthValues(test, 21, 1);

            //predictionMethod = new SMAModel();
            //predictionMethod.PredictNextValues(test4, 2);
            //predictionMethod.PredictNextValues(test4, 3);
            //predictionMethod.PredictNextValues(test4, 4);
            //predictionMethod.PredictNextValues(test4, 10);

            defaultData = new StaticDefaultData();
            defaultData.InitializeDefaultData();

            ShowCategoriesTree(StaticDefaultData.defaultCategories);
            CheckPresets();

            InitilizeControlsData();
        }

        #region Передача данных между формами

        public List<SaleDataRow> GetDataFromGridView()
        {
            List<SaleDataRow> dataRows = new List<SaleDataRow>();

            if(checkBox1.Checked)
            {
                for(int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    dataRows.Add(new SaleDataRow(
                            dataGridView1.Rows[i].Cells[0].Value.ToString(), dataGridView1.Rows[i].Cells[1].Value.ToString(),
                            Double.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString()), dataGridView1.Rows[i].Cells[3].Value.ToString()));
                }
            }
            else
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    dataRows.Add(new SaleDataRow(Double.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString()), dataGridView1.Rows[i].Cells[3].Value.ToString()));
                }
            }

            return dataRows;
        }

        public void SetImportedData(List<SaleDataRow> data)
        {
            _importedData = data;
        }

        #endregion

        #region Инициализация UI и компонентов

        /*
         * Инициализация UI
         */

        private void InitilizeControlsData()
        {
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;

            checkBox2.Enabled = false;

            comboBox1.Items.AddRange(new object[] 
            { 
                StaticDefaultData.plourProducts.CategoryName, StaticDefaultData.chocolateProducts.CategoryName, StaticDefaultData.sugarProducts.CategoryName
            });
            comboBox3.Items.AddRange(Enum.GetNames<Month>());
        }

        private void ShowCategoriesTree(List<Category> categories)
        {
            List<TreeNode> treeNodes = new List<TreeNode>();

            foreach(Category category in categories)
            {             
                treeNodes.Add(new TreeNode(category.CategoryName, GetNodesFromCategory(category)));
            }

            treeView1.Nodes.AddRange(treeNodes.ToArray());

            treeView1.Show();
            treeView1.ExpandAll();
        }

        private TreeNode[] GetNodesFromCategory(Category category)
        {
            List<TreeNode> treeChildNodes = new List<TreeNode>();

            foreach (SubCategory subCategory in category.SubCategories)
            {
                treeChildNodes.Add(new TreeNode(subCategory.CategoryName));
            }

            return treeChildNodes.ToArray();
        }

        private void UpdateCategoriesTree(Category category, bool showMessage)
        {
            TreeNode newNode = new TreeNode(category.CategoryName, GetNodesFromCategory(category));

            treeView1.Nodes.Add(newNode);

            if(showMessage)
            {
                MessageBox.Show(_newCategAdded);
            }

            newNode.Expand();
        }

        #endregion

        #region Сохранение и загрузка

        /*
         * Сохранение и загрузка
         */

        private void CheckPresets()
        {
            if (!File.Exists(StaticDefaultData.runFlag))
            {
                CreateStartArtifacts();
            }
            else
            {
                LoadCategoriesPresets();
            }
        }

        private void CreateStartArtifacts()
        {
            File.Create(StaticDefaultData.runFlag);
            Directory.CreateDirectory(StaticDefaultData.categoriesSavePath);
        }

        private void LoadCategoriesPresets()
        {
            string[] serialisedCategories = Directory.GetDirectories(StaticDefaultData.categoriesSavePath);

            if (serialisedCategories.Length != 0)
            {
                foreach(string categoryName in serialisedCategories)
                {
                    StaticDefaultData.savedCategories.Add(
                        categoryName, StaticDefaultData.categoriesSavePath 
                        + $"\\{StaticDefaultData.GetSerializedCategoryName(categoryName)}" + $"\\SubCategories.dat");
                }

                List<string> subCategoriesNames = new List<string>();
                var name = string.Empty;

                foreach (KeyValuePair<string, string> pair in StaticDefaultData.savedCategories)
                {
                    subCategoriesNames.Clear();

                    using (StreamReader reader = new StreamReader(pair.Value))
                    {
                        while (!reader.EndOfStream)
                        {
                            subCategoriesNames.Add(reader.ReadLine());
                        }
                    }

                    name = StaticDefaultData.GetSerializedCategoryName(pair.Key);

                    Category newCategory = new Category(name, true);
                    newCategory.SetSubcategoriesList(subCategoriesNames);

                    UpdateCategoriesTree(newCategory, false);
                }
            }
        }

        //сообщение о сохранении импорта
        public void UpdateSavedData()
        {
            MessageBox.Show(_imortedDataSaved);
        }

        #endregion

        #region Инициализация UI и компонентов

        /*
         * Инициализация и обновление UI
         */
        private void Form1_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        #endregion

        private List<SaleDataRow> GetSavedData()
        {
            List<SaleDataRow> savedData = null;

            if (_importedData == null)
            {
                savedData = GetDataFromGridView();
            }
            else
            {
                savedData = _importedData;
            }

            return savedData;
        }

        #region UI обработчики
        /*
         * UI ---> Кнопки
         */

        // Добавить раздел
        private void button1_Click(object sender, EventArgs e)
        {
            AddCategory addCategoryForm = new AddCategory(this);
            addCategoryForm.onCategoryAdded += UpdateCategoriesTree;
            addCategoryForm.ShowDialog();
        }

        // Импорт данных из таблиц
        private void button2_Click(object sender, EventArgs e)
        {
            Import import = new Import(instance);
            import.onDataSaved += UpdateSavedData;
            import.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
             StartupMenu.instance.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = checkBox1.Checked;

            checkBox2.Enabled = checkBox1.Checked;
            comboBox2.Enabled = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
           if(checkBox2.Enabled)
           {
                comboBox2.Enabled = checkBox2.Checked;
           }
        }

        // SMA
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 1 && _importedData == null)
            {
                MessageBox.Show(_errInputs);
                return;
            }

            List<SaleDataRow> savedData = GetSavedData();

            predictionMethod = new SMAModel();

            FormSMA form = new FormSMA(savedData, predictionMethod);
            form.Show();
        }

        // Шрайбфедер
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 1 && _importedData == null)
            {
                MessageBox.Show(_errInputs);
                return;
            }

            List<SaleDataRow> savedData = GetSavedData();

            SchreibfederForm schreibfederForm = new SchreibfederForm(savedData);
            schreibfederForm.ShowDialog();
        }

        // Добавить
        private void button6_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(maskedTextBox1.Text) || comboBox3.SelectedItem == null)
            {
                MessageBox.Show(_errInputs);
                return;
            }

            var curr = dataGridView1.Rows.Add();

            dataGridView1.Rows[curr].Cells[0].Value= comboBox1.SelectedItem;
            dataGridView1.Rows[curr].Cells[1].Value= comboBox2.SelectedItem;
            dataGridView1.Rows[curr].Cells[2].Value= maskedTextBox1.Text;
            dataGridView1.Rows[curr].Cells[3].Value= comboBox3.SelectedItem;
        }

        // Удалить последнее
        private void button7_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count == 1)
            {
                MessageBox.Show(_errDelEmpty);
                return;
            }

            dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 2);
        }

        // Удалить все
        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 1)
            {
                MessageBox.Show(_errDelEmpty);
                return;
            }

            if (MessageBox.Show(_sureDelAll, _sureDelTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dataGridView1.Rows.Clear();
            }
        }

        #endregion
    }
}
