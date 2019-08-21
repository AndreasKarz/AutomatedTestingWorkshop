using FunkyBDD.SxS.Framework.Selenium.Pages;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Xunit;

namespace FunkyBDD.SxS.Framework.Specs
{
    [Binding, Scope(Feature = "Website search function")]
    public sealed class Search
    {
        public Homepage Homepage;
        public SearchResultPage SearchResultPage;
        public DetailPage Detailpage;

        public Search()
        {
            Homepage = new Homepage(Hooks.Driver);
        }

        [Then(@"The homepage has the title '(.*)'")]
        public void ThenTheHomepageHasTheTitle(string title)
        {
            Assert.Equal(title, Homepage.Title);
        }

        [When(@"I'm searching for '(.*)'")]
        public void WhenIProvision(string term)
        {
            Homepage.SearchFor(term + Keys.Enter);
            SearchResultPage = new SearchResultPage(Hooks.Driver);
        }

        [Then(@"I expect more then (.*) results")]
        public void ThenIExpectMoreThenResults(int expectedResultCount)
        {
            Assert.True(SearchResultPage.ResultCount >= expectedResultCount, $"Less then {expectedResultCount} results found!");
        }

        [When(@"I select the (.*)\. result")]
        public void WhenISelectThe_Result(int index)
        {
            SearchResultPage.Results.SelectResult(index);
            Detailpage = new DetailPage(Hooks.Driver);
        }

        [Then(@"The detail page should have the title '(.*)'")]
        public void ThenTheDetailPageShouldHaveTheTitle(string pageTitle)
        {
            Assert.Contains(pageTitle, Detailpage.Title);
        }

    }
}
