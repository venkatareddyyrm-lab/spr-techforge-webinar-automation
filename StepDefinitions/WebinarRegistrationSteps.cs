using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using Reqnroll;
using SprTechForge.Automation.Pages;

namespace SprTechForge.Automation.StepDefinitions;

[Binding]
public sealed class WebinarRegistrationSteps
{
    private readonly IWebDriver _driver;
    private readonly IConfiguration _configuration;
    private readonly ScenarioContext _scenarioContext;
    private readonly WebinarEventPage _page;


    public WebinarRegistrationSteps(
     IWebDriver driver,
     IConfiguration configuration,
     ScenarioContext scenarioContext)
    {
        _driver = driver;
        _configuration = configuration;
        _scenarioContext = scenarioContext;
        _page = new WebinarEventPage(driver, configuration);
    }
    [Given("I am on the AI software testing webinar registration page")]
    public void GivenIAmOnTheWebinarRegistrationPage()
    {
        _page.NavigateTo();
        _page.IsRegistrationFormPresent().Should().BeTrue(
            "the webinar page should expose the name and email registration controls");
    }

    [When("I submit the registration form with dynamically generated attendee data")]
    public void WhenISubmitTheRegistrationFormWithDynamicData()
    {
        var faker = new Faker("en");

        var name = faker.Name.FullName();
        var email = $"qa.{Guid.NewGuid():N}@example.test";
        var phone = faker.Phone.PhoneNumber("##########");
        var organization = $"QA-{faker.Company.CompanyName()}";

        _scenarioContext["GeneratedEmail"] = email;
        _scenarioContext["GeneratedName"] = name;
        _scenarioContext["GeneratedPhone"] = phone;
        _scenarioContext["GeneratedOrganization"] = organization;

        _page.FillForm(
            name,
            email,
            phone,
            organization);

        _page.ClickRegister();
    }

    [Then("I should see a successful registration confirmation")]
    public void ThenIShouldSeeSuccessfulRegistrationConfirmation()
    {
        var message = _page.GetConfirmationMessage();

        message.Should().NotBeNullOrWhiteSpace(
            "a successful registration should display a confirmation message");

        message.Should().MatchRegex(
            "(?i)(success|thank|registered|confirmation|submitted|join)");
    }

    [When("I submit the registration form with empty name and invalid email")]
    public void WhenISubmitInvalidRegistration()
    {
        _scenarioContext["GeneratedEmail"] = $"invalid.{Guid.NewGuid():N}@example.test";

        _page.FillForm(string.Empty, "invalid-email", "123");
        _page.ClickRegister();
    }

    [Then("I should see registration validation errors")]
    public void ThenIShouldSeeRegistrationValidationErrors()
    {
        _page.HasValidationError().Should().BeTrue(
            "invalid registration data should trigger visible validation errors");
    }
    
}
