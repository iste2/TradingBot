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
    public event Action<DateTime>? DataInitialized;

    public virtual Task<ChartDataPoint> UpdateChart()
    {
        throw new NotImplementedException();
    }

    public virtual Task InitializeData()
    {
        DataInitialized?.Invoke(DateTime.Now);
        return Task.CompletedTask;
    }

    public void Run()
    {
        var hTimeOfPreviousUpdate = DateTime.Now;
        while (Running)
        {
            var hTime = DateTime.Now - hTimeOfPreviousUpdate;
            if (hTime.TotalSeconds < AimedUpdateInterval) continue;
            var hNewChartDataPoint = UpdateChart().Result;
            ChartDataPoints.Add(hNewChartDataPoint);
            if(ChartDataPoints.Count > MaxNumberOfDataPoints) ChartDataPoints.RemoveAt(0);
            ChartUpdated?.Invoke(hNewChartDataPoint);
            hTimeOfPreviousUpdate = DateTime.Now;
        }
        
    }

    public void Stop() => Running = false;

    public void Start()
    {
        Running = true;
        InitializeData();
        Run();
    }
}