# SPR TechForge Webinar Registration Automation

A production-oriented **.NET 8 BDD UI automation framework** for testing the SPR TechForge AI & Future of Software Testing webinar registration flow.

The framework automates the registration journey using **C#, Selenium WebDriver, Reqnroll, NUnit, Page Object Model, FluentAssertions and Bogus test data**.

## Technology Stack

* .NET 8
* C#
* Selenium WebDriver
* Reqnroll 3.3.4
* NUnit
* FluentAssertions
* Bogus
* Page Object Model (POM)
* Selenium Manager
* Azure DevOps Pipelines
* GitHub

Selenium Manager automatically manages the required browser driver.

## Framework Architecture

```text
SprTechForge_Automation_Framework/
│
├── Drivers/
│   └── DriverFactory.cs
│
├── Hooks/
│   └── TestHooks.cs
│
├── Pages/
│   ├── BasePage.cs
│   └── WebinarEventPage.cs
│
├── Features/
│   └── WebinarRegistration.feature
│
├── StepDefinitions/
│   └── WebinarRegistrationSteps.cs
│
├── Test cases/
│   └── Webinar_Registration_Testcases.md
│
├── TestData/
│   └── .gitkeep
│
├── Artifacts/
│   └── Screenshots/
│
├── appsettings.json
├── SprTechForge.Automation.csproj
├── azure-pipelines.yml
└── README.md
```

## Test Coverage

The framework currently covers the following automated scenarios:

### Positive Scenario

**Successful webinar registration**

* Navigate to the webinar registration page
* Generate dynamic attendee data
* Enter name
* Enter email
* Enter mobile number
* Select qualification
* Enter passing year
* Select current employment status
* Select referral source
* Select testing-career question
* Accept terms and conditions
* Submit registration
* Verify successful registration confirmation

### Negative Scenario

**Registration with invalid required information**

* Navigate to registration page
* Submit empty name
* Submit invalid email
* Submit invalid mobile number
* Submit the form
* Verify validation errors are displayed
* Verify registration is not accepted

## Dynamic Test Data

The positive test uses **Bogus** and `Guid` to generate unique test data.

Example:

```csharp
var faker = new Faker("en");

var email = $"qa.{Guid.NewGuid():N}@example.test";

var attendeeName = faker.Name.FullName();

var phone = faker.Phone.PhoneNumber("##########");

var organization = $"QA-{faker.Company.CompanyName()}";
```

This prevents the automation from depending on hard-coded attendee information.

## Page Object Model

The framework follows the **Page Object Model (POM)** design pattern.

### BasePage

Contains reusable Selenium functionality such as:

* Element waiting
* Visibility checks
* Element interaction
* Common browser operations

### WebinarEventPage

Contains webinar-specific:

* Locators
* Form interactions
* Registration submission
* Confirmation validation
* Registration error validation

This keeps Selenium implementation details separated from the Gherkin scenarios.

## BDD Structure

The framework uses Reqnroll feature files.

Example:

```gherkin
Feature: Webinar registration

  Scenario: Successful webinar registration
    Given I am on the AI software testing webinar registration page
    When I submit the registration form with dynamically generated attendee data
    Then I should see a successful registration confirmation
```

The Gherkin steps are implemented in:

```text
StepDefinitions/WebinarRegistrationSteps.cs
```

## Assertions

The framework uses **FluentAssertions** for readable validations.

Example:

```csharp
message.Should().NotBeNullOrWhiteSpace();

message.Should().MatchRegex(
    "(?i)(success|thank|registered|confirmation|submitted|join)");
```

## Browser Configuration

Browser and execution settings are controlled through `appsettings.json`.

Example:

```json
{
  "BaseUrl": "https://sprtechforge.com/",
  "Browser": "chrome",
  "Headless": true,
  "TimeoutSeconds": 20
}
```

For local debugging, headless execution can be disabled.

PowerShell:

```powershell
$env:HEADLESS="false"
dotnet test
```

## Running Tests Locally

### 1. Restore packages

```powershell
dotnet restore
```

### 2. Build

```powershell
dotnet build
```

### 3. Run all tests

```powershell
dotnet test
```

### 4. Run webinar registration tests

```powershell
dotnet test --filter "TestCategory=WebinarRegistration"
```

### 5. Run with visible Chrome

```powershell
$env:BROWSER="chrome"
$env:HEADLESS="false"

dotnet test --filter "TestCategory=WebinarRegistration"
```

## Expected Test Result

A successful execution should display results similar to:

```text
Test summary:
Total: 6
Failed: 0
Succeeded: 4
Skipped: 0
```

Some generated Reqnroll/NUnit discovery entries may be counted separately depending on the test adapter configuration.

## Failure Evidence

When a scenario fails, the framework captures a screenshot.

Screenshots are stored under:

```text
Artifacts/Screenshots/
```

Example:

```text
Artifacts/
└── Screenshots/
    └── 20260923_074959_Registration_is_rejected_when_required_fields_are_invalid.png
```

This makes debugging failed UI tests easier.

## Manual Test Cases

Manual test cases are maintained separately under:

```text
Test cases/Webinar_Registration_Testcases.md
```

The manual test suite covers areas such as:

* Registration form UI
* Mandatory field validation
* Email validation
* Mobile number validation
* Dropdown validation
* Terms and conditions
* Successful registration
* Invalid registration
* UI/navigation validation
* Responsive behavior
* Browser compatibility
* Regression scenarios

The automated tests implement selected high-value regression scenarios from the manual test suite.

## CI/CD

The project includes an Azure DevOps pipeline:

```text
azure-pipelines.yml
```

The pipeline performs:

```text
Restore
   ↓
Build
   ↓
Run UI Automation
   ↓
Publish Test Results
   ↓
Publish Failure Screenshots
```

The framework can also be executed through GitHub Actions.

## Git Workflow

The project is maintained in Git.

Typical workflow:

```powershell
git status

git add .

git commit -m "Update automation tests"

git push
```

The repository uses the `main` branch.

## Database Dependency

This framework intentionally **does not require database access**.

There is no dependency on:

* SQL Server
* Dapper
* Microsoft.Data.SqlClient
* SQL connection strings
* Database validation
* Database cleanup

The framework validates the registration through the application's UI and confirmation behavior.

## Key Automation Practices Demonstrated

This project demonstrates:

* C# Selenium automation
* BDD with Reqnroll
* NUnit
* Page Object Model
* Explicit waits
* Dynamic test data
* Positive and negative testing
* Form validation
* CSS selectors
* SPA automation
* Screenshot-based failure evidence
* Configuration management
* CI/CD pipeline integration
* Git/GitHub workflow
* Manual + automation testing approach

## Project Purpose

This project demonstrates an end-to-end **SDET automation approach** covering:

```text
Manual Test Design
        ↓
BDD Feature
        ↓
Selenium Automation
        ↓
Page Object Model
        ↓
Assertions
        ↓
Failure Evidence
        ↓
CI/CD
```

The framework is designed as a portfolio project demonstrating practical UI automation and test engineering practices.
