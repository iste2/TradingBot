using TradingBot.ChartService;
using TradingBot.OrderEngine;

namespace TradingBot.TradingLogic;

public interface ITradingBot : IDisposable
{
    public IChartService? ChartService { get; set; }
    
    public IOrderEngine OrderEngine { get; set; }

    public void ConnectToChartService(IChartService? chartService);

    public void DisconnectFromCurrentChartService();

    public void ReconnectToCurrentChartService();

    public void OnChartServiceUpdated(ChartDataPoint chartDataPoint);
}