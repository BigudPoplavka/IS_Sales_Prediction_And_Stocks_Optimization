using System.Collections.Generic;

namespace IS_Predidiction_and_store_optimize.PredictionsMethods
{
    public class HoltWintersModel: PredictionMethod
    {
        // Параметры модели
        private double _alpha;
        private double _beta;
        private double _gamma;

        // Переменные состояния модели
        private double _level;
        private double _trend;
        private double _seasonal;

        public HoltWintersModel(double alpha, double beta, double gamma)
        {
            _alpha = alpha;
            _beta = beta;
            _gamma = gamma;
        }

        public void Fit(Dictionary<string, double> data)
        {
            _level = data["Jan"];
            _trend = (data["Feb"] - data["Jan"]) / 2.0;
            _seasonal = (data["Jan"] - data["Mar"]) / 2.0;

            foreach (var kvp in data)
            {
                string month = kvp.Key;
                double value = kvp.Value;

                if (month == "Jan" || month == "Feb" || month == "Mar")
                {
                    continue;
                }

                double prevLevel = _level;

                _level = _alpha * value + (1 - _alpha) * (prevLevel + _trend);
                _trend = _beta * (_level - prevLevel) + (1 - _beta) * _trend;
                _seasonal = _gamma * (value - _level) + (1 - _gamma) * _seasonal;
            }
        }

        public double Predict()
        {
            return _level + _trend + _seasonal;
        }
    }
}
