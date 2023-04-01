using System.Collections.ObjectModel;
using TradingBot.ChartService;

namespace TradingBot.Tool;

public enum ToolSignalKind
{
    Buy, Sell
}

public interface IToolSignal
{
    public ToolSignalKind ToolSignalKind { get; set; }
    public IChartDataPoint LastChartDataPoint { get; set; }
}

public interface ITool
{
    public void CalculateDataPoint(IEnumerable<IChartDataPoint> historicalData, IChartDataPoint targetPoint, int? indexOfLastChartDataPoint = null);

    public void GenerateSignal(IEnumerable<IChartDataPoint> historicalData, IChartDataPoint targetPoint,
        int? indexOfLastChartDataPoint = null);

    public event Action<IToolSignal>? SignalGenerated;
    public ObservableCollection<IToolSignal> Signals { get; set; }
}