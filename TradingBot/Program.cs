using Kraken.Net.Clients;
using Kraken.Net.Objects;
using Spectre.Console;
using TradingBot.ChartService;

AnsiConsole.Write(new Padder(new FigletText("COINFURY").LeftJustified().Color(Color.Orange1), new Padding(0, 0, 0, 1)));

var hKrakenChartService = new KrakenChartService(new KrakenClient(new KrakenClientOptions()));
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
