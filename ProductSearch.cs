using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using static Excercise.Work.Utils.StaticHelpers;
using Excercise.Work.Utils;
using OpenQA.Selenium.Support.Extensions;



namespace Excercise.Work
{
    [TestFixture]                            //Entirely optional; indicates to NUnit that the class has tests, aids humans, irrelevant to NUnit
    internal class ProductSearch : Utils.BaseTest   //Inherits SetUp and TearDown from BaseTest
    {
              
        //public class ProductSearch//      //Adding a base class to inherit code from
        
            //public IWebDriver driver;

            //[SetUp]
            //public void SetUp()
            //{
            //    ////Maximising chrome screen when launching
            //    //ChromeOptions option = new ChromeOptions();
            //    //option.AddArgument("start-maximized");
            //    //driver = new ChromeDriver(option);

            //    //Implicit Wait
            //    //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            //}

            //[TearDown]
            //public void TearDown()
            //{
            //    //Close the browser
            //    //Thread.Sleep(3000);
            //    //driver.Close();
            //}

            [Test, Category("Functional")]
            public void SearchItem()
            {
                //Attempt Search
                Console.WriteLine("Starting Search");

                //IWebDriver = new ChromeDriver
                driver.Url = "https://www.edgewordstraining.co.uk/demo-site/";

                //Search for Cap
                Console.WriteLine("In search field, type in cap");
                driver.FindElement(By.Id("woocommerce-product-search-field-0")).SendKeys("Cap");

                //Click submit button
                Console.WriteLine("Search item is submitted");
                driver.FindElement(By.Id("woocommerce-product-search-field-0")).Submit();

                //Adding Cap to basket
                Console.WriteLine("Clicking 'add to cart' link");
                driver.FindElement(By.CssSelector(".woocommerce-store-notice__dismiss-link")).Click();              //Need to dismiss the bottom link on the firefox page and add an explicit wait
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.FindElement(By.Name("add-to-cart")).Click();
                //driver.FindElement(By.ClassName(".single_add_to_cart_button.button.alt")).Submit();
                //driver.FindElement(By.CssSelector("button[name='add-to-cart']")).Submit();
                //driver.FindElement(By.CssSelector("#product-29 > div.summary.entry-summary > form > button")).Submit();

                //Clicking Cart Link
                Console.WriteLine("Opening the Cart");
                driver.FindElement(By.LinkText("Cart")).Click();

                //Removing Cap from the basket
                Console.WriteLine("Removing the Cap from the Cart");
                driver.FindElement(By.CssSelector("#post-5 > div > div > form > table > tbody > tr.woocommerce-cart-form__cart-item.cart_item > td.product-remove > a")).Click();
                //driver.FindElement(By.ClassName("remove")).Click();
                //driver.FindElement(By.LinkText("×")).Click();     -->Alternative method

                //Adding an explicit wait
                Console.WriteLine("Adding an explicit wait as the removal takes some time");

                //Synchronisation
                //WebDriverWait WaitForEIDisplayed = new WebDriverWait(driver, TimeSpan.FromSeconds(10));            
                //WaitForEIDisplayed.Until(drv => drv.FindElement(By.PartialLinkText("Return to shop")).Displayed);

                //Use of instance helper class, passing the driver
                //Helper help = new Helper(driver);
                //Then using your help object you have access to the methods of the instance class
                //help.WaitForElement(By.PartialLinkText("Return to shop"), 10);

                //Use of static helper
                WaitForElement(By.PartialLinkText("Return to shop"), 10, driver);

                driver.FindElement(By.PartialLinkText("Return to shop")).Click();

                //Click Cart Link at top of page
                Console.WriteLine("Clicking Cart Link again");
                driver.FindElement(By.LinkText("Cart")).Click();

                //Screenshot capturing the Cart is empty
                Console.WriteLine("Screenshot captured showing an empty cart");
                //Use of Static Helper
                //IWebElement form = driver.FindElement(By.Id("main"));
                //ITakesScreenshot formss = form as ITakesScreenshot;
                //var screenshotForm = formss.GetScreenshot();
                //screenshotForm.SaveAsFile(@"C:\Users\NethmaWimalasuriya\OneDrive - nFocus Limited\Pictures\Screenshots\fullpage.png", ScreenshotImageFormat.Png);
                TakeScreenshotOfElement(driver, By.Id("main"), "Screenshot-EmptyCart.png");
                TestContext.WriteLine("Screenshot taken - see report");
                TestContext.AddTestAttachment(@"C:\Users\NethmaWimalasuriya\OneDrive - nFocus Limited\Pictures\Screenshots\fullpage.png");

                //Assertion
                Assert.That(driver.FindElement(By.CssSelector(".cart-empty")).Displayed);

                //Validation
                Console.WriteLine("Validate the Cart is empty");
                driver.FindElement(By.LinkText("Cart")).Click();
                Assert.That(driver.FindElement(By.CssSelector(".cart-empty")).Displayed);

                //Test is completed
                Console.WriteLine("The test is completed");





            }

            //[Test]
            //public void Test1()
            //{
            //    Assert.Pass(); 
            //}
        
    }
}