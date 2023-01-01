using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_Predidiction_and_store_optimize.PredictionsMethods
{
    public class SchreibfederModel: PredictionMethod
    {
        private List<double> _coeffMidWeighted;
        private List<double> _customCoeffMidWeight;

        public SchreibfederModel()
        {
            /* Рассмотрим более общую систему взвешивания для расчета прогноза
            спроса на несезонные товары, продающиеся и потребляемые довольно регулярно. 
            Эта система повышает важность последних периодов времени и
            охватывает диапазон, достаточный для исключения влияния временных
            «пиков»  -  Эффективное управление запасами - Д. Шрайбфедер */

            _coeffMidWeighted = new List<double>()
            { 
                3, 2.5, 2, 1.5, 1
            };

        }

        public List<double> PredictNextMonthValues(Dictionary<double, int> monthConsumption, List<double> coeffs)
        {
            List<double> dayConsumption = new List<double>();

            double sumDayConsumption = 0;
            double sumCoeff = coeffs.Sum();

            foreach(KeyValuePair<double, int> keyValuePair in monthConsumption)
            {
                dayConsumption.Add(keyValuePair.Key / keyValuePair.Value);
            }

            for(int i = 0; i < coeffs.Count; i++)
            {

            }



        }

    }
}
