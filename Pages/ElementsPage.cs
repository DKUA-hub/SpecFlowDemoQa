using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowDemoQa.Pages
{
    internal class ElementsPage 
    {
        WebDriver _driver;

        private By MakeXPathSelector(string item) => By.XPath($"//span[text()='{item}']");
        public ElementsPage(WebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement FindElement(By name, TimeSpan? timeout = null)
        {
            WebDriverWait wait = new WebDriverWait(_driver, timeout ?? TimeSpan.FromSeconds(10));
            return wait.Until(_driver => _driver.FindElement(name));
        }

        public void GoTo(string menu)
        {
            FindElement(MakeXPathSelector(menu)).Click();
        }
    }
}
