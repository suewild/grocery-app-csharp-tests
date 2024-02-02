using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace grocery_app_tests.Pages
{
    public class WebPage
    {

        private readonly IWebDriver driver;
        protected readonly WebDriverWait wait;

        protected WebPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        protected IWebElement WaitAndFindElement(By bySelector)
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(bySelector));
        }

        protected IList<IWebElement> WaitAndFindElements(By bySelector)
        {
            try
            {
                return wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(bySelector));
            }
            catch (WebDriverTimeoutException)
            {
                return new List<IWebElement>(); // Return an empty list if the wait times out
            }
        }

    }
}

