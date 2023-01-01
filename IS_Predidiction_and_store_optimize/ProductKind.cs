using System;
using System.Collections.Generic;
using System.Text;

namespace IS_Predidiction_and_store_optimize
{
    public abstract class ProductKind
    {
        protected string kindName;
        protected int kindSales;
        protected bool isUserType;

        protected virtual int SumTotalSales() 
        {
            return 0;
        }
    }
}
