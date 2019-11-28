using TechTalk.SpecFlow;
using Xunit;

namespace FunkyBDD.SxS.Framework.Specs
{
    [Binding, Scope(Feature = "API testing")]
    public sealed class ApiSteps
    {
        private Api call;

        [When(@"I request user (.*) of the dummy api")]
        public void WhenIRequestUserOfTheDummyApi(int id)
        {
            call = new Api($"https://reqres.in/api/users/{id}");
        }

        [Then(@"the response status should be '(.*)'")]
        public void ThenTheResponseStatusShouldBe(string status)
        {
            Assert.Equal(status, call.Status);
        }

    }
}
