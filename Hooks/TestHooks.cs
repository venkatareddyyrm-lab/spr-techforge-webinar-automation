using System.Text;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using Reqnroll;
using Reqnroll.BoDi;
using SprTechForge.Automation.Drivers;

namespace SprTechForge.Automation.Hooks;

[Binding]
public sealed class TestHooks
{
    private readonly ScenarioContext _scenarioContext;
    private readonly IObjectContainer _container;
    private readonly IConfiguration _configuration;
    private readonly DriverFactory _driverFactory;
    private IWebDriver? _driver;

    public TestHooks(
    ScenarioContext scenarioContext,
    IObjectContainer container)
    {
        _scenarioContext = scenarioContext;
        _container = container;

        _configuration = BuildConfiguration();

        _driverFactory = new DriverFactory(_configuration);
    }

    private static IConfiguration BuildConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();
    }

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        var artifacts = Path.Combine(AppContext.BaseDirectory, "Artifacts", "Screenshots");
        Directory.CreateDirectory(artifacts);
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        _container.RegisterInstanceAs<IConfiguration>(_configuration);

        _driver = _driverFactory.Create();

        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
        _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);

        _container.RegisterInstanceAs<IWebDriver>(_driver);
    }

    [AfterScenario]
    public void AfterScenario()
    {
        try
        {
            if (_scenarioContext.TestError is not null && _driver is not null)
            {
                var screenshotPath = CaptureFullPageScreenshot(
                    _driver,
                    _scenarioContext.ScenarioInfo.Title);

                Console.WriteLine(
                    $"Failure screenshot: {screenshotPath}");
            }
        }
        finally
        {
            if (_driver is not null)
            {
                try
                {
                    _driver.Quit();
                }
                catch
                {
                }

                _driver.Dispose();
                _driver = null;
            }
        }
    }

    private static string CaptureFullPageScreenshot(
    IWebDriver driver,
    string scenarioTitle)
    {
        var directory = Path.Combine(
            AppContext.BaseDirectory,
            "Artifacts",
            "Screenshots");

        Directory.CreateDirectory(directory);

        var safeName = string.Join(
            "_",
            scenarioTitle.Split(Path.GetInvalidFileNameChars()))
            .Replace(' ', '_');

        var path = Path.Combine(
            directory,
            $"{DateTime.UtcNow:yyyyMMdd_HHmmss}_{safeName}.png");

        var screenshot = ((ITakesScreenshot)driver).GetScreenshot();

        screenshot.SaveAsFile(path);

        return path;
    }
}
