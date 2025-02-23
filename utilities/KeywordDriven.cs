using System.Collections.ObjectModel;
using MarvelSelenium.basetest;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MarvelSelenium.utilities
{
    internal class KeywordDriven
    {
        private static string text;
        private ReadOnlyCollection<IWebElement> elements;

        public void Click(string pageName, string locatorName, string locatorType)
        {
            if (locatorType.Contains("ID"))
            {
                BaseTest.GetDriver().FindElement(By.Id(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType))).Click();
            }
            else if (locatorType.Contains("XPATH"))
            {
                BaseTest.GetDriver().FindElement(By.XPath(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType))).Click();
            }
            else if (locatorType.Contains("CSS"))
            {
                BaseTest.GetDriver().FindElement(By.CssSelector(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType))).Click();
            }
            else if (locatorType.Contains("LINK"))
            {
                BaseTest.GetDriver().FindElement(By.LinkText(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType))).Click();
            }

            BaseTest.GetExtentTest().Info("Clicking on an Element : " + locatorName);
        }
        public ReadOnlyCollection<IWebElement> FindElements(string pageName, string locatorName, string locatorType)
        {
            if (locatorType.Contains("ID"))
            {
                elements = BaseTest.GetDriver().FindElements(By.Id(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType)));
            }
            else if (locatorType.Contains("XPATH"))
            {
                elements = BaseTest.GetDriver().FindElements(By.XPath(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType)));
            }
            else if (locatorType.Contains("CSS"))
            {
                elements = BaseTest.GetDriver().FindElements(By.CssSelector(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType)));
            }
            else if (locatorType.Contains("LINK"))
            {
                elements = BaseTest.GetDriver().FindElements(By.LinkText(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType)));
            }

            BaseTest.GetExtentTest().Info("Clicking on an Element : " + locatorName);
            return elements;
        }
        public string GetText(string pageName, string locatorName, string locatorType)
        {
            if (locatorType.Contains("ID"))
            {
                text = BaseTest.GetDriver().FindElement(By.Id(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType))).Text;
            }
            else if (locatorType.Contains("XPATH"))
            {
                text = BaseTest.GetDriver().FindElement(By.XPath(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType))).Text;
            }
            else if (locatorType.Contains("CSS"))
            {
                text = BaseTest.GetDriver().FindElement(By.CssSelector(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType))).Text;
            }
            else if (locatorType.Contains("LINK"))
            {
                text = BaseTest.GetDriver().FindElement(By.LinkText(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType))).Text;
            }

            BaseTest.GetExtentTest().Info("Getting the text an Element : " + locatorName);
            return text;
        }

        public void Type(string pageName, string locatorName, string locatorType, string value)
        {
            if (locatorType.Contains("ID"))
            {
                BaseTest.GetDriver().FindElement(By.Id(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType))).SendKeys(value);
            }
            else if (locatorType.Contains("XPATH"))
            {
                BaseTest.GetDriver().FindElement(By.XPath(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType))).SendKeys(value);
            }
            else if (locatorType.Contains("CSS"))
            {
                BaseTest.GetDriver().FindElement(By.CssSelector(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType))).SendKeys(value);
            }

            BaseTest.GetExtentTest().Info($"Typing on an Element : {locatorName} value entered as {value}");
        }
        public void Select(string pageName, string locatorName, string locatorType, string value)
        {
            IWebElement element = null;

            if (locatorType.Contains("ID"))
            {
                element = BaseTest.GetDriver().FindElement(By.Id(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType)));
            }
            else if (locatorType.Contains("XPATH"))
            {
                element = BaseTest.GetDriver().FindElement(By.XPath(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType)));
            }
            else if (locatorType.Contains("CSS"))
            {
                element = BaseTest.GetDriver().FindElement(By.CssSelector(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType)));
            }

            SelectElement selectElement = new SelectElement(element);
            selectElement.SelectByValue(value);

            BaseTest.GetExtentTest().Info($"Selecting an Element : {locatorName} selected value as {value}");
        }
        public bool isElementPresent(string pageName, string locatorName, string locatorType)
        {
            try
            {
                if (locatorType.Contains("ID"))
                {
                    BaseTest.GetDriver().FindElement(By.Id(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType)));
                }
                else if (locatorType.Contains("XPATH"))
                {
                    BaseTest.GetDriver().FindElement(By.XPath(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType)));
                }
                else if (locatorType.Contains("CSS"))
                {
                    BaseTest.GetDriver().FindElement(By.CssSelector(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType)));
                }

                BaseTest.GetExtentTest().Info("Finding an Element : " + locatorName);
                return true;
            }
            catch (Exception ex)
            {
                BaseTest.GetExtentTest().Info("Error while finding an Element : " + locatorName);
                return false;
            }
        }
    }
}
