using Spectre.Console;

namespace TradingBot.ChartService;

public class ChartDataPoint
{
    public double Value { get; set; }
    public DateTime DateTime { get; set; }
}