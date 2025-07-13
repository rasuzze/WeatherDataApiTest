namespace WeatherDataApiTest.Hooks;

using Reqnroll;

[Binding]
public class BeforeAfterScenario
{
    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        Console.WriteLine("Starting Weather API Test Suite...");
        Console.WriteLine($"Test run started at: {DateTime.Now}");
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        Console.WriteLine("Weather API Test Suite completed.");
        Console.WriteLine($"Test run finished at: {DateTime.Now}");
    }

    [BeforeScenario]
    public void BeforeScenario(ScenarioContext scenarioContext)
    {
        Console.WriteLine($"Starting scenario: {scenarioContext.ScenarioInfo.Title}");
    }

    [AfterScenario]
    public void AfterScenario(ScenarioContext scenarioContext)
    {
        Console.WriteLine($"Scenario {(scenarioContext.TestError != null ? "failed" : "passed")}: {scenarioContext.ScenarioInfo.Title}");

        var keysToClear = new[] { "response", "weatherResponse", "errorResponse", "apiKey" };

        foreach (var key in keysToClear)
        {
            if (scenarioContext.ContainsKey(key))
                scenarioContext.Remove(key);
        }
    }
}