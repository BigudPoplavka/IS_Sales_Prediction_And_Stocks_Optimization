using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_Predidiction_and_store_optimize.PredictionsMethods.SchreibfederSchemes
{
    public class SimpleSeasonalMid: SchreibfederModels
    {
        public SimpleSeasonalMid()
        {
            coeffs = new List<double>()
            { 1, 1, 1 };

            MenuText = "Простая сезонная средняя";
            Description = "Рассматриваются показатели за соответствующие " +
                "три месяца прошлого года(с учетом или без учета несезонного коэффициента тенденции)";
        }
    }
}
