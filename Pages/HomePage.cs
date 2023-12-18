using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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
        private static HomePage? _homePage;
        IJavaScriptExecutor _js;
        
        public HomePage(WebDriver driver)
        {
            _driver = driver;
        }

        public static HomePage? GetHomePage()
        {
            return _homePage;
        }

        public static void SetHomePage(HomePage homePage)
        {
            _homePage = homePage;
        }

        public By MakeXPathMenuSelector(string menu) => By.XPath($"//h5[text()='{menu}']");

        public IWebElement FindElement(By by) => _driver.FindElement(by);

        public void GoTo(string menu)
        {
            IWebElement menuLink = FindElement(MakeXPathMenuSelector(menu));

            try
            {
                menuLink.Click();
            }
            catch
            {
                _js = _driver;
                _js.ExecuteScript($"window.scroll(0,{menuLink.Location.Y/2})");
                menuLink.Click();
            }
        }

        public string GetUrl()
        {
            return _driver.Url;
        }

        internal bool IsMenuDisplayed(string link)
        {
            IWebElement menuLink = FindElement(MakeXPathMenuSelector(link));
            return menuLink.Displayed;
        }
    }
}
