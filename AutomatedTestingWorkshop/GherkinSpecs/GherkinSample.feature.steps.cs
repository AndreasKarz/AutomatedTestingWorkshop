using OpenQA.Selenium;
using System.Threading;
using TechTalk.SpecFlow;
using Xunit;

namespace AutomatedTestingWorkshop.GherkinSpecs
{
    [Binding]
    public sealed class GherkinSample
    {
        [Given(@"I have click on the Tab WOMEN")]
        public void GivenIHaveClickOnTheTabWOMEN()
        {
            
        }

        [When(@"I click on the category Tops")]
        public void WhenIClickOnTheCategoryTops()
        {

        }

        [Then(@"I see two items")]
        public void ThenISeeItems()
        {
            
        }

        [When(@"I search for (.*)")]
        public void WhenISearchForShoe(string Therm)
        {
            
        }

        [Then(@"I will recieve (.*) results")]
        public void ThenIWillRecieveResults(int Results)
        {
            
        }

        [Then(@"I see all my tabs")]
        public void ThenISeeAllMyTabs(Table table)
        {
            // samples for properties
            IWebElement PhoneNumber = Hooks.Driver.FindElement(By.CssSelector(".shop-phone"));
            string Label = PhoneNumber.Text;
            int Left = PhoneNumber.Location.X;
            int Top = PhoneNumber.Location.Y;
            int Width = PhoneNumber.Size.Width;
            string VAlign = PhoneNumber.GetCssValue("vertical-align");
            bool Enabled = PhoneNumber.Enabled;
            bool Displayed = PhoneNumber.Displayed;
            string ClassName = PhoneNumber.GetAttribute("class");
            string TagName = PhoneNumber.TagName;

            //samples for methods
            IWebElement Searchbox = Hooks.Driver.FindElement(By.Id("search_query_top"));
            Searchbox.SendKeys("Top");
            Thread.Sleep(1000);
            Searchbox.Clear();
            Thread.Sleep(1000);
            Searchbox.SendKeys("Shoe");
            Searchbox.Submit();
            Thread.Sleep(1000);
            IWebElement DemoProduct = Hooks.Driver.FindElement(By.XPath("//a[contains(@href, 'id_product=2')]"));
            DemoProduct.Click();
            Thread.Sleep(5000);

            var x = 1;
        }


    }
}
