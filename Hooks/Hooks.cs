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
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-web-security");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--no-sandbox");
            
            var driver = new ChromeDriver(options);
            _scenarioContext["WebDriver"] = driver;
            
            Console.WriteLine($"🚀 Starting scenario: {_scenarioContext.ScenarioInfo.Title}");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Console.WriteLine($"🔍 AfterScenario called for: {_scenarioContext.ScenarioInfo.Title}");
            
            if (_scenarioContext.ContainsKey("WebDriver"))
            {
                var driver = (IWebDriver)_scenarioContext["WebDriver"];
                
                try
                {
                    // Debug: Check test error status
                    if (_scenarioContext.TestError != null)
                    {
                        Console.WriteLine($"❌ Scenario failed: {_scenarioContext.ScenarioInfo.Title}");
                        Console.WriteLine($"🔍 Error: {_scenarioContext.TestError.Message}");
                        Console.WriteLine($"📸 Attempting to take screenshot...");
                        TakeScreenshot(driver, "FAILED");
                    }
                    else
                    {
                        Console.WriteLine($"✅ Scenario passed: {_scenarioContext.ScenarioInfo.Title}");
                        Console.WriteLine($"📸 Taking screenshot for passed test (optional)...");
                        // Uncomment to take screenshots for passed tests too
                        // TakeScreenshot(driver, "PASSED");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ Error during cleanup: {ex.Message}");
                    Console.WriteLine($"🔍 Stack trace: {ex.StackTrace}");
                }
                finally
                {
                    Console.WriteLine($"🔚 Closing browser for: {_scenarioContext.ScenarioInfo.Title}");
                    driver.Quit();
                }
            }
            else
            {
                Console.WriteLine($"⚠️ No WebDriver found in scenario context!");
            }
        }

        private void TakeScreenshot(IWebDriver driver, string status)
        {
            try
            {
                Console.WriteLine($"📸 TakeScreenshot method called with status: {status}");
                
                string projectDir = Directory.GetCurrentDirectory();
                string screenshotsDir = Path.Combine(projectDir, "TestResults", "Screenshots");
                
                Console.WriteLine($"📁 Project directory: {projectDir}");
                Console.WriteLine($"📁 Screenshots directory: {screenshotsDir}");
                
                // Create directory
                Directory.CreateDirectory(screenshotsDir);
                Console.WriteLine($"✅ Screenshots directory created/verified");

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
                string scenarioName = SanitizeFileName(_scenarioContext.ScenarioInfo.Title);
                string fileName = $"{status}_{scenarioName}_{timestamp}.png";
                string fullPath = Path.Combine(screenshotsDir, fileName);

                Console.WriteLine($"📸 Screenshot file path: {fullPath}");

                // Take screenshot
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(fullPath);

                Console.WriteLine($"✅ Screenshot saved successfully: {fileName}");
                
                // Verify file exists
                if (File.Exists(fullPath))
                {
                    Console.WriteLine($"✅ Screenshot file verified: {new FileInfo(fullPath).Length} bytes");
                }
                else
                {
                    Console.WriteLine($"❌ Screenshot file not found after save!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Screenshot failed: {ex.Message}");
                Console.WriteLine($"🔍 Screenshot stack trace: {ex.StackTrace}");
            }
        }

        private string SanitizeFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return "UnknownTest";

            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char c in invalidChars)
            {
                fileName = fileName.Replace(c, '_');
            }
            
            fileName = fileName.Replace(' ', '_')
                              .Replace("'", "")
                              .Replace("\"", "")
                              .Replace(":", "_")
                              .Replace("/", "_")
                              .Replace("\\", "_");
            
            if (fileName.Length > 50)
                fileName = fileName.Substring(0, 50);
                
            return fileName;
        }

        public static IWebDriver GetDriver(ScenarioContext scenarioContext)
        {
            return (IWebDriver)scenarioContext["WebDriver"];
        }
    }
}
