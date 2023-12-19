using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SpecFlowDemoQa.Pages
{
    internal class AutoCompletePage
    {
        private readonly IWebDriver _driver;
        private List<IWebElement> _dropDownList;
        
        private readonly By _inputField = By.XPath("//div[contains(@class,'auto-complete__value-container--is-multi')]");
        private readonly By _listItems = By.XPath("//div[contains(@class,'auto-complete__option')]");
        private readonly By _googleAdd = By.XPath("//div[@id='google_center_div']/div/a/amp-img/img[contains(@class, 'i-amphtml-replaced-content')]");
        private readonly By _inputFieldColors = By.XPath("//div[contains(@class, 'auto-complete__multi-value__label')]");

        public AutoCompletePage(IWebDriver driver)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public By MakeInputFieldXPathSelector(string inputField) => By.XPath($"//div[@id='{inputField}']//div[@class='auto-complete__input']");
        public By MakeColorDeleteButtonXPathLocator(string color) => By.XPath($"//div[text()='{color}']/following-sibling::div[contains(@class, 'auto-complete__multi-value__remove')]");
        public By MakeColorMultiFieldXPathLocator(string color) => By.XPath($"//div[contains(@class, 'auto-complete__multi-value__label') and text()='{color}']");

        public IWebElement? FindElement(By by, TimeSpan? timeout = null)
        {
            WebDriverWait wait = new WebDriverWait(_driver, timeout ?? TimeSpan.FromSeconds(5));
            try
            {
                return wait.Until(driver => driver.FindElement(by));
            }
            catch
            {
                throw new Exception("Couldn't find an element");
            }
            
        }

        public List<IWebElement> FindElements(By by, TimeSpan? timeout = null)
        {
            WebDriverWait wait = new WebDriverWait(_driver, timeout ?? TimeSpan.FromSeconds(5));
            return wait.Until(driver => driver.FindElements(by).ToList());
        }

        public void SubmitMultiInput(List<string> values)
        {
            // Need to wait for Google ads to appear to avoid misclicking
            List<IWebElement> googleAdd = FindElements(_googleAdd);
            By multiInputField = MakeInputFieldXPathSelector("autoCompleteMultipleContainer");
            IWebElement inputField = FindElement(multiInputField);
            Actions action = new Actions(_driver);
            action.Click(inputField).Perform();
            foreach (var value in values)
            {
                action.SendKeys(inputField, value).Perform();
                action.SendKeys(inputField, Keys.Enter).Perform();
            }
        }

        public void SubmitKeysToMultiInputField(string keys)
        {
            // Need to wait for Google ads to appear to avoid misclicking
            List<IWebElement> googleAdd = FindElements(_googleAdd);
            By multiInputField = MakeInputFieldXPathSelector("autoCompleteMultipleContainer");
            IWebElement inputField = FindElement(multiInputField);
            Actions action = new Actions(_driver);
            action.Click(inputField).Perform();
            action.SendKeys(inputField, keys).Perform();
            FindDropDownItems();
        }

        public void SubmitKeys(string input, string field)
        {
            if (field == "multifield") SubmitKeysToMultiInputField(input);
        }

        public void FindDropDownItems()
        {
            _dropDownList = _driver.FindElements(_listItems).ToList();
        }

        internal int CountDropDownItems() => _dropDownList.Count;

        internal bool ListContainsString(string str) =>
            _dropDownList.All(item => item.Text.ToLower().Contains(str.ToLower()));

        internal void DeleteColors(List<string> colors)
        {
            foreach (string color in colors) FindElement(MakeColorDeleteButtonXPathLocator(color)).Click();
        }

        internal bool OnlyColorsInList(List<string> colors)
        {
           if (colors.Count() != FindElements(_inputFieldColors).ToList().Count()) return false;
           foreach (string color in colors)
            {
                try
                {
                    FindElement(MakeColorMultiFieldXPathLocator(color));
                }
                catch
                {
                    Console.WriteLine($"{color} color missing from the list");
                    return false;
                }
            }

            return true;
        }


    }
}
