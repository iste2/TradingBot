using TradingBot.ChartService;
using TradingBot.OrderEngine;

namespace TradingBot.TradingLogic;

public abstract class BasicTradingBot : ITradingBot
{
    public IChartService? ChartService { get; set; }
    public IOrderEngine OrderEngine { get; set; }

    public void ConnectToChartService(IChartService? chartService)
    {
        DisconnectFromCurrentChartService();
        ChartService = chartService;
        ReconnectToCurrentChartService();
    }

    public void DisconnectFromCurrentChartService()
    {
        if (ChartService != null) ChartService.ChartUpdated -= OnChartServiceUpdated;
    }

    public void ReconnectToCurrentChartService()
    {
        if (ChartService != null) ChartService.ChartUpdated += OnChartServiceUpdated;
    }
    
    public virtual void OnChartServiceUpdated(ChartDataPoint chartDataPoint)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        if (ChartService != null) ChartService.ChartUpdated -= OnChartServiceUpdated;
    }
}