using System;
using System.Collections.Generic;
using System.Text;

namespace IS_Predidiction_and_store_optimize
{
    public interface ICategory
    {
        string CategoryName { get; set; }
        int SumSales { get; set; }
    }
}
