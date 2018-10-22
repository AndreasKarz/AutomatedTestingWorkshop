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

xUnit is required as test runner and in the step files to verify the assertions. 

### Assertion

The assertion is needed in the [Then...] steps to validate the expected result. A small dummy sample you will find in the *GherkinSample.feature.steps.cs* file.

With assertions, one point is very important:

**Each scenario should only contain ONE test!** 

Otherwise you will never know if everything works as expected. If, for example, 5 tests are executed in one scenario and the 3rd test terminates with an error, then it is not clear whether points 4 and 5 would have worked.

Therefore again very important: **ONE scenario contains ONE test**!

For the most common assertion please checkout this [cheat sheet](https://lukewickstead.wordpress.com/2013/01/16/xunit-cheat-sheet/#index73) from line #73

### Configuration

For the configuration details please visit the [original documentation](https://xunit.github.io/docs/configuring-with-json)

### Getting Test Results in Visual Studio Team Services

Please visit the [original documentation](https://xunit.github.io/docs/getting-test-results-in-vsts)

## Atomic Design

Before we come to the most important part of the workshop, we make a short excursion to the topic atomic design to understand. This will help us later to build a clean and modular framework.

Please read also the [original documentation](http://bradfrost.com/blog/post/atomic-web-design/) from Brad Frost. 

### Atoms

Atoms are the basic building blocks of matter. Applied to web interfaces, atoms are our HTML tags, such as a form label, an input or a button.   

#### Default Props

All the atoms have some defaut values like width, height or the position.  To simplify the structure of the atoms, the extender class SwissLife.Selenium.IWebElement.DefaultProps.cs is implemented. It provides all getter for the default props. Thus only the specific properties and methods have to be defined in the atom itself.

```c#
using OpenQA.Selenium;
using SwissLife.Selenium.IWebElement.DefaultProps;

public class InputText : DefaultProps
{
    //InputText has now all the getters for the default props
}
```

### Molecules

Molecules are React components at altitude. Let's take again the example text input. The HTML input element is the atom, but our React component is a combination of html input field, label and a description and/or error message. At this moment the React component TextInput is a molecule.

Molecule tests are carried out directly on the style guide. This includes, for example, that the floating label floats correctly and does not overlay the content above or that an error message has the correct color and is displayed at the correct position.

Also all tests of the Responsive Design and the behavior on mobile devices of the molecules are executed on the styleguide.

### Organisms

An organism is a composition of different molecules. This can be, for example, a contact form. This consists of several text entries - some with special functions, such as an e-mail field.

The form is therefore an organism whose basic behavior is also tested on the style guide - especially the responsive behavior. 

### Pages

The pages are a composition of different organisms and/or molecules.

The tests of the pages are those which will be performed later in the CD pipeline. These only contain the functional tests, i.e. if the correct error messages are displayed, the correct confirmation pages appear or the texts are correct in all languages.

### Summary

With this understanding we can build the framework very modular and flexible. 

Imagine if the color of a text input changes at some point. With the concept of Atomic design we only have to adapt the test to the text atom of the TextInput molecule and that only in one place.

## Selenium

*Selenium automates browsers*. What you do with that power is entirely up to you.  And Selenium is not only powerful, but is also the main ingredient in automated UI testing.

### Driver

The driver is the API to the corresponding browser. Later Selenium will use this API to control the browser remotely. Each browser has its own driver, the corresponding browser is installed, of course.

For platforms, which are not locally available, BrowserStack can be used. This topic will be dealt with in another workshop. 

Currently we have installed the driver for Chrome and Firefox in our example. Now it's time to install the rest as well over the Nuget Console.

```npm
Install-Package Selenium.WebDriver.IEDriver
Install-Package Selenium.WebDriver.IEDriver64
Install-Package Selenium.WebDriver.MicrosoftDriver

```

#### Cookies

TODO

#### Navigate

TODO

#### Timeouts

Specifies the amount of time the driver should wait when searching for an element if it is not immediately present.

When searching for a single element, the driver should poll the page until the element has been found, or this timeout expires before throwing a *NoSuchElementException*. When searching for multiple elements, the driver should poll the page until at least one element has been found or this timeout has expired.

In our example, this is defined in the hooks in the _initDriver method. 

```c#
Wait = new OpenQA.Selenium.Support.UI.WebDriverWait(Driver, TimeSpan.FromSeconds(10));
Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
```

#### Options/Capabilities

This is used to make basic settings for the browser. This setting is debrecated, but will be used later for BrowserStack.

In this workshop we will therefore use the Driver options. Please copy the following code in the hooks of the _initBrowser method into the switch:

```c#
case "Edge":
    Driver = new EdgeDriver("./");
    break;
case "IE":
    InternetExplorerOptions options = new InternetExplorerOptions();
    options.EnsureCleanSession = true;
    options.IgnoreZoomLevel = true;               
    options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
    options.InitialBrowserUrl = "https://www.swisslife.com/";
    Driver = new InternetExplorerDriver("./", options);
    Driver.Navigate().Refresh();
    Driver.LocalStorageClear();
    break;

```

And in the *BeforeScenario Order = 2* region

```c#
[BeforeScenario(Order = 2)]
[Scope(Tag = "Edge")]
public void SetEdge()
{
    _browserName = "Edge";
}

[BeforeScenario(Order = 2)]
[Scope(Tag = "IE")]
public void SetIE()
{
    _browserName = "IE";
}
```

#### Extensions

The driver does not (yet) provide certain functionalities. Therefore we have installed the extension *SwissLife.Selenium.Webdriver.Extensions*. More information about you will find in the source code on [GitHub](https://github.com/AndreasKarz/SeleniumWebdriverExtensions).

This package will add the following functionalities to the driver:

##### NavigateToPath

Allows to navigate with relative URLs

##### LocalStorage

LocalStorageSetItem, LocalStorageGetItem and LocalStorageClear to manipulate the LocalStorage.

##### IsElementPresent

Check if a element is really present with some options

##### GetElementSafe

Get a element if this really exists and it's visible and it's clickable.

##### SetMobileSize

Resize the browser to a specific width and height. Without parameters the function will use 750 * 1024 pixels.

##### ExecuteScript

Wrapper methode to execute a JavaScript.

##### GetRootDirectory

Get the repository root directory

### Selectors (By)

The selectors are used to identify the DOM elements in the browser. His used as a parameter for the driver method FindElement or FindElements, which searches for the element in the DOM.

Here it is extremely important to have understood Atomic Design. You should always keep in mind what could be changed in the design and how the selectors have to be structured so that any changes can be tested with as little effort as possible.

The most important point here is certainly that you don't always search the whole DOM, but identify it cascadingly through the elements.

First you identify the organism "form", then within the form the molecule "first name" and then via the atom "TextInput" the corresponding atom within the molecule.  So the composition of the page can be changed later - the tests will continue to work.

==**In a well built framework the selectors are only needed inside the atoms.**==

#### Name

Finds the element using the name. 

```c#
Driver.FindElement(By.Name("firstname"));
```

#### ClassName

Finds the element using the ClassName. Should only be used within organisms, because a class name is rarely unique within the whole DOM. Attention, a change in the design can have an impact to the test.

```c#
Driver.FindElement(By.ClassName("m-rich-text"));
```

#### TagName

If you search a specific unique element inside a molecule you can use this selector. It should be noted, however, that if a 2nd identical element suddenly enters the same atom, e.g. an image or a link, the selector will no longer work as expected.

```c#
Driver.FindElement(By.TagName("span"));
```

#### Id

The clearest selector. Within the DOM, an ID must be unique, so this selector always finds the correct element. 

It is recommended that at least every organism has a unique ID.

```c#
Driver.FindElement(By.Id("contact_form"));
```

#### LinkText

Find the link element with matching visible text. Attention: The link text must be completely identical! This also means that as soon as even a part of the link text changes, this selector will no longer work.

```c#
Driver.FindElement(By.LinkText("Next"));
```

#### PartialLinkText

Find the link element with partial matching visible text. This selector still works if the link text changes marginally as long as the searched term continues to occur.

```c#
Driver.FindElement(By.PartialLinkText("Step"));
```

#### CssSelector

This is a very powerful selector and works on the principle of jQuery. Not all pseudo classes are supported, but most work.

To test the CSS selectors the Chrome extension [Selector Gadget](https://chrome.google.com/webstore/detail/selectorgadget/mhjhnkcfbdhnjickkkdbjoemdmbfginb) is very helpful.

```c#
Driver.FindElement(By.CssSelector(".g-lg-span-6:nth-child(1)"));
```

#### xPath

Also a very powerful selector, but also one with high danger potential. xPath searches through the DOM with path instructions and a beginner error is to always describe the path from the root.

With the smallest position shift of an element, such a selector will no longer work!

Therefore, it is very important to think in Atomic Design and build the path.

```c#
Driver.FindElement(By.XPath(".//*[@data-test-id='" + Id + "']"));
```

#### Best practice

##### Think atomic

This applies to both development and testing. This is the only way to gain the flexibility not to have to rewrite all tests when changing the UI.

```html
<form id="contactform">
    <input name="firstname" />
    <input name="password" />
</form>
<form id="loginform">
    <input name="username" />
    <input name="password" />
</form>
```

```c#
// molecule "contactform"
_parent = Driver.FindElement(By.Id("contact_form"));
_password = _parent.FindElement(By.Name("password");
// molecule "loginform"
_parent = Driver.FindElement(By.Id("login_form"));
_password = _parent.FindElement(By.Name("password");

```

With this strategy, for example, the forms can be placed anywhere within the UI and several forms can be present simultaneously in the same UI -- the "password" molecule will always be uniquely identifiable via the _parent.

##### Use the right selectors to stay flexible

For each selector, think about what influence a possible change in the UI will have on it.

A link text is often changed -- i.e. the LinkText selector will no longer work, the PartialLinkText will.

Class names are also very dangerous. Especially selectors on class names of known frameworks like Bootstrap. If this is changed, ALL these selectors will not work anymore. If at all, pseudo classes should be used. But what happens if CSS classes suddenly have the same name as your pseudo classes?

What happens if the designer suddenly has the idea to display the error message with a DIV instead of a SPAN? Your TagName selector will stop working at the same moment! Better give the container of the error message a pseudotag like data-test-type="errormsg" and then use an xPath selector. So you stay flexible.

##### Use the right way

A little personal tip -- work from the atoms towards the organism and not vice versa.

### Properties

Once the element has been identified, the various properties can now be accessed. 

As an example we extend the step `[Then(@"I see all my tabs")]` in the step file *GherkinSample.feature.steps.cs* and we use a selector that we should NEVER use in practice.

```c#
IWebElement PhoneNumber = Hooks.Driver.FindElement(By.CssSelector(".shop-phone"));
```

Now we can access the properties of this element.

#### Text

Returns the text of the element, in this example the text with the phone number. 

```c#
string Label = PhoneNumber.Text;
```

#### Location

Gets a System.Drawing.Point object containing the coordinates of the upper-left corner of this element relative to the upper-left corner of the page.

```c#
int Left = PhoneNumber.Location.X;
int Top = PhoneNumber.Location.Y;
```

#### Size

Gets the height and width  of this element in pixel;

```c#
int Height = PhoneNumber.Size.Height;
int Width = PhoneNumber.Size.Width;
```

#### GetCssValue

Get the specific CSS value of this element.

```c#
string VAlign = PhoneNumber.GetCssValue("vertical-align");
```

#### Enabled

This property must not be confused with Displayed. Enabled returns whether a form element can be used or not. EVERY element returns true, except form elements that are explicitly set to disabled.

```c#
bool Enabled = PhoneNumber.Enabled;
```

#### Displayed

Gets a value indicating whether or not this element is displayed.

```c#
bool Displayed = PhoneNumber.Displayed;
```

#### GetAttribute

Gets the value of the specified attribute for this element. The attribute's current value. Returns a null if the value is not set.

```c#
string ClassName = PhoneNumber.GetAttribute("class");
```

#### TagName

Gets the tag name of this element.

```c#
string TagName = PhoneNumber.TagName;
```

### Methods

Now it is not the idea only to query values but also to execute actions. Therefore Selenium also offers methods for the elements.

#### SendKeys()

Sends a text or control character to an input field. 

```c#
Searchbox.SendKeys("Shoe");
```

#### Clear()

Clears the content of this input element.

```c#
Searchbox.Clear();
```

#### Submit()

If this current element is a form, or an element within a form, then this will be submitted to the web server. If this causes the current page to change, then this method will block until the new page is loaded.

```c#
Searchbox.Submit();
```

#### Thread.Sleep()

A method that does not come from selenium but from C# -- but is very often needed.

For animations, for example, you have to wait some time to validate properties until the animation is finished. Or after a submit to wait to the new content.

```c#
Thread.Sleep(1000);
```

#### Click()

Simulate a click on this element.

```c#
DemoProduct.Click();
```

# Compose the framework

Now we can start building our framework. Here we have to remember the Atomic design all the time. In this example we focus on the search.

## The atoms

First we need 2 atoms input type="text" and the button with the icon. 

### InputText

For the DOM object `<input type="text">`  move the file /_CodeSnippets/InputText.txt to /Selenium/Atoms/InputText.cs and then open it in the editor.

The atom has a constructor that searches within the parent (molecule) for a HTML input of type text.

Now we have to consider which properties and methods we will need for this atom. Always think about the future, what might come. The DefaultProps come from the class SwissLife.Selenium.IWebElement.DefaultProps, so you only have to define what is needed additionally.

For atoms it is very important to always document in the code. This simplifies cooperation enormously later on.

### Button

move the file /_CodeSnippets/Button.txt to /Selenium/Atoms/Button.cs and then open it in the editor.

After the constructor, we need only the methode Click() and the Icon to validate the right icon.

## The molecule

No we can compose the search box as molecule. Create the class SearchBox.cs inside /Selenium/Molecules with the code below.

```c#
using OpenQA.Selenium;
using Framework.Selenium.Atoms;

namespace Framework.Selenium.Molecules
{
    public class SearchBox
    {
        private IWebElement _component;
        public InputText Input;
        public Button Button;

        public SearchBox(IWebElement Parent, By by)
        {
            _component = Hooks.Driver.FindElement(by);
            Input = new InputText(_component);
            Button = new Button(_component);
        }
    }
}

```

You see, the more we work our way up, the less code we need. It's more and more just a composing.

## The organism

The first organism in our example is the header row with the logo. In our example, only the search box is filled with the logo, you can expand the organism later.

Create the class HeaderRow.cs inside /Selenium/Organisms with the code below.

```c#
using OpenQA.Selenium;
using Framework.Selenium.Molecules;

namespace Framework.Selenium.Organisms
{
    public class HeaderRow
    {
        private IWebElement _component;
        public SearchBox Search;

        public HeaderRow(IWebElement Parent, By by)
        {
            _component = Hooks.Driver.FindElement(by);
            Search = new SearchBox(_component, By.Id("searchbox"));
        }
    }
}

```

A little less code. Pay attention to the namespacing: It's structured in a way that you can't directly access the molecules or atoms.

In this example we select the molecule with the ID. If the molecule had no ID, we could have instantiated it using the tagName form.

## The page

Now we can build the page object. Create the class Homepage.cs inside /Selenium/Pages with the code below.

```c#
using OpenQA.Selenium;
using Framework.Selenium.Organisms;

namespace Framework.Selenium.Pages
{
    public class Homepage
    {
        private IWebElement _component;
        public HeaderRow Header;

        public Homepage()
        {
            _component = Hooks.Driver.FindElement(By.TagName("body"));
            Header = new HeaderRow(_component, By.CssSelector("#header div:nth-of-type(3) .container .row"));
        }
    }
}
```

The selector for the organism is a little bit tricky. In practice, this element should have a unique ID.

## Use the element

If we have done everything right, we can use the page object in the step file and build the tests accordingly.

Extend the scenario Test the search function with a new And after the Given comment

```Gher
And Im on the Homepage
```

Copy the step code with F12 an copy it into the step file. Extend the using section and add a private property _homepage. 

Then instantiate the homepage in the new Given.

```c#
using TechTalk.SpecFlow;
using Framework.Selenium.Pages;
using Xunit;

namespace AutomatedTestingWorkshop.GherkinSpecs
{
    [Binding]
    public sealed class GherkinSample
    {
        private Homepage _homepage;

        [Given(@"Im on the Homepage")]
        public void GivenImOnTheHomepage()
        {
            _homepage = new Homepage();
        }
...

```

Then you can extend the step I search for...

```c#
        [When(@"I search for (.*)")]
        public void WhenISearchForShoe(string Therm)
        {
            _homepage.Header.Search.Input.SendKeys(Therm);
            _homepage.Header.Search.Button.Click();
        }
```

Do you realize how quickly we can write tests when atoms and molecules exist?

## Next step

Examine the test results page. For the current example we need a new atom span, a new molecule counter, a new organism SearchResultHeader and a new page SearchResults.

In which scenario step do you have to initialize Page SearchResults?

What else do you have to do to validate the number of hits?





