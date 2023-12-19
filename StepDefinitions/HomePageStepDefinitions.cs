using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SpecFlowDemoQa.Pages;
using System;
using TechTalk.SpecFlow;

namespace SpecFlowDemoQa.StepDefinitions
{
    [Binding]
    public class HomePageStepDefinitions : BaseStepDefinitions
    {
        private static WebDriver? _driver;
        string _url = "https://demoqa.com/";
        HomePage? _homePage;

        [Given(@"I am on the DemoQA homepage")]
        public void GivenIAmOnTheDemoQAHomepage()
        {
            _driver = SharedDriver.GetDriver();
            _driver.Navigate().GoToUrl(_url);
            _homePage = new HomePage(_driver);
            HomePage.SetHomePage(_homePage);
        }


        [When(@"I click on the ""([^""]*)"" link")]
        public void WhenIClickOnTheLink(string menu)
        {
            _homePage.GoTo(menu);
        }

        [Then(@"I am navigated to the ""([^""]*)"" page")]
        public void ThenIAmNavigatedToThePage(string page)
        {
            Assert.That(_homePage.GetUrl(), Is.EqualTo(_url + page.ToLower()));
        }

        [Then(@"I should see the following links:")]
        public void ThenIShouldSeeTheFollowingLinks(Table expectedLinks)
        {
            var expectedLinksList = expectedLinks.Rows.Select(row => row["Link"]).ToList();
            foreach (string link in expectedLinksList)
            {
                Assert.That(_homePage.IsMenuDisplayed(link), Is.True);
            }
        }

        [Then(@"Forms")]
        public void ThenForms()
        {
            throw new PendingStepException();
        }

        [Then(@"Alerts, Modals, & Browser Windows")]
        public void ThenAlertsModalsBrowserWindows()
        {
            throw new PendingStepException();
        }

        [Then(@"Frames & Elements")]
        public void ThenFramesElements()
        {
            throw new PendingStepException();
        }

        [Then(@"Drag and Drop")]
        public void ThenDragAndDrop()
        {
            throw new PendingStepException();
        }

        [Then(@"Inputs")]
        public void ThenInputs()
        {
            throw new PendingStepException();
        }

        [Then(@"Data Tables")]
        public void ThenDataTables()
        {
            throw new PendingStepException();
        }
    }
}
