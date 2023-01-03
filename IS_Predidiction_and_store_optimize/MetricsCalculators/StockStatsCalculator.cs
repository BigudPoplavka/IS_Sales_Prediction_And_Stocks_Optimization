using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS_Predidiction_and_store_optimize.PredictionsMethods;
using IS_Predidiction_and_store_optimize.PredictionsMethods.SchreibfederSchemes;

namespace IS_Predidiction_and_store_optimize.MetricsCalculators
{
    public struct MethodDescription
    {
        public string title;

        public string description;
        public string[] methodParams;
    }

    public abstract class StockStatsCalculator
    {
        protected MethodDescription methodDescription;
        protected MethodDescription CalcDescription => methodDescription;
    }

    public class CalcInitilizer
    {
        public static OutOfStockCalculator outOfStockCalculator;
        public static TurnoveralCalculator turnoveralCalculator;
        public static AvailableStocksLevelCalculator availableStocksLevelCalculator;

        public CalcInitilizer()
        {
            outOfStockCalculator = new OutOfStockCalculator();
            turnoveralCalculator = new TurnoveralCalculator();
            availableStocksLevelCalculator = new AvailableStocksLevelCalculator();   
        }
    }

    public class PresictionMethodsInitilizer
    {
        public static HoltWintersModel holtWintersModel;
        public static SchreibfederModels schreibfederModels;
        public static SMAModel smaModel;

        public PresictionMethodsInitilizer()
        {
            // holtWintersModel = new HoltWintersModel();
            schreibfederModels = new MidWeighted();
            smaModel = new SMAModel();

        }
    }
}
