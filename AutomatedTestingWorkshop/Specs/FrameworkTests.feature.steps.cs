using Framework;
using Framework.Selenium.Pages;
using TechTalk.SpecFlow;
using Xunit;

namespace AutomatedTestingWorkshop.GherkinSpecs
{
    [Binding]
    public sealed class FrameworkTests
    {
        public Homepage Homepage;

        [Then(@"The homepage has the title '(.*)'")]
        public void ThenTheHomepageHasTheTitle(string title)
        {
            if(Homepage == null)
            {
                Homepage = new Homepage(Hooks.Driver);
            }
            Assert.Equal(title, Homepage.Title);
        }
    }
}
