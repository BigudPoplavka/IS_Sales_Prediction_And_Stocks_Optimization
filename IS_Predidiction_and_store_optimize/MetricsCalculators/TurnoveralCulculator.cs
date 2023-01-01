using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_Predidiction_and_store_optimize.MetricsCalculators
{
    public class TurnoveralCalculator: StockStatsCalculator
    {
        public TurnoveralCalculator()
        {
            methodDescription = new MethodDescription();

            methodDescription.title = "Отсутствие излишков запасов";
            methodDescription.description = "Отсутствие излишков запасов можно выразить путем эффективности использования денег," +
                " инвестированных в запасы. Первичный показатель – это скорость, с которой запас движется. Чем выше скорость, " +
                "тем более эффективна инвестиция. Общепринятым измерителем скорости является показатель оборачиваемости запаса.";

            methodDescription.methodParams = new string[] { "Общий объем годовых продаж в закупочных ценах",
                                                            "Средний ежемесячный объем товарного запаса — на основании предыдущих 3 или 6 мес." };
        }

        /// <summary>
        /// Рассчет отсутствия излишков запасов
        /// </summary>
        /// <param name="allYearSales">Общий объем годовых продаж в закупочных ценах</param>
        /// <param name="midMonthSales">Средний ежемесячный объем товарного запаса — на основании предыдущих 3 или 6 мес.</param>
        /// <returns></returns>
        public double CalculateTurnoveral(int allYearSales, int midMonthSales)
        {
            return allYearSales / midMonthSales;
        }
    }
}
