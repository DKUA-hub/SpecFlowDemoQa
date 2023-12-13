using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowDemoQa.Pages
{
    internal class CheckBoxPage
    {
        WebDriver _driver;
        By _button = By.XPath("//span/button[@type='button']");
        By _buttonTitle = By.XPath("..//label/span[@class='rct-title']");
        By _checkBox = By.XPath("//span[@class='rct-checkbox']");
        By _checkBoxTitle = By.XPath("..//span[@class='rct-title']");
        By _lable = By.XPath("//label/span[@class='rct-title']");
        By _downloads = By.XPath("//span/label/span[text()='Downloads']");
        By _result = By.XPath("//div[@id='result']");

        IJavaScriptExecutor _js;

        public CheckBoxPage(WebDriver driver)
        {
            _driver = driver;
        }

        public string ButtonLabelXPathLocator(string label) => $"..//label/span[text()='{label}']";

        public string CheckBoxXPathLocator(string label) => $"..//span[text()='{label}']";

        public IWebElement FindButton(string label)
        {
            Console.WriteLine($"Looking for button {label}");
            List<IWebElement> allButtons = _driver.FindElements(_button).ToList();
            foreach (IWebElement button in allButtons)
            {
                if (button.FindElement(_buttonTitle).Text.Contains(label))
                    return button;
            }
            return null;
        }

        public IWebElement FindCheckBox(string label)
        {
            List<IWebElement> allCheckBoxes = _driver.FindElements(_checkBox).ToList();
            foreach (IWebElement checkBox in allCheckBoxes)
            {
                if (checkBox.FindElement(_checkBoxTitle).Text.Contains(label))
                    return checkBox;
            }
            return null;
        }

        public IWebElement FindElement(string label) 
        {
            List<IWebElement> allElements = _driver.FindElements(_lable).ToList();
            foreach (IWebElement element in allElements)
            {
                if (element.Text.Equals(label))
                {
                    return element;
                }
            }
            return null;
        }

        public IWebElement FindResult()
        { 
            return _driver.FindElement(_result);
        }
            

        public bool IsFolderVisible()
        {
            return _driver.FindElement(_downloads).Displayed;
        }

        internal void ExpandFolder(string folder)
        {
            IWebElement button = FindButton(folder).FindElement(By.XPath(".//*[contains(@class,'rct-icon')]"));
            if (button.GetAttribute("class").Contains("close"))
            {
                FindButton(folder).Click();
            }
        }

        internal void SelectEveryItem(string folder)
        {
            ExpandFolder(folder);
            List<IWebElement> itemsList = FindButton(folder).FindElements(By.XPath("../..//ol/li/span/label/span[@class='rct-checkbox']")).ToList();
            foreach (IWebElement item in itemsList)
            {
                try
                {
                    item.Click();
                }
                catch
                {
                    _js = _driver;
                    _js.ExecuteScript("window.scroll(0,200)");
                    item.Click();
                }
            }   
        }

        internal void SelectFolderByClickOnIt(string folder)
        {
            IWebElement label = FindElement(folder);
            label.Click();
         }

        internal void SelectItem(string item)
        {
            FindCheckBox(item).Click();
        }

        public bool IsResultDisplayed()
        {
            return _driver.FindElement(_result).Displayed;
        }

        internal string[] GetResultArray()
        {
            List<IWebElement> resultItems = FindResult().FindElements(By.XPath(".//span")).ToList();
            string resultString = "";
            foreach(IWebElement item in resultItems)
            {
                resultString = resultString + item.Text + " ";
                
            }
            resultString = resultString.Trim();
            string[] resultArray = resultString.Split();
            return resultArray;
        }
    }
}
