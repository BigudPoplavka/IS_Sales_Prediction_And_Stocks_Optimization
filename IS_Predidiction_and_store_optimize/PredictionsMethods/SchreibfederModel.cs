using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_Predidiction_and_store_optimize.PredictionsMethods
{
    public class SchreibfederModel: PredictionMethod
    {
        private const int _NOT_SEASONAL_W_COEFF_SUM = 10;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="monthConsumption"></param>
        /// <param name="targetMonthWorkDays"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public int PredictNextMonthValuesNotSeasonal(List<(double, int)> monthConsumption, int targetMonthWorkDays, int decimals)
        {
            if(monthConsumption.Count != _coeffMidWeighted.Count)
            {
                return -1;
            }

            List<double> dayConsumption = new List<double>();
            List<double> dayConsumptionPrediction = dayConsumption;

            foreach ((double, int) ValuesPair in monthConsumption)
            {
                dayConsumption.Add(Math.Round(ValuesPair.Item1 / ValuesPair.Item2, decimals));
            }

            for (int i = 0; i < _coeffMidWeighted.Count; i++)
            {
                dayConsumptionPrediction[i] = Math.Round(dayConsumptionPrediction[i] * _coeffMidWeighted[i], decimals);
            }

            double result = Math.Round(dayConsumptionPrediction.Sum(), decimals);
            result = Math.Round(result / _NOT_SEASONAL_W_COEFF_SUM, decimals);
            result = Math.Round(result * targetMonthWorkDays, decimals);

            return (int)result;
        }


    }
}
