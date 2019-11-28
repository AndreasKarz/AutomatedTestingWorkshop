﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.4.0.0
//      SpecFlow Generator Version:2.4.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace FunkyBDD.SxS.Framework.Specs
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.4.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Xunit.TraitAttribute("Category", "Business")]
    [Xunit.TraitAttribute("Category", "Homepage")]
    public partial class HomepageElementsFeature : Xunit.IClassFixture<HomepageElementsFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Homepage.feature"
#line hidden
        
        public HomepageElementsFeature(HomepageElementsFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Homepage elements", "\tIn order to \r\n\t\tfind the expected information\r\n\tAs a \r\n\t\tcustomer\r\n\tI want to \r\n" +
                    "\t\tsee all relevant elements", ProgrammingLanguage.CSharp, new string[] {
                        "Business",
                        "Homepage"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 10
#line 11
 testRunner.Given("I open the test page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
        }
        
        void System.IDisposable.Dispose()
        {
            this.ScenarioTearDown();
        }
        
        [Xunit.FactAttribute(DisplayName="All distribution teasers are visible")]
        [Xunit.TraitAttribute("FeatureTitle", "Homepage elements")]
        [Xunit.TraitAttribute("Description", "All distribution teasers are visible")]
        [Xunit.TraitAttribute("Category", "distribution")]
        public virtual void AllDistributionTeasersAreVisible()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("All distribution teasers are visible", null, new string[] {
                        "distribution"});
#line 14
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 10
this.FeatureBackground();
#line 15
 testRunner.Given("I change the language to \'EN\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "title",
                        "subTitle"});
            table1.AddRow(new string[] {
                        "Future provisions and wealth accumulation",
                        "To product selection"});
            table1.AddRow(new string[] {
                        "Property and asset insurance",
                        "To product selection"});
            table1.AddRow(new string[] {
                        "Health insurance",
                        "To product selection"});
            table1.AddRow(new string[] {
                        "Property financing and residential property",
                        "To product selection"});
#line 16
 testRunner.Then("I see the following distribution teasers", ((string)(null)), table1, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.4.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                HomepageElementsFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                HomepageElementsFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
