using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowDemoQa.Pages
{
    internal class HomePage
    {
        WebDriver _driver;
        IJavaScriptExecutor? _js;
        public HomePage(WebDriver driver)
        {
            _driver = driver;
        }

        public By MakeXPathMenuSelector(string menu) => By.XPath($"//h5[text()='{menu}']");

        public IWebElement FindElement(By by) => _driver.FindElement(by);

        public void GoTo(string menu)
        {
            try
            {
                IWebElement menuLink = FindElement(MakeXPathMenuSelector(menu));
                _js = _driver;
                _js.ExecuteScript($"window.scroll(0, {menuLink.Location.Y / 2})");

                menuLink.Click();
            }
            catch (Exception e)
            {
                throw new Exception($"Couldn't navigate by {menu} link");
            }
        }

        public string GetUrl()
        {
            return _driver.Url;
        }

        internal bool IsMenuDisplayed(string link)
        {
            try
            {
                IWebElement menuLink = FindElement(MakeXPathMenuSelector(link));
                _js = _driver;
                _js.ExecuteScript($"window.scroll(0, {menuLink.Location.Y / 2})");

                return menuLink.Displayed;
            }
            catch (Exception e)
            {
                throw new Exception($"Couldn't find {link} menu");
            }
        }
    }
}
