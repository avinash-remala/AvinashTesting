using System.Diagnostics;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using NUnit.Framework;
using AvinashTesting.Pages;

namespace AvinashTesting.StepDefinitions
{
    [Binding]
    public class AviLoginTestSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private LoginPage? _loginPage;
        private LogOut? _loggedInPage;

        public AviLoginTestSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given("the user is on the Avi login page")]
        public void GivenTheUserIsOnTheAviLoginPage()
        {
            var driver = (IWebDriver)_scenarioContext["WebDriver"];
            _loginPage = new LoginPage(driver);
            _loginPage.GoToLoginPage();
        }

        [When("the user enters username '(.*)' and password '(.*)'")]
        public void WhenTheUserEntersUsernameAndPassword(string username, string password)
        {
            _loginPage!.EnterUsername(username);
            _loginPage.EnterPassword(password);
        }

        [When("clicks the login button")]
        public void WhenClicksTheLoginButton()
        {
            _loginPage!.ClickLoginButton();
        }

        [Then("the user should be navigated to the secure area")]
        public void ThenTheUserShouldBeNavigatedToTheSecureArea()
        {
            var driver = (IWebDriver)_scenarioContext["WebDriver"];
            _loggedInPage = new LogOut(driver);
            NUnit.Framework.Assert.That(_loggedInPage.IsLoggedInPageDisplayed(), Is.True);
        }

        [When("the user clicks the logout button")]
        public void WhenTheUserClicksTheLogoutButton()
        {
            _loggedInPage!.ClickLogoutButton();
        }

        [Then("the user should be logged out and redirected to the login page")]
        public void ThenTheUserShouldBeLoggedOutAndRedirectedToTheLoginPage()
        {
            NUnit.Framework.Assert.That(_loggedInPage!.IsLogoutSuccessful(), Is.True);
        }
    }
}
