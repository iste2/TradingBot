using Kraken.Net.Clients;
using Kraken.Net.Enums;
using Kraken.Net.Objects;

namespace TradingBot.ChartService;

public class KrakenChartService : BasicChartService
{
    private KrakenClient KrakenClient { get; }

    public string TickerSymbol { get; set; } = "XBTUSD";
    
    public KlineInterval KlineInterval { get; set; }
    
    public KrakenChartService(KrakenClient krakenClient)
    {
        KrakenClient = krakenClient;
        KlineInterval = KlineInterval.OneMinute;
        AimedUpdateInterval = (double) KlineInterval;
        MaxNumberOfDataPoints = 200;
    }
    
    public override async Task<ChartDataPoint> UpdateChart()
    {
        var hTickerData = await KrakenClient.SpotApi.ExchangeData.GetKlinesAsync(TickerSymbol, KlineInterval, DateTime.Now);
        return new KrakenChartDataPoint(hTickerData.Data.Data.First());
    }

    public override async Task InitializeData()
    {
        var hSince = DateTime.Now - TimeSpan.FromSeconds(AimedUpdateInterval * MaxNumberOfDataPoints);
        var hTickerData =
            await KrakenClient.SpotApi.ExchangeData.GetKlinesAsync(TickerSymbol, KlineInterval, hSince);
        ChartDataPoints.Clear();
        foreach (var krakenKline in hTickerData.Data.Data)
        {
            ChartDataPoints.Add(new KrakenChartDataPoint(krakenKline));
        }
        await base.InitializeData();
    }
}