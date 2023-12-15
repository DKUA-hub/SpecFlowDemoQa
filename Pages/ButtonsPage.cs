using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace SpecFlowDemoQa.Pages
{
    internal class ButtonsPage
    {
        private WebDriver _driver;

        public ButtonsPage(WebDriver driver)
        {
            _driver = driver;
        }

        internal void ClickOnAButton(string action, string button)
        {
            IWebElement buttonElement;
            Actions click = new Actions(_driver);
            By buttonXPathLocator;

            switch (button)
            {
                case "Double Click Me":
                    buttonXPathLocator = MakeButtonXPathLocator("doubleClickBtn");
                    break;
                case "Right Click Me":
                    buttonXPathLocator = MakeButtonXPathLocator("rightClickBtn");
                    break;
                case "Click Me":
                    buttonXPathLocator = By.XPath($"//button[text()='{button}']");
                    break;
                default: 
                    throw new Exception($"Unknown button \"{button}\" can't be located.");
            }

            buttonElement = _driver.FindElement(buttonXPathLocator);

            switch (action)
            {
                case "double click":
                    click.DoubleClick(buttonElement).Perform();
                    break;
                case "right click":
                    click.ContextClick(buttonElement).Perform();
                    break;
                case "one click":
                    buttonElement.Click();
                    break;
                default: 
                    throw new Exception($"Unknown action \"{action}\" can't be performed");
            }
        }

        private By MakeButtonXPathLocator(string button) => By.XPath($"//button[@id='{button}']");
        private By MakeMessageXPathLocator(string message) => By.XPath($"//p[@id='{message}']");

        internal bool IsMessageDisplayed(string message)
        {
            string messageId = "";
            switch (message)
            {
                case "You have done a double click":
                    messageId = "doubleClickMessage";
                    break;
                case "You have done a right click":
                    messageId = "rightClickMessage";
                    break;
                case "You have done a dynamic click":
                    messageId = "dynamicClickMessage";
                    break;
                default: 
                    throw new Exception($"Unknown message \"{messageId}\" can't be handled");
            }

            return _driver.FindElement(MakeMessageXPathLocator(messageId)).Displayed;
        }
    }
}
