using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Protractor.Samples.Basic
{
    [TestFixture]
    public class BasicTests
    {
        private IWebDriver _driver;

        [SetUp]
        public void SetUp()
        {
            // Using NuGet Package 'PhantomJS'
            _driver = new ChromeDriver();

            // Using NuGet Package 'WebDriver.ChromeDriver.win32'
            //driver = new ChromeDriver();

            _driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(5));
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }

        [Test]
        public void ShouldGreetUsingBinding()
        {
            IWebDriver ngDriver = new NgWebDriver(_driver);
            ngDriver.Navigate().GoToUrl("http://www.angularjs.org");
            ngDriver.FindElement(NgBy.Model("yourName")).SendKeys("Julie");
            Assert.AreEqual("Hello Julie!", ngDriver.FindElement(NgBy.Binding("yourName")).Text);
        }

        [Test]
        public void ShouldListTodos()
        {
            var ngDriver = new NgWebDriver(_driver);
            ngDriver.Navigate().GoToUrl("http://www.angularjs.org");
            var elements = ngDriver.FindElements(NgBy.Repeater("todo in todoList.todos"));
            Assert.AreEqual("build an angular app", elements[1].Text);
            Assert.AreEqual(false, elements[1].Evaluate("todo.done"));
        }
    }
}
