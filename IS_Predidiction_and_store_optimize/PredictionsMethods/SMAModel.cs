using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_Predidiction_and_store_optimize.PredictionsMethods
{
    public class SMAModel: PredictionMethod
    {
        private const int _MIN_T = 2;
        private const int _MIN_SALES_LEN = 2;

        public SMAModel() { }

        /// <summary>
        /// Расчёт по среднему (SMA), или простая скользящая средняя
        /// Прогноз(t+1) = (1/(T+1)) * [Продажи(t) + Продажи(t-1) + ... + Продажи(t-T)]
        /// </summary>
        /// <param name="salesList">Список значений продаж за период</param>
        /// <param name="T">Ширина окна Т, которая указывает, за какой период мы будем усреднять продажи</param>
        /// <returns>Массив пркдсказанных значений с выбранным окном</returns>
        public override List<double> PredictNextValues(List<double> salesList, int T)
        {
            if(salesList.Count < _MIN_SALES_LEN || T < _MIN_T)
            {
                return null;
            }

            List<double> prediction = new List<double>();

            double salesSum = salesList[0];

            for (int i = 1; i < T; i++)
            {
                salesSum += salesList[i];
            }

            salesSum /= T;

            prediction.Add(salesSum);

            
            for (int i = 1; i < salesList.Count - 1; i++)
            {
                salesSum = 0;

                for (int j = i; j < i + T; j++)
                {
                    salesSum += salesList[j];
                }

                salesSum /= T;
                prediction.Add(salesSum);

                if(prediction.Count == salesList.Count - T)
                {
                    break;
                }
            }

            return prediction;
        }

        
    }
}
