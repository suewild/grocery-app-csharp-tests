using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace grocery_app_tests.Tests
{
    public abstract class BaseTest
	{
        [ThreadStatic]
        protected static IWebDriver? driver;

        protected void Given(string description, Action action)
        {
            Console.WriteLine($"Given {description}");
            action();
        }

        protected void When(string description, Action action)
        {
            Console.WriteLine($"Given {description}");
            action();
        }

        protected void Then(string description, Action action)
        {
            Console.WriteLine($"Given {description}");
            action();
        }

        protected void And(string description, Action action)
        {
            Console.WriteLine($"Given {description}");
            action();
        }

        [SetUp]
        public virtual void SetUp()
        {
            if (driver == null)
            {
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            }
        }

        [TearDown]
        public virtual void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                TakeScreenshot(TestContext.CurrentContext.Test.Name);
            }

            driver?.Quit();
            driver = null;  
        }

        protected void TakeScreenshot(string testName)
        {
            if (driver == null || !(driver is ITakesScreenshot))
            {
                Console.WriteLine("Driver is not initialized or does not support taking screenshots.");
                return;
            }

            try
            {
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                var fileName = $"{testName}-{timestamp}.png";
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                var screenshotsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
                var screenshotPath = Path.Combine(screenshotsDirectory, fileName);

                Directory.CreateDirectory(screenshotsDirectory);

                screenshot.SaveAsFile(screenshotPath);
                Console.WriteLine($"Screenshot saved: {screenshotPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while taking screenshot: {ex.Message}");
            }
        }
    }
}

