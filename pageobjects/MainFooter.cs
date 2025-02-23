using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace MarvelSelenium.pageobjects
{
    internal class MainFooter : BasePage
    {
        public MainFooter(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement PrivacyPolicyLink => driver.FindElement(By.XPath("//a[@id='privacy']"));

        public void ClickPrivacyPolicy() => PrivacyPolicyLink.Click();
    }
}
