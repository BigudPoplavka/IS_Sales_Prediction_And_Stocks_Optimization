using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_Predidiction_and_store_optimize.MetricsCalculators
{
    public class OutOfStockCalculator: StockStatsCalculator
    {
        public OutOfStockCalculator()
        {
            methodDescription = new MethodDescription();

            methodDescription.title = "Уровень наличия запасов / учет нехватки";
            methodDescription.description = "Наличие товара достигается тогда, когда в звене цепи поставки " +
                "(магазин, склад) существует достаточный запас номенклатурной позиции (SKU). Простой способ " +
                "измерить уровень наличия товара – это провести учет нехватки запаса (Out-of-stock), то есть, " +
                "тех номенклатурных позиций, запас которых истощен.";

            methodDescription.methodParams = new string[] { "Общее количество номенклатурных позиций (SKU) в ассортименте товаров",
                                                            "Количество номенклатурных позиций (SKU), имеющих нулевой запас" };
        }

        /// <summary>
        /// Рассчет уровня наличия запасов
        /// </summary>
        /// <param name="SKUPositions">Общее количество номенклатурных позиций (SKU) в ассортименте товаров</param>
        /// <param name="allPositions">Количество номенклатурных позиций (SKU), имеющих нулевой запас</param>
        /// <returns>Уровень наличия запасов</returns>
        public double CalculateOutOfStock(int SKUPositions, int allPositions)
        {
            return SKUPositions / allPositions;
        }
    }
}
