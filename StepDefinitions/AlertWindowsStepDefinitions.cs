using NUnit.Framework;
using OpenQA.Selenium;
using SpecFlowDemoQa.Pages;
using System;
using TechTalk.SpecFlow;

namespace SpecFlowDemoQa.StepDefinitions
{
    [Binding]
    public class AlertWindowsStepDefinitions : BaseStepDefinitions
    {
        private WebDriver _driver;
        //private HomePage? _homePage;
        private BrowserWindowsPage _browserWindowsPage;

        [When(@"I click on a ""([^""]*)"" button")]
        public void WhenIClickOnAButton(string button)
        {
            string buttonId = "";
            _driver = SharedDriver.GetDriver();
            _browserWindowsPage = new BrowserWindowsPage(_driver);
            switch (button)
            {
                case "New Tab":
                    buttonId = "tabButton";
                    break;
                case "New Window":
                    buttonId = "windowButton";
                    break;
                    default: throw new ArgumentException($"Unknown {button} button.");
            }
            _browserWindowsPage.ClickOnButton(buttonId);
        }

        [Then(@"I navigate to new tab")]
        public void ThenINavigateToNewTab()
        {
            Assert.True(true);
        }

        [Then(@"I see ""([^""]*)"" message")]
        public void ThenISeeMessage(string message)
        {
            Assert.That(_browserWindowsPage.IsMessageDisplayed(), Is.True, "The message is not displayed");
            string actualMessage = _browserWindowsPage?.ReadMessage();
            Assert.That(message, Is.EqualTo(actualMessage), "Actual message is different than expected one");
        }

        [Then(@"I navigate to new window")]
        public void ThenINavigateToNewWindow()
        {
            Assert.True(true);
        }
    }
}
