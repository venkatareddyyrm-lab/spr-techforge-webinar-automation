using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;

namespace SprTechForge.Automation.Pages;

public sealed class WebinarEventPage : BasePage
{
    private readonly By NameInput =
        By.CssSelector("input[type='text']:not([placeholder='2024'])");

    private readonly By EmailInput =
        By.CssSelector("input[type='email']");

    private readonly By PhoneInput =
        By.CssSelector("input[type='tel']");

    private readonly By RegisterButton =
        By.XPath(
            "//button[@type='submit' and " +
            "contains(normalize-space(.),'Register Free')]");

    private readonly By SubmitButton =
        By.XPath(
            "//button[@type='submit' and " +
            "normalize-space(.)='Submit']");

    private readonly By ConfirmationMessage =
        By.XPath(
            "//*[contains(translate(normalize-space(.), " +
            "'ABCDEFGHIJKLMNOPQRSTUVWXYZ', " +
            "'abcdefghijklmnopqrstuvwxyz'), 'thank you') " +
            "or contains(translate(normalize-space(.), " +
            "'ABCDEFGHIJKLMNOPQRSTUVWXYZ', " +
            "'abcdefghijklmnopqrstuvwxyz'), " +
            "'successfully registered') " +
            "or contains(translate(normalize-space(.), " +
            "'ABCDEFGHIJKLMNOPQRSTUVWXYZ', " +
            "'abcdefghijklmnopqrstuvwxyz'), " +
            "'registration successful')]");

    private readonly By[] ValidationSelectors =
    {
        By.CssSelector("input:invalid"),
        By.CssSelector("select:invalid"),
        By.CssSelector("textarea:invalid"),
        By.CssSelector("[role='alert']"),
        By.CssSelector(".error"),
        By.CssSelector(".errors"),
        By.CssSelector(".validation-error"),
        By.CssSelector(".invalid-feedback")
    };

    public WebinarEventPage(
        IWebDriver driver,
        IConfiguration configuration)
        : base(driver, configuration)
    {
    }

    public void NavigateTo()
    {
        Driver.Navigate().GoToUrl(
    "https://sprtechforge.com/#/events/" +
    "ai-the-future-of-software-testing-free-live-webinar?ref=singh");

        WaitForSpaContent();
    }

