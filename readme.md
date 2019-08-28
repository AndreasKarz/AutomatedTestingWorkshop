# Automated E2E Testing with **S**pecflow-**x**Unit-**S**elenium / BDD

## What is **B**ehavior **D**riven **D**evelopment?
In software engineering, behavior-driven development (BDD) is an Agile software development process that encourages collaboration among developers, QA and non-technical or business participants in a software project.

BDD is largely facilitated using a simple domain-specific language (DSL) using natural language constructs (e.g., English-like sentences) that can express the behavior and the expected outcomes. Test scripts have long been a popular application of DSLs with varying degrees of sophistication. BDD is considered an effective technical practice especially when the "problem space" of the business problem to solve is complex.
Acceptance criteria or scenarios, a description of each specific case of the narrative. Such a scenario has the following structure:

· It starts by specifying the initial condition that is assumed to be true at the beginning of the scenario. This may consist of a single clause, or several.
· It then states which event triggers the start of the scenario.
· Finally, it states the expected outcome, in one or more clauses.

The following video shows in a short example how to work out the description and what must be considered. [YouTube BDD example](https://www.youtube.com/watch?v=M238SpxRtqA) (3:12)
How everything works together is shown in [this presentation](https://prezi.com/p/1yygbt5c9kni/).

## Tools in this workshop
The following list gives a rudimentary overview of what is being worked with in this workshop. You will learn the details later in this workshop.

### SpecFlow
SpecFlow is the backbone of BDD and Agile Development and generates the corresponding test flows from the Gherkin descriptions.

### xUnit
xUnit is the Test runner for Visual Studio and is also used for the assertions of the expected test results.

### Selenium
Selenium automates browsers. That's it! What you do with that power is entirely up to you. Primarily, it is for automating web applications for testing purposes, but is certainly not limited to just that.

### Browserstack
Browserstack is an online service with which Selenium can run tests on real (mobile) devices and older browsers.

### Azure DevOps
Simplify and speed up the DevOps process with Azure DevOps services. The following labs will help you to get started with Azure DevOps services to automate software delivery and meet business needs.

In this Workshop we need Azure DevOps for the build pipelines to run the tests.

## Base Solution
As a working tool for this workshop there is a [solution on GitHub](https://github.com/AndreasKarz/AutomatedTestingWorkshop), in which all necessary elements and examples are available.

### Visual Studio 2019
The Base Solution is built for Visual Studio 2019. You can download the [Visual Studio Community Edition](https://visualstudio.microsoft.com/vs/community/) for free at Microsoft.

### Visual Studio Extension
This solution contains the extension for Visual Studio 2019 called `TechTalk.SpecFlow.VisualStudioIntegration.vsix`. Run this file to install the extension. 
After that, **immediately** after starting Visual Studio in the [Extenions], [Manage Extensions] menu, the automatic update for this extension must be disabled.

### Important NuGet Packages
The following NuGet Packages are already included in the Basis Solution. I only list the most important ones, which will be discussed later in the workshop. 

#### SpecFlow and SpecFlow.xUnit
We work with the version 2.4.0 – this is the last version without bugs. You should not update the version!

#### FunkyBDD.SxS.Helpers
Some helper classes for the SxS Framework.

#### FunkyBDD.SxS.Selenium.APOM
Base class for Selenium Page Objects for the POM and APOM principles.

#### FunkyBDD.SxS.Selenium.Browserstack
Selenium Helpers for a smarter use of Browserstack.

#### FunkyBDD.SxS.Selenium.WebDriver
Extensions for the Selenium IWebDriver with often missed functions.

#### FunkyBDD.SxS.Selenium.WebElement
Extensions for the Selenium IWebElement. Integrates properties and methods that are always needed. Supports the POM and APOM principles.

### Preparation
To use the Base Solution some preparations are needed. 

#### Browser configuration
If you do not want to work with different and real browsers, you can skip this step. Otherwise go to [Browserstack](https://www.browserstack.com/) and open a free trial account. Then copy the appsettings.browserstack.json from the root path into the bin folder. In this file, all the Browsers are defined to use later.
How BrowserStack works, especially the BrowserStack local, you can read on the [original documentation](https://www.browserstack.com/local-testing#command-line) of Browserstack.

#### Environment variables
Since there should be no credentials in the configuration file, the username and access key must be set as environment variables. The exact names of this vars are BROWSERSTACK_USERNAME and BROWSERSTACK_ACCESS_KEY.

## Know-how

### Specflow/Gherkin

#### Basics
Gherkin uses a set of special keywords to give structure and meaning to executable specifications. Each keyword is translated to many spoken languages; in this reference we’ll use English.
It’s very simple, but please read the [original reference](https://cucumber.io/docs/gherkin/reference/) to understand the code. There is also a nice [Video Tutorial](https://www.youtube.com/watch?v=xFrEXX1FrSQ) about Gherkin and Specflow.

#### Parameters & Tables
In most situations it is necessary to work with data. SpecFlow provides three basic methods for this purpose.

##### Basic Parameters
If you put in a Gherkin step a number or a text in apostrophes, SpecFlow interprets this as parameter.

```Gherkin
    When I'm searching for '*provision*'
```

```C#
    [When(@"I'm searching for '(.*)'")]
    public void WhenIProvision(string term)
    {
        Homepage.SearchFor(term + Keys.Enter);
        SearchResultPage = new SearchResultPage(Hooks.Driver);
    }
```

##### Data Tables
If you have one step with multiple values, you can use a Data Table. If necessary, each step can have its own data table. This is often used for form input. In the following example, the data table is needed for the assertion 'Then'.

```Gherkin
@distribution
Scenario: All distribution teasers are visible

    Given I change the language to 'EN'
    Then I see the following distribution teasers
    | title                                       | subTitle             |
    | Future provisions and wealth accumulation   | To product selection |
    | Property and asset insurance                | To product selection |
    | Health insurance                            | To product selection |
    | Property financing and residential property | To product selection |
```

```C#
​    [Then(@"I see the following distribution teasers")]
    public void ThenISeeTheFollowingDistributionTeasers(Table teasers)
    {
        …
    }
```

#### Examples Table
It is often necessary to run through scenarios with different data. This is particularly useful for multilingual elements. In this case, scenario outlines with example tables are used. 
The big difference here is that the scenario is executed once for each data row. The values are defined as variables in the different steps. And pay attention: it’s a Scenario Outline, not just a scenario!

```Gherkin
Scenario Outline: Check the titel translation
    When I change the language to '<lang>'
    Then The banner title should be '<title>'
        And The family teaser title schould be '<familyTeaserTitle>'
        And The occupational teaser title schould be '<occupationalTeaserTitle>'
    Examples: 
    | lang | title                                          | familyTeaserTitle               | occupationalTeaserTitle    |
    | DE   | Die passende Vorsorge für jede Lebensphase     | Vorsorge für Familien           | Berufliche Vorsorge        |
    | EN   | The right provision for each stage of life     | Future provisions for families  | Occupational provisions    |
    | FR   | La prévoyance adaptée à chaque phase de la vie | La prévoyance pour les familles | Prévoyance professionnelle |
    | IT   | La previdenza giusta per ogni fase della vita  | Previdenza per la famiglia      | Previdenza professionale   |
```

The values are then forwarded by SpecFlow as parameters.

```C#
    [Then(@"The occupational teaser title schould be '(.*)'")]
    public void ThenTheOccupationalTeaserTitleSchouldBe(string title)
    {
        Assert.Equal(title, Homepage.Banner.OccupationalTeaser.Header.Text);
    }
```

### Tagging
In Gherkin, both features and scenarios can be marked with tags. These tags can be used later to filter the tests. So tests can be categorized easily.
As an example, each feature can have the number of the corresponding PBI as a tag, or business and style guide tests can be executed in separate pipelines at different times.
Feature tags are inherited to each scenario.

```Gherkin
@Business @Homepage

Feature: Homepage elements

@distribution
Scenario: All distribution teasers are visible
```

### Step Dateien
If the behaviors are described in Gherkin, the necessary glue code must be written. This is done with the so-called step files. In these the necessary code is implemented for each step.
A new step file is created with `[Add]`, `[New Item...]`, `[SpecFlow]`, `[SpecFlow Step Definition]`. The name should be the same as the one of the feature file, simply extended with `.steps.cs`. This way the files are always displayed in the same way.

The example steps can then be deleted. But it is very important to work with scoped binding. So only the corresponding feature file sees the defined glue code.

```C#
    [Binding, Scope(Feature = "Homepage elements")]
    public sealed class HomepageSteps
```

For a new code block for a step, the cursor can be placed on the step in the Gherkin file and `[F12]` pressed. This copies the basic structure of the glue code to the clipboard. This can then simply be pasted into the step file.

### Hooks
For recurring steps that are required in all or most features, a hooks file can be created. This must NOT have scoped binding and will be seen by all feature files.

```C#
    [Given(@"I open the test page")]
    public void IOpenTheTestPage()
    {
        _testURL = "https://www.swisslife.ch/";
        Driver.Navigate().GoToUrl(_testURL);
    }
```

In the hooks file you can also define which code should be executed at which position of the test. For example, you can generate a screenshot after each scenario.

Further information and which hooks are provided by SpecFlow can be found in [this documentation](https://www.automatetheplanet.com/extend-test-execution-workflow-specflow-hooks/).

### Extensions
Inside the package FunkyBDD.SxS.Helpers is a helper method to compare SpecFlow tables with C# data tables.

```Gherkin
Scenario: Validate FunkyBDD.SxS.Helpers table comparer
    Then the TableComparer should work right
    | Col1    | Col2    | Col3    |
    | Value 1 | Value 2 | Value 3 |
```

```C#
    List<string> result = SpecflowHelpers.CompareTables(sTable, tTable);
    Assert.True(result.Count == 0, $"\r\n{string.Join("\r\n", result)}\r\n");
```

### Conclusion

Plan first:
- which steps occur in several Scenarion and/or features?
- What needs to be done before a test, a featuture or a scenario?
- What must be executed after a test, after a Featuture or after a Scenario?

All of this must be implemented in the Hooks.cs.

In the step files that specifically belong to a feature file, always work with scoped binding. But also remember that the scope will be lost if the feature title is renamed.

Avoid code duplication from the beginning.

## Selenium
Selenium automates browsers. That's it! What you do with that power is entirely up to you. Primarily, it is for automating web applications for testing purposes, but is certainly not limited to just that.

### WebDriver
The Selenium WebDriver is the interface to the different browsers. To work with it easily, there is a browser class in the package FunkyBDD.SxS.Helpers. This makes it very easy to create a WebDriver instance including additional parameters.

```C#
    Browser = new Browser("FirefoxLocal");
    Driver = Browser.Driver;
    Driver.SetSeleniumFlag();
```
You can see which browsers are already preconfigured in the file appsettings.browserstack.json.

### Selectors
If you have the connection to the browser, you can now select the different DOM elements.

```C#
IWebElement element = Driver.FindElement (By.Id("distributionteaser"));
```

An overview of all possibilities can be found in [this documentation](https://www.guru99.com/locators-in-selenium-ide.html).

### Ranorex Selocity
To localize the elements easily, Ranorex Selocity is a very powerful extension for Chrome. I always test in Firefox, but I just like the console of the Chrome browser better.
How it works you will see in [this short video](https://www.ranorex.com/selocity/browser-extension/#SelocityVideo). I also use Selocity very often to check my selectors. I don't work like in the video, where I copy the selectors 1:1, but I work with inheritances. But more about this topic APOM.

### Properties & methods
If an element is found, various properties can be queried from it.  The same applies to the methods that can be executed on an element.  
An overview over all methods you will find in [this documentation](https://www.toolsqa.com/selenium-webdriver/c-sharp/webelement-commands-in-c/).

### Extension
Since this requires a lot of hard work, I built the NuGet package FunkyBDD.SxS.Selenium.WebElement, which provides all basic properties and methods as a base class.

```C#
using FunkyBDD.SxS.Selenium.WebElement;
 …
 public class DistributionTeaser : DefaultProps
```

IWebDriver Driver       get the wrapped driver of the element. Needed for APOM
string BrowserName      get the name of the wrapped browser. Often needed for responsive tests
int Width
int Height
bool Displayed                  
bool Enabled            means also clickable
bool Found              means the element is there
int X, int Y            the coordinates of the element with IOS fallback
string color
string BackgroundColor

### APOM
APOM is the combination of Atomic Design and the Selenium Page Object Model paradigm. With this procedure model, the tests are not only robust but also maintainable. The initial effort is somewhat higher, but later when writing the tests, this time is loosely recovered. 

#### Atomic Design
To understand the really simple principle of Atomic design, you should read this [short documentation](http://bradfrost.com/blog/post/atomic-web-design/). 

#### APOM in practice
Each individual page consists of several organisms. These in turn consist of several molecules, which in turn consist of several atoms. It is important that a) the organisms are clearly identifiable (e.g. by IDs) and that they reference themselves downwards. 

This means that we search for a molecule, then we search for it isolated in the referencing organism. And this method goes on to the atoms.

Thus one is independent where and in which hierarchy level the organisms, molecules and atoms are placed. 

And if something changes for example with an icon (and that will happen sooner or later), then only the corresponding atom must be adapted, and all tests work again.

But the biggest advantage is that the developers can easily assemble their tests later with this kit and only have to develop the assertions.

### BrowserStack
Browserstack is an online service with which Selenium can run tests on real (mobile) devices and older browsers.

In order to work with [BrowserStack](https://www.browserstack.com/), you first need an account. You can open a test account. Then you have to copy the file appsettings.browserstack.json into the bin folder in the solution to get an initial configuration.

Since there should be no credentials in the configuration file, the username and access key must be set as environment variables. The exact names of this vars are BROWSERSTACK_USERNAME and BROWSERSTACK_ACCESS_KEY.

After that you can simple initialize a Browser instance with

```C#
    Browser = new Browser("iPhoneXS");
    Driver = Browser.Driver;
 ```
 

## xUnit
xUnit is used in this project as a testrunner as well as for the assertions. As a developer, however, you are mainly concerned with assertions.
A cheat sheet for the assertions can be [found here](https://lukewickstead.wordpress.com/2013/01/16/xunit-cheat-sheet/).

## API Testing
In this basic solution also a very simple API test is implemented. Instead of using a browser via Selenium a simple API class is used. So that no browser is opened, the corresponding feature is marked with the tag @API. This is evaluated in the hooks.
We will use this later in the How-to section.

## Azure DevOps
To run the tests in Azure DevOps, there is a short [introduction Video](https://www.youtube.com/watch?v=vTWV1x1lg6Q). For detailed information about Azure DevOps testing, read the [Original Documentation](https://docs.microsoft.com/en-us/azure/devops/pipelines/test/test-glossary?view=azure-devops).

# How-to

## Create a new Feature
First and most important: Feature Files should NOT be copied! The connection to the code behind gets lost with the renaming and you have to fix everything manually.
To create a new feature file, you should always use the normal dialog in Visual Studio. Right click the Folder `[Specs]` and then select `[Add]`, `[New Item…]`, `[SpecFlow]`, `[SpecFlow Feature File]`. So you will have a new, clean feature file.

If the feature file is completed with tags and a meaningful feature title, the corresponding step file can be created. Right click the Folder `[Specs]` and then select `[Add]`, `[New Item…]`, `[SpecFlow]`, `[SpecFlow Step Definition]`. The name should be the same as the feature file with the extension `.steps.cs`. Thus, the files are always displayed under each other. After the step file is generated, make it scoped. 

```C#
[Binding, Scope(Feature = "Homepage elements")]
```

Now you can write the Gherkin descriptions and with F12 on the Gherkin steps you can create the step definitions for the step file.

## Create a new Atom
To create a new Atom (the smallest part in the APOM) you can create a new class in the folder [Atoms] and extend this class with the base class [DefaultProps]. Then create a constructor inside to initialize the atom. 
Here it is important to work with the parent, i.e. the organism, as a parameter. So we look for the atom within the organism and you have a clear reference.

## Creating a new molecule or organism
A new molecule or organism is created like an atom. Again, it is important to pass the parent on and use it in the constructor. So, these elements can not only be reused very flexibly but the APOM becomes very maintainable.

## Create a new page
To create a new page, create a new class and then extend the base class `[Basepage]`. Then you can create a new page with the organisms you have created. Which is again composed of molecules that are a group of atoms. This is the APOM principle.

## Link Feature Steps to APOM

Once the APOM is created, the Feature Steps can consume the elements. In the constructor of the step file the corresponding page is instantiated

```C#
    public FrameworkTests()
    {
        Homepage = new Homepage(Hooks.Driver);
    }
```

Then you can access all sub-elements, e.g. 

```C#
var x = Homepage.Banner.FamilyTeaser.Header.Text;
```

## Run Tests only with specific filters
As an example, how you can run only specific tests in the pipeline later, take a look to the file [RunTests.bat]

## Test with BrowserStack

## Testing with long running workflows
2 Pipelines

## Management of failed tests
Teams Messaging 
