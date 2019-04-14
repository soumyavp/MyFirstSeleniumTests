using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    [TestFixture]
    class EmployeeTests
    {
        IWebDriver driver = null;

        string USERNAME = "//*[@id=\"login-form\"]/fieldset/label[1]/input";
        string PASSWORD = "//*[@id=\"login-form\"]/fieldset/label[2]/input";
        string FIRST_NAME = "/html/body/div/div/div/form/fieldset/label[1]/input";
        string LAST_NAME = "/html/body/div/div/div/form/fieldset/label[2]/input";
        string START_DATE = "/html/body/div/div/div/form/fieldset/label[3]/input";
        string EMAIL = "/html/body/div/div/div/form/fieldset/label[4]/input";

        string CREATE_BUTTON = "//ul[@id=\"sub-nav\"]/li[1]/a[@id=\"bAdd\"]";
        string EDIT_BUTTON = "//ul[@id=\"sub-nav\"]/li[2]/a[@id=\"bEdit\"]";
        string DELETE_BUTTON = "//ul[@id=\"sub-nav\"]/li[3]/a[@id=\"bDelete\"]";

        string UPDATE_BUTTON = "/html/body/div/div/div/form/fieldset/div/button[1]";

        [SetUp]
        public void Initialise()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://cafetownsend-angular-rails.herokuapp.com");
        }

        [Test]
        public void CreateEmployeeSuccess()
        {
            //Explicitly Wait for the page to load
            WebDriverWait DriverWaitLogin = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            DriverWaitLogin.Until(driver => driver.FindElement(By.Id("login-form")));

            //Enter the username and password
            IWebElement ElementUserName = driver.FindElement(By.XPath(USERNAME));
            IWebElement ElementPassword = driver.FindElement(By.XPath(PASSWORD));

            ElementUserName.SendKeys("Luke");
            ElementPassword.SendKeys("Skywalker");

            //Login
            driver.FindElement(By.XPath("//*[@id=\"login-form\"]/fieldset/button")).Click();
            Console.WriteLine("Login Successful");
            //Explicitly Wait for the page to load
            WebDriverWait DriverWaitCreate = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            DriverWaitCreate.Until(driver => driver.FindElement(By.XPath(CREATE_BUTTON)));

            driver.FindElement(By.XPath(CREATE_BUTTON)).Click();

            //Explicitly wait for the new employee creation page to load
            WebDriverWait DriverWaitNew = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            DriverWaitNew.Until(driver => driver.FindElement(By.XPath(FIRST_NAME)));
            //Entering the valid details for Creating an employee record
            driver.FindElement(By.XPath(FIRST_NAME)).SendKeys("Tony");
            driver.FindElement(By.XPath(LAST_NAME)).SendKeys("Stark");
            driver.FindElement(By.XPath(START_DATE)).SendKeys("2019-04-01");
            driver.FindElement(By.XPath(EMAIL)).SendKeys("T.Stark@abc.com");

            //Save the record
            driver.FindElement(By.XPath("/html/body/div/div/div/form/fieldset/div/button[2]")).Click();

            //Explicitly wait for navigating to employee page
            DriverWaitCreate.Until(driver => driver.FindElement(By.XPath(CREATE_BUTTON)));

            Assert.IsTrue(driver.FindElement(By.XPath(CREATE_BUTTON)).Enabled);
        }

        [Test]
        public void CreateEmployeeCancel()
        {
            //Explicitly Wait for the page to load
            WebDriverWait DriverWaitLogin = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            DriverWaitLogin.Until(driver => driver.FindElement(By.Id("login-form")));

            //Enter the username and password
            IWebElement ElementUserName = driver.FindElement(By.XPath(USERNAME));
            IWebElement ElementPassword = driver.FindElement(By.XPath(PASSWORD));

            ElementUserName.SendKeys("Luke");
            ElementPassword.SendKeys("Skywalker");

            //Login
            driver.FindElement(By.XPath("//*[@id=\"login-form\"]/fieldset/button")).Click();
            
            //Explicitly Wait for the page to load
            WebDriverWait DriverWaitCreate = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            DriverWaitCreate.Until(driver => driver.FindElement(By.XPath(CREATE_BUTTON)));

            driver.FindElement(By.XPath(CREATE_BUTTON)).Click();

            //Explicitly wait for the new employee creation page to load
            WebDriverWait DriverWaitNew = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            DriverWaitNew.Until(driver => driver.FindElement(By.XPath(FIRST_NAME)));

            //Click on the cancel button
            driver.FindElement(By.XPath("//*[@id=\"sub-nav\"]/li/a[@class=\"subButton bCancel\"]")).Click();
            DriverWaitNew.Until(driver => driver.FindElement(By.XPath(CREATE_BUTTON)));

            Assert.IsTrue(driver.FindElement(By.XPath(CREATE_BUTTON)).Enabled);
        }

        [Test]
        public void CreateEmployeeWithoutMandatoryFields()
        {
            //Explicitly Wait for the page to load
            WebDriverWait DriverWaitLogin = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            DriverWaitLogin.Until(driver => driver.FindElement(By.Id("login-form")));

            //Enter the username and password
            IWebElement ElementUserName = driver.FindElement(By.XPath(USERNAME));
            IWebElement ElementPassword = driver.FindElement(By.XPath(PASSWORD));

            ElementUserName.SendKeys("Luke");
            ElementPassword.SendKeys("Skywalker");

            //Login
            driver.FindElement(By.XPath("//*[@id=\"login-form\"]/fieldset/button")).Click();
            
            //Explicitly Wait for the page to load
            WebDriverWait DriverWaitCreate = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            DriverWaitCreate.Until(driver => driver.FindElement(By.XPath(CREATE_BUTTON)));

            driver.FindElement(By.XPath(CREATE_BUTTON)).Click();

            //Explicitly wait for the new employee creation page to load
            WebDriverWait DriverWaitNew = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            DriverWaitNew.Until(driver => driver.FindElement(By.XPath(FIRST_NAME)));

            //START: First name validation
            //Entering the valid details without first name for Creating an employee record
            Console.WriteLine("Starting First name validation");
            driver.FindElement(By.XPath(LAST_NAME)).SendKeys("Stark");
            driver.FindElement(By.XPath(START_DATE)).SendKeys("2019-04-01");
            driver.FindElement(By.XPath(EMAIL)).SendKeys("T.Stark@abc.com");

            //Save the record
            driver.FindElement(By.XPath("/html/body/div/div/div/form/fieldset/div/button[2]")).Click();
            DriverWaitNew.Until(driver => driver.FindElement(By.XPath(FIRST_NAME)));
            Assert.IsTrue(driver.FindElement(By.XPath(FIRST_NAME)).Enabled);
            Assert.IsTrue(driver.FindElement(By.XPath(FIRST_NAME)).GetAttribute("class").Contains("ng-invalid-required"));
            Console.WriteLine("First name validation completed");
            //END: First name validation

            //START: Last name validation
            //Entering the valid details without first name for Creating an employee record
            Console.WriteLine("Starting Last name validation");
            ClearCreateFields();
            driver.FindElement(By.XPath(FIRST_NAME)).SendKeys("Tony");
            driver.FindElement(By.XPath(START_DATE)).SendKeys("2019-04-01");
            driver.FindElement(By.XPath(EMAIL)).SendKeys("T.Stark@abc.com");
            //Save the record
            driver.FindElement(By.XPath("/html/body/div/div/div/form/fieldset/div/button[2]")).Click();
            DriverWaitNew.Until(driver => driver.FindElement(By.XPath(LAST_NAME)));
            Assert.IsTrue(driver.FindElement(By.XPath(LAST_NAME)).Enabled);
            Assert.IsTrue(driver.FindElement(By.XPath(LAST_NAME)).GetAttribute("class").Contains("ng-invalid-required"));
            Console.WriteLine("Last name validation completed");
            //END: Last name validation

            //START: Start date validation
            //Entering the valid details without first name for Creating an employee record
            Console.WriteLine("Starting Start Date validation");
            ClearCreateFields();
            driver.FindElement(By.XPath(FIRST_NAME)).SendKeys("Tony");
            driver.FindElement(By.XPath(LAST_NAME)).SendKeys("Stark");
            driver.FindElement(By.XPath(EMAIL)).SendKeys("T.Stark@abc.com");
            
            //Save the record
            driver.FindElement(By.XPath("/html/body/div/div/div/form/fieldset/div/button[2]")).Click();
            DriverWaitNew.Until(driver => driver.FindElement(By.XPath(START_DATE)));
            Assert.IsTrue(driver.FindElement(By.XPath(START_DATE)).Enabled);
            Assert.IsTrue(driver.FindElement(By.XPath(START_DATE)).GetAttribute("class").Contains("ng-invalid-required"));
            Console.WriteLine("Start Date validation completed");
            //END: Start Date validation

            //START: Email validation
            //Entering the valid details without first name for Creating an employee record
            Console.WriteLine("Starting Email validation");
            ClearCreateFields();
            driver.FindElement(By.XPath(FIRST_NAME)).SendKeys("Tony");
            driver.FindElement(By.XPath(LAST_NAME)).SendKeys("Stark");
            driver.FindElement(By.XPath(START_DATE)).SendKeys("2019-04-01");
            
            //Save the record
            driver.FindElement(By.XPath("/html/body/div/div/div/form/fieldset/div/button[2]")).Click();
            DriverWaitNew.Until(driver => driver.FindElement(By.XPath(EMAIL)));
            Assert.IsTrue(driver.FindElement(By.XPath(EMAIL)).Enabled);
            Assert.IsTrue(driver.FindElement(By.XPath(EMAIL)).GetAttribute("class").Contains("ng-invalid-required"));
            Console.WriteLine("Email validation completed");
            //END: Email validation
        }

        [Test]
        public void EditEmployeeSuccess()
        {
            //Explicitly Wait for the page to load
            WebDriverWait DriverWaitLogin = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            DriverWaitLogin.Until(driver => driver.FindElement(By.Id("login-form")));

            //Enter the username and password
            IWebElement ElementUserName = driver.FindElement(By.XPath(USERNAME));
            IWebElement ElementPassword = driver.FindElement(By.XPath(PASSWORD));

            ElementUserName.SendKeys("Luke");
            ElementPassword.SendKeys("Skywalker");

            //Login
            driver.FindElement(By.XPath("//*[@id=\"login-form\"]/fieldset/button")).Click();
            
            //Explicitly Wait for the page to load
            WebDriverWait DriverWaitCreate = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            DriverWaitCreate.Until(driver => driver.FindElement(By.XPath(CREATE_BUTTON)));

            driver.FindElement(By.XPath(CREATE_BUTTON)).Click();

            //Explicitly wait for the new employee creation page to load
            WebDriverWait DriverWaitNew = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            DriverWaitNew.Until(driver => driver.FindElement(By.XPath(FIRST_NAME)));
            //Entering the valid details for Creating an employee record
            driver.FindElement(By.XPath(FIRST_NAME)).SendKeys("Tony");
            driver.FindElement(By.XPath(LAST_NAME)).SendKeys("Stark");
            driver.FindElement(By.XPath(START_DATE)).SendKeys("2019-04-01");
            driver.FindElement(By.XPath(EMAIL)).SendKeys("T.Stark@abc.com");

            //Save the record
            driver.FindElement(By.XPath("/html/body/div/div/div/form/fieldset/div/button[2]")).Click();

            //Explicitly wait for navigating to employee page
            DriverWaitCreate.Until(driver => driver.FindElement(By.XPath(CREATE_BUTTON)));

            Assert.IsTrue(driver.FindElement(By.XPath(DELETE_BUTTON)).GetAttribute("class").Contains("disabled"));

            DriverWaitNew.Until(driver => driver.FindElement(By.XPath("//*[@id=\"employee-list\"]/li[1]")));

            var ulElement = driver.FindElement(By.XPath("//*[@id=\"employee-list\"]"));
            var liElements = ulElement.FindElements(By.TagName("li"));
            foreach (var li in liElements)
            {
                if (li.Text == "Tony Stark")
                {
                    li.Click();
                    driver.FindElement(By.XPath(EDIT_BUTTON)).Click();
                    driver.FindElement(By.XPath(FIRST_NAME)).Clear();
                    driver.FindElement(By.XPath(FIRST_NAME)).SendKeys("Thor");
                    driver.FindElement(By.XPath(LAST_NAME)).Clear();
                    driver.FindElement(By.XPath(LAST_NAME)).SendKeys("Mjolnir");
                    driver.FindElement(By.XPath(UPDATE_BUTTON)).Click();
                    break;
                }
            }

            //Explicitly wait for navigating to employee page
            DriverWaitNew.Until(driver => driver.FindElement(By.XPath(CREATE_BUTTON)));

            ulElement = driver.FindElement(By.XPath("//*[@id=\"employee-list\"]"));
            liElements = ulElement.FindElements(By.TagName("li"));
            foreach (var li in liElements)
            {
                if (li.Text == "Thor Mjolnir")
                {
                    Assert.IsTrue(li.Text == "Thor Mjolnir");
                    break;
                }
            }
            
        }

        [Test]
        public void DeleteEmployeeSuccess()
        {
            //Explicitly Wait for the page to load
            WebDriverWait DriverWaitLogin = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            DriverWaitLogin.Until(driver => driver.FindElement(By.Id("login-form")));

            //Enter the username and password
            IWebElement ElementUserName = driver.FindElement(By.XPath(USERNAME));
            IWebElement ElementPassword = driver.FindElement(By.XPath(PASSWORD));

            ElementUserName.SendKeys("Luke");
            ElementPassword.SendKeys("Skywalker");

            //Login
            driver.FindElement(By.XPath("//*[@id=\"login-form\"]/fieldset/button")).Click();
            
            //Explicitly Wait for the page to load
            WebDriverWait DriverWaitCreate = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            DriverWaitCreate.Until(driver => driver.FindElement(By.XPath(CREATE_BUTTON)));

            driver.FindElement(By.XPath(CREATE_BUTTON)).Click();

            //Explicitly wait for the new employee creation page to load
            WebDriverWait DriverWaitNew = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            DriverWaitNew.Until(driver => driver.FindElement(By.XPath(FIRST_NAME)));
            //Entering the valid details for Creating an employee record
            driver.FindElement(By.XPath(FIRST_NAME)).SendKeys("Tony");
            driver.FindElement(By.XPath(LAST_NAME)).SendKeys("Stark");
            driver.FindElement(By.XPath(START_DATE)).SendKeys("2019-04-01");
            driver.FindElement(By.XPath(EMAIL)).SendKeys("T.Stark@abc.com");

            //Save the record
            driver.FindElement(By.XPath("/html/body/div/div/div/form/fieldset/div/button[2]")).Click();

            //Explicitly wait for navigating to employee page
            DriverWaitCreate.Until(driver => driver.FindElement(By.XPath(CREATE_BUTTON)));

            Assert.IsTrue(driver.FindElement(By.XPath(DELETE_BUTTON)).GetAttribute("class").Contains("disabled"));

            DriverWaitNew.Until(driver => driver.FindElement(By.XPath("//*[@id=\"employee-list\"]/li[1]")));

            var ulElement = driver.FindElement(By.XPath("//*[@id=\"employee-list\"]"));
            var liElements = ulElement.FindElements(By.TagName("li"));
            foreach(var li in liElements)
            {
                if (li.Text == "Tony Stark")
                {
                    li.Click();
                    driver.FindElement(By.XPath(DELETE_BUTTON)).Click();
                    driver.SwitchTo().Alert().Accept();
                    break;
                }
            }
            
            //Explicitly wait for navigating to employee page
            DriverWaitCreate.Until(driver => driver.FindElement(By.XPath(EDIT_BUTTON)).GetAttribute("class").Contains("disabled"));

            Assert.IsTrue(driver.FindElement(By.XPath(DELETE_BUTTON)).GetAttribute("class").Contains("disabled"));
        }

        [TearDown]
        public void Terminate()
        {
            driver.Quit();
        }

        private void ClearCreateFields()
        {
            driver.FindElement(By.XPath(FIRST_NAME)).Clear();
            driver.FindElement(By.XPath(LAST_NAME)).Clear();
            driver.FindElement(By.XPath(START_DATE)).Clear();
            driver.FindElement(By.XPath(EMAIL)).Clear();
        }
    }
}
