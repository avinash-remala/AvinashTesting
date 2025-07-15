using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace AvinashTesting.Pages
{
    public class LogOut
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public LogOut(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public bool IsLoggedInPageDisplayed()
        {
            try
            {
                var successHeader = _driver.FindElement(By.XPath("//*[text()='Congratulations student. You successfully logged in!']"));
                return successHeader.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void ClickLogoutButton()
        {
            var logoutButton = _wait.Until(driver => driver.FindElement(By.LinkText("Log out")));
            logoutButton.Click();
        }

        public bool IsLogoutSuccessful()
        {
            try
            {
                // Wait for the login page to load after logout
                _wait.Until(driver => driver.FindElement(By.Id("username")));
                return _driver.Url.Contains("practice-test-login");
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetCurrentUrl()
        {
            return _driver.Url;
        }
    }
}
