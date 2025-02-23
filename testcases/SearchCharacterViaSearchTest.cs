using MarvelSelenium.basetest;
using MarvelSelenium.models;
using MarvelSelenium.pageobjects;
using MarvelSelenium.utilities;
using FluentAssertions;
using log4net;

namespace MarvelSelenium.testcases
{
    [TestFixture]
    internal class SearchCharacterViaSearchTest:BaseTest
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SearchCharacterViaSearchTest));
       
        [Parallelizable]
        [Test, TestCaseSource(nameof(GetTestData))]
        public void SearchCharacterViaSearch(string browserType, string executionMode, string primaryCharacter, string secondaryCharacter, string expectedResultCount)
        {
            string testName = TestContext.CurrentContext.Test.Name;
            log.Info($"Rozpoczynam test: {testName}");

            if (executionMode.Equals("N"))
            {
                log.Info("Ignoring the test as the run mode is NO");
                Assert.Ignore("Ignoring the test as the run mode is NO");
            }
            Setup(browserType);
            HomePage home = new HomePage(driver.Value);
            home.ClickAgreement();
            DesktopNav desktopNav = new DesktopNav(driver.Value);
            SearchPage search = desktopNav.ClickSearchLink();
            search.SelectAndTypeFilter("characters", primaryCharacter);
            var resultsQty = search.GetQuantityResult();

            List<SearchResultItem> results = search.GetSearchResults();

            bool isMainCharacterPresent = results.Any(item =>
                item.ContentType.Equals("character", StringComparison.OrdinalIgnoreCase) &&
                item.ContentText.Contains(primaryCharacter));
            bool isSecondCharacterPresent = results.Any(item =>
                item.ContentType.Equals("character", StringComparison.OrdinalIgnoreCase) &&
                item.ContentText.Contains(secondaryCharacter));

            results.Should().NotBeEmpty("there are no results on this page");
            resultsQty.Should().Be(expectedResultCount,$"{resultsQty} is not equals {expectedResultCount} expected quantity");
            isMainCharacterPresent.Should().BeTrue($"the lack of {primaryCharacter} in the list");
            isSecondCharacterPresent.Should().BeTrue($"the lack of {secondaryCharacter} in the list");

            log.Info($"Zakończono test: {testName}");
        }
        public static IEnumerable<TestCaseData> GetTestData()
        {
            var columns = new List<string> { "browserType", "executionMode", "primaryCharacter", "secondaryCharacter", "expectedResultCount" };
            return DataUtilities.GetTestDataFromExcel(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\resources\testdata.xlsx", "FindComicsBook", columns);
        }

    }
}
