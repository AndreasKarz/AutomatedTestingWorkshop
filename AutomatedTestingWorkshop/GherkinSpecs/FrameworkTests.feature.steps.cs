using TechTalk.SpecFlow;
using Xunit;

namespace AutomatedTestingWorkshop.GherkinSpecs
{
    [Binding]
    public sealed class FrameworkTests
    {
        [When(@"I do nothing")]
        public void WhenIDoNothing()
        {
            
        }

        [Then(@"The dummy assertion works")]
        public void DummyAssertionWorks()
        {
            Assert.Equal(1, 1);
        }
    }
}
