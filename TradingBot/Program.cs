using Kraken.Net.Clients;
using Kraken.Net.Objects;
using Spectre.Console;
using TradingBot.ChartService;

AnsiConsole.Write(new Padder(new FigletText("CF").LeftJustified().Color(Color.Orange1), new Padding(0, 0, 0, 1)));

var hKrakenChartService = new KrakenChartService(new KrakenClient(new KrakenClientOptions()));
hKrakenChartService.ChartUpdated += point =>
{
    Console.WriteLine(point.Value);
};
hKrakenChartService.Start();
