using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowDemoQa.Pages
{
    internal class TextBoxPage
    {
        WebDriver _driver;
        By _nameField             = By.XPath("//input[@id='userName']");
        By _emailField            = By.XPath("//input[@id='userEmail']");
        By _permanentaddressField = By.XPath("//textarea[@id='permanentAddress']");
        By _curentAddressField    = By.XPath("//textarea[@id='currentAddress']");
        By _submitBtn             = By.XPath("//button[@id='submit']");
        By _outputField = By.XPath("//div[@id='output']");
        By _outputName = By.XPath("//p[@id='name']");
        By _outputEmail = By.XPath("//p[@id='email']");
        By _outputCurrentAddress = By.XPath("//p[@id='currentAddress']");
        By _outputPermanentAddress = By.XPath("//p[@id='permanentAddress']");
        IJavaScriptExecutor _js;
        public TextBoxPage(WebDriver driver)
        {
            _driver = driver;
        }

        public void FillInName(string fullName)
        {
            _driver.FindElement(_nameField).SendKeys(fullName);
        }

        public void FillInEmail(string email)
        {
            _driver.FindElement(_emailField).SendKeys(email);
        }

        public void FillInPermanentAddress(string permanentaddress)
        {
            _driver.FindElement(_permanentaddressField).SendKeys(permanentaddress);
        }

        public void FillInCurrentAddress(string currentaddress)
        {
            _driver.FindElement(_curentAddressField).SendKeys(currentaddress);
        }

        public void SubmitData()
        {
            IWebElement submitBtn = _driver.FindElement(_submitBtn);
            _js = _driver;
            _js.ExecuteScript($"window.scroll(0, {submitBtn.Location.Y / 2})");
            submitBtn.Click();
        }

        public bool IsOutputDisplayed()
        {
            return _driver.FindElement(_outputField).Displayed;
        }

        public bool IsNameEqual(string name)
        {
            IWebElement outputName = _driver.FindElement(_outputName);
            return outputName.Text.Contains(name);
        }

        public bool IsEmailEqual(string email)
        {
            IWebElement outputEmail = _driver.FindElement(_outputEmail);
            return outputEmail.Text.Contains(email);
        }

        public bool IsCurentAddressEqual(string currentAddress)
        {
            IWebElement outputCurrentAddress = _driver.FindElement(_outputCurrentAddress);
            return outputCurrentAddress.Text.Contains(currentAddress);
        }

        public bool IsPermanentAddressEqual(string permanentAddress)
        {
            IWebElement outputpermanentAddress = _driver.FindElement(_outputPermanentAddress);
            return outputpermanentAddress.Text.Contains(permanentAddress);
        }

    }
}
