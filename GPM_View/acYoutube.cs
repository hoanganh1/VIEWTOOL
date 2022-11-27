using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPM_View
{
    internal class acYoutube
    {
        public UndetectChromeDriver driver { get; set; }
        public acYoutube(UndetectChromeDriver driver)
        {
            this.driver = driver;
        }
        public void gotoHome()
        {
            try { driver.Url = "https://www.youtube.com/"; } catch { }
        }
        void clickAction(IWebElement Xpath)
        {
            Actions actionss = new Actions(driver);
            actionss.MoveToElement(Xpath).Click().Build().Perform();
        }
        public bool searchKeyword(string txt)
        {
            driver.Url = "https://www.youtube.com/results?search_query=" + WebUtility.UrlEncode(txt);
            try
            {
                return true;
            }
            catch { return false; }
        }
        string getXpathVideo(string url)
        {
            string idVideo = "";
            try { idVideo = url.Replace("/watch?v=", "`").Split('`')[1].Split('&')[0]; }
            catch
            {
                idVideo = url.Replace("u.be/", "`").Split('`')[1].Split('&')[0];
            }
            return idVideo;
        }
        string getXpathChannel(string url)
        {
            string idVideo = "";
            try { idVideo = url.Replace("channel/", "`").Split('`')[1].Split('&')[0]; }
            catch
            {
                idVideo = url.Replace("channel/", "`").Split('`')[1].Split('&')[0];
            }
            return idVideo;
        }
        public int checkResultVideo(string urrl)
        {
            try
            {
                string idVideo = getXpathVideo(urrl);
                var item = driver.FindElements(By.XPath("//ytd-video-renderer//ytd-thumbnail//a"));
                int number = 0;
                foreach(var temp in item)
                {
                    string urlVideo = temp.GetAttribute("href");
                    if (urlVideo.Contains(idVideo))
                    {
                        return number;
                    }
                    number++;
                }
                return -1;
            }
            catch { return -1; }
        }
        public int checkResultChannel(string urrl)
        {
            try
            {
                string idVideo = getXpathChannel(urrl);
                var item = driver.FindElements(By.XPath("//ytd-video-renderer//ytd-channel-name//a"));
                int number = 0;
                foreach (var temp in item)
                {
                    if (number % 2 == 0)
                    {
                        continue;
                    }
                    string urlVideo = temp.GetAttribute("href");
                    if (urlVideo.Contains(idVideo))
                    {
                        return number;
                    }
                    number++;
                }
                return -1;
            }
            catch { return -1; }
        }
        Random random = new Random();
        public bool clickPalyList()
        {
            try
            {
                int str = Convert.ToInt32(driver.ExecuteScript("var t = document.getElementsByClassName('ytd-playlist-thumbnail').length; return t;"));
                if(str > 0)
                {
                    int t = random.Next(0, str);
                    driver.ExecuteScript("document.getElementsByClassName('ytd-playlist-thumbnail')["+ t + "].click()");
                }
                return true;
            }
            catch { return false; }
        }
        public bool setRand()
        {
            try
            {
                
                var lop = driver.FindElement(By.XPath("//ytd-playlist-loop-button-renderer//button[@class='style-scope yt-icon-button']"));
                string title = lop.GetAttribute("aria-label");
                if ((title == "Loop video") || (lop.GetAttribute("aria-label") == "Видеог давтах"))
                {


                }
                else { lop.Click(); }
                Thread.Sleep(500);
                var rand = driver.FindElement(By.XPath("//ytd-menu-renderer//ytd-toggle-button-renderer[@class='style-scope ytd-menu-renderer style-grey-text size-default']//button[@class='style-scope yt-icon-button']"));
                if((rand.GetAttribute("aria-label") == "Shuffle playlist") || (rand.GetAttribute("aria-label") == "Тоглуулах жагсаалтыг холих"))
                {

                }
                else { rand.Click(); }
                Thread.Sleep(500);
                return true;
            }
            catch { return false; }
        }
        public bool checkPercen(int count,int all,int limit )
        {
            int result = ((count * 100) / all);
            if(limit <= result)
            {
                return true;
            }
            else { return false; }
        }
        public int getTimeVideo()
        {
            try
            {
                string str = Convert.ToString(driver.ExecuteScript("var t = document.getElementsByClassName('ytp-time-duration')[0].textContent; return t;"));
                int total = 0;
                var item = str.Split(':');
                if(item.Length == 3)
                {
                    total += Convert.ToInt32(item[0]) * 3600 + Convert.ToInt32(item[1]) * 60 + Convert.ToInt32(item[2]);
                }
                if (item.Length == 2)
                {
                    total += Convert.ToInt32(item[0]) * 60 + Convert.ToInt32(item[1]);
                }
                if (item.Length == 1)
                {
                    return 0;
                }
                if(total < 200)
                {
                    return 0;
                }
                return total;
            }
            catch
            {
                return 0;
            }
        }
        public bool clickVideo(int num)
        {
            try
            {
                var item = driver.FindElements(By.XPath("//ytd-video-renderer//ytd-thumbnail//a"))[num];
                item.Click();
                return true;
            }
            catch { return false; }
        }
        public bool playVideo()
        {
            try
            {
                //driver.Url = driver.Url + "&t=2s";
                return true;
            }
            catch { return false; }
        }
        public bool likeVideo()
        {
            try
            {
                driver.ExecuteScript("document.getElementsByTagName('ytd-video-primary-info-renderer')[0].getElementsByTagName('ytd-menu-renderer')[0].getElementsByClassName('ytd-toggle-button-renderer')[0].click()");
                return true;
            }
            catch { return false; }
        }
        public bool subVideo()
        {
            try
            {
                var icon = driver.ExecuteScript("document.getElementsByClassName('style-scope ytd-subscribe-button-renderer')[0].click()");
                Thread.Sleep(3000);
                try { driver.ExecuteScript("document.getElementsByClassName('style-scope ytd-subscribe-button-renderer')[1].click()"); Thread.Sleep(3000); } catch { }


                try { driver.ExecuteScript("document.getElementsByClassName('ytd-subscription-notification-toggle-button-renderer')[0].click()");
                    Thread.Sleep(3000);
                    try
                    {
                        driver.ExecuteScript("document.getElementsByClassName('ytd-menu-service-item-renderer')[0].click()");
                    }
                    catch { }
                    Thread.Sleep(3000);
                } catch { 
                }

                return true;
            }
            catch { return false; }
        }
        public bool goToComment(string text)
        {
            
            try
            {
                int x = 100;
                for (int i = 1; i < 50; i++)
                {
                    driver.ExecuteScript("window.scrollTo(100," + (x * i) + ")");
                    try
                    {
                        var cmt = driver.FindElement(By.XPath("//ytd-comment-simplebox-renderer//yt-formatted-string"));
                        clickAction(cmt);
                        break;
                    }
                    catch
                    {
                    }
                    Thread.Sleep(300);
                }
                var send = driver.FindElement(By.XPath("//ytd-comment-simplebox-renderer//yt-formatted-string//div"));
                senComment(send, text);
                Thread.Sleep(3000);
                driver.FindElement(By.XPath("//ytd-comment-simplebox-renderer//ytd-button-renderer[@id='submit-button']//yt-button-shape")).Click();
                Thread.Sleep(6000);
                return true;
            }
            catch { return false; }
        }
        public void senComment(IWebElement Xpath,string daya)
        {
            foreach(var item in daya)
            {
                Actions actionss = new Actions(driver);
                actionss.MoveToElement(Xpath).SendKeys(item +"").Build().Perform();
                Thread.Sleep(50);
            }
        }

        public void scroll()
        {
            Random random = new Random();

            int x = (random.Next(1,1000));
            int j = (random.Next(1, 500));
            driver.ExecuteScript("window.scrollTo(100," + (x * j + ")"));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);
            Thread.Sleep(TimeSpan.FromSeconds(2));
            driver.ExecuteScript("window.scrollTo({ top: 0, behavior: 'smooth' });");
        }

        public void viewVideo(int totalTimeVideo, int startPercent, int endPercent)
        {
            int timeAction = 20;
            Actions action = new Actions(driver);

            // Start video
            action.KeyDown(OpenQA.Selenium.Keys.NumberPad0);
            action.SendKeys(OpenQA.Selenium.Keys.NumberPad0).Perform();

            int rand = random.Next(startPercent, endPercent);
            int sleepTime = rand * totalTimeVideo / 100;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(12);
            Thread.Sleep(TimeSpan.FromSeconds(10));

            //Scroll after 20min
            if (sleepTime > timeAction * 60)
            {
                int sleepCount = sleepTime / (timeAction*60);
                int sleepCountDiv = sleepTime % (timeAction*60);
                if (sleepCountDiv > 0)
                {
                    sleepCount += 1;
                }
                for (int j = 0; j < sleepCount; j++)
                {
                    if (j == sleepCount - 1)
                    {
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(sleepCountDiv);
                        Thread.Sleep(TimeSpan.FromSeconds(sleepCountDiv));
                    }
                    else
                    {
                        viewVideoInTimeAction(timeAction*60);
                    }
                }
                
            }
            else
            {
                Thread.Sleep(TimeSpan.FromSeconds(sleepTime));
            }
            Thread.Sleep(5000);
        }

        public void viewVideoInTimeAction(int sleepTime)
        {
            int div = sleepTime / 60;
            int count = sleepTime % 60;
            if(div> 0)
            {
                count += 1;
            }

            for(int i= 0; i< count; i++)
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
                Thread.Sleep(TimeSpan.FromSeconds(60));
                if (i % 3 == 0)
                {
                    viewComment();
                    scrollToTop();
                }
                if(i == 1)
                {
                    likeVideo();
                }

                if(i % 7 == 0)
                {
                    pauseVideo();
                    resumeVideo();
                }

            }
        }


        public void viewComment()
        {
            Random random = new Random();
            int x = (random.Next(1, 1000));
            int j = (random.Next(1, 1000));
            driver.ExecuteScript("window.scrollTo(100," + (x * j + ")"));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }

        public void scrollToTop()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
            driver.ExecuteScript("window.scrollTo({ top: 0, behavior: 'smooth' });");
        }

        public void pauseVideo()
        {
            Actions action = new Actions(driver);
            // Pause video
            action.SendKeys(OpenQA.Selenium.Keys.Space).Perform();
        }

        public void resumeVideo()
        {
            Actions action = new Actions(driver);
            // Resume video
            action.SendKeys(OpenQA.Selenium.Keys.Space).Perform();
        }

        public void seekVideo(int percent)
        {
            string key = "";
            Actions action = new Actions(driver);
            switch (percent)
            {
                case 1:
                    key = OpenQA.Selenium.Keys.NumberPad1;
                    break;
                case 2:
                    key = OpenQA.Selenium.Keys.NumberPad2;
                    break;
                case 3:
                    key = OpenQA.Selenium.Keys.NumberPad3;
                    break;
                case 4:
                    key = OpenQA.Selenium.Keys.NumberPad4; 
                    break;
                case 5:
                    key = OpenQA.Selenium.Keys.NumberPad5;
                    break;
                case 6:
                    key = OpenQA.Selenium.Keys.NumberPad6;
                    break;
                case 7:
                    key = OpenQA.Selenium.Keys.NumberPad7;
                    break;
                case 8:
                    key = OpenQA.Selenium.Keys.NumberPad8;
                    break;
                case 9:
                    key = OpenQA.Selenium.Keys.NumberPad9; 
                    break;
                default:
                    action.SendKeys(key).Perform();
                    break;
            }
                // seek video
                action.SendKeys(key).Perform();
         }

        public void prevVideo()
        {
            Actions action = new Actions(driver);
            // Next video
            action.KeyDown(OpenQA.Selenium.Keys.Shift).SendKeys("p").Perform();
        }
        public void nextVideo()
        {
            Actions action = new Actions(driver);
            // Next video
            action.KeyDown(OpenQA.Selenium.Keys.Shift).SendKeys("s").Perform();
        }

        public void ceek10sNext()
        {
            Actions action = new Actions(driver);
            // Next 10s
            action.SendKeys("l").Perform();
        }

        public void ceek10sPrev()
        {
            Actions action = new Actions(driver);
            // Prev 10s
            action.SendKeys("j").Perform();
        }

    }
}
