﻿using TechTalk.SpecFlow;
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
            var x = 1;
        }


    }
}