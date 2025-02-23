using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarvelSelenium.helpers;
using OpenQA.Selenium;

namespace MarvelSelenium.pageobjects
{
    internal class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {

        }

        public void ClickAgreement()
        {
            IWebElement clickableElement = WaitHelper.WaitForElementClickable(driver, By.XPath("//button[contains(text(),'I Accept')]"));
            clickableElement.Click();
        }


    }
}
