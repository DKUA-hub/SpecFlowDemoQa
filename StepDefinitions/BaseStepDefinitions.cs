using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowDemoQa.StepDefinitions
{
    public class BaseStepDefinitions
    {
        WebDriver? _driver;

        [BeforeScenario]
        public void SetUp()
        {
            _driver = SharedDriver.GetDriver();
            _driver.Manage().Window.Maximize();
            SharedDriver.SetDriver(_driver);
        }

        [AfterScenario]
        public void TearDown()
        {
            if (_driver is not null)
            {
                SharedDriver.QuitDriver();
                _driver = null;
            }
        }
    }
}
