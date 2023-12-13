using OpenQA.Selenium;
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
        By _textBoxMenu   = By.XPath("//span[text()='Text Box']");
        By _checkBoxMenu  = By.XPath("//span[text()='Check Box']");
        By _webTablesMenu = By.XPath("//span[text()='Web Tables']");
        By _buttonsMenu   = By.XPath("//span[text()='Buttons']");
        public ElementsPage(WebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement FindElement(By name)
        {
            return _driver.FindElement(name);
        }

        public void GoToTextBox()
        {
            FindElement(_textBoxMenu).Click();
        }

        public void GoToCheckBox()
        {
            FindElement(_checkBoxMenu).Click();
        }

        public void GoToButton()
        {
            FindElement(_buttonsMenu).Click();
        }

        public void GoToWebTables()
        {
            FindElement(_webTablesMenu).Click();
        }
        public void ClickOn(string category)
        {

        }


    }
}
