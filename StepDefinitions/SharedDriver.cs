using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowDemoQa.StepDefinitions
{
    internal class SharedDriver
    {
        private static WebDriver? _driver;

        [BeforeScenario]
        public static void BeforeScenario()
        {
            //_driver = new ChromeDriver();  // Ви можете використовувати інший драйвер за потреби
            InitializeDriver();
            _driver.Manage().Window.Maximize();
            Thread.Sleep(3000);
            _driver.Navigate().GoToUrl("https://demoqa.com/");
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver = null;
            }
        }

        public SharedDriver()
        {

        }

        private static void InitializeDriver()
        {
            string[] args = Environment.GetCommandLineArgs();
            string browser = "chrome";
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].ToLower() == "-browser" && i + 1 < args.Length)
                {
                    browser = args[i + 1].ToLower();
                    break;
                }
            }

            switch (browser)
            {
                case "chrome":
                    _driver = new ChromeDriver();
                    break;
                case "firefox":
                    _driver = new FirefoxDriver();
                    break;
                case "edge":
                    _driver = new EdgeDriver();
                    break;
            }
        }

        public static WebDriver? GetDriver()
        {
            if (_driver is null) InitializeDriver();
            return _driver;
        }
        
        public static void SetDriver(WebDriver driver)
        {
            _driver = driver;
        }

        public static void QuitDriver()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver = null;
            }
        }
    }
}
