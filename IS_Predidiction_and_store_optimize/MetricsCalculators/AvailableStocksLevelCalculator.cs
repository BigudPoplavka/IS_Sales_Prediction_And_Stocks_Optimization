using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_Predidiction_and_store_optimize.MetricsCalculators
{
    public class AvailableStocksLevelCalculator: StockStatsCalculator
    {
        public AvailableStocksLevelCalculator() { }

        /// <summary>
        /// Уровень располагаемых запасов в средних значениях за период
        /// </summary>
        /// <param name="startPeriodValue">Объем запасов на начало периода, в натуральном или денежном выражении</param>
        /// <param name="endPeriodValue">Объем запасов на конец периода, в натуральном или денежном выражении</param>
        /// <returns>Средний уровень за период</returns>
        public double CalculateMidLevel(int startPeriodValue, int endPeriodValue)
        {
            return (startPeriodValue + endPeriodValue) / 2;
        }

        /// <summary>
        /// Средний запас по формуле средней хронологической
        /// </summary>
        /// <param name="valuesForDates">Величины запасов на определенные даты, разделенные равными интервалами</param>
        /// <returns>Средний запас</returns>
        public double CalculateMidChronological(List<double> valuesForDates)
        {
            var sum = (valuesForDates[0] / 2) + (valuesForDates[valuesForDates.Count - 1] / 2);
            
            for(int i = 1; i < valuesForDates.Count - 2; i++)
            {
                sum += valuesForDates[i];
            }

            return sum / (valuesForDates.Count - 1);
        }

        /// <summary>
        /// Средний запас по формуле средней арифметической взвешенной. Можно использовать для рассчета среднего дефицита
        /// </summary>
        /// <param name="valuesForPeriods">Словарь периодов и соответсвтующих им значений </param>
        /// <returns>Средний запас</returns>
        public double CalculateArithmeticWeightedAverage(Dictionary<double, int> valuesForPeriods)
        {
            var sumTop = 0.0;
            var sumBottom = 0.0;

            var values = valuesForPeriods.Keys.ToArray();
            var periods = valuesForPeriods.Values.ToArray();

            for(int i = 0; i < valuesForPeriods.Count - 2; i++)
            {
                sumTop += values[i] * periods[i];
                sumBottom += periods[i];
            }

            return sumTop / sumBottom;
        }

        /// <summary>
        /// Обеспеченность предприятия запасами в днях
        /// </summary>
        /// <param name="currStock">Размер наличного текущего запаса в момент времени i, ед</param>
        /// <param name="averageDailyConsumption">Среднесуточный расход запаса, ед./день</param>
        /// <returns>Дни обеспеченности</returns>
        public int CalculateStockAvailabilityInDay(int currStock, int averageDailyConsumption)
        {
            return currStock / averageDailyConsumption;
        }

        /// <summary>
        /// Отношение среднего дефицита к среднему запасу
        /// </summary>
        /// <param name="midDeficite">Средний дефицит</param>
        /// <param name="midStocks">Средний запас</param>
        /// <returns>Процент </returns>
        public double CalculateMidDeficiteToMidStocks(int midDeficite, int midStocks)
        {
            return midDeficite / midStocks * 100;
        }
    }
}
