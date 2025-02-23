using log4net;
using MarvelSelenium.models;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MarvelSelenium.pageobjects
{
    internal class SearchPage : BasePage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SearchPage));
        public SearchPage(IWebDriver driver) : base(driver)
        {
        }

        public void SelectAndTypeFilter(string filter, string text)
        {
            SelectFilter(filter);
            TypeFilter(text);
        }

        public void SelectFilter(string filter)
        {
            try
            {
                var element = driver.FindElement(By.XPath($"//a[normalize-space()='{filter}']"));
                element.Click();
            }
            catch (NoSuchElementException)
            {
                log.Info($"Filtr '{filter}' nie został znaleziony.");
            }
        }

        public void TypeFilter(string text)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                var searchInput = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder='Search']")));
                searchInput.Click();
                searchInput.Clear();
                searchInput.SendKeys(text);
                searchInput.SendKeys(Keys.Enter);
            }
            catch (NoSuchElementException)
            {
                log.Info("Pole wyszukiwania nie zostało znalezione.");
            }
            catch (WebDriverTimeoutException)
            {
                log.Info("Pole wyszukiwania nie pojawiło się w oczekiwanym czasie.");
            }
        }

        public List<SearchResultItem> GetSearchResults()
        {
            List<SearchResultItem> searchResults = new List<SearchResultItem>();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            bool isElementPresent = driver.FindElements(By.XPath("//div[contains(@class,'mvl-card mvl-card--search')]")).Count > 0;

            if (isElementPresent)
            {
                while (true)
                {
                    var cards = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                         .PresenceOfAllElementsLocatedBy(By.XPath("//div[contains(@class,'mvl-card mvl-card--search')]")));

                    log.Info($"Znaleziono {cards.Count} wyników na tej stronie.");

                    foreach (var card in cards)
                    {
                        var result = ExtractResultItem(card);
                        if (result != null)
                        {
                            searchResults.Add(result);
                        }
                    }
                    var nextButton = driver.FindElements(By.CssSelector(".pagination__item-nav-next")).FirstOrDefault();
                    if (nextButton != null && nextButton.Displayed && nextButton.Enabled)
                    {
                        log.Info("Klikam przycisk 'Next' i przechodzę do następnej strony.");
                        nextButton.Click();
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(cards.First()));
                    }
                    else
                    {
                        log.Info("Brak kolejnej strony. Zakończenie pobierania wyników.");
                        break;
                    }
                }
            }
            else
            {
                log.Info("Brak wyników na tej stronie.");
                searchResults.Clear();
            }
            log.Info($"Zakończono GetSearchResults(), znaleziono {searchResults.Count} unikalnych wyników.");
            return searchResults;
        }

        private SearchResultItem ExtractResultItem(IWebElement card)
        {
            try
            {
                var contentTypeElement = card.FindElement(By.XPath(".//a[contains(@class, 'card-body__content-type')]")).Text.Trim();
                var contentTextElement = card.FindElement(By.XPath(".//p[@class='card-body__headline']/a")).Text.Trim();
                return new SearchResultItem(contentTypeElement, contentTextElement);
            }
            catch (NoSuchElementException)
            {
                log.Info("Nie znaleziono wymaganych elementów w karcie.");
                return null;
            }
        }

        public string GetQuantityResult()
        {
            try
            {
                var element = driver.FindElement(By.XPath("//div[@class='search-list__results-count']"));
                return new string(element.Text.Where(char.IsDigit).ToArray());
            }
            catch (NoSuchElementException)
            {
                log.Info("Nie znaleziono elementu z liczbą wyników.");
                return "0";
            }
        }
    }
}