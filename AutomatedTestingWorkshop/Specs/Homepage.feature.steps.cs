using FunkyBDD.SxS.Framework.Selenium.Pages;
using FunkyBDD.SxS.Selenium.WebElement;
using System.Linq;
using TechTalk.SpecFlow;
using Xunit;

namespace FunkyBDD.SxS.Framework.Specs
{
    [Binding, Scope(Feature = "Homepage elements")]
    public sealed class HomepageSteps
    {
        public Homepage Homepage;

        public HomepageSteps()
        {
            Homepage = new Homepage(Hooks.Driver);
        }

        [Then(@"I see the following distribution teasers")]
        public void ThenISeeTheFollowingDistributionTeasers(Table teasers)
        {
            /* Scroll to the component, its dynamic loaded */
            Homepage.DistributionTeaser.Component.ScrollTo();

            var errReport = "";
            var errCount = 0;

            for (int i = 0; i < teasers.Rows.Count; i++)
            {
                var title = teasers.Rows[i]["title"];
                var subTitle = teasers.Rows[i]["subTitle"];

                var found = from item in Homepage.DistributionTeaser.Items
                            where item.Title == title
                            select item;
                var itemExists = (found.Count() > 0);

                if(itemExists != true)
                {
                    errCount++;
                    errReport += $"Line {i+1} - Teaser element '{title}' not found!\n\r";
                }
                else { 
                    var hasTheRightValues = (found.First().SubTitle == subTitle);
                    if(hasTheRightValues != true)
                    {
                        errCount++;
                        errReport += $"Line {i + 1} - subTitle '{subTitle}' expected but '{found.First().SubTitle}' found!\n\r";
                    }
                    var existOnRightPosition = (Homepage.DistributionTeaser.Items[i].Title == title);
                    if (existOnRightPosition != true)
                    {
                        errCount++;
                        errReport += $"Line {i + 1} - Teaser element '{title}' not in the correct order!\n\r";
                    }
                }
            }

            if(teasers.Rows.Count != Homepage.DistributionTeaser.Items.Count)
            {
                errCount++;
                errReport += $"{teasers.Rows.Count} teaser elements expected but '{Homepage.DistributionTeaser.Items.Count}' elements found!\n\r";
            }

            Assert.True(0 == errCount, errReport);
        }

    }
}
