using Kraken.Net.Clients;
using Kraken.Net.Objects;
using Spectre.Console;
using TradingBot.ChartService;
using TradingBot.Tool;

AnsiConsole.Write(new Padder(new FigletText("COINFURY").LeftJustified().Color(Color.Orange1), new Padding(0, 0, 0, 1)));

var hKrakenChartService = new KrakenChartService(new KrakenClient(new KrakenClientOptions()));
var hMacdTool = new MovingAverageConvergenceDivergenceIndicatorTool();
hKrakenChartService.Tools.Add(hMacdTool);

hMacdTool.SignalGenerated += signal =>
{
    Console.WriteLine(signal.ToolSignalKind.ToString().ToUpper() + " " + signal.LastChartDataPoint.OpenTime);
};
hKrakenChartService.ChartUpdated += point =>
{
    Console.WriteLine(point.ToString());
};
hKrakenChartService.DataInitialized += time =>
{
    foreach (var chartDataPoint in hKrakenChartService.ChartDataPoints)
    {
        Console.WriteLine(chartDataPoint);
    }
};
hKrakenChartService.Start();