    public bool IsRegistrationFormPresent()
    {
        try
        {
            Wait.Until(driver =>
                driver.FindElements(EmailInput).Count > 0);

            return Driver.FindElements(EmailInput).Count > 0
                && Driver.FindElements(NameInput).Count > 0;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }

    public void FillForm(
      string name,
      string email,
      string phone,
      string? organization = null)
    {
        // Full Name
        var nameInput = WaitForVisible(
            By.CssSelector("input[autocomplete='name']"));

        nameInput.Clear();
        nameInput.SendKeys(name);

        // Email
        var emailInput = WaitForVisible(
            By.CssSelector("input[autocomplete='email']"));

        emailInput.Clear();
        emailInput.SendKeys(email);

        // Mobile
        var phoneInput = WaitForVisible(
            By.CssSelector("input[autocomplete='tel-national']"));

        phoneInput.Clear();
        phoneInput.SendKeys(phone);

        // Qualification / Degree
        SelectFirstAvailableOption(
            "select",
            "B.Tech / B.E");

        // Passing Year
        var passingYear = Driver.FindElements(
            By.CssSelector("input[placeholder='2024']"))
            .FirstOrDefault();

        if (passingYear is not null)
        {
            passingYear.Clear();
            passingYear.SendKeys("2024");
        }

        // Current Status
        SelectFirstAvailableOption(
            "select",
            "Working professional");

        // How did you hear about this event?
        SelectFirstAvailableOption(
            "select",
            "LinkedIn");

        // Biggest question about testing career
        SelectFirstAvailableOption(
            "select",
            "How do I move from manual to automation?");

        // Terms & Conditions
        var checkbox = Driver.FindElements(
            By.CssSelector("input[type='checkbox']"))
            .FirstOrDefault();

        if (checkbox is not null && !checkbox.Selected)
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript(
                "arguments[0].click();",
                checkbox);
        }
    }
    public void ClickRegister()
    {
        Console.WriteLine("Looking for registration submit button...");

        var submitButton = By.XPath(
            "//button[normalize-space(.)='Submit']");

        var registerButton = By.XPath(
            "//button[contains(normalize-space(.),'Register Free')]");

        var reserveButton = By.XPath(
            "//button[contains(normalize-space(.),'Reserve your free seat')]");

        try
        {
            var button = WaitForVisible(submitButton);

            Console.WriteLine(
                $"Found Submit button: '{button.Text}'");

            ((IJavaScriptExecutor)Driver).ExecuteScript(
                "arguments[0].scrollIntoView({block:'center'});",
                button);

            try
            {
                button.Click();
            }
            catch (ElementClickInterceptedException)
            {
                ((IJavaScriptExecutor)Driver).ExecuteScript(
                    "arguments[0].click();",
                    button);
            }

            Console.WriteLine("Submit button clicked.");
            return;
        }
        catch (WebDriverTimeoutException)
        {
            Console.WriteLine(
                "Submit button was not found. Trying Register Free.");
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine(
                "Submit button disappeared. Trying Register Free.");
        }

        try
        {
            var button = WaitForVisible(registerButton);

            Console.WriteLine(
                $"Found Register Free button: '{button.Text}'");

            ((IJavaScriptExecutor)Driver).ExecuteScript(
                "arguments[0].scrollIntoView({block:'center'});",
                button);

            button.Click();

            Console.WriteLine("Register Free button clicked.");
            return;
        }
        catch (WebDriverTimeoutException)
        {
            Console.WriteLine(
                "Register Free button was not found. Trying Reserve.");
        }

        var reserve = WaitForVisible(reserveButton);

        Console.WriteLine(
            $"Found Reserve button: '{reserve.Text}'");

        ((IJavaScriptExecutor)Driver).ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'});",
            reserve);

        reserve.Click();

        Console.WriteLine("Reserve button clicked.");
    }
    public string GetConfirmationMessage()
    {
        System.Threading.Thread.Sleep(2000);

        var bodyText = Driver.FindElement(By.TagName("body")).Text;

        Console.WriteLine("========== POST-SUBMIT BODY TEXT ==========");
        Console.WriteLine(bodyText);
        Console.WriteLine("========== END POST-SUBMIT BODY TEXT ==========");

        return bodyText;
    }

    public bool HasValidationError()
    {
        foreach (var selector in ValidationSelectors)
        {
            if (Driver.FindElements(selector).Any(IsDisplayed))
            {
                return true;
            }
        }

        return false;
    }

    private void WaitForSpaContent()
    {
        Wait.Until(driver =>
        {
            try
            {
                return ((IJavaScriptExecutor)driver)
                    .ExecuteScript("return document.readyState")
                    ?.ToString()
                    == "complete";
            }
            catch
            {
                return false;
            }
        });

        Wait.Until(driver =>
            driver.FindElements(EmailInput).Count > 0);
    }

    private static bool IsDisplayed(IWebElement element)
    {
        try
        {
            return element.Displayed;
        }
        catch (StaleElementReferenceException)
        {
            return false;
        }
    }

    private void SelectFirstAvailableOption(
    string selector,
    string optionText)
    {
        var selects = Driver.FindElements(
            By.CssSelector(selector));

        foreach (var selectElement in selects)
        {
            try
            {
                var options = selectElement.FindElements(
                    By.TagName("option"));

                var option = options.FirstOrDefault(
                    x => string.Equals(
                        x.Text.Trim(),
                        optionText,
                        StringComparison.OrdinalIgnoreCase));

                if (option is not null)
                {
                    option.Click();
                    return;
                }
            }
            catch (StaleElementReferenceException)
            {
                // React re-render; retry by continuing the search.
            }
        }

        throw new InvalidOperationException(
            $"Could not find option '{optionText}'.");
    }
}