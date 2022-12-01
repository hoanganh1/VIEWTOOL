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

        public VideoAutomation(UndetectChromeDriver driver)
        {
            this.driver = driver;
            this.action = new Actions(driver);
            js = (IJavaScriptExecutor)driver;

        }


        public void Run()
        {
            try
            {
                bool done = false;
                bool commentD = true;

                Console.WriteLine("Pause/Start");
                checkAndPlay();
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine("set Sound");
                isOnSound();
                Console.WriteLine("GetTotal Time");
                List<int> tTime = totalTime();
                Thread.Sleep(TimeSpan.FromSeconds(3));
                Console.WriteLine("setQuality");
                setQuality();
                Thread.Sleep(TimeSpan.FromSeconds(2));
                int stopT = 0;
                if (tTime.Count > 0)
                {
                    stopT = stopStime(tTime[0], tTime[1]);
                }
                Console.WriteLine("Bắt Đầu Thực hiện hành vi bên trong video đang xem!");

                while (true)
                {

                    int ranSleep = ran.Next(_waitTimeStart, _waitTimeEnd);
                    int current = getRealTime();
                    int acceptTime = (tTime[0] - current);

                    Console.WriteLine("Thời gian còn lại: " + acceptTime);
                    Console.WriteLine("Thời gian dừng/nghỉ: " + ranSleep);
                    if ((acceptTime - 120) < ranSleep || (current + ranSleep) > _duration) // Neeus thon gian ma nho hon thi call.
                    {
                        Console.WriteLine("Dừng cho đến hết video và không hành động gì: " + ranSleep);
                        Thread.Sleep(TimeSpan.FromSeconds(acceptTime));
                        return;
                    }
                    Console.WriteLine("Bắt đầu chờ đợi.....!: " + ranSleep);
                    // Thực hiện thời gian nghỉ chờ đợi
                    for(int i = 0; i < (int)((ranSleep/4)); i ++ )
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(5));
                        Console.WriteLine(driver.ExecuteScript("return document.getElementById('movie_player').getCurrentTime()"));
                        
                    }
                   

                    Console.WriteLine("Bắt đầu thực hiện hành động tương tác.....!: ");
                    // Thực hiện random hành động khác nhau
                    int ccs = ran.Next(0, 5);
                    switch (ccs)
                    {
                        case 0:
                           
                            if (_isLike)
                            {
                                Console.WriteLine("Đăng thực hiện Like video!");
                                likeVideo();
                                _isLike = false;
                            }
                            else
                            {
                                int cs = ran.Next(0, 3);
                                if (cs == 0)
                                {
                                    Console.WriteLine("Đăng thực hiện cuộn video");
                                    ScrollToScroll();
                                }
                                else if (cs == 1)
                                {
                                    Console.WriteLine("Thực hiện xem video đề xuất");
                                    watchSuggestVideo();
                                }
                                else
                                {
                                    Console.WriteLine("Thực hiện bấm tua nhanh video hoặc chậm");
                                    UpAnDown();
                                }

                            }
                            break;
                        case 1:
                            if (_isComment)
                            {
                                Console.WriteLine("Thực hiện comment video!");
                                Comment(_Comment);
                                _isComment = false;
                            }
                            else
                            {
                                int cs = ran.Next(0, 3);
                                if (cs == 0)
                                {
                                    Console.WriteLine("Đăng thực hiện cuộn video");
                                    ScrollToScroll();
                                }
                                else if (cs == 1)
                                {
                                    Console.WriteLine("Thực hiện xem video đề xuất");
                                    watchSuggestVideo();
                                }
                                else
                                {
                                    Console.WriteLine("Thực hiện bấm tua nhanh video hoặc chậm");
                                    UpAnDown();
                                }

                            }
                            break;
                        case 2:
                            Console.WriteLine("Đăng thực hiện cuộn video");
                            ScrollToScroll();
                            break;
                        case 3:
                            Console.WriteLine("Thực hiện xem video đề xuất");
                            watchSuggestVideo();
                            break;
                        case 4:
                            Console.WriteLine("Thực hiện bấm tua nhanh video hoặc chậm");
                            UpAnDown(); break;
                        default: break;
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
                Actions act = new Actions(driver);
                for (int i = 0; i < ran.Next(1, 5); i++)
                {
                    if (leftOrRight == 0)
                    {
                        act.MoveToElement(item).SendKeys(Keys.Left).Pause(TimeSpan.FromSeconds(ran.Next(1, 3))).Build().Perform();
                    }
                    else
                    {
                        act.MoveToElement(item).SendKeys(Keys.Left).Pause(TimeSpan.FromSeconds(ran.Next(1, 3))).Build().Perform();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpanDown() thất bại");
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
                bool like = (bool)js.ExecuteScript(" var t = document.querySelector(\"#segmented-like-button > ytd-toggle-button-renderer > yt-button-shape > button\").getAttribute(\"aria-pressed\"); return t;");
                if (like)
                { return; }

                
                var likeButton = driver.FindElement(By.Id("segmented-like-button"));
                Console.WriteLine("ĐÃ tìm thấy nút like");
                Actions act = new Actions(driver);
                act.MoveToElement(likeButton).Pause(TimeSpan.FromSeconds(1)).Click().Build().Perform();
                Thread.Sleep(1000);
                js.ExecuteScript("window.scrollTo({ top: 0, behavior: 'smooth' })");
            }
            catch
            {
               
                Console.WriteLine("Like video thất bại");
              
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
                    { js.ExecuteScript("window.scrollTo({ top: 0, behavior: 'smooth' })"); return;
                    }
                    Actions act = new Actions(driver);
                    act.ScrollByAmount(0, 50).Build().Perform();
                    act.MoveToElement(items[i]).Pause(TimeSpan.FromSeconds(ran.Next(1, 4))).Build().Perform();
                    act.ScrollByAmount(0, 100).Build().Perform();
                }

            }
            catch
            {
                Console.WriteLine("watchSuggestVideo() Thất bại");
            }
        }


        /// <summary>
        ///  Phuonwg thu Comment
        /// </summary>

        public void Comment(string comment)
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
                        Actions act = new Actions(driver);
                        act.MoveToElement(cmt).Click().Build().Perform();
                     
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
                Console.WriteLine("Comment video thất bại");
            }

        }

        public void senComment(IWebElement Xpath, string daya)
        {
            foreach (var item in daya)
            {
                Actions actionss = new Actions(driver);
                actionss.MoveToElement(Xpath).SendKeys(item + "").Build().Perform();
                Thread.Sleep(50);
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
                    Actions act = new Actions(driver);
                    act.ScrollByAmount(0, i).Pause(TimeSpan.FromSeconds(ran.Next(1, 3))).Build().Perform();
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
                foreach (int i in arrScroll)
                {
                    Actions act = new Actions(driver);
                    act.ScrollByAmount(0, i * (-1)).Pause(TimeSpan.FromSeconds(ran.Next(1, 3))).Build().Perform();
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));


            }
            catch (Exception ex)
            {
                Console.WriteLine("ScrollToScroll() thất bại");
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
                    Console.WriteLine("Lấy Duration thất bại!");
                }
              
                Thread.Sleep(1000);
                if (current > (int)(_duration / 3))
                {
                    int c = ran.Next(0, 10);
                    js.ExecuteScript("document.getElementById(\"movie_player\").seekTo(" + c + ",true);");
                    current = c;
                }
                tt.Add(_duration - current);
                tt.Add(current);
            }
            catch
            {
                Console.WriteLine("totalTime() Lấy không thành công!");

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
                Thread.Sleep(2000);
                js.ExecuteScript("document.getElementsByClassName(\"ytp-button ytp-settings-button\")[0].click();");
                Thread.Sleep(2000);
                js.ExecuteScript("document.getElementsByClassName(\"ytp-panel-menu\")[0].lastChild.click()");
                Thread.Sleep(2000);
                var count = js.ExecuteScript("return document.getElementsByClassName(\"ytp-panel-menu\")[0].childNodes.length;");
                Console.WriteLine(count);
                int indx = (Convert.ToInt32(count) - 1) - ran.Next(1, 4);
                if(indx < 0)
                {
                    indx = Convert.ToInt32(count)-(1 + 1);
                }
                var item = driver.FindElements(By.ClassName("ytp-menuitem"));
                Actions act = new Actions(driver);
                Thread.Sleep(1000);
                act.MoveToElement(item[indx]).Click().Build().Perform();
            }
            catch
            {
                try
                {
                    Console.WriteLine("SetQuality thủ công không thành công");
                    Thread.Sleep(2000);
                    js.ExecuteScript("document.getElementsByClassName(\"ytp-button ytp-settings-button\")[0].click();");
                    Thread.Sleep(2000);
                    string[] quality = { "tiny", "small", "medium" };
                    js.ExecuteScript("document.getElementById('movie_player').setPlaybackQualityRange('" + quality[ran.Next(0, 3)] + "')");
                }
                catch
                {
                    Console.WriteLine("SetQuality tự động không thành công");
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
                Console.WriteLine("isSound() thất bại");
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
                    Console.WriteLine("getRealTime() Thất bại lần "+i);
                }
            }
           

            return 0;
        }


        /// <summary>
        ///  Phương thức này sẽ kiểm tra xem video có đang chạy hay không
        ///  Nếu không chạy thì video sẽ thực hiện việc chạy
        /// </summary>
        public void checkAndPlay()
        {
            try
            {
                int start = getRealTime();
                Thread.Sleep(TimeSpan.FromSeconds(5));
                int end = getRealTime();
                if (start == end)
                {
                    js.ExecuteScript("document.getElementsByClassName(\"ytp-play-button ytp-button\")[0].click()");
                }
            }
            catch
            {
                Console.Write("CheckAndPlay thất bại!");
            }


        }

    }
}
