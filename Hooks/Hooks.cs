using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using System;
using System.IO;

namespace AvinashTesting.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        
        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            _scenarioContext["WebDriver"] = driver;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_scenarioContext.ContainsKey("WebDriver"))
            {
                var driver = (IWebDriver)_scenarioContext["WebDriver"];
                
                // Take screenshot if test failed
                if (_scenarioContext.TestError != null)
                {
                    TakeScreenshot(driver);
                }
                
                driver.Quit();
            }
        }

        private void TakeScreenshot(IWebDriver driver)
        {
            try
            {
                // Create directory
                string dir = Path.Combine(Directory.GetCurrentDirectory(), "TestResults", "Screenshots");
                Directory.CreateDirectory(dir);

                // Create filename
                string testName = _scenarioContext.ScenarioInfo.Title.Replace(" ", "_");
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string fileName = $"FAILED_{testName}_{timestamp}.png";
                string fullPath = Path.Combine(dir, fileName);

                // Take and save screenshot
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(fullPath);

                Console.WriteLine($"📸 Screenshot saved: {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Screenshot failed: {ex.Message}");
            }
        }
    }
}
