using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace AvinashTesting.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public LoginPage(IWebDriver driver)
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

        public bool IsLoginSuccessful()
        {
            var successHeader = _driver.FindElement(By.XPath("//*[text()='Congratulations student. You successfully logged in!']"));
            return successHeader.Displayed;
        }
    }
}
