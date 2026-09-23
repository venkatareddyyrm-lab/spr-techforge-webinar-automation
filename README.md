# SPR TechForge Webinar Registration Automation

Production-oriented .NET 8 BDD automation framework for the SPR TechForge AI software testing webinar registration flow.

## Technology

- .NET 8 / C#
- Reqnroll 3.3.4 + NUnit
- Selenium WebDriver
- Selenium Manager for browser-driver management
- FluentAssertions
- Bogus
- Azure DevOps Pipelines

Selenium Manager is built into modern Selenium releases and automatically resolves browser drivers when a driver is not supplied explicitly.

## Project Structure

```text
SprTechForge_Automation_Framework/
├── Drivers/
│   └── DriverFactory.cs
├── Hooks/
│   ├── DependencyHooks.cs
│   └── TestHooks.cs
├── Pages/
│   ├── BasePage.cs
│   └── WebinarEventPage.cs
├── Helpers/
│   └── DatabaseHelper.cs
├── Models/
│   └── AttendeeRecord.cs
├── Features/
│   └── WebinarRegistration.feature
├── StepDefinitions/
│   └── WebinarRegistrationSteps.cs
├── Artifacts/
│   └── Screenshots/
├── appsettings.json
├── SprTechForge.Automation.csproj
├── azure-pipelines.yml
└── README.md
```

## Open in Visual Studio

1. Extract the ZIP.
2. Open `SprTechForge.Automation.csproj` in Visual Studio 2022/2026 with .NET 8 support.
3. Restore NuGet packages.
4. Install the Reqnroll Visual Studio extension if feature-file navigation/code generation is not already available.
5. Build the project.

## Local execution

PowerShell:

```powershell
$env:BROWSER="chrome"
$env:HEADLESS="false"
$env:DATABASE_ENABLED="true"
$env:SQL_CONNECTION_STRING="Server=...;Database=...;User Id=...;Password=...;TrustServerCertificate=True"
dotnet test --filter "TestCategory=WebinarRegistration"
```

Linux/macOS:

```bash
export BROWSER=chrome
export HEADLESS=true
export DATABASE_ENABLED=true
export SQL_CONNECTION_STRING='Server=...;Database=...;User Id=...;Password=...;TrustServerCertificate=True'
dotnet test --filter "TestCategory=WebinarRegistration"
```

## Azure DevOps

Create a secret pipeline variable named:

`SQL_CONNECTION_STRING`

The pipeline passes this value to the test process without putting the connection string in source control.

## Database contract

The database query is intentionally configurable because the target site's private database schema is not publicly exposed.

The current default configuration expects:

```sql
WebinarRegistrations
(
    Id,
    Name,
    Email,
    Phone,
    Organization,
    CreatedAt
)
```

If the application's actual schema uses different table/column names, update only:

- `Database:AttendeeByEmailSql`
- `Database:DeleteAttendeeByEmailSql`

The Dapper model remains strongly typed.

## Important test-data behavior

The positive test generates a unique email for every execution and stores it in `ScenarioContext`. The post-scenario hook attempts to remove that record using the configured delete statement.

The negative test uses an invalid email and verifies that no persisted attendee record exists for that generated test value.

## Failure evidence

When a scenario fails, `TestHooks` attempts a full-page Chrome DevTools screenshot and stores it under:

`Artifacts/Screenshots/`

The Azure DevOps pipeline publishes that directory as the `Failed-Screenshots` pipeline artifact.

## Locator strategy

The page object uses resilient CSS selectors based on semantic attributes such as:

- `name`
- `id`
- `type`
- `placeholder`
- button text/value/ARIA label

Because the target is a hash-routed SPA and its DOM can change independently of this framework, the selectors are deliberately centralized in `WebinarEventPage.cs`.
