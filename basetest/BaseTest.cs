using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter.Config;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.MarkupUtils;
using log4net;
using Microsoft.Extensions.Configuration;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;


namespace MarvelSelenium.basetest
{
    [SetUpFixture]
    internal class BaseTest
    {
        public static ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        private static ExtentReports extent;
        public static ThreadLocal<ExtentTest> exTest = new();

        private static readonly ILog log = LogManager.GetLogger(typeof(BaseTest));
        IConfiguration configuration;
        static string fileName;
        static BaseTest()
        {
            DateTime currentTime = DateTime.Now;
            string fileName = "Extent_" + currentTime.ToString("yyyy-MM-dd_HH-mm-ss") + ".html";
            extent = CreateInstance(fileName);
        }

        public static IWebDriver GetDriver()
        {
            return driver.Value;
        }
        public static ExtentTest GetExtentTest()
        {
            return exTest.Value;
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            log.Info("Test execution started !!!");
            var path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\resources\";
            configuration = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        [SetUp]
        public void BeforeEachTest()
        {
            exTest.Value = extent.CreateTest(TestContext.CurrentContext.Test.ClassName + " @ Test Case Name : " + TestContext.CurrentContext.Test.Name);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            extent.Flush();
            if (driver == null)
            {
                driver.Dispose();
                exTest.Dispose();
                log.Info("Test execution comleted !!!");
            }
        }

        public void Setup(string browserName)
        {
            //C:\Users\Tomek\Downloads>java -jar selenium-server-4.28.1.jar standalone
            //http://localhost:4444
            dynamic options = getBrowserOtpion(browserName);
            options.PlatformName = "windows";
            //options.AddUserProfilePreference("profile.default_content_setting_values.notifications", 2); //1 - alllow 2 - block
            driver.Value = new RemoteWebDriver(new Uri(configuration["AppSettings:gridurl"]), options.ToCapabilities());

            GetDriver().Url = configuration["AppSettings:testsiteurl"];
            options.AddArgument("--incognito");
            GetDriver().Manage().Window.Maximize();
            GetDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(int.Parse(configuration["AppSettings:implicit.wait"]));
        }

        private dynamic getBrowserOtpion(string browserName)
        {
            switch (browserName)
            {
                case "chrome":
                    return new ChromeOptions();
                case "firefox":
                    return new FirefoxOptions();
            }
            return new ChromeOptions();
        }

        public static ExtentReports CreateInstance(string fileName)
        {
            var htmlReporter = new ExtentSparkReporter(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\reports\" + fileName);
            htmlReporter.Config.Theme = Theme.Dark;
            htmlReporter.Config.DocumentTitle = "MarvelComicBooksWebSite Test Suit";
            htmlReporter.Config.ReportName = "Automation Test Results";
            htmlReporter.Config.Encoding = "utf-8";

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            extent.AddSystemInfo("Automation Tester", "Tomasz Polski");
            extent.AddSystemInfo("Organization", "Marvel's Fan");
            extent.AddSystemInfo("Buid No ", "Earth-616");

            return extent;
        }

        [TearDown]
        public void AfterEachTest()
        {
            //Get the test status 
            var status = TestContext.CurrentContext.Result.Outcome.Status;

            if (status == TestStatus.Failed)
            {
                CaptureScreenshot();

                GetExtentTest().Fail("Test failed: " + TestContext.CurrentContext.Result.Message);
                IMarkup markup = MarkupHelper.CreateLabel("FAIL", ExtentColor.Red);
                GetExtentTest().Fail(markup);
            }
            else if (status == TestStatus.Skipped)
            {
                GetExtentTest().Skip("Test is skipped : " + TestContext.CurrentContext.Result.Message);
                IMarkup markup = MarkupHelper.CreateLabel("SKIP", ExtentColor.Amber);
                GetExtentTest().Skip(markup);
            }
            else if (status == TestStatus.Passed)
            {
                GetExtentTest().Pass("Test case pass");
                IMarkup markup = MarkupHelper.CreateLabel("PASS", ExtentColor.Green);
                GetExtentTest().Pass(markup);
            }

            if (driver.Value != null)
            {
                GetDriver().Quit();
            }
        }

        private void CaptureScreenshot()
        {
            DateTime currentTime = DateTime.Now;
            string fileName = currentTime.ToString("yyyy-MM-dd_HH-mm-ss") + ".jpg";
            var screenshotPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "screenshots", "error" + fileName);

            ITakesScreenshot screenshotDriver = GetDriver() as ITakesScreenshot;

            if (screenshotDriver != null)
            {
                Screenshot screenshot = screenshotDriver.GetScreenshot();
                screenshot.SaveAsFile(screenshotPath);
                GetExtentTest().Fail("<b><font color= red> Screenshot of failure </font></b>",
                    MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
            }
            else
            {
                GetExtentTest().Fail("Screenshot capture failed: WebDriver does not support screenshots.");
            }
        }

    }
}
