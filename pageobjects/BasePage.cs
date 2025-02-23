
using MarvelSelenium.utilities;
using OpenQA.Selenium;

namespace MarvelSelenium.pageobjects
{
    internal class BasePage
    {
        public IWebDriver driver;
        public static KeywordDriven keyword;


        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            keyword = new KeywordDriven();
            this.driver = driver;
        }
    }
}
