using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_Predidiction_and_store_optimize.PredictionsMethods.SchreibfederSchemes
{
    public class SchreibfederModels: PredictionMethod
    {
        private const int _NOT_SEASONAL_W_COEFF_SUM = 10;

        protected List<double> coeffs;

        /// <summary>
        /// Общий алгоритм предсказания по средним значеням с коэффициентами Шрайбфедера
        /// </summary>
        /// <param name="monthConsumption">Список из пяти значений прошлых месяцев от целевого</param>
        /// <param name="targetMonthWorkDays">Число рабочих дней в цеелвом месяце</param>
        /// <param name="decimals">Точность (знаков после запятой)</param>
        /// <returns>Предсказанный результат продаж</returns>
        public override int PredictNextMonthValues(List<(double, int)> monthConsumption, int targetMonthWorkDays, int decimals)
        {
            if (monthConsumption.Count != coeffs.Count)
            {
                return -1;
            }

            List<double> dayConsumption = new List<double>();
            List<double> dayConsumptionPrediction = dayConsumption;

            foreach ((double, int) ValuesPair in monthConsumption)
            {
                dayConsumption.Add(Math.Round(ValuesPair.Item1 / ValuesPair.Item2, decimals));
            }

            for (int i = 0; i < coeffs.Count; i++)
            {
                dayConsumptionPrediction[i] = Math.Round(dayConsumptionPrediction[i] * coeffs[i], decimals);
            }

            double result = Math.Round(dayConsumptionPrediction.Sum(), decimals);
            result = Math.Round(result / coeffs.Sum(), decimals);

            if (coeffs.Sum() == _NOT_SEASONAL_W_COEFF_SUM)
            {
                result = Math.Round(result * targetMonthWorkDays, decimals);
            }

            return (int)result;
        }
    }

}
