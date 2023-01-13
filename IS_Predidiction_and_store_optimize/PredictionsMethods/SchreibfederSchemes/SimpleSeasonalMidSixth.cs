using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_Predidiction_and_store_optimize.PredictionsMethods.SchreibfederSchemes
{
    class SimpleSeasonalMidSixth: SchreibfederModels
    {
        public SimpleSeasonalMidSixth()
        {   
            coeffs = new List<double>()
            { 1, 1, 1, 1, 1, 1 };

            MenuText = "Простая шестимесячная средняя";
            Description = "Рассматриваются показатели за соответствующие " +
                "шесть месяцев прошлого года (с учетом или без учета несезонного коэффициента тенденции)";
        }
    }
}
