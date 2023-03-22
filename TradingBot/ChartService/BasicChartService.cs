using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace TradingBot.ChartService;

public abstract class BasicChartService : IChartService
{
    public double AimedUpdateInterval { get; set; }
    public bool Running { get; set; }
    public int MaxNumberOfDataPoints { get; set; }
    public ObservableCollection<ChartDataPoint> ChartDataPoints { get; } = new ObservableCollection<ChartDataPoint>();
    public event Action<ChartDataPoint>? ChartUpdated;

    public virtual Task<ChartDataPoint> UpdateChart()
    {
        throw new NotImplementedException();
    }

    public void Run()
    {
        var hTimeOfPreviousUpdate = DateTime.Now;
        while (Running)
        {
            var hTime = DateTime.Now - hTimeOfPreviousUpdate;
            if (hTime.TotalSeconds < AimedUpdateInterval) continue;
            var hNewChartDataPoint = UpdateChart().Result;
            ChartUpdated?.Invoke(hNewChartDataPoint);
            ChartDataPoints.Add(hNewChartDataPoint);
            if(ChartDataPoints.Count > MaxNumberOfDataPoints) ChartDataPoints.RemoveAt(0);
            hTimeOfPreviousUpdate = DateTime.Now;
        }
        
    }

    public void Stop() => Running = false;

    public void Start()
    {
        Running = true;
        Run();
    }
}