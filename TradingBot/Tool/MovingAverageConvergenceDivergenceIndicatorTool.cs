using TradingBot.ChartService;

namespace TradingBot.Tool;

public interface IMovingAverageConvergenceDivergenceIndicatorDataPoint
{
    public double Signal { get; set; }
    public double Macd { get; set; }
    public double Ema12Period { get; set; }
    public double Ema26Period { get; set; }
}

public class MovingAverageConvergenceDivergenceIndicatorTool : ITool
{
    /**
     * Calculate Macd and Signal line
     * https://www.investopedia.com/terms/m/macd.asp
     * https://www.investopedia.com/terms/m/movingaverage.asp
     */
    public void CalculateDataPoint(IEnumerable<IChartDataPoint> historicalData, IChartDataPoint targetPoint)
    {
        var hCurrentValue = targetPoint.ClosePrice;
        var hLastDataPoint = historicalData.LastOrDefault();
        
        var hEma12PeriodLast = hLastDataPoint?.Ema12Period ?? hCurrentValue;
        var hEma26PeriodLast = hLastDataPoint?.Ema26Period ?? hCurrentValue;
        
        targetPoint.Ema12Period = Ema(hCurrentValue, hEma12PeriodLast, 12);
        targetPoint.Ema26Period = Ema(hCurrentValue, hEma26PeriodLast, 26);

        targetPoint.Macd = targetPoint.Ema12Period - targetPoint.Ema26Period;

        var hMacdLastPeriod = hLastDataPoint?.Macd ?? targetPoint.Macd;
        targetPoint.Signal = Ema(targetPoint.Macd, hMacdLastPeriod, 9);

    }

    private static double SmoothingFactor(int periodLength)
    {
        return 2 / (periodLength + 1.0);
    }

    private static double Ema(double currentValue, double emaLastPeriod, int periodLength)
    {
        var hSmoothing = SmoothingFactor(periodLength);
        return currentValue * (hSmoothing / (1.0 + periodLength)) +
               emaLastPeriod * (1 - hSmoothing / (1.0 + periodLength));
    }
}