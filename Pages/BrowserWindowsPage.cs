using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowDemoQa.Pages
{
    internal class BrowserWindowsPage
    {
        private WebDriver _driver;
        private static BrowserWindowsPage _browserPage;
        private string _currentWindowHandle;
        ReadOnlyCollection<string> _windowHandles;
        By _messageXPath = By.XPath("//h1");

        public BrowserWindowsPage(WebDriver driver) => _driver = driver;
        
        public static BrowserWindowsPage GetBrowserPage() => _browserPage;
        
        public static void SetBrowserPage(BrowserWindowsPage browserPage) => _browserPage = browserPage;

        public By MakeButtonXPathLocator(string button) => By.XPath($"//button[@id='{button}']");

        public void ClickOnButton(string button)
        {
            _driver.FindElement(MakeButtonXPathLocator(button)).Click();
            _currentWindowHandle = _driver.CurrentWindowHandle;
            _windowHandles = _driver.WindowHandles;
        }

        public string ReadMessage()
        {
            SwitchToChildWindow();
            string actualMessage = _driver.FindElement(_messageXPath).Text;
            SwitchToParentWindow();
            return actualMessage;
        }

        private void SwitchToChildWindow()
        {
            foreach (string windowHandle in _windowHandles)
            {
                if (windowHandle != _currentWindowHandle)
                {
                    _driver.SwitchTo().Window(windowHandle);
                    break;
                }
            }
        }

        private void SwitchToParentWindow() => _driver.SwitchTo().Window(_currentWindowHandle);

        internal bool IsMessageDisplayed()
        {
            SwitchToChildWindow();
            bool flag = _driver.FindElement(_messageXPath).Displayed;
            SwitchToParentWindow();
            return flag;
        }
    }
}
