using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IS_Predidiction_and_store_optimize
{
    public class StaticDefaultData
    {
        public static Category plourProducts;
        public static Category sugarProducts;
        public static Category chocolateProducts;

        public static List<Category> defaultCategories;

        public static string categoriesSavePath;
        public static string runFlag;

        public static Dictionary<string, string> savedCategories;

        public static bool IsSavedCategoriesExist { get => savedCategories.Keys.Count != 0; }

        private static List<SubCategory> _subPlourCategories;
        private static List<SubCategory> _subShugarCategories;
        private static List<SubCategory> _subChocolateCategories;

        public StaticDefaultData()
        { }

        public void InitializeDefaultData()
        {
            savedCategories = new Dictionary<string, string>();

            plourProducts = new Category("Мучные кондитерские изделия");
            sugarProducts = new Category("Сахаристые кондитерские изделия");
            chocolateProducts = new Category("Шоколадные кондитерские изделия");

            _subPlourCategories = new List<SubCategory>()
            {
                new SubCategory("Вафли и облатки вафельные"),
                new SubCategory("Кексы и рулеты"),
                new SubCategory("Печенье сладкое, в том числе имбирное"),
                new SubCategory("Печенье сухое (галеты и крекеры)"),
                new SubCategory("Пирожные"),
                new SubCategory("Пряники"),
                new SubCategory("Сладости восточные и мучные изделия недлительного хранения"),
                new SubCategory("Прочие мучные кондитерские изделия длительного хранения")
            };

            _subShugarCategories = new List<SubCategory>()
            {
                new SubCategory("Зефир и пастила"),
                new SubCategory("Ирис"),
                new SubCategory("Карамель"),
                new SubCategory("Конфеты, глазированные помадой, сахарной и жировой глазурью и неглазированные"),
                new SubCategory("Мармелад"),
                new SubCategory("Сладости восточные"),
                new SubCategory("Халва"),
                new SubCategory("Прочие сахаристые кондитерские изделия"),
            };

            _subChocolateCategories = new List<SubCategory>()
            {
                new SubCategory("Какао-порошок (с сахаром)"),
                new SubCategory("Конфеты шоколадные или глазированные шоколадной глазурью"),
                new SubCategory("Шоколад (кроме белого шоколада)"),
                new SubCategory("Шоколадная глазурь"),
                new SubCategory("Шоколадная паста"),
                new SubCategory("Прочие кондитерские изделия с какао")
            };

            plourProducts.SubCategories.AddRange(_subPlourCategories);
            sugarProducts.SubCategories.AddRange(_subShugarCategories);
            chocolateProducts.SubCategories.AddRange(_subChocolateCategories);

            defaultCategories = new List<Category>()
            {
                plourProducts,
                sugarProducts,
                chocolateProducts
            };

            categoriesSavePath = Directory.GetCurrentDirectory() + "\\Categories";
            runFlag = "RUN.dat";
        }

        public static string GetSerializedCategoryName(string path)
        {
            string res = string.Empty;

            for(int i = path.Length - 1; i > 0; i--)
            {
                if(path[i] == '\\')
                {
                    break;
                }

                res += path[i];
            }

            char[] charArray = res.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
