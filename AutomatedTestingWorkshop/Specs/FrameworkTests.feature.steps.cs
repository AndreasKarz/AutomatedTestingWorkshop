using FunkyBDD.SxS.Framework.Selenium.Pages;
using SwissLife.SxS.Helpers;
using System.Drawing;
using TechTalk.SpecFlow;
using Xunit;

namespace FunkyBDD.SxS.Framework.Specs
{
    [Binding, Scope(Feature = "The base framework works well")]
    public sealed class FrameworkTests
    {
        public Homepage Homepage;
        public SearchResultPage SearchResultPage;
        public DetailPage Detailpage;

        public FrameworkTests()
        {
            Homepage = new Homepage(Hooks.Driver);
        }

        [Then(@"The homepage has the title '(.*)'")]
        public void ThenTheHomepageHasTheTitle(string title)
        {
            Assert.Equal(title, Homepage.Title);
        }

        [Then(@"The page has the color (.*), (.*), (.*), (.*)")]
        public void ThenTheTitleHasTheColor(int a, int r, int g, int b)
        {
            var expectedColor = Color.FromArgb(a, r, g, b);
            var color = ColorHelpers.ParseColor(Homepage.Color);
            Assert.Equal(expectedColor, color);
        }

        [Then(@"The banner title should be '(.*)'")]
        public void ThenTheBannerTitleShouldBe(string title)
        {
            Assert.Equal(title, Homepage.Banner.Header.Text);
        }

        [Then(@"The family teaser title schould be '(.*)'")]
        public void ThenTheFamilyTeaserTitleSchouldBe(string title)
        {
            Assert.Equal(title, Homepage.Banner.FamilyTeaser.Header.Text);
        }

        [Then(@"The occupational teaser title schould be '(.*)'")]
        public void ThenTheOccupationalTeaserTitleSchouldBe(string title)
        {
            Assert.Equal(title, Homepage.Banner.OccupationalTeaser.Header.Text);
        }

        [When(@"I mover over the family teaser")]
        public void WhenIMoverOverTheFamilyTeaser()
        {
            Homepage.Banner.FamilyTeaser.MouserOver();
        }

        [Then(@"The button should be shown")]
        public void ThenTheButtonShouldBeShown()
        {
            Assert.True(Homepage.Banner.FamilyTeaser.IsButtonClickable, "The button is not clickable!");
        }

        [When(@"I call a page not exists")]
        public void WhenICallAPageNotExists()
        {
            Hooks.Driver.Navigate().GoToUrl("https://www.swisslife.ch/funkyBDD.html");
        }

    }
}
