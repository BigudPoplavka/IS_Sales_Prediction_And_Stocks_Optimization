using System;
using System.Collections.Generic;
using System.Text;

namespace IS_Predidiction_and_store_optimize
{
    public class Category : ProductKind, ICategory
    {
 
        private List<SubCategory> _subCategories;

        public string CategoryName { get => kindName; set => kindName = value; }
        internal List<SubCategory> SubCategories { get => _subCategories; set => _subCategories = value; }
        public int SubCategoriesCount { get => _subCategories.Count; }
        public int SumSales { get => kindSales; set => kindSales = value; }
        public bool IsUserCategory { get => isUserType; set => isUserType = value; }

        public Category(string categoryName, bool isUserCategory = false)
        {
            kindName = categoryName;
            SumSales = 0;
            _subCategories = new List<SubCategory>();
            IsUserCategory = isUserCategory;
        }

        protected override int SumTotalSales()
        {
            foreach(SubCategory subCategory in _subCategories)
            {
                SumSales += subCategory.SumSales;
            }

            return SumSales;
        }

        public void SetSubcategoriesList(List<string> names) 
        {
            foreach (string name in names)
            {
                _subCategories.Add(new SubCategory(name));
            }
        }
    }
}
