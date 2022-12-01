using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPM_View.Controller
{
    class LoginAutomation
    {
        private UndetectChromeDriver _driver;
        private Random _rand = new Random();
        

        public LoginAutomation(UndetectChromeDriver driver)
        {
            this._driver = driver;
        }


        public bool WaitElementByXpath(string xpath, int timeOut = 60)
        {
            int num = 0;
            while (this._driver.FindElements(By.XPath(xpath)).Count == 0)
            {
                Thread.Sleep(1000);
                num++;
                if (num > timeOut)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Phương thức kiểm tra xem đã đăng nhập google hay chưa
        /// </summary>
        /// <returns></returns>
        public bool Login(string user, string pass , string recovery)
        {

            DriverHelper driverHelper = new DriverHelper(_driver);
            try
            {
                _driver.Get("https://accounts.google.com/");
                viaDelay();
                if (_driver.Url.Contains("myaccount."))
                {
                    return true;
                }
                string url = _driver.Url;
                driverHelper.WaitElementByXpath("//input[@name='identifier']", 60);
                IWebElement webElement = _driver.FindElement(By.XPath("//input[@name='identifier']"));
                viaSendKey(webElement, user);
                Thread.Sleep(1000);
                webElement.SendKeys(Keys.Enter);
                waitToURLChanged(_driver, url);
                viaDelay();
                driverHelper.WaitElementByXpath("//input[@name='Passwd']", 60);
                webElement = _driver.FindElement(By.XPath("//input[@name='Passwd']"));
                viaSendKey(webElement, pass);
                Thread.Sleep(1000);
                webElement.SendKeys(Keys.Enter);
                waitToURLChanged(_driver, url);
                viaDelay();
                if (_driver.FindElements(By.XPath("//*[@data-challengetype='12']")).Count > 0)
                {
                    url = _driver.Url;
                    IWebElement element = _driver.FindElement(By.XPath("//*[@data-challengetype='12']"));
                    driverHelper.ClickByJs(element);
                    waitToURLChanged(_driver, url);
                    viaDelay();
                    url = _driver.Url;
                    IWebElement webElement2 = _driver.FindElement(By.XPath("//input[@name='knowledgePreregisteredEmailResponse']"));
                    viaSendKey(webElement2, recovery);
                    Thread.Sleep(1000);
                    webElement2.SendKeys(Keys.Enter);
                    waitToURLChanged(_driver, url);
                    viaDelay();
                }
                _driver.Get("https://mail.google.com/");
                Thread.Sleep(5000);
                if (_driver.Url.Contains("mail.google.com"))
                {
                   // driverHelper.Quit();
                    return true;
                }

            }
            catch
            {
            }

            return false;
        }


        protected void viaSendKey(IWebElement webElement, string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                webElement.SendKeys(text[i].ToString());
                Thread.Sleep(this._rand.Next(20, 50));
            }
        }
        protected void viaDelay()
        {
            Thread.Sleep(this._rand.Next(1000, 2000));
        }

        protected void waitToURLChanged(IWebDriver driver, string oldUrl)
        {
            try
            {
                int num = 0;
                while (oldUrl == driver.Url)
                {
                    num++;
                    Thread.Sleep(1000);
                    if (num > 15)
                    {
                        throw new Exception("Không chờ chuyển URL");
                    }
                }
                this.viaDelay();
            }
            catch
            {

            }
          
        }
    }
}
