using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPM_View.Controller
{
    public class DriverHelper
    {
        private IWebDriver _driver;
        public DriverHelper(IWebDriver driver)
        {
            this._driver = driver;
        }

        // Token: 0x06000002 RID: 2 RVA: 0x00002060 File Offset: 0x00000260
        public void GoToUrl(string url)
        {
            for (;;)
            {
                try
                {
                    this._driver.Navigate().GoToUrl(url);
                }
                catch
                {
                    this._driver.Navigate().Refresh();
                    Thread.Sleep(3000);
                    continue;
                }
                break;
            }
        }

        // Token: 0x06000003 RID: 3 RVA: 0x000020B0 File Offset: 0x000002B0
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

        // Token: 0x06000004 RID: 4 RVA: 0x000020F0 File Offset: 0x000002F0
        public int HasElement(List<string> elementsXpath, int timeOut = 20)
        {
            int num = 0;
            int i;
            for (;;)
            {
                for (i = 0; i < elementsXpath.Count; i++)
                {
                    if (this._driver.FindElements(By.XPath(elementsXpath[i])).Count > 0)
                    {
                        return i;
                    }
                }
                Thread.Sleep(1000);
                num++;
                if (num > timeOut)
                {
                    return -1;
                }
            }
            return i;
        }

        // Token: 0x06000005 RID: 5 RVA: 0x00002144 File Offset: 0x00000344
        public void ScrollToElement(IWebElement element, int offset = 0)
        {
            ((IJavaScriptExecutor)this._driver).ExecuteScript("window.scrollTo({top: " + (element.Location.Y + offset).ToString() + ",behavior: 'smooth'})");
        }

        // Token: 0x06000006 RID: 6 RVA: 0x0000218E File Offset: 0x0000038E
        public void ScrollToBottom()
        {
            ((IJavaScriptExecutor)this._driver).ExecuteScript("window.scrollTo({top: document.body.scrollHeight ,behavior: 'smooth'});");
        }

        // Token: 0x06000007 RID: 7 RVA: 0x000021AB File Offset: 0x000003AB
        public void ScrollToPosition(int y)
        {
            ((IJavaScriptExecutor)this._driver).ExecuteScript("window.scrollTo({top: " + y.ToString() + ",behavior: 'smooth'})");
        }

        // Token: 0x06000008 RID: 8 RVA: 0x000021DC File Offset: 0x000003DC
        public void OpenNewTab(string url, bool isSwitchToNewTab = true)
        {
            ((IJavaScriptExecutor)this._driver).ExecuteScript("window.open('" + url + "')");
            Thread.Sleep(1000);
            if (this._driver.WindowHandles.Count > 1 && isSwitchToNewTab)
            {
                this._driver.SwitchTo().Window(this._driver.WindowHandles[this._driver.WindowHandles.Count - 1]);
            }
        }

        // Token: 0x06000009 RID: 9 RVA: 0x00002264 File Offset: 0x00000464
        public void CloseCurrentTab()
        {
            try
            {
                this._driver.Close();
                this._driver.SwitchTo().Window(this._driver.WindowHandles[0]);
            }
            catch
            {
            }
        }

        // Token: 0x0600000A RID: 10 RVA: 0x000022B4 File Offset: 0x000004B4
        public void ClickByJs(IWebElement element)
        {
            ((IJavaScriptExecutor)this._driver).ExecuteScript("arguments[0].click();", new object[]
            {
                element
            });
        }

        // Token: 0x0600000B RID: 11 RVA: 0x000022D6 File Offset: 0x000004D6
        public void GoToUrlByJs(string url)
        {
            ((IJavaScriptExecutor)this._driver).ExecuteScript("window.location.href = '" + url + "'");
        }

        // Token: 0x0600000C RID: 12 RVA: 0x00002300 File Offset: 0x00000500
        public void CloseAllOtherTab()
        {
            while (this._driver.WindowHandles.Count > 1)
            {
                this._driver.SwitchTo().Window(this._driver.WindowHandles[this._driver.WindowHandles.Count - 1]);
                this._driver.Close();
            }
            this._driver.SwitchTo().Window(this._driver.WindowHandles[0]);
        }

        // Token: 0x0600000D RID: 13 RVA: 0x00002384 File Offset: 0x00000584
        public void Quit()
        {
            try
            {
                while (this._driver.WindowHandles.Count > 0)
                {
                    this._driver.SwitchTo().Window(this._driver.WindowHandles[this._driver.WindowHandles.Count - 1]);
                    this._driver.Close();
                }
            }
            catch
            {
            }
            try
            {
                this._driver.Quit();
            }
            catch
            {
            }
            try
            {
                this._driver.Dispose();
            }
            catch
            {
            }
        }



        // Token: 0x06000010 RID: 16 RVA: 0x0000252C File Offset: 0x0000072C
        public static string GetLocalChromeVersion()
        {
            string text = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Google\\Chrome\\Application";
            if (!Directory.Exists(text))
            {
                text = text.Replace(" (x86)", "");
            }
            if (!Directory.Exists(text))
            {
                return null;
            }
            foreach (string text2 in Directory.GetDirectories(text))
            {
                string text3 = text2.Substring(text2.LastIndexOf('\\') + 1);
                if (text3.Contains("."))
                {
                    return text3.Split(new char[]
                    {
                        '.'
                    })[0];
                }
            }
            return null;
        }

        // Token: 0x06000011 RID: 17 RVA: 0x000025B8 File Offset: 0x000007B8
        public static void CloseAllChrome()
        {
            foreach (Process process in Process.GetProcessesByName("chromedriver"))
            {
                try
                {
                    process.Kill();
                }
                catch
                {
                }
            }
            foreach (Process process2 in Process.GetProcessesByName("chrome"))
            {
                try
                {
                    process2.Kill();
                }
                catch
                {
                }
            }
        }

        // Token: 0x06000012 RID: 18 RVA: 0x00002634 File Offset: 0x00000834
        public static void CloseAllFirefox()
        {
            foreach (Process process in Process.GetProcessesByName("geckodriver"))
            {
                try
                {
                    process.Kill();
                }
                catch
                {
                }
            }
            foreach (Process process2 in Process.GetProcessesByName("firefox"))
            {
                try
                {
                    process2.Kill();
                }
                catch
                {
                }
            }
        }

    }
       
    }
