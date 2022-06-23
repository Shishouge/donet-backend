using BloodManagement.ML;
using BloodManagement.Util;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BloodManagement.Model
{
    public class Train
    {
        static readonly string dataPath = Path.Combine("E:/大三下/.NET/Project/Backend/BloodManagement/BloodManagement.ML/Data/bloodData.txt");
        static readonly string dataPath2 = Path.Combine("E:/大三下/.NET/Project/Backend/BloodManagement/BloodManagement.ML/Data/test.txt");
        public static void train()
        {
            MLContext mlContext = new MLContext();
            IDataView dataView = mlContext.Data.LoadFromTextFile<ModelInput>(dataPath, separatorChar: ' ', hasHeader: false);
            var forecastingPipeline = mlContext.Forecasting.ForecastBySsa(
                outputColumnName: "forecasted_count",
                inputColumnName: "count",
                windowSize: 30,
                seriesLength: 30,
                trainSize: 17,
                horizon: 30,
                confidenceLevel: 0.95f,
                confidenceLowerBoundColumn: "lower_count",
                confidenceUpperBoundColumn: "upper_count");
            SsaForecastingTransformer forecaster = forecastingPipeline.Fit(dataView);
        }
        public static void eva(IDataView testData, ITransformer model, MLContext mlContext)
        {
            IDataView predictions = model.Transform(testData);

            IEnumerable<float> actual =
                mlContext.Data.CreateEnumerable<ModelInput>(testData, true)
                    .Select(p => p.count);

            IEnumerable<float> forecast =
                mlContext.Data.CreateEnumerable<ModelOutput>(predictions, true)
                    .Select(p => p.forecasted_count[0]);

            var metrics = actual.Zip(forecast, (actualValue, forecastValue) => actualValue - forecastValue);

            var MAE = metrics.Average(error => Math.Abs(error));
            var RMSE = Math.Sqrt(metrics.Average(error => Math.Pow(error, 2)));

            Console.WriteLine("评估结果");
            Console.WriteLine("---------------------");
            Console.WriteLine($"平均绝对误差: {MAE:F3}");
            Console.WriteLine($"均方根误差: {RMSE:F3}\n");
        }

        public MLHelper run()
        {
            MLContext mlContext = new MLContext();
            IDataView dataView = mlContext.Data.LoadFromTextFile<ModelInput>(dataPath, separatorChar: ',', hasHeader: false);
            List<float> sevenData = new List<float>();
            var forecastingPipeline = mlContext.Forecasting.ForecastBySsa(
                outputColumnName: "forecasted_count",
                inputColumnName: "count",
                windowSize: 7,
                seriesLength: 30,
                trainSize: 17,
                horizon: 30,
                confidenceLevel: 0.95f,
                confidenceLowerBoundColumn: "lower_count",
                confidenceUpperBoundColumn: "upper_count");
            SsaForecastingTransformer forecaster = forecastingPipeline.Fit(dataView);

            var forecastEngine = forecaster.CreateTimeSeriesEngine<ModelInput, ModelOutput>(mlContext);

            IDataView dataView2 = mlContext.Data.LoadFromTextFile<ModelInput>(dataPath2, separatorChar: ' ', hasHeader: false);

            ModelOutput forecast = forecastEngine.Predict();
            float sum = 0.0F;
            IEnumerable<string> forecastOutput =
            mlContext.Data.CreateEnumerable<ModelInput>(dataView2, reuseRowObject: false)
                .Take(7)
                .Select((ModelInput data, int index) =>
                {
                    string action_date = data.action_time.ToString("yyyy-MM-dd");
                    float actual_count = data.count;
                    float lowerEstimate = Math.Max(0, forecast.lower_count[index]);
                    float estimate = forecast.forecasted_count[index];
                    float upperEstimate = forecast.upper_count[index];
                    sevenData.Add(estimate);
                    sum += estimate;
                    return $"日期: {action_date}\n" +
                    $"实际值: {actual_count}\n" +
                    $"预测下限估值: {lowerEstimate}\n" +
                    $"预测估值: {estimate}\n" +
                    $"预测上限估值: {upperEstimate}\n";
                });
            System.Diagnostics.Debug.WriteLine("预测结果");

            System.Diagnostics.Debug.WriteLine("---------------------");
            foreach (var prediction in forecastOutput)
            {
                System.Diagnostics.Debug.WriteLine(prediction);
            }
            System.Diagnostics.Debug.WriteLine(sum);
            return new MLHelper(sevenData,sum);
        }
    }
}
