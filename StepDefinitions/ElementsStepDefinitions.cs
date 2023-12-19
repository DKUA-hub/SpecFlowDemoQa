using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SpecFlowDemoQa.Pages;
using System;
using System.Security.AccessControl;
using System.Security.Policy;
using TechTalk.SpecFlow;

namespace SpecFlowDemoQa.StepDefinitions
{
    [Binding]
    public class ElementsStepDefinitions : BaseStepDefinitions
    {
        private static WebDriver? _driver;
        HomePage? _homePage;
        ElementsPage? _elementsPage;
        TextBoxPage? _textBoxPage;
        CheckBoxPage? _checkBoxPage;
        WebTablesPage? _webTablesPage;
        ButtonsPage? _buttonsPage;

        [Given(@"I navigate to the ""([^""]*)"" menu")]
        public void GivenINavigateToTheMenu(string menu)
        {
            _driver = SharedDriver.GetDriver();
            _homePage = HomePage.GetHomePage();
            _homePage?.GoTo(menu);
            if (_driver is not null) SharedDriver.SetDriver(_driver);
        }

        [When(@"I select ""([^""]*)"" from the menu")]
        public void WhenISelectFromTheMenu(string menu)
        {
            _driver = SharedDriver.GetDriver();
            _elementsPage = new ElementsPage(_driver);
            _elementsPage?.GoTo(menu);

            if (_driver is not null)
            {
                switch (menu)
                {
                    case "Text Box":
                        _textBoxPage = new TextBoxPage(_driver);
                        break;
                    case "Check Box":
                        _checkBoxPage = new CheckBoxPage(_driver);
                        break;
                    case "Web Tables":
                        _webTablesPage = new WebTablesPage(_driver);
                        break;
                    case "Buttons":
                        _buttonsPage = new ButtonsPage(_driver);
                        break;
                    default: break;
                }
            }
        }

        [When(@"I enter the following user information:")]
        public void WhenIEnterTheFollowingUserInformation(Table table)
        {
            foreach (var raw in table.Rows)
            {
                string fieldName = raw["Field"];
                string value = raw["Value"];

                switch (fieldName?.ToLower())
                {
                    case "full name":
                        _textBoxPage?.FillInName(value); break;
                    case "email":
                        _textBoxPage?.FillInEmail(value); break;
                    case "permanent address":
                        _textBoxPage?.FillInPermanentAddress(value); break;
                    case "current address":
                        _textBoxPage?.FillInCurrentAddress(value); break;
                    default: break;
                }

            }
        }

        [When(@"I click the ""([^""]*)"" button")]
        public void WhenIClickTheButton(string submit)
        {
            _textBoxPage?.SubmitData();
        }

        [Then(@"I should see additional text box")]
        public void ThenIShouldSeeAdditionalTextBox()
        {
            Assert.That(_textBoxPage?.IsOutputDisplayed(), Is.True);
        }

        [Then(@"I verify the submitted user information in the text box contains")]
        public void ThenIVerifyTheSubmittedUserInformationInTheTextBoxContains(Table table)
        {
            foreach (var raw in table.Rows)
            {
                string field = raw["Field"];
                string value = raw["Value"];

                switch (field?.ToLower())
                {
                    case "full name":
                        Assert.That(_textBoxPage?.IsNameEqual(value), Is.True, "Full Name comparison failed"); break;
                    case "email":
                        Assert.That(_textBoxPage?.IsEmailEqual(value), Is.True, "Email comparison failed"); break;
                    case "current address":
                        Assert.That(_textBoxPage?.IsCurentAddressEqual(value), Is.True, "Current Address comparison failed"); break;
                    case "permanent address":
                        Assert.That(_textBoxPage?.IsPermanentAddressEqual(value), Is.True, "Permanent Address comparison failed"); break;
                    default: break;
                }
            }
        }

        [When(@"I expand ""([^""]*)"" folder")]
        public void WhenIExpandFolder(string folder)
        {
            IWebElement? expandButton = _checkBoxPage?.FindButton(folder);
            expandButton?.Click();
        }

        [When(@"I select ""([^""]*)"" folder")]
        public void WhenISelectFolder(string folder)
        {
            IWebElement? checkBox = _checkBoxPage?.FindCheckBox(folder);
            checkBox?.Click();
        }

        [When(@"I select ""([^""]*)"" from ""([^""]*)"" folder")]
        public void WhenISelectFromFolder(string item, string folder)
        {
            _checkBoxPage?.ExpandFolder(folder);
            _checkBoxPage?.SelectItem(item);
        }

        [When(@"I select each item from ""([^""]*)"" folder")]
        public void WhenISelectEachItemFromFolder(string folder)
        {
            _checkBoxPage?.SelectEveryItem(folder);
        }

        [When(@"I select ""([^""]*)"" folder by click on its name")]
        public void WhenISelectFolderByClickOnItsName(string folder)
        {
            _checkBoxPage?.SelectFolderByClickOnIt(folder);
        }

        [Then(@"I see output message ""([^""]*)""")]
        public void ThenISeeOutputMessage(string expectedResult)
        {
            Assert.That(_checkBoxPage?.IsResultDisplayed(), Is.True);
            string[]? displayedItems = _checkBoxPage?.GetResultArray();
            string[] expectedResultArray = expectedResult.Split();
            Assert.AreEqual(expectedResultArray, displayedItems, "Result comparison failed");
        }

        [When(@"I click on ""([^""]*)"" column")]
        public void WhenIClickOnColumn(string columnName)
        {
            _webTablesPage?.ClickOnColumn(columnName);
        }

        [Then(@"values in the Salary column should be in ascending order")]
        public void ThenValuesInTheSalaryColumnShouldBeInAscendingOrder()
        {
            Assert.That(_webTablesPage?.IsOrderAscending(), Is.True,"Salary values are not in ascending order");
        }

        [When(@"I delete the second row with name ""([^""]*)""")]
        public void WhenIDeleteTheSecondRowWithName(string firstName)
        {
            _webTablesPage?.DeleteRowWithFirstName(firstName);
        }

        [Then(@"the table should have only (\d+) rows")]
        public void ThenTheTableShouldHaveOnlyTwoRows(int expectedCount)
        {
            Assert.That(_webTablesPage?.RowsCount(), Is.EqualTo(expectedCount), "Rows count doesn't match expected count value");
        }

        [Then(@"the ""([^""]*)"" column should not contain the value ""([^""]*)""")]
        public void ThenTheColumnShouldNotContainTheValue(string department, string depName)
        {
            Assert.That(_webTablesPage?.ColumnContainsValue(department, depName), Is.False, $"{department} still contains {depName}");
        }

        [When(@"I ""([^""]*)"" on a ""([^""]*)""")]
        public void WhenIOnClickOnAButton(string action, string button)
        {
            _buttonsPage?.ClickOnAButton(action, button);
        }

        [Then(@"""([^""]*)"" appears on the screen")]
        public void ThenMessageAppearsOnTheScreen(string message)
        {
            Assert.That(_buttonsPage?.IsMessageDisplayed(message), Is.True, $"Message {message} didn't appear");
        }
    }
}
