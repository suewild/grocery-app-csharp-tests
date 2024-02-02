using OpenQA.Selenium;

namespace grocery_app_tests.Pages
{
    public class GroceryListPage : WebPage
	{
        private IWebElement Alert => WaitAndFindElement(By.CssSelector(".alert"));
        private IWebElement ClearItemsBtn => WaitAndFindElement(By.CssSelector("[data-testid=clear-btn]"));
        private IWebElement Input => WaitAndFindElement(By.CssSelector("[data-testid=grocery-input]"));
        private IWebElement SubmitBtn => WaitAndFindElement(By.CssSelector("[data-testid=submit-btn]"));
        private List<IWebElement> ListItems => WaitAndFindElements(By.CssSelector(".grocery-list .grocery-item")).ToList();

        public GroceryListPage(IWebDriver driver) : base(driver)
        {
        }

        private void Submit()
        {
            SubmitBtn.Click();
        }

        public IWebElement? GetItem(string groceryItem)
        {
            return ListItems.FirstOrDefault(element => element.Text.ToLower().Equals(groceryItem));

        }

        public List<string> GetItemsText()
        {
            return ListItems.Select(element => element.Text.ToLower()).ToList();
        }

        public bool IsEditBtn()
        {
            return SubmitBtn.Text.ToLower().Equals("edit");
        }

        public void AddItem(string groceryItem)
        {
            Input.SendKeys(groceryItem);
            Submit();
        }

        public void ClearItems()
        {
            ClearItemsBtn.Click();
        }

        public void DeleteItem(string groceryItem)
        {
            IWebElement? element = GetItem(groceryItem);
            IWebElement deleteButton = element!.FindElement(By.CssSelector(".delete-btn"));
            deleteButton.Click();
   
        }

        public void EditItem(string groceryItem)
        {
            IWebElement? element = GetItem(groceryItem);
            IWebElement editBtn = element!.FindElement(By.CssSelector(".edit-btn"));
            editBtn.Click();
        }

        public GroceryListPage ClearInput()
        {
            Input.Clear();
            return this;
        }

        public bool IsItemDisplayedInInput(string groceryItem)
        {
            return Input.GetAttribute("value").Contains(groceryItem);
        }

        public bool IsItemsCountCorrect(int expectedNumber)
        {
            return ListItems.Count == expectedNumber;
        }

        public bool IsItemTextPresent(string groceryItem)
        {
            List<string> itemTexts = GetItemsText();
            return itemTexts.Any(text => text.Contains(groceryItem));
        }

        public bool IsItemTextAbsent(string groceryItem)
        {
            List<string> itemTexts = GetItemsText();
            return !itemTexts.Any(text => text.Contains(groceryItem.ToLower()));
        }

        public bool IsAlertTextPresent(string expectedAlertText)
        {
            return Alert.Text.ToLower().Contains(expectedAlertText);
        }

        public bool IsAlertClassPresent(string className)
        {
            List<string> alertClasses = Alert.GetAttribute("class").Split(' ').ToList();
            return alertClasses.Any(c => c.Equals(className, StringComparison.OrdinalIgnoreCase));
        }

     
    }
}

