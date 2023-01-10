using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_Predidiction_and_store_optimize
{
    public class SaleDataRow
    {
        public string category;
        public string subcategory;
        public double sales;
        public string month;

        public SaleDataRow(double sales, string month)
        {
            this.sales = sales;
            this.month = month;
        }

        public SaleDataRow(string category, string subcategory, double sales, string month)
        {
            this.category = category;
            this.subcategory = subcategory;
            this.sales = sales;
            this.month = month;
        }
    }
}
