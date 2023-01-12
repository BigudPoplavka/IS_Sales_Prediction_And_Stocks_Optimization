using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_Predidiction_and_store_optimize.PredictionsMethods.SchreibfederSchemes
{
    public class MidWeightedMonotonus: SchreibfederModels
    {
        public MidWeightedMonotonus()
        {
            coeffs = new List<double>()
            { 3, 2, 1 };

            MenuText = "Монотонный рост или падение спроса";
            Description = "Если наблюдается существенный рост или снижение потребления товара, " +
                "то показателям, зафиксированным пять месяцев назад, не следует уделять " +
                "существенного внимания при прогнозировании спроса на грядущий отчетный период.";
        
        }
    }
}
