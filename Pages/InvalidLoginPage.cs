using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace AvinashTesting.Pages
{
    public class InvalidLoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public InvalidLoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void GoToLoginPage()
        {
            _driver.Navigate().GoToUrl("https://practicetestautomation.com/practice-test-login/");
        }

        public void EnterUsername(string username)
        {
            _driver.FindElement(By.Id("username")).SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            _driver.FindElement(By.Id("password")).SendKeys(password);
        }

        public void ClickLoginButton()
        {
            _driver.FindElement(By.Id("submit")).Click();
        }

        public bool IsErrorMessageDisplayed()
        {
            try
            {
                var errorMessage = _wait.Until(driver => driver.FindElement(By.Id("error")));
                return errorMessage.Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetErrorMessage()
        {
            try
            {
                var errorMessage = _wait.Until(driver => driver.FindElement(By.Id("error")));
                return errorMessage.Text;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public bool IsInvalidUsernameErrorDisplayed()
        {
            return IsErrorMessageDisplayed() && GetErrorMessage().Contains("username");
        }

        public bool IsInvalidPasswordErrorDisplayed()
        {
            return IsErrorMessageDisplayed() && GetErrorMessage().Contains("password");
        }

        public bool IsInvalidCredentialsErrorDisplayed()
        {
            return IsErrorMessageDisplayed();
        }
    }
}
