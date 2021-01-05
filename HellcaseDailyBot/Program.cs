using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace HellcaseDailyBot
{
    class Program
    {
        static void Main(string[] args)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("incognito");
            //options.BinaryLocation = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe";

            IWebDriver driver = new ChromeDriver(options);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(5));
            driver.Navigate().GoToUrl(@"https://hellcase.com/login");

            string loggedInURL = @"https://hellcase.com/en";
            wait.Until(ExpectedConditions.UrlToBe(loggedInURL));
            Console.WriteLine($"\nReached {loggedInURL}\n");

            string dailyFreeURL = @"http://hellcase.com/en/dailyfree";
            string dailyFreeButtonId = "btn_open_daily_free";
            //driver.FindElements(By.ClassName("core-dailyfree"))[0].Click();
            driver.Navigate().GoToUrl(dailyFreeURL);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(dailyFreeButtonId)));
            Console.WriteLine($"\nReached {dailyFreeURL}\n");

            //dailyfreeloop
            while (true)
            {
                driver.Navigate().Refresh();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(dailyFreeButtonId)));

                driver.FindElement(By.Id(dailyFreeButtonId)).Click();
                Console.WriteLine($"Clicked daily free button at {DateTime.Now.ToString("HH:mm:ss")}");

                //wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("hell-winner-left")));
                //Console.WriteLine($"Daily reward claimed at {DateTime.Now.ToString("HH:mm:ss")}");
                Console.WriteLine("Starting 24h+1m wait");
                Thread.Sleep(TimeSpan.FromMinutes(60 * 24 + 1));
            }
        }
    }
}
