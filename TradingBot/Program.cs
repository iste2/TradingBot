using Kraken.Net.Clients;
using Kraken.Net.Enums;
using Kraken.Net.Objects;
using ScottPlot;
using Spectre.Console;
using TradingBot.ChartService;
using TradingBot.Tool;
using Color = Spectre.Console.Color;

AnsiConsole.Write(new Padder(new FigletText("COINFURY").LeftJustified().Color(Color.Orange1), new Padding(0, 0, 0, 1)));

var hChartImagePath = AnsiConsole.Ask<string>("Where should the image of the chart be saved?");

var hKrakenChartService = new KrakenChartService(new KrakenClient(new KrakenClientOptions()), KlineInterval.OneDay);
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

    if (string.IsNullOrEmpty(hChartImagePath)) return;
    
    Console.WriteLine("Saving chart to image...");
    
    // Show candles
    var plot = new Plot();
    plot.Add.Candlestick(hKrakenChartService.ChartDataPoints.Select(_ => new OHLC(_.OpenPrice, _.HighPrice, _.LowPrice,
            _.ClosePrice, _.OpenTime, TimeSpan.FromSeconds((int) hKrakenChartService.KlineInterval))).ToList().AsReadOnly());
    plot.Axes.DateTimeTicks(Edge.Bottom);

    plot.SavePng(hChartImagePath, 2000, 1000);
    
};
hKrakenChartService.Start();
