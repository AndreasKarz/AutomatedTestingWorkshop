# Automated Testing with Specflow, xUnit and Selenium

## Overview

This workshop shows the interaction of the individual components Specflow, xUnit and Selenium. 
The focus is on Selenium and the development of a framework with reusable components.

The Visual Studio base solution for this workshop is located at [GitHub](https://github.com/AndreasKarz/AutomatedTestingWorkshop). 

### BDD

Automated testing is a central aspect of Behavior Driven Development. To understand the idea behind BDD, the following [video on YouTube](https://www.youtube.com/watch?v=PR7WIS8JYyY) is recommended.

### Specflow

Specflow is the C#/Visual Studio version of Cucumber. Specflow connects the Gherkin feature files with .net code and generates tests from them. 

Specflow parses the feature files and connects them to the C# step files, which connect to the browser via Selenium.

### xUnit

xUnit is required in the step files to verify the assertions. Also can an additional logging be implemented with xUnit, with which e.g. alarms can be triggered automatically.

### Selenium

*Selenium automates browsers*. What you do with that power is entirely up to you. Primarily, it is for automating web applications for testing purposes. 

## Framework Setup

On [GitHub](https://github.com/AndreasKarz/AutomatedTestingWorkshop) you will find a base solution for Visual Studio. Nevertheless, the structure should be understood. In addition, the preparations must also be carried out when using the base solution.

### Visual Studio Extension

As a first step the Specflow Extension for Visual Studio must be installed, if not already done. Please read the [original Specflow manual](https://specflow.org/getting-started/#InstallSetup). After that Visual Studio should be restarted.

### New Visual Studio Project

Since Specflow currently does not run correctly with .Net Core, the project type "Class Library (.NET Framework)" must be used in Visual Studio. 

Since xUnit is used as the test runner, the nUnit template must not be used. A simple class library is sufficient for our project.

### InstallPackages

Once the new project has been created, the following Nuget packages must be installed:

#### ```Install-Package SpecFlow.xUnit```

Test connector between Specflow and xUnit to run the tests in the testrunner. For all the possible configuration options please visit the [original documentation](https://specflow.org/documentation/Configuration/) of Specflow.

#### ```Install-Package xunit.runner.visualstudio```

Visual Studio testrunner 

#### `Install-Package Selenium.Chrome.WebDriver`

For the beginning of this workshop we work with the Chrome Browser. More browsers will follow later.

#### `Install-Package SwissLife.Selenium.Webdriver.Extensions`

Extensions for the Selenium Webdriver with functionalities which are not (yet) implemented in the standard driver.

#### `Install-Package SwissLife.Selenium.IWebElement.DefaultProps`

Extensions for the IWebElement to extend the atoms (will be explained later) with default default properties.

### Set the environment variables

In order to be as flexible as possible during configuration and so that the same tests are executed later on different platforms, all dynamic values should be handled using environment variables.
For this workshop we need the following two environment variables:

```
TEST_URL => http://automationpractice.com/

TEST_BROWSER => Chrome
```

To set environment variables in Windows, press  `WIN`  and  `BREAK`  and then select  `Advanced system settings`  =>  `Environment Variables` .

### Hooks

A powerful feature of Specflow is the ability to work with hooks. This allows not only recurring steps to be handled centrally, they also extend the test execution workflow .

### Folder structure

Now we prepare the directory structure before we check with a first small test whether our base works as expected and everything is installed and configured correctly.

As first folder we need one with the name `GherkinSpecs`.  The remaining folders will be created later.

### First base test

To validate the base framework create the file `FrameworkTests.feature` in the folder `GherkinSpecs`. In it we describe as an expectation that the browser should be opened with our test page and then checked a dummy assertion. 

```gh
Feature: The base framework works well
	In order to 
		run automated tests
	As a 
		tester
	I want to 
		have a working base framework

Scenario: All packages correctly installed
	Given I open the test page
	When I do nothing
	Then The dummy assertion works
```

The Given will be in the hooks but for the When and the Then step we need a step file. Create for this in the same folder the file FrameworkTests.feature.steps.cs

```c#
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

```

Run a rebuild of the solution and open the Test Window in Visual Studio. Now you will find the first test and you can run it. If the test goes green, the installation and configuration is correct.

## Specflow

Specflow is the C#/Visual Studio version of Cucumber. Specflow connects the Gherkin feature files with .net code and generates tests from them. 

Specflow parses the feature files and connects them to the C# step files, which connect to the browser via Selenium.

### Feature files (Gherkin)

In the feature files, the product owner or business analyst defines the expectations of the product. The syntax used for this is Gherkin, an understandable description language that is an interface between business and IT.

For more details visit the [original documentation](https://specflow.org/documentation/Using-Gherkin-Language-in-SpecFlow/) 

The Gherkin language defines the structure and a basic syntax for describing tests and consists of the following components:

#### Feature

The feature element provides a header for the feature file. The feature element includes the name and a high level description of the corresponding feature in your application. SpecFlow generates a unit test class for the feature element, with the class name derived from the name of the feature.

#### Scenarios

A feature file may contain multiple scenarios used to describe the feature's acceptance tests. Scenarios have a name and can consist of multiple scenario steps. 

#### Table and multi-line text arguments

You can include tables and multi-line arguments in scenario steps. These are used by the step definitions and are either passed as additional `Table` or `string` arguments.

#### Tags

Tags are markers that can be assigned to features and scenarios. Assigning a tag to a feature is equivalent to assigning the tag to all scenarios in the feature file. 

If your unit test framework does not support categories, you can still use tags to implement special logic for tagged scenarios in hooks, scoped bindings or step definitions by querying the tag property.

#### Background

The background language element allows specifying a common precondition for all scenarios in a feature file. The background part of the file can contain one or more scenario steps that are executed before any other steps of the scenarios. 

#### Scenario Outlines

Scenario outlines can be used to define data-driven acceptance tests. They can be also seen as scenario templates. The scenario outline always consists of a scenario template specification (a scenario with data placeholders using the `<placeholder>` syntax) and a set of examples that provide values for the placeholders. 

Each Scenario will run once for each data set.

#### Comments

You can add comment lines to the feature files at any place by starting the line with `#`. Be careful however, as comments in your specification can be a sign that acceptance criteria have been specified wrongly.

#### Example

Copy the file GherkinSample.feature.txt in the folder `GherkinSpecs` and remove the postfix .txt from the filename. Then open the file in Visual Studio.  

All the purple code means that the corresponding steps do not yet exist in the scenarios. So create in the same folder the *SpecFlow Step Definition* file `GherkinSample.feature.steps.cs`

Now we can create all the steps for the scenarios.

##### Scenario: Show tops for women 

The Given step is set by the background. Set the cursor on the first purple line an then press F12 in Visual Studio. A dialog appears asking if the glue code should be copied to the clipboard. Click on Yes and passing the code into the step file.

```Ghe
Scenario: Show tops for women
	# Given is set by background
		And I have click on the Tab WOMEN
	When I click on the category Tops
	Then I see two items
```

```c#
[Given(@"I have click on the Tab WOMEN")]
public void GivenIHaveClickOnTheTabWOMEN()
{
    ScenarioContext.Current.Pending();
}

```

When you save the file, the purple color will removes from the step into the scenario and when you now press F12, Visual Studio go to the corresponding step.

Repeat this for each step in this scenario, remove from every step the line ScenarioContext.Current.Pending(); and save the file. 

When you now rebuild the solution there will be another test wich only open the browser and close it after the scenario.

##### Scenario Outline: Test the search function

Repeat the steps above for this scenario. When you now run this test, this scenario will be executed twice. This is also displayed in the test runner.

```Ghe
Scenario Outline: Test the search function
	# Given is set by background
	When I search for <therm>
	Then I will recieve <count> results
	Examples: 
	| therm | count |
	| shoe  | 7     |
	| funky | 0     |
```

```c#
[When(@"I search for (.*)")]
public void WhenISearchForShoe(string Therm)
{

}

[Then(@"I will recieve (.*) results")]
public void ThenIWillRecieveResults(int Results)
{

}
```

As you see in the steps, we have now parameters that we can use later.

#### Scenario: Check the tabs

Repeat the steps above for this scenario. Now you see, we have a table as argument with different values.  

In contrast to the Outline scenario, the Scenarion is executed only once and all data is delivered as a table for the step. The data from the table must now be processed within the step.

```Ghe
Scenario: Check the tabs
	# Given is set by background
	Then I see all my tabs
    | Tab | Label    |
    | 1   | WOMEN    |
    | 2   | DRESSES  |
    | 3   | T-SHIRTS |
```

```c#
[Then(@"I see all my tabs")]
public void ThenISeeAllMyTabs(Table table)
{
    
}

```

When you debug the selected test and set a breakpoint into the step then you see the data in the table var.

### Hooks

SpecFlow comes with some predefined hooks that are executed after some events are fired during the tests’ execution. To make an analogy, think about TestInitialize and TestCleanup from MSTest framework. For more information and a description of all the possibilities read [this article](https://www.automatetheplanet.com/extend-test-execution-workflow-specflow-hooks/).

In this example I have rename the Class1.cs from the template to Hooks.cs and then I have implemented a solid base functionality. 

#### BeforeScenario

This code will run before each scenario starts. In this example we have two BeforeScenario hooks with a different order.  

The first `BeforeFeature` hook read the configuration for the browser from the environment variable and set the value to the class property. 

*Why don't we do that in a BeforeFeature hook?* With this technique we can use later  tags to execute specific scenarios (e.g. tests only for mobile platforms) on specific platforms. This will be discussed later in the workshop as the second `BeforeFeature` hook.

The third `BeforeFeature` hook now initializes the desired browser.

#### AfterScenario

This code runs after each scenario. In our example we use it to close the browser and dispose the driver instance.

#### AfterFeature

This code runs after the complete test of all scenarios. In this example I use it to destroy all open driver processes.

#### The first global Given

In this example we always need to open out testpage. This will be a given step in the Hooks class. This step read the URL from the environment variable and set the value to the class property. This step can you now use in every feature file and if change on time the name of the environment variable for example than you only change the code at one place.

```c#
[Given(@"I open the test page")]
public void IOpenTheTestPage()
{
    if (Environment.GetEnvironmentVariable("TEST_URL") == null)
    {
        _testURL = "http://automationpractice.com/";
    }
    else
    {
        _testURL = Environment.GetEnvironmentVariable("TEST_URL");
    }
    Driver.Navigate().GoToUrl(_testURL);
}
```

You will find the complete code on [GitHub](https://github.com/AndreasKarz/AutomatedTestingWorkshop). 

### Step files

The step file contains the so-called glue code, which establishes the connection between the Gherkin code and the Selenium objects, which we will get to know later.

I recommend the same name for naming as the feature file with the postfix .steps.cs. So the feature and step files are always together.

If steps occur in several feature files, they should be moved to the hooks.

### Tags

Tags are markers that can be assigned to features and scenarios. Assigning a tag to a feature is equivalent to assigning the tag to all scenarios in the feature file. 

On the one hand, you can use the tags to execute scenarios grouped in the test runner. This can be used, for example, to define whether certain scenarios are to be executed every hour.

On the other hand, certain processes can also be executed in the hooks using scope binding.

```Ghe
@Firefox
Scenario: Test only for Firefox
	# Given is set by background
```

In our example we use  tags to set the browser for special scenarios. For this example copy the code below in the hooks file.

```c#
[BeforeScenario(Order = 2)]
[Scope(Tag = "Chrome")]
public void SetChrome()
{
    _browserName = "Chrome";
}

[BeforeScenario(Order = 2)]
[Scope(Tag = "Firefox")]
public void SetFirefox()
{
    _browserName = "Firefox";
}
```

To use the Firefox browser, we need to install the Firefox Webdriver Package first.

Run `Install-Package Selenium.Firefox.WebDriver` in the Package Manager Console and add the line `using OpenQA.Selenium.Firefox;` at the head of the hooks file.

Then insert the code below in the switch(browser) as new case

```c#
case "Firefox":
	FirefoxOptions firefoxOptions = new FirefoxOptions();
	firefoxOptions.AcceptInsecureCertificates = true;
	firefoxOptions.AddArguments("-purgecaches", "-private");
	Driver = new FirefoxDriver("./", firefoxOptions);
	break;
```

When you now run the new test "Test only for Firefox" this scenario should run allways in Firefox.

The concept behind it is simple. 
In [`BeforeScenario(Order = 1)]` the browser is read from the environment variable. If no environment variable is set, Chrome is used as default (e.g. for local development). Now all tests in the CD pipeline can be executed with different browsers by writing a different browser into the environment variable during each run.
The `[BeforeScenario(Order = 2)]` now checks whether the browser is explicitly overridden by a tag. This is especially necessary if certain tests are to be executed on mobile devices only, for example. 
In `[BeforeScenario(Order = 3)]` the corresponding browser will be initialized.

For more detailed information about hooks I recommend the post [Using Hooks to Extend Test Execution Workflow]( https://www.automatetheplanet.com/extend-test-execution-workflow-specflow-hooks/) 

## xUnit

### Assertion

### Logging

### Configuration

## Atomic Design

### Atoms

#### Default Props

### Molecules

### Pages

## Selenium

### Driver

#### Timeouts

#### Capabilities

#### Extensions

### Selectors

#### ClassName

#### TagName

#### Id

#### CssSelector

#### xPath

#### LinkText

#### PartialLinkText

#### Trick shots

### Methods

#### Click

#### SendKeys

#### Clear

#### Cookies

#### Navigate

#### Actions

#### Thread.Sleep

### Properties

#### Text

#### Location

#### Size

#### GetCssValue

#### Enabled

#### Displayed

#### GetAttribute