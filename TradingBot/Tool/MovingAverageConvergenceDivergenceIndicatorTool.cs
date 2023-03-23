using TradingBot.ChartService;

namespace TradingBot.Tool;

public interface IMovingAverageConvergenceDivergenceIndicatorDataPoint
{
    public double Signal { get; set; }
    public double Macd { get; set; }
}

public class MovingAverageConvergenceDivergenceIndicatorTool : ITool
{
    public void CalculateDataPoint(IEnumerable<IChartDataPoint> historicalData, IChartDataPoint targetPoint)
    {
        var h12PeriodEma = historicalData.TakeLast(12).Sum(_ => _.ClosePrice) / 12;
        var h26PeriodEma = historicalData.TakeLast(26).Sum(_ => _.ClosePrice) / 26;
        targetPoint.Macd = h12PeriodEma - h26PeriodEma;
    }
}