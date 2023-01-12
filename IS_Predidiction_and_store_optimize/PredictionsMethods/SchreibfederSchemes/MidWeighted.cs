using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_Predidiction_and_store_optimize.PredictionsMethods.SchreibfederSchemes
{
    public class MidWeighted: SchreibfederModels
    {
        public MidWeighted()
        {
             /* Рассмотрим более общую систему взвешивания для расчета прогноза
            спроса на несезонные товары, продающиеся и потребляемые довольно регулярно. 
            Эта система повышает важность последних периодов времени и
            охватывает диапазон, достаточный для исключения влияния временных
            «пиков»  -  Эффективное управление запасами - Д. Шрайбфедер 
               ||
              _||_
              \  /
               \/
            Взвешенная средняя, схема 1. Присваиваемые нескольким прошлым месяцам веса уменьшаются */
            coeffs = new List<double>()
                { 3, 2.5, 2, 1.5, 1 };

            MenuText = "Средняя взвешенная. Схема 1";
            Description = "Присваиваемые нескольким прошлым месяцам веса уменьшаются " +
                "Рассмотрим более общую систему взвешивания для расчета прогноза " +
                "спроса на несезонные товары, продающиеся и потребляемые довольно регулярно." + 
                "Эта система повышает важность последних периодов времени и " +
                "охватывает диапазон, достаточный для исключения влияния временных «пиков»";
        }

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
            result = Math.Round(result * targetMonthWorkDays, decimals);
            
            return (int)result;
        }
    }
}
