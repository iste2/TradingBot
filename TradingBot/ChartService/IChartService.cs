using System.Collections.ObjectModel;

namespace TradingBot.ChartService;

public interface IChartService
{
    /**
     *  Update interval in seconds
     */
    public double AimedUpdateInterval { get; set; }
    
    protected bool Running { get; set; }
    
    /**
     *  Max number of entries in the ChartDataPoints-List (used for display)
     */
    public int MaxNumberOfDataPoints { get; set; }
    
    /**
     *  List of all DataPoints
     */
    public ObservableCollection<ChartDataPoint> ChartDataPoints { get; }

    /**
     *  Event that is triggered after UpdateChart()
     */
    public event Action<ChartDataPoint> ChartUpdated;

    public Task<ChartDataPoint> UpdateChart();

    public void Run();

    public void Stop();

    public void Start();
}