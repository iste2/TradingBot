using System.Text;
using Spectre.Console;
using TradingBot.Tool;

namespace TradingBot.ChartService;

public interface IChartDataPoint : IMovingAverageConvergenceDivergenceIndicatorDataPoint
{
    public double Volume { get; set; }
    public double ClosePrice { get; set; }
    public double OpenPrice { get; set; }
    public double HighPrice { get; set; }
    public double LowPrice { get; set; }
    public double VolumeWeightedAveragePrice { get; set; }
    public DateTime OpenTime { get; set; }
    public int TradeCount { get; set; }
}

public class ChartDataPoint : IChartDataPoint
{
    public double Volume { get; set; }
    public double ClosePrice { get; set; }
    public double OpenPrice { get; set; }
    public double HighPrice { get; set; }
    public double LowPrice { get; set; }
    public double VolumeWeightedAveragePrice { get; set; }
    public DateTime OpenTime { get; set; }
    public int TradeCount { get; set; }

    public override string ToString()
    {
        var hReturnStringBuilder = new StringBuilder();
        hReturnStringBuilder.Append("[");
        hReturnStringBuilder.Append(GetType().Name + " { ");
        foreach (var hPropertyInfo in GetType().GetProperties())
        {
            hReturnStringBuilder.Append(hPropertyInfo.Name + ": " + GetType().GetProperty(hPropertyInfo.Name)?.GetValue(this, null));
            hReturnStringBuilder.Append("; ");
        }
        hReturnStringBuilder.Append("}]");
        return hReturnStringBuilder.ToString();
    }

    public double Signal { get; set; }
    public double Macd { get; set; }
    public double Ema12Period { get; set; }
    public double Ema26Period { get; set; }
    public double MacdMinusSignal { get; set; }
}