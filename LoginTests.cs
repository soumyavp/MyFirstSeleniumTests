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
    public class LoginTests
    {
        IWebDriver driver = null;

        string USERNAME = "//*[@id=\"login-form\"]/fieldset/label[1]/input";
        string PASSWORD = "//*[@id=\"login-form\"]/fieldset/label[2]/input";
        string LOGIN_BUTTON = "//*[@id=\"login-form\"]/fieldset/button";

        [SetUp]
        public void Initialise()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://cafetownsend-angular-rails.herokuapp.com");
        }

        [Test]
        public void OpenLoginPage()
        {
            WebDriverWait DriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            DriverWait.Until(driver => driver.FindElement(By.Id("login-form")));
            IWebElement ElementLoginForm = driver.FindElement(By.Id("login-form"));
            Assert.AreEqual("loginForm", ElementLoginForm.GetAttribute("name"));
        }
        
        [Test]
        public void LoginSuccessful()
        {
            //Wait for the page to load
            WebDriverWait DriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            DriverWait.Until(driver => driver.FindElement(By.Id("login-form")));
            //Enter the username and password
            IWebElement ElementUserName = driver.FindElement(By.XPath(USERNAME));
            IWebElement ElementPassword = driver.FindElement(By.XPath(PASSWORD));

            ElementUserName.SendKeys("Luke");
            ElementPassword.SendKeys("Skywalker");

            //Login
            driver.FindElement(By.XPath(LOGIN_BUTTON)).Click();

            //Assert that Login is successful
             //Wait for the page to load
            WebDriverWait DriverWaitEmployee = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            DriverWaitEmployee.Until(driver => driver.FindElement(By.XPath("/html/body/div/header/div/p[1]")));
            
            Assert.IsTrue(driver.FindElement(By.XPath("/html/body/div/header/div/p[1]")).Enabled);
            Console.WriteLine("Login is successful");

        }
        
        [Test]
        public void LoginUnsuccesful ()
        {
            //Wait for the page to load
            WebDriverWait DriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            DriverWait.Until(driver => driver.FindElement(By.Id("login-form")));
            //Enter the username and password
            IWebElement ElementUserName = driver.FindElement(By.XPath(USERNAME));
            IWebElement ElementPassword = driver.FindElement(By.XPath(PASSWORD));

            ElementUserName.SendKeys("admin");
            ElementPassword.SendKeys("admin");

            //Login
            driver.FindElement(By.XPath(LOGIN_BUTTON)).Click();

            //Assert that Login is unsuccessful
            Assert.IsTrue(ElementUserName.Displayed);
            Assert.IsTrue(ElementPassword.Displayed);

        }

        [TearDown]
        public void Terminate()
        {
            driver.Quit();
        }
    }
}
