using Kraken.Net.Clients;
using Kraken.Net.Objects;

namespace TradingBot.ChartService;

public class KrakenChartService : BasicChartService
{
    private KrakenClient KrakenClient { get; }
    
    public KrakenChartService(KrakenClient krakenClient)
    {
        KrakenClient = krakenClient;
        AimedUpdateInterval = 3;
        MaxNumberOfDataPoints = 10;
    }
    
    public override async Task<ChartDataPoint> UpdateChart()
    {
        var hTickerData = await KrakenClient.SpotApi.ExchangeData.GetTickerAsync("XBTUSD");
        return new ChartDataPoint() { Value = (double) hTickerData.Data.ToList().FirstOrDefault().Value.LastTrade.Price, DateTime = DateTime.Now };
    }
}