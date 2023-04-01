using System.Collections.ObjectModel;
using TradingBot.ChartService;

namespace TradingBot.Tool;

public interface IMovingAverageConvergenceDivergenceIndicatorDataPoint
{
    public double Signal { get; set; }
    public double Macd { get; set; }
    public double Ema12Period { get; set; }
    public double Ema26Period { get; set; }
    public double MacdMinusSignal { get; set; }
}

public class MovingAverageConvergenceDivergenceIndicatorTool : ITool
{
    /**
     * Calculate Macd and Signal line
     * https://www.investopedia.com/terms/m/macd.asp
     * https://www.investopedia.com/terms/m/movingaverage.asp
     */
    public void CalculateDataPoint(IEnumerable<IChartDataPoint> historicalData, IChartDataPoint targetPoint, int? indexOfLastChartDataPoint = null)
    {
        var hHistoricalData = historicalData.ToArray();
        
        var hCurrentValue = targetPoint.ClosePrice;
        var hLastDataPoint = indexOfLastChartDataPoint == null 
            ? hHistoricalData.LastOrDefault() 
            : hHistoricalData.Length > indexOfLastChartDataPoint.Value && indexOfLastChartDataPoint.Value >= 0
                ? hHistoricalData[indexOfLastChartDataPoint.Value] 
                : null;
        
        var hEma12PeriodLast = hLastDataPoint?.Ema12Period ?? hCurrentValue;
        var hEma26PeriodLast = hLastDataPoint?.Ema26Period ?? hCurrentValue;
        
        targetPoint.Ema12Period = Ema(hCurrentValue, hEma12PeriodLast, 12);
        targetPoint.Ema26Period = Ema(hCurrentValue, hEma26PeriodLast, 26);

        targetPoint.Macd = targetPoint.Ema12Period - targetPoint.Ema26Period;

        var hMacdLastPeriod = hLastDataPoint?.Macd ?? targetPoint.Macd;
        targetPoint.Signal = Ema(targetPoint.Macd, hMacdLastPeriod, 9);

        targetPoint.MacdMinusSignal = targetPoint.Macd - targetPoint.Signal;

    }

    /**
     * Look for change of sign and wait some periods to confirm signal
     */
    public void GenerateSignal(IEnumerable<IChartDataPoint> historicalData, IChartDataPoint targetPoint, int? indexOfLastChartDataPoint = null)
    {
        var hHistoricalData = historicalData.ToArray();
        
        var hCurrentValue = targetPoint.MacdMinusSignal;
        var hLastDataPoint = indexOfLastChartDataPoint == null 
            ? hHistoricalData.LastOrDefault() 
            : hHistoricalData.Length > indexOfLastChartDataPoint.Value && indexOfLastChartDataPoint.Value >= 0
                ? hHistoricalData[indexOfLastChartDataPoint.Value] 
                : null;

        if (hLastDataPoint == null) return;
        
        var hLastValue = hLastDataPoint.MacdMinusSignal;

        // Generate Signal
        if (!(hLastValue == 0 && hCurrentValue == 0) && hLastValue <= 0 && hCurrentValue >= 0)
            _currentBasicSignalToBeConfirmed = new BasicSignal(ToolSignalKind.Buy, targetPoint);
        else if (!(hLastValue == 0 && hCurrentValue == 0) && hLastValue >= 0 && hCurrentValue <= 0)
            _currentBasicSignalToBeConfirmed = new BasicSignal(ToolSignalKind.Sell, targetPoint);
        
        // Confirm Signal
        if (_currentBasicSignalToBeConfirmed == null)
        {
            _confirmationPeriodCounter = NumberOfConfirmationPeriods;
            return;
        }

        if ((_currentBasicSignalToBeConfirmed.ToolSignalKind != ToolSignalKind.Buy || !(hCurrentValue >= 0))
            && (_currentBasicSignalToBeConfirmed.ToolSignalKind != ToolSignalKind.Sell || !(hCurrentValue >= 0))) return;
        
        _confirmationPeriodCounter--;
        
        if (_confirmationPeriodCounter > 0) return;
        
        Signals.Add(_currentBasicSignalToBeConfirmed);
        SignalGenerated?.Invoke(_currentBasicSignalToBeConfirmed);
        _currentBasicSignalToBeConfirmed = null;
    }

    public event Action<IToolSignal>? SignalGenerated;
    public ObservableCollection<IToolSignal> Signals { get; set; } = new ObservableCollection<IToolSignal>();

    private BasicSignal? _currentBasicSignalToBeConfirmed;
    private int _confirmationPeriodCounter;

    private int NumberOfConfirmationPeriods { get; set; } = 3; 
    

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