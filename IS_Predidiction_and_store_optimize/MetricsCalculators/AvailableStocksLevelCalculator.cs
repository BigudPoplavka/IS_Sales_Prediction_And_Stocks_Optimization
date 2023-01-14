using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_Predidiction_and_store_optimize.MetricsCalculators
{
    public struct ASLCalcDescription
    {
        public string title;
        public List<string> paramsDesription;

        public ASLCalcDescription(string titleParam, List<string> paramsDesrList)
        {
            title = titleParam;
            paramsDesription = paramsDesrList;
        }
    }

    public class AvailableStocksLevelCalculator: StockStatsCalculator
    {
        public ASLCalcDescription descrMidLevel;
        public ASLCalcDescription descrAvailabilityInDay;
        public ASLCalcDescription descrMidDeficiteToMidStocks;
        public ASLCalcDescription descrStockRentabl;

        public AvailableStocksLevelCalculator() 
        {
            descrMidLevel = new ASLCalcDescription("Уровень располагаемых запасов в средних значениях за период", 
                new List<string>() { "Объем запасов на начало периода, в натуральном или денежном выражении",
                                     "Объем запасов на конец периода, в натуральном или денежном выражении" });

            descrAvailabilityInDay = new ASLCalcDescription("Обеспеченность предприятия запасами в днях", 
                new List<string>() { "Размер наличного текущего запаса в момент времени i, ед",
                                     "Среднесуточный расход запаса, ед./день"});

            descrMidDeficiteToMidStocks = new ASLCalcDescription("Отношение среднего дефицита к среднему запасу (%)", 
                new List<string>() { "Средний дефицит",
                                     "Средний запас"});

            descrStockRentabl = new ASLCalcDescription("Рентабельность запасов", 
                new List<string>() { "чистая прибыль от реализации запасов (валовая прибыль за минусом налога)",
                                     "себестоимость запасов или затраты на формирование запасов" });
        }

        /// <summary>
        /// Уровень располагаемых запасов в средних значениях за период
        /// </summary>
        /// <param name="startPeriodValue">Объем запасов на начало периода, в натуральном или денежном выражении</param>
        /// <param name="endPeriodValue">Объем запасов на конец периода, в натуральном или денежном выражении</param>
        /// <returns>Средний уровень за период</returns>
            public double CalculateMidLevel(int startPeriodValue, int endPeriodValue)
        {
            return ((startPeriodValue * 1.0) + (endPeriodValue * 1.0)) / 2;
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

            return Math.Round(sum / (valuesForDates.Count - 1), 2);
        }

        /// <summary>
        /// Коэффициент обеспеченности предприятия запасами в день
        /// </summary>
        /// <param name="currStock">Размер наличного текущего запаса в момент времени i, ед</param>
        /// <param name="averageDailyConsumption">Среднесуточный расход запаса, ед./день</param>
        /// <returns>Показатель обеспеченности</returns>
        public double CalculateStockAvailabilityInDay(int currStock, int averageDailyConsumption)
        {
            return Math.Round((currStock / averageDailyConsumption) * 1.0, 1);
        }

        /// <summary>
        /// Отношение среднего дефицита к среднему запасу
        /// </summary>
        /// <param name="midDeficite">Средний дефицит</param>
        /// <param name="midStocks">Средний запас</param>
        /// <returns>Процент </returns>
        public double CalculateMidDeficiteToMidStocks(int midStocks, int midDeficite)
        {
            return Math.Round(((midDeficite * 1.0) / (midStocks * 1.0)) * 100, 1);
        }

        /// <summary>
        /// Индекс доходности запасов
        /// </summary>
        /// <param name="CRealizMZ">себестоимость реализованных за рассматриваемый период запасов, ден. ед.</param>
        /// <param name="midStockPrice">средняя себестоимость запасов, хранимых на складе за рассматриваемый период времени, ден. ед.</param>
        /// <param name="stonks">выручка от реализации запасов, ден. ед.</param>
        /// <param name="PrealizMZ">прибыль от реализации запасов за рассматриваемый период, ден. ед.</param>
        /// <returns></returns>
        public double CalculateStonksIndex(double CRealizMZ, double midStockPrice, double stonks, double PrealizMZ)
        {
            return Math.Round(CRealizMZ / midStockPrice * ((PrealizMZ / stonks) * 100), 2);
        }

        /// <summary>
        /// Рентабельность запасов
        /// </summary>
        /// <param name="clearStonks">чистая прибыль от реализации запасов, которая определяется по данным бухгалтерской
        /// отчетности как валовая прибыль за минусом налога на прибыль (заработной платы и процентных платежей).</param>
        /// <param name="stonksFormingPrice"> себестоимость запасов или затраты на формирование запасов 
        /// (включая стоимость самих МЦ в запасах и затраты на обслуживание запаса).</param>
        /// <returns></returns>
        public double CalculateStocksRentabl(int clearStonks, int stonksFormingPrice)
        {
            return (clearStonks * 1.0) / (stonksFormingPrice * 1.0) * 100;
        }
    }
}
