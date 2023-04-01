using System.Collections.ObjectModel;
using TradingBot.Tool;

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

    public event Action<DateTime> DataInitialized;
    
    public IList<ITool> Tools { get; }

    public Task<ChartDataPoint> UpdateChart();

    public Task InitializeData();

    public void Run();

    public void Stop();

    public void Start();
}