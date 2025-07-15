using System.Diagnostics;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using NUnit.Framework;
using AvinashTesting.Pages;

namespace AvinashTesting.StepDefinitions
{
    [Binding]
    public class InvalidLoginTestSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private InvalidLoginPage? _invalidLoginPage;

        public InvalidLoginTestSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given("the user is on the invalid login page")]
        public void GivenTheUserIsOnTheInvalidLoginPage()
        {
            var driver = (IWebDriver)_scenarioContext["WebDriver"];
            _invalidLoginPage = new InvalidLoginPage(driver);
            _invalidLoginPage.GoToLoginPage();
        }

        [When("the user enters invalid username '(.*)' and password '(.*)'")]
        public void WhenTheUserEntersInvalidUsernameAndPassword(string username, string password)
        {
            var driver = (IWebDriver)_scenarioContext["WebDriver"];
            _invalidLoginPage = new InvalidLoginPage(driver);
            _invalidLoginPage.EnterUsername(username);
            _invalidLoginPage.EnterPassword(password);
        }

        [When("clicks the invalid login button")]
        public void WhenClicksTheInvalidLoginButton()
        {
            _invalidLoginPage!.ClickLoginButton();
        }

        [Then("the user should see an error message for invalid username")]
        public void ThenTheUserShouldSeeAnErrorMessageForInvalidUsername()
        {
            var driver = (IWebDriver)_scenarioContext["WebDriver"];
            _invalidLoginPage = new InvalidLoginPage(driver);
            NUnit.Framework.Assert.That(_invalidLoginPage.IsInvalidUsernameErrorDisplayed(), Is.True, 
                "Expected to see an error message for invalid username");
        }

        [Then("the user should see an error message for invalid password")]
        public void ThenTheUserShouldSeeAnErrorMessageForInvalidPassword()
        {
            var driver = (IWebDriver)_scenarioContext["WebDriver"];
            _invalidLoginPage = new InvalidLoginPage(driver);
            NUnit.Framework.Assert.That(_invalidLoginPage.IsInvalidPasswordErrorDisplayed(), Is.True, 
                "Expected to see an error message for invalid password");
        }

        [Then("the user should see an error message for invalid credentials")]
        public void ThenTheUserShouldSeeAnErrorMessageForInvalidCredentials()
        {
            var driver = (IWebDriver)_scenarioContext["WebDriver"];
            _invalidLoginPage = new InvalidLoginPage(driver);
            NUnit.Framework.Assert.That(_invalidLoginPage.IsInvalidCredentialsErrorDisplayed(), Is.True, 
                "Expected to see an error message for invalid credentials");
        }
    }
}
