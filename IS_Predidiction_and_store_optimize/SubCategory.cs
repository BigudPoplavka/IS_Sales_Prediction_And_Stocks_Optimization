namespace IS_Predidiction_and_store_optimize
{
    public class SubCategory : ProductKind, ICategory
    {
        public string CategoryName { get => kindName; set => kindName = value; }
        public int SumSales { get => kindSales; set => kindSales = value; }
        
        public SubCategory(string categoryName)
        {
            this.CategoryName = categoryName;
        }
        
        public void IncreaseCount(int count)
        {
            SumSales += count;
        }
    }
}