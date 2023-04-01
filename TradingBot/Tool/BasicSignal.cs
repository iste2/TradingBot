using TradingBot.ChartService;

namespace TradingBot.Tool;

public class BasicSignal : IToolSignal
{
    public BasicSignal(ToolSignalKind toolSignalKind, IChartDataPoint lastChartDataPoint)
    {
        ToolSignalKind = toolSignalKind;
        LastChartDataPoint = lastChartDataPoint;
    }

    public ToolSignalKind ToolSignalKind { get; set; }
    public IChartDataPoint LastChartDataPoint { get; set; }
}