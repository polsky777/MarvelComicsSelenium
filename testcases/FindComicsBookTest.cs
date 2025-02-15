using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarvelSelenium.basetest;
using MarvelSelenium.utilities;


namespace MarvelSelenium.testcases
{
    [TestFixture]
    internal class FindComicsBookTest:BaseTest
    {
        [Test, TestCaseSource(nameof(GetTestData))]
        public void FindComicsBook(string browser, string runmode)
        {
            if (runmode.Equals("N"))
            {
                Assert.Ignore("Ignoring the test as the run mode is NO");
            }
            Setup(browser);
        }


        public static IEnumerable<TestCaseData> GetTestData()
        {
            var columns = new List<string> { "browser", "runmode"};
            return DataUtilities.GetTestDataFromExcel(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\resources\testdata.xlsx", "FindComicsBook", columns);
        }

    }
}
