using TradingBot.ChartService;

namespace TradingBot.Tool;

public interface ITool
{
    public void CalculateDataPoint(IEnumerable<IChartDataPoint> historicalData, IChartDataPoint targetPoint);
}