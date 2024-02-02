using grocery_app_csharp_tests.Pages;

namespace grocery_app_csharp_tests.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class GroceryListTest : BaseTest
    {

        private GroceryListPage groceryListPage;
        private readonly List<string> groceries = new() { "yoghurt", "bananas", "butter", "ham" };
        private readonly string successAlert = "alert-success";
        private readonly string dangerAlert = "alert-danger";



        [SetUp]
        public override void SetUp()
        {
            base.SetUp(); // Ensures the driver is initialized
            groceryListPage = new GroceryListPage(driver!); // Now the driver is guaranteed to be non-null
            driver!.Navigate().GoToUrl("http://localhost:8080");
            groceryListPage.AddItem(groceries[0]);
            groceryListPage.AddItem(groceries[1]);
            Assert.That(groceryListPage.IsItemsCountCorrect(2), Is.True);
        }

        [Test]
        public void AddItemToList()
        {
            When("I submit an item", () =>
            {
                groceryListPage.AddItem(groceries[2]);
            });


            Then("an alert displays", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(groceryListPage.IsAlertClassPresent(successAlert), Is.True);
                    Assert.That(groceryListPage.IsAlertTextPresent("item added to the list"), Is.True);
                });
            });

            And("it displays in the list", () =>
            {

                Assert.Multiple(() =>
                {
                    Assert.That(groceryListPage.IsItemTextPresent(groceries[2]), Is.True);
                    Assert.That(groceryListPage.IsItemsCountCorrect(3), Is.True);
                });
            });


        }

        [Test]
        public void CannotAddAnEmptyItem()
        {
            When("I add an empty string as an item", () =>
            {
                groceryListPage.AddItem("");
            });

            Then("it complains", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(groceryListPage.IsAlertClassPresent(dangerAlert), Is.True);
                    Assert.That(groceryListPage.IsAlertTextPresent("please enter value"), Is.True);
                });
            });

            And("nothing is added", () =>
            {
                Assert.That(groceryListPage.IsItemsCountCorrect(2), Is.True);
            });

        }


        [Test]
        public void CanDeleteItem()
        {
            When("I delete an item", () =>
            {
                groceryListPage.DeleteItem(groceries[1]);

            });

            Then("an alert displays", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(groceryListPage.IsAlertClassPresent(dangerAlert), Is.True);
                    Assert.That(groceryListPage.IsAlertTextPresent("item removed"), Is.True);
                });
            });

            And("it doesn't display in the list", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(groceryListPage.IsItemTextAbsent(groceries[1]), Is.True);
                    Assert.That(groceryListPage.IsItemsCountCorrect(1));
                });

            });

        }

        [Test]
        public void CanEditAnItem()
        {
            When("I update an item", () =>
            {
                groceryListPage.EditItem(groceries[1]);
            });

            Then($"{groceries[1]} displays in the input", () =>
            {
                Assert.That(groceryListPage.IsItemDisplayedInInput(groceries[1]), Is.True);
            });

            And("submit changes to edit", () =>
            {
                Assert.That(groceryListPage.IsEditBtn(), Is.True);
            });

            When("I clear the text and add another item", () =>
            {
                groceryListPage.ClearInput().AddItem(groceries[3]);
            });


            Then("an alert displays", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(groceryListPage.IsAlertTextPresent("value changed"), Is.True);
                    Assert.That(groceryListPage.IsAlertClassPresent(successAlert), Is.True);
                });
            });

            Then($"{groceries[3]} displays in the list", () =>
            {
                Assert.That(groceryListPage.IsItemTextPresent(groceries[3]), Is.True);
            });

            And($"{groceries[1]} does not display in the list", () =>
            {
                Assert.That(groceryListPage.IsItemTextAbsent(groceries[1]), Is.True);
            });

            And("the item count is 2", () =>
            {
                Assert.That(groceryListPage.IsItemsCountCorrect(2), Is.True);
            });
        }

        [Test]
        public void ItemsCanBeCleared()
        {
            When("I clear items", () =>
            {
                groceryListPage.ClearItems();
            });

            Then("an alert displays", () =>
            {
                Assert.Multiple(() =>
                {
                    Assert.That(groceryListPage.IsAlertTextPresent("empty list"), Is.True);
                    Assert.That(groceryListPage.IsAlertClassPresent(dangerAlert), Is.True);
                });
            });

            And("the list is empty", () =>
            {
                Assert.That(groceryListPage.IsItemsCountCorrect(0), Is.True);
            });
        }

    }
}

