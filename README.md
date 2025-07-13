# OpenWeatherMap API Test Automation Framework

## Overview
This is a comprehensive test automation framework for testing the OpenWeatherMap API using .NET, Reqnroll (BDD), and MSTest. The framework follows Behavior-Driven Development (BDD) approach with Gherkin syntax for test scenarios.

## Architecture & Approach

### Technology Stack
- **.NET 8.0** - Core framework
- **Reqnroll** - BDD framework
- **MSTest** - Test runner
- **RestSharp** - HTTP client for API calls
- **Newtonsoft.Json** - JSON serialization
- **FluentAssertions** - Assertion library for readable tests
- **JetBrains Rider or Visual Studio** - IDE (recommended)

### Framework Structure
```
WeatherDataApiTest/
├── Configuration/            # Test configuration
├── Features/                 # BDD scenarios in Gherkin
├── Hooks/                    # Global setup/teardown
├── Models/                   # DTOs for API response bodies
├── Services/                 # API service classes
├── StepDefinitions/          # Step implementations
├── TestResults/              # # Output folder for reports (if needed)
├── Validators/               # Shared response validation logic
```

## Setup Instructions

### Prerequisites
1. **.NET 8.0 SDK** - Download from [Microsoft .NET](https://dotnet.microsoft.com/download)
2. Download and install **Visual Studio 2022/Rider**.
3. **OpenWeatherMap API Key** - Register at [OpenWeatherMap](https://openweathermap.org/api) to obtain your API key.

### Installation Steps

1. **Clone the Repository**
   ```bash
   git clone https://github.com/rasuzze/WeatherDataApiTest.git
   cd WeatherDataApiTest
   ```

2. **Restore Dependencies**
   - If needed, restore all packages:
      ```bash
      dotnet restore
      ```

3. **Configure API Key**
    - Open `appsettings.json`
    - Replace `YOUR_API_KEY_HERE` with your actual API key
   ```json
   {
     "WeatherApi": {
       "BaseUrl": "https://api.openweathermap.org/data/2.5",
       "ApiKey": "YOUR_ACTUAL_API_KEY_HERE"
     }
   }
   ```

4. **Build the Solution**
   ```bash
   dotnet build
   ```

## Running Tests
You can run the tests using Rider, Visual Studio, or the .NET CLI.

### Command Line Execution
```bash
# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --verbosity normal

# Run tests with logger for HTML reports
dotnet test --logger "html;logfilename=TestReport.html"

# Run tests with multiple Loggers:
dotnet test --logger "trx;logfilename=TestResults.trx" --logger "html;logfilename=TestReport.html"

# With coverage:
dotnet test --collect:"XPlat Code Coverage" --logger "html;logfilename=TestReport.html"
```

### From Visual Studio
1. Open the solution in Visual Studio
2. Go to **Test Explorer**
3. Run all tests or select specific scenarios
4. View results in the Test Explorer window
5. Or view Console Output:
   - Go to **View** → **Output**
   - Select **Tests** from the dropdown
   - This shows detailed step execution from your BDD scenarios

### Test Reports
- **Console Output**: Real-time results during execution
- **HTML Reports**: Generated in `TestResults/` folder
- **TRX Files**: MSTest format for CI/CD integration
