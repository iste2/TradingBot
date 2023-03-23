using Kraken.Net.Objects.Models;

namespace TradingBot.ChartService;

public class KrakenChartDataPoint : ChartDataPoint
{
    public KrakenChartDataPoint(KrakenKline krakenKline)
    {
        Volume = (double) krakenKline.Volume;
        ClosePrice = (double) krakenKline.ClosePrice;
        OpenPrice = (double) krakenKline.OpenPrice;
        HighPrice = (double) krakenKline.HighPrice;
        LowPrice = (double) krakenKline.LowPrice;
        VolumeWeightedAveragePrice = (double) krakenKline.VolumeWeightedAveragePrice;
        OpenTime = krakenKline.OpenTime;
        TradeCount = krakenKline.TradeCount;
    }
}