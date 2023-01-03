using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_Predidiction_and_store_optimize.PredictionsMethods.SchreibfederSchemes
{
    public class MidWeightedSeasonal: SchreibfederModels
    {
        public MidWeightedSeasonal()
        {
            coeffs = new List<double>()
            { 1, 2 };

            menuText = "Сезонная взвешенная средняя. Схема 1";
            description = "Средняя взвешенная прошлогодних показателей двух месяцев после текущего " +
                "(с учетом или без учета несезонного коэффициента тенденции)";
        }
    }
}
