using NUnit.Framework;
using TradingBot.ChartService;
using TradingBot.Tool;

namespace TradingBot.Tests;

[TestFixture]
public class MovingAverageConvergenceDivergenceIndicatorToolTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestThatBuySignalIsGenerated()
    {
        // Arrange
        var hSignals = new List<IToolSignal>();
        var hMacdTool = new MovingAverageConvergenceDivergenceIndicatorTool();
        hMacdTool.SignalGenerated += signal => hSignals.Add(signal);
        var hHistoricalData = new List<ChartDataPoint>()
        {
            new ChartDataPoint() { ClosePrice = -1 },
            new ChartDataPoint() { ClosePrice = -2 },
            new ChartDataPoint() { ClosePrice = -3 },
            new ChartDataPoint() { ClosePrice = 4 },
            new ChartDataPoint() { ClosePrice = 5 },
            new ChartDataPoint() { ClosePrice = 6 },
            new ChartDataPoint() { ClosePrice = 7 },
            new ChartDataPoint() { ClosePrice = 8 },
            new ChartDataPoint() { ClosePrice = 9 },
            new ChartDataPoint() { ClosePrice = 10 },
            new ChartDataPoint() { ClosePrice = 11 },
            new ChartDataPoint() { ClosePrice = 12 },
            new ChartDataPoint() { ClosePrice = 13 },
            new ChartDataPoint() { ClosePrice = 14 },
            new ChartDataPoint() { ClosePrice = 15 },
        };
        var hCurrentDataPoint = new ChartDataPoint() {ClosePrice = 9};

        // Act
        hMacdTool.CalculateDataPoint(hHistoricalData, hCurrentDataPoint);
        hMacdTool.GenerateSignal(hHistoricalData, hCurrentDataPoint);

        // Assert
        Assert.That(hSignals, Has.Count.EqualTo(1));
    }
}