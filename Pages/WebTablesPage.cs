using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowDemoQa.Pages
{
    internal class WebTablesPage
    {
        private IWebDriver _driver;
        By _salaryValues = By.XPath("//div[@role='row']/div[@role='gridcell'][5]");
        By _tableRows = By.XPath("//div[@role='row']");
        By _deleteButton = By.XPath(".//span[@title='Delete']");

        public WebTablesPage(WebDriver driver)
        {
            _driver = driver;
        }

        public By MakeColumnXpathLocator(string columnName) => By.XPath($"//div[text()='{columnName}']");
        public By MakeColumnXPathLocator(int index) => By.XPath($".//div[{index}]");

        public void ClickOnColumn(string columnName)
        {
            _driver.FindElement(MakeColumnXpathLocator(columnName)).Click();
        }

        internal bool IsOrderAscending()
        {
            List<IWebElement> salaries = _driver.FindElements(_salaryValues).ToList();

            int previousValue = Int32.Parse(salaries[0].Text.ToString());

            foreach (IWebElement element in salaries)
            {
                if (!string.IsNullOrWhiteSpace(element.Text))
                {
                    int currentValue = Int32.Parse(element.Text.ToString());
                    if (previousValue > currentValue)
                    {
                        return false;
                    }
                    else
                    {
                        previousValue = currentValue;
                    }
                }
                else
                {
                    break;
                }
            }
            return true;
        }

        public List<IWebElement> FindTableRows()
        {
            List<IWebElement> rows = _driver.FindElements(_tableRows).ToList();
            List<IWebElement> dataRows = new();

            foreach (IWebElement row in rows)
            {
                if (row.FindElement(By.XPath(".//div[1]")).GetAttribute("role").Contains("columnheader")) continue;
                if (!string.IsNullOrWhiteSpace(row.FindElement(By.XPath(".//div[1]")).Text))
                {
                    dataRows.Add(row);
                }
            }
            return dataRows;
        }

        internal void DeleteRowWithFirstName(string firstName)
        {
            List<IWebElement> rows = FindTableRows();
            foreach (IWebElement row in rows)
            {
                if (row.FindElement(By.XPath(".//div[1]")).Text.Contains(firstName))
                {
                    row.FindElement(_deleteButton).Click();
                    break;
                }
            }
        }

        internal int RowsCount()
        {
            int count = 0;
            List<IWebElement> rows = FindTableRows();

            return rows.Count();
        }

        internal bool ColumnContainsValue(string column, string value)
        {
            List<IWebElement> columnValues = GetColumnValues(column);

            foreach (var currentValue in columnValues)
            {
                if (currentValue.Text.Contains(value)) return true;
            }
            return false;
        }

        private List<IWebElement> GetColumnValues(string column)
        {
            List<IWebElement> rows = FindTableRows();
            List<IWebElement> columnValues = new();
            int index = 0;

            switch (column)
            {
                case "First Name":
                    index = 1;
                    break;
                case "Last Name":
                    index = 2;
                    break;
                case "Age":
                    index = 3;
                    break;
                case "Email":
                    index = 4;
                    break;
                case "Salary":
                    index = 5;
                    break;
                case "Department":
                    index = 6;
                    break;
                default:
                    throw new Exception($"Column with {column} name not found");
            }

            foreach (var row in rows)
            {
                columnValues.Add(row.FindElement(MakeColumnXPathLocator(index)));
            }
            
            return columnValues;
        }
    }
}
