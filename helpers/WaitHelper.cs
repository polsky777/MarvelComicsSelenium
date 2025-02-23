using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;


namespace MarvelSelenium.helpers
{
    public static class WaitHelper
    {
        private static int defaultTimeout = 5;
        public static void SetConfiguration(IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            string timeoutValue = configuration["AppSettings:implicitWait"];
            if (!string.IsNullOrEmpty(timeoutValue) && int.TryParse(timeoutValue, out int timeout))
            {
                defaultTimeout = timeout;
            }
        }

        /// <summary>
        /// Czeka na pojawienie się elementu w DOM i zwraca go.
        /// </summary>
        public static IWebElement WaitForElementPresent(IWebDriver driver, By by, int timeoutInSeconds = 0)
        {
            return WaitForCondition(driver, ExpectedConditions.ElementExists(by), timeoutInSeconds);
        }

        /// <summary>
        /// Czeka na widoczność elementu.
        /// </summary>
        public static IWebElement WaitForElementVisible(IWebDriver driver, By by, int timeoutInSeconds = 0)
        {
            return WaitForCondition(driver, ExpectedConditions.ElementIsVisible(by), timeoutInSeconds);
        }

        /// <summary>
        /// Czeka na możliwość kliknięcia elementu.
        /// </summary>
        public static IWebElement WaitForElementClickable(IWebDriver driver, By by, int timeoutInSeconds = 0)
        {

            var xxx = WaitForCondition(driver, ExpectedConditions.ElementToBeClickable(by), timeoutInSeconds);
            return xxx;
        }

        /// <summary>
        /// Pomocnicza metoda do czekania na warunek Selenium.
        /// </summary>
        private static IWebElement WaitForCondition(IWebDriver driver, Func<IWebDriver, IWebElement> condition, int timeoutInSeconds)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));

            int timeout = timeoutInSeconds > 0 ? timeoutInSeconds : defaultTimeout;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            return wait.Until(condition);
        }
    }
}
