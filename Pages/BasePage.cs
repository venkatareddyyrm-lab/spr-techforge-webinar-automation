using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace SprTechForge.Automation.Pages;

public abstract class BasePage
{
    protected readonly IWebDriver Driver;
    protected readonly WebDriverWait Wait;
    protected readonly IConfiguration Configuration;

    protected BasePage(IWebDriver driver, IConfiguration configuration)
    {
        Driver = driver;
        Configuration = configuration;

        var timeout = int.TryParse(
      configuration["TimeoutSeconds"],
      out var configuredTimeout)
      ? configuredTimeout
      : 20;
        Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
        Wait.IgnoreExceptionTypes(
            typeof(NoSuchElementException),
            typeof(StaleElementReferenceException));
    }

    protected IWebElement WaitForVisible(By locator)
    {
        return Wait.Until(driver =>
        {
            var element = driver.FindElement(locator);
            return element.Displayed ? element : null;
        })!;
    }

    protected IWebElement WaitForClickable(By locator)
    {
        return Wait.Until(driver =>
        {
            var element = driver.FindElement(locator);
            return element.Displayed && element.Enabled ? element : null;
        })!;
    }



    protected bool IsVisible(By locator, TimeSpan? timeout = null)
    {
        try
        {
            var wait = timeout.HasValue
                ? new WebDriverWait(Driver, timeout.Value)
                : Wait;

            return wait.Until(driver =>
            {
                try
                {
                    return driver.FindElement(locator).Displayed;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }

    protected void WaitForUrlContains(string expectedFragment)
    {
        Wait.Until(driver => driver.Url.Contains(expectedFragment, StringComparison.OrdinalIgnoreCase));
    }

    protected void WaitForDocumentReady()
    {
        Wait.Until(driver =>
        {
            var state = ((IJavaScriptExecutor)driver)
                .ExecuteScript("return document.readyState")?.ToString();

            return state == "complete" || state == "interactive";
        });
    }

    protected void ExecuteJavaScript(string script, params object[] arguments)
    {
        ((IJavaScriptExecutor)Driver).ExecuteScript(script, arguments);
    }

    protected void ScrollIntoView(IWebElement element)
    {
        ExecuteJavaScript(
            "arguments[0].scrollIntoView({behavior:'instant', block:'center'});",
            element);
    }

    protected IWebElement FindFirstVisible(params By[] locators)
    {
        foreach (var locator in locators)
        {
            try
            {
                var element = Driver.FindElement(locator);
                if (element.Displayed)
                    return element;
            }
            catch (NoSuchElementException)
            {
            }
        }

        throw new NoSuchElementException(
            "None of the configured locators matched a visible element.");
    }

    protected static string Normalize(string? value) =>
        string.IsNullOrWhiteSpace(value)
            ? string.Empty
            : string.Join(" ", value.Split(
                (char[]?)null,
                StringSplitOptions.RemoveEmptyEntries));
}
