using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPM_View
{
    class VideoAutomation
    {
        Random ran = new Random();

        public UndetectChromeDriver driver { get; set; }

        public Actions action { get; set; }
        IJavaScriptExecutor js { get; set; }

        public string _Comment { get; set; }

        public bool _isLike { get; set; }

        public bool _isComment { get; set; }

        public int _from { get; set; }

        public int _to { get; set; }

        public int _waitTimeStart { get; set; }

        public int _waitTimeEnd { get; set; }

        public int _duration = 0;

        public int index { get; set; }

        public delegate void CallbackEventHandler(int index, string status, int type);
        public event CallbackEventHandler AddStatus;

        public VideoAutomation(UndetectChromeDriver driver, int index)
        {
            this.driver = driver;
            this.action = new Actions(driver);
            js = (IJavaScriptExecutor)driver;
            this.index = index;

        }


        public void Run()
        {
            try
            {
                Thread.Sleep(5000);
                // Nếu url không phải video thì return về luôn
                if(!driver.Url.Contains("watch?v"))
                {
                    return;
                }

                bool done = false;
                bool commentD = true;
                AddStatus(this.index, "Bỏ qua popup nếu có!", 0);
                bypass_another_popup();
                Thread.Sleep(2000);
                AddStatus(this.index, "Bỏ qua Ads nếu có!!", 0);
                if(!checkAdsRuning())
                {
                    skipAds();
                }
               
                Thread.Sleep(2000);
                AddStatus(this.index, "Play video nếu đang dừng", 0);
                checkAndPlay();
                Thread.Sleep(TimeSpan.FromSeconds(2));
                AddStatus(this.index, "Bật âm nếu âm tắt", 0);
                isOnSound();
                Thread.Sleep(1000);
                AddStatus(this.index, "Lấy thời gian video!", 0);
                List<int> tTime = totalTime();
                Thread.Sleep(TimeSpan.FromSeconds(3));
                AddStatus(this.index, "Cài đặt chất lượng video!", 0);
                setQuality();
                Thread.Sleep(TimeSpan.FromSeconds(2));
                int stopT = 0;
                if (tTime.Count > 0)
                {
                    stopT = stopStime(tTime[0], tTime[1]);
                }

                AddStatus(this.index, "Bắt đầu thực hiện tính toán ..!", 0);
                string oldUrl = driver.Url;

                bool isSet = true;
                int realSleep = 0;
                for(int i =0; i< (int)stopT/4; i ++)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(60));
                    string currentUrl = driver.Url;
                    if (!oldUrl.Equals(currentUrl))
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(ran.Next(30, 60)));
                        return;
                    }

                    if (isSet)
                    {
                        int ranSleep = ran.Next(_waitTimeStart, _waitTimeEnd);
                        realSleep = getRealTime() + ranSleep;
                        isSet = false;
                    }

                    if(getRealTime() >= stopT)
                    {
                        return;
                    }

                    int TimeRemaining = stopT - getRealTime();

                    AddStatus(this.index, "Thời gian còn lại: " + TimeRemaining + " s", 0);

                    if(getRealTime() >= realSleep)
                    {
                        isSet = true;

                        int ccs = ran.Next(0, 5);
                        switch (ccs)
                        {
                            case 0:

                                if (_isLike)
                                {
                                    AddStatus(this.index, "Bắt đầu like video", 0);
                                    likeVideo();
                                    _isLike = false;
                                }
                                else
                                {
                                    int cs = ran.Next(0, 3);
                                    if (cs == 0)
                                    {
                                        AddStatus(this.index, "Bắt đầu cuộn video", 0);
                                        ScrollToScroll();
                                    }
                                    else if (cs == 1)
                                    {
                                        AddStatus(this.index, "Xem lướt video đề xuất", 0);
                                        watchSuggestVideo();
                                    }
                                    else
                                    {
                                        AddStatus(this.index, "Tua về trước/sau video", 0);
                                        UpAnDown();
                                    }

                                }
                                break;
                            case 1:
                                if (_isComment)
                                {
                                    AddStatus(this.index, "Bắt đầu comment video", 0);
                                    Comment(_Comment);
                                    _isComment = false;
                                }
                                else
                                {
                                    int cs = ran.Next(0, 3);
                                    if (cs == 0)
                                    {
                                        AddStatus(this.index, "Bắt đầu cuộn video", 0);
                                        ScrollToScroll();
                                    }
                                    else if (cs == 1)
                                    {
                                        AddStatus(this.index, "Xem lướt video đề xuất", 0);
                                        watchSuggestVideo();
                                    }
                                    else
                                    {
                                        AddStatus(this.index, "Tua về trước/sau video", 0);
                                        UpAnDown();
                                    }

                                }
                                break;
                            case 2:
                                AddStatus(this.index, "Bắt đầu cuộn video", 0);
                                ScrollToScroll();
                                break;
                            case 3:
                                AddStatus(this.index, "Xem lướt video đề xuất", 0);
                                watchSuggestVideo();
                                break;
                            case 4:
                                AddStatus(this.index, "Tua về trước/sau video", 0);
                                UpAnDown(); break;
                            default: break;
                        }



                    }
                 

                   

                    


                }







            }
            catch
            {

            }

        }

        /// <summary>
        ///  Làm nhanh hoặc lùi chậm lại vài giấy
        /// </summary>
        public void UpAnDown()
        {
            int leftOrRight = ran.Next(0, 2);
            try
            {
                var item = driver.FindElement(By.Id("movie_player"));
                for (int i = 0; i < ran.Next(1, 5); i++)
                {
                    if (leftOrRight == 0)
                    {
                        new Actions(driver).MoveToElement(item).SendKeys(Keys.Left).Pause(TimeSpan.FromSeconds(ran.Next(1, 3))).Build().Perform();
                    }
                    else
                    {
                        new Actions(driver).MoveToElement(item).SendKeys(Keys.Left).Pause(TimeSpan.FromSeconds(ran.Next(1, 3))).Build().Perform();
                    }

                }
            }
            catch (Exception ex)
            {
                AddStatus(this.index, "UpanDown chưa thành công", 1);
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Phương thức thực hiện Like video
        /// </summary>
        public void likeVideo()
        {
            try
            {
                bool like = Convert.ToBoolean(driver.ExecuteScript("var t = document.querySelector(\"#segmented-like-button > ytd-toggle-button-renderer > yt-button-shape > button\").getAttribute(\"aria-pressed\"); return t;"));
                if (like)
                { return; }

                
                var likeButton = driver.FindElement(By.Id("segmented-like-button"));
                new Actions(driver).MoveToElement(likeButton).Pause(TimeSpan.FromSeconds(1)).MoveToElement(likeButton).Click().Build().Perform();
                Thread.Sleep(1000);
                js.ExecuteScript("window.scrollTo({ top: 0, behavior: 'smooth' })");
            }
            catch
            {

                AddStatus(this.index, "Like video chưa thành công!", 2);
              
            }
        }


        /// <summary>
        /// Phuonwg thuc nay se thuc hien xem video ngau nhien 
        /// </summary>
        public void watchSuggestVideo()
        {
            try
            {
                var items = driver.FindElements(By.XPath("//ytd-compact-video-renderer//ytd-thumbnail"));
                int end = ran.Next(3, 10);
                for (int i = 0; i < items.Count; i++)
                {
                    if (end == i)
                    {
                        js.ExecuteScript("window.scrollTo({ top: 0, behavior: 'smooth' })");
                        return;
                    }
                    Actions act = new Actions(driver);
                    act.ScrollByAmount(0, 50).Build().Perform();
                    act.MoveToElement(items[i]).Pause(TimeSpan.FromSeconds(ran.Next(1, 4))).Build().Perform();
                    act.ScrollByAmount(0, 100).Build().Perform();
                }

            }
            catch(Exception ex)
            {
                AddStatus(this.index, "Xem lướt video đề xuất chưa thành công", 1);
                Console.WriteLine(ex);
            }
        }


        /// <summary>
        ///  Phuonwg thu Comment
        /// </summary>

        public void Comment(string comment)
        {
            try
            {

                int x = 250;
                for (int i = 1; i < 50; i++)
                {
                    driver.ExecuteScript("window.scrollTo(100," + (x * i) + ")");
                    try
                    {
                        var cmt = driver.FindElement(By.XPath("//ytd-comment-simplebox-renderer//yt-formatted-string"));
                        new Actions(driver).MoveToElement(cmt).Click().Build().Perform();
                     
                        break;
                    }
                    catch
                    {
                    }
                    Thread.Sleep(300);
                }
                var send = driver.FindElement(By.XPath("//ytd-comment-simplebox-renderer//yt-formatted-string//div"));
                senComment(send, comment);
                Thread.Sleep(3000);
                driver.FindElement(By.XPath("//ytd-comment-simplebox-renderer//ytd-button-renderer[@id='submit-button']//yt-button-shape")).Click();
                Thread.Sleep(6000);
                js.ExecuteScript("window.scrollTo({ top: 0, behavior: 'smooth' })");
            }
            catch
            {
                AddStatus(this.index, "Comment video chưa thành công!", 0);
            }

        }

        public void senComment(IWebElement Xpath, string daya)
        {
            try
            {
                foreach (var item in daya)
                {
                    new Actions(driver).MoveToElement(Xpath).SendKeys(item + "").Build().Perform();
                    Thread.Sleep(50);
                }
            }
            catch
            {

            }
           
        }

        /// <summary>
        /// Phuongw thuc cuon ngau nhien
        /// </summary>
        public void ScrollToScroll()
        {

            try
            {
                int sc = ran.Next(4, 10);
                List<int> arrScroll = new List<int>();
                for (int i = 0; i < sc; i++)
                {
                    int lcY = ran.Next(50, 300);
                    arrScroll.Add(lcY);
                }

                foreach (int i in arrScroll)
                {
                    new Actions(driver).ScrollByAmount(0, i).Pause(TimeSpan.FromSeconds(ran.Next(1, 3))).Build().Perform();
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
                foreach (int i in arrScroll)
                {

                    new Actions(driver).ScrollByAmount(0, i * (-1)).Pause(TimeSpan.FromSeconds(ran.Next(1, 3))).Build().Perform();
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));


            }
            catch (Exception ex)
            {
                AddStatus(this.index, "Scroll xem video chưa thành công!", 1);
                Console.WriteLine(ex);
            }
        }


        /// <summary>
        /// Bypass casc thoong bao hien len khi su dung tai khoan thuong
        /// </summary>
        public void bypass_another_popup()
        {
            try
            {
                string[] popups = { "Got it", "Skip trial", "No thanks", "Dismiss", "Not now" };
                foreach (string i in popups)
                {
                    driver.FindElement(By.XPath("//*[@id='button' and @aria-label='" + i + "']")).Click();
                }
            }
            catch
            {

            }

            try
            {

                driver.FindElement(By.XPath("//*[@id=\"dismiss-button\"]/yt-button-shape/button")).Click();
            }
            catch
            {

            }

        }



        /// <summary>
        /// Tính toán thời gian sẽ dừng video lại khi chạy đủ số lượng thời gian đã thiết lập
        /// </summary>
        /// <returns></returns>
        public int stopStime(int total, int current)
        {
            int result = 0;
            try
            {
                int rc = ran.Next(_from, _to);
                int totalT = total;

                return Convert.ToInt32((totalT * rc / 100) + current);

            }
            catch
            {
                Console.WriteLine("stopStime() Lấy không thành công!");
            }
            return result;
        }





        /// <summary>
        /// Kiểm tra nếu thời lượn video hiện tại lớn hơn 1/3 video thì cho nó về 0, còn không thì tiếp tục xem
        /// Sau đó thực hiện tính toán tổng thời gian xem hiện tại
        /// </summary>

        public List<int> totalTime()
        {
            List<int> tt = new List<int>();
            try
            {
                int current = getRealTime();
                Thread.Sleep(5000);
                var time = js.ExecuteScript("var i = document.getElementById(\"movie_player\").getDuration(); return i");
               
                _duration = Convert.ToInt32(time);
                  
                if(_duration == 0)
                {
                    AddStatus(this.index, "Get Durration thất bại", 1);
                }
              
                Thread.Sleep(1000);
                if (current > (int)(_duration / 3))
                {
                    int c = ran.Next(0, 20);
                    js.ExecuteScript("document.getElementById(\"movie_player\").seekTo(" + c + ",true);");
                    current = c;
                }
                tt.Add(_duration - current);
                tt.Add(current);
            }
            catch
            {
                AddStatus(this.index, "Get Durration thất bại", 1);

            }

            return tt;

        }

        /// <summary>
        /// Phương thức lấy thời gian của video
        /// "var t = document.getElementsByClassName('ytp-time-duration')[0].textContent; return t;"
        /// "var t = document.getElementsByClassName('ytp-time-current')[0].textContent; return t;"
        /// </summary>
        /// <returns></returns>
        public int getTimeVideo(string time)
        {
            int total = 0;
            try
            {
                string str = Convert.ToString(driver.ExecuteScript(time));
                Console.WriteLine("Thời gian lấy đc là: "+str);
                var item = str.Trim().Split(':');
                if (item.Length == 3)
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
            }
            catch
            {
                Console.WriteLine("getTimeVideo Lấy không thành công!");
            }

            return total;
        }





        /// <summary>
        ///  Thực hiện setup chất lượng cho vieo
        /// </summary>
        public void setQuality()
        {
            try
            {
                var quality = driver.ExecuteScript("return document.getElementsByClassName(\"ytp-button ytp-settings-button\").length;");
                int num = Convert.ToInt32(quality);
                int index = -1;
                for (int i = 0; i < num; i++)
                {

                    Thread.Sleep(1000);
                    string isQua = (string)driver.ExecuteScript("return document.getElementsByClassName(\"ytp-button ytp-settings-button\")[" + i + "].style.display");
                    if (isQua.Trim() == "")
                    {
                        index = i;
                        break;
                    }

                }
                if (index == -1)
                {
                    throw new Exception();
                }

                driver.ExecuteScript("document.getElementsByClassName(\"ytp-button ytp-settings-button\")[" + index + "].click();");
                Thread.Sleep(2000);
                driver.ExecuteScript("document.getElementsByClassName(\"ytp-panel-menu\")[" + index + "].lastChild.click()");
                Thread.Sleep(2000);
                var count = driver.ExecuteScript("return document.getElementsByClassName(\"ytp-panel-menu\")[" + index + "].childNodes.length;");
                Console.WriteLine(count);
                int indx = (Convert.ToInt32(count) - 1) - ran.Next(1,4);
                if (indx < 0)
                {
                    indx = Convert.ToInt32(count) - (1 + 1);
                }
                var item = driver.FindElements(By.ClassName("ytp-menuitem"));
                driver.ExecuteScript("arguments[0].scrollIntoViewIfNeeded();", item[indx]);
                Thread.Sleep(1000);
                driver.ExecuteScript("arguments[0].click()", item[indx]);

            }
            catch
            {
                try
                {
                    AddStatus(this.index, "Set Quality thủ công thất bại!", 1);
                    Thread.Sleep(2000);
                    js.ExecuteScript("document.getElementsByClassName(\"ytp-button ytp-settings-button\")[0].click();");
                    Thread.Sleep(2000);
                    string[] quality = { "tiny", "small", "medium" };
                    js.ExecuteScript("document.getElementById('movie_player').setPlaybackQualityRange('" + quality[ran.Next(0, 3)] + "')");
                }
                catch
                {
                    AddStatus(this.index, "Set Quality tự động thất bại!", 1);
                }

            }

        }


        public void isautoPlay()
        {
            try
            {

            }
            catch
            {

            }
        }


        /// <summary>
        /// Kiểm tra xem âm thanh đã được mở hay chưa. nếu chưa thì thực hiện mở tiếng.
        /// </summary>
        public void isOnSound()
        {
            try
            {
                string on = (string)js.ExecuteScript("return document.getElementsByClassName(\"ytp-volume-slider-handle\")[0].getAttribute(\"style\");");
                if (on.Contains("0px"))
                {
                    js.ExecuteScript("document.getElementsByClassName(\"ytp-mute-button ytp-button\")[0].click()");
                }
            }
            catch
            {
                AddStatus(this.index, "Set sound thất bại", 1);
            }


        }

        /// <summary>
        /// Laays thoi gian thuc dang chay video
        /// </summary>
        /// <returns></returns>
        public int getRealTime()
        {
            
            for(int i =0; i< 3; i ++)
            {
                try
                {
                    Thread.Sleep(2000);
                    var time = js.ExecuteScript("var i = document.getElementById(\"movie_player\").getCurrentTime(); return i");
                    return Convert.ToInt32(time);
                    
                }
                catch
                {
                    AddStatus(this.index, "Get Current time thất bại", 1);
                }
            }
           

            return 0;
        }

        /// <summary>
        ///  Click bỏ qua quảng cáo
        /// </summary>
        public void skipAds()
        {
            try
            {
                driver.ExecuteScript("var btn=document.createElement('newbutton');" + "document.body.appendChild(btn);");
                string command = "setInterval(function(){" +
                                   " if(document.getElementsByClassName(\"ytp-ad-text ytp-ad-skip-button-text\")[0]!==undefined){ " +
                                    " let skipBtn=document.getElementsByClassName(\"ytp-ad-text ytp-ad-skip-button-text\")[0];" +
                                    "skipBtn.click(); }" +
                                    "},5000);";
                js.ExecuteScript(command);
            }
            catch
            {

            }
           
        }

        /// <summary>
        /// Kiểm tra xem bỏ qua ads đã được chạy hay chưa
        /// </summary>
        /// <returns></returns>
        public bool checkAdsRuning()
        {
            try
            {
                int count = Convert.ToInt32(driver.ExecuteScript("return document.getElementsByTagName(\"newbutton\").length"));
                if (count != 0)
                {
                    return true ;
                }
            }
            catch
            {

            }
            return false;
        }


        /// <summary>
        ///  Phương thức này sẽ kiểm tra xem video có đang chạy hay không
        ///  Nếu không chạy thì video sẽ thực hiện việc chạy
        /// </summary>
        public void checkAndPlay()
        {
            try
            {
                for(int i = 0; i < 20; i ++)
                {
                    int state = Convert.ToInt32( js.ExecuteScript("return document.getElementById('movie_player').getPlayerState()"));
                    if(state == 5)
                    {
                        Thread.Sleep(5000);
                    }else if(state == 2)
                    {
                        js.ExecuteScript("document.getElementsByClassName(\"ytp-play-button ytp-button\")[0].click()");
                        return;
                    }
                }
              
            }
            catch
            {
                AddStatus(this.index, "Play and check thất bại",1);
            }


        }

    }
}
