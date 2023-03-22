namespace TradingBot.ChartService;

public class RandomChartService : BasicChartService
{
    private double LastValue = 25;
    
    public RandomChartService()
    {
        AimedUpdateInterval = 1;
        MaxNumberOfDataPoints = 25;
    }

    public override Task<ChartDataPoint> UpdateChart()
    {
        var hRandom = new Random();
        var hDirectionUp = Convert.ToBoolean(hRandom.Next(0, 1));
        var hPercentage = hRandom.Next(0, 15) / 100.0;
        var hNewValue = hDirectionUp ? LastValue * (1.0 + hPercentage) : LastValue * (1.0 - hPercentage/2);
        LastValue = hNewValue;
        return Task.FromResult(new ChartDataPoint() { Value = hNewValue, DateTime = DateTime.Now });
    }
}