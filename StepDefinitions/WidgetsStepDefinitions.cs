using FluentAssertions.Equivalency;
using NUnit.Framework;
using OpenQA.Selenium;
using SpecFlowDemoQa.Pages;
using System;
using TechTalk.SpecFlow;

namespace SpecFlowDemoQa.StepDefinitions
{
    [Binding]
    public class WidgetsStepDefinitions
    {
        WebDriver _driver;
        AutoCompletePage _autoCompletePage;

        
        [When(@"I enter ""([^""]*)"" in the ""([^""]*)"" field")]
        public void WhenIEnterIntoInputField(string input, string field)
        {
            string fieldId = "singlefield";
            if (field == "Type multiple color names") fieldId = "multifield";
            _driver = SharedDriver.GetDriver();
            _autoCompletePage = new AutoCompletePage(_driver);
            _autoCompletePage.SubmitKeys(input, fieldId);

        }

        [Then(@"the dropdown list suggests (.*) options")]
        public void ThenTheDropdownListSuggestsOptions(int expectedCount)
        {
            Assert.That(_autoCompletePage.CountDropDownItems(), Is.EqualTo(expectedCount), "Count of items in dropdown list doesn't match expected value");
        }

        [Then(@"each list item contains ""([^""]*)""")]
        public void ThenEachListItemContains(string str)
        {
            Assert.That(_autoCompletePage.ListContainsString(str), Is.True, $"At least one item doesn't include string '{str}'");
        }

        [When(@"I enter colors in the ""([^""]*)"" field")]
        public void WhenIEnterColorsInTheField(string field, Table table)
        {
            _driver = SharedDriver.GetDriver();
            _autoCompletePage = new AutoCompletePage(_driver);
            List<string> colors = TableToList(table);
            if (field == "Type multiple color names") _autoCompletePage.SubmitMultiInput(colors);
        }

        private static List<string> TableToList(Table table)
        {
            return table.Rows.Select(row => row["Color"]).ToList();
        }

        [When(@"I delete colors from the input field")]
        public void WhenIDeleteColorsFromTheInputField(Table table)
        {
            List<string> colors = TableToList(table);
            _autoCompletePage.DeleteColors(colors);
        }

        [Then(@"Input field contains colors")]
        public void ThenInputFieldContainsColors(Table table)
        {
            List<string> colors = TableToList(table);
            Assert.That(_autoCompletePage.OnlyColorsInList(colors), Is.True, "Can't find all colors");
        }

        [When(@"click on a ""([^""]*)"" button")]
        public void WhenClickOnAButton(string start)
        {
            throw new PendingStepException();
        }

        [When(@"wait untill the progress is ""([^""]*)""")]
        public void WhenWaitUntillTheProgressIs(string p0)
        {
            throw new PendingStepException();
        }

        [Then(@"The button title chages to ""([^""]*)""")]
        public void ThenTheButtonTitleChagesTo(string reset)
        {
            throw new PendingStepException();
        }


    }
}
