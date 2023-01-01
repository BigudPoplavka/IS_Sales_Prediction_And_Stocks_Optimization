using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using IS_Predidiction_and_store_optimize.PredictionsMethods;


namespace IS_Predidiction_and_store_optimize
{
    public partial class Form1 : Form
    {
        protected StaticDefaultData defaultData;

        private string _newCategAdded = "Новая категория успешно добавлена";

        private PredictionMethod predictionMethod;

        public Form1()
        {
            InitializeComponent();

            defaultData = new StaticDefaultData();
            defaultData.InitializeDefaultData();

            ShowCategoriesTree(StaticDefaultData.defaultCategories);
            CheckPresets();
        }

        #region Инициализация калькуляторов

        public void InitializeCalculators()
        {

        }

        #endregion

        #region Инициализация UI и компонентов

        /*
         * Инициализация UI
         */

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

        }


        #endregion

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StartupMenu.instance.Show();
        }
    }
}
