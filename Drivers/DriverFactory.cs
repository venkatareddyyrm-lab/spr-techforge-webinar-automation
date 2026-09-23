using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace SprTechForge.Automation.Drivers;

public sealed class DriverFactory
{
    private readonly IConfiguration _configuration;

    public DriverFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IWebDriver Create()
    {
        var browser = Environment.GetEnvironmentVariable("BROWSER")
                      ?? _configuration["Browser"]
                      ?? "chrome";

        var headless = GetBool("HEADLESS", _configuration["Headless"], true);

        return browser.Trim().ToLowerInvariant() switch
        {
            "chrome" => CreateChrome(headless),
            "firefox" => CreateFirefox(headless),
            "edge" => CreateEdge(headless),
            _ => throw new ArgumentException(
                $"Unsupported browser '{browser}'. Supported browsers: chrome, firefox, edge.")
        };
    }

    private static IWebDriver CreateChrome(bool headless)
    {
        var options = new ChromeOptions();
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");
        options.AddArgument("--window-size=1920,1080");
        options.AddArgument("--disable-gpu");
        options.AddArgument("--disable-notifications");
        options.AddArgument("--disable-popup-blocking");

        if (headless)
            options.AddArgument("--headless=new");

        return new ChromeDriver(options);
    }

    private static IWebDriver CreateFirefox(bool headless)
    {
        var options = new FirefoxOptions();
        options.AddArgument("--width=1920");
        options.AddArgument("--height=1080");

        if (headless)
            options.AddArgument("--headless");

        return new FirefoxDriver(options);
    }

    private static IWebDriver CreateEdge(bool headless)
    {
        var options = new EdgeOptions();
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");
        options.AddArgument("--window-size=1920,1080");
        options.AddArgument("--disable-gpu");
        options.AddArgument("--disable-notifications");

        if (headless)
            options.AddArgument("--headless=new");

        return new EdgeDriver(options);
    }

    private static bool GetBool(string environmentVariable, string? configuredValue, bool defaultValue)
    {
        var value = Environment.GetEnvironmentVariable(environmentVariable);

        if (!string.IsNullOrWhiteSpace(value) && bool.TryParse(value, out var envResult))
            return envResult;

        if (!string.IsNullOrWhiteSpace(configuredValue) && bool.TryParse(configuredValue, out var configResult))
            return configResult;

        return defaultValue;
    }
}
