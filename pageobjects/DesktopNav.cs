using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace MarvelSelenium.pageobjects
{
    internal class DesktopNav : BasePage
    {
        public DesktopNav(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement HomeLink => driver.FindElement(By.XPath("//span[@class='icon--svg icon--svg mvl-animated-logo']"));
        private IWebElement SearchLink => driver.FindElement(By.XPath("//a[@id='search']"));

        public void ClickMarvelHome() => HomeLink.Click();
        public SearchPage ClickSearchLink()
        {
            SearchLink.Click();
            return new SearchPage(driver);
        }

    }
}
