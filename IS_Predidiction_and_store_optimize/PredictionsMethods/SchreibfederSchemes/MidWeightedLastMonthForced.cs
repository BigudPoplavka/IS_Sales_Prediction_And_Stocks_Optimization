using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_Predidiction_and_store_optimize.PredictionsMethods.SchreibfederSchemes
{
    public class MidWeightedLastMonthForced: SchreibfederModels
    {
        public MidWeightedLastMonthForced()
        {
            coeffs = new List<double>()
            { 5, 2, 1 };

            menuText = "Взвешенная средняя. Схема 2";
            description = "Усиливается значение предыдущего месяца " +
                "(с учетом или без учета несезонного коэффициента тенденции)";

        }
    }
}
