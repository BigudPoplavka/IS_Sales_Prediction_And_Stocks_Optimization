using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_Predidiction_and_store_optimize.PredictionsMethods
{
    public class PredictionMethod
    {
        protected string menuText;
        protected string description;

        public virtual List<double> PredictNextValues(List<double> salesList, int T)
        {
            return null;
        }

        public virtual int PredictNextMonthValues(List<(double, int)> monthConsumption, int targetMonthWorkDays, int decimals)
        {
            return -1;
        }
    }
}
