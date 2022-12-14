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
    class SearchAutomation
    {
        Random ran = new Random();
        public UndetectChromeDriver driver { get; set; }

        public Actions action { get; set; }

        public int index { get; set; }

        public delegate void CallbackEventHandler(int index, string status, int type);
        public event CallbackEventHandler AddStatus;


        public SearchAutomation(UndetectChromeDriver driver, int index)
        {
            this.driver = driver;
            this.action = new Actions(driver);
            this.index = index;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool Run(int type, string key, string ID)
        {
            if (type == 0)
            {
                int sc = ran.Next(0, 3);
                if (sc == 0)
                {
                    AddStatus(this.index, "Scroll + view trang chủ", 0);
                    if (ScrollToScroll(0))
                    {
                        AddStatus(this.index, "Xem video ngau nhien", 0);
                        Thread.Sleep(TimeSpan.FromSeconds(ran.Next(20, 60)));
                    }
                    else
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(ran.Next(1, 4)));
                    }
                   
                }
                else if (sc == 1)
                {
                    AddStatus(this.index, "Move+ view trang chủ", 0);
                    if (MovetoHomeVideos())
                    {
                        AddStatus(this.index, "Xem video ngau nhien", 0);
                        Thread.Sleep(TimeSpan.FromSeconds(ran.Next(20, 60)));
                    }
                    else
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(ran.Next(1, 4)));
                    }
                   
                   
                }
                else
                {
                    AddStatus(this.index, "Chọn topic trang chủ", 0);
                    if (ChooseTopic()) {
                        AddStatus(this.index, "Xem video ngau nhien", 0);
                        Thread.Sleep(TimeSpan.FromSeconds(ran.Next(20, 60)));
                    }
                    else
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(ran.Next(1, 4)));
                    }
                   
                }

                AddStatus(this.index, "Search keyword", 0);
                return SearchKey(key, ID);
            }
            else
            {
                AddStatus(this.index, "Search keyword", 0);
                return SearchKey(key, ID);
            }


        }

        /// <summary>
        /// Lớp thực hiện việc cuộn cuộn rồi chọn một video ngẫu nhiên để xem
        /// type =0 thì cuộn rồi click ở màn hình home
        /// type =1 thì chọ cuộn ở màn hình search
        /// </summary>
        public bool ScrollToScroll(int type)
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


                if (type == 0)
                {
                    int videoIndex = ran.Next(0, 6);
                    var items = driver.FindElements(By.XPath("//ytd-rich-grid-media//ytd-thumbnail"));
                    new Actions(driver).MoveToElement(items[videoIndex]).Pause(TimeSpan.FromSeconds(2)).Click().Build().Perform();
                }
                return true;

            }
            catch
            {
                AddStatus(this.index, "Scroll + view trang chủ", 2);
            }
            return false;
        }



        /// <summary>
        /// Lớp thực hiện việc di chuyển lần lượt qua các video trước khi thực hiện bấm vào video đã chọn
        /// </summary>
        public bool MovetoHomeVideos()
        {
            try
            {
                int sc = ran.Next(0, 6);
                var items = driver.FindElements(By.XPath("//ytd-rich-grid-media//ytd-thumbnail"));
                for (int i = 0; i < sc; i++)
                {
                    if (i == sc - 1)
                    {
                        new Actions(driver).MoveToElement(items[i]).Pause(TimeSpan.FromSeconds(ran.Next(2, 5))).Click().Build().Perform();
                        break;
                    }
                    else
                    {
                        new Actions(driver).MoveToElement(items[i]).Pause(TimeSpan.FromSeconds(2)).Build().Perform();
                    }

                }
                return true;

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                AddStatus(this.index, "Move+ view trang chủ", 2);
            }
            return false;
        }

        /// <summary>
        /// Phương thức chọn chủ đề trước khi thực hiện chọn ngẫu nhiên video
        /// </summary>
        public bool ChooseTopic()
        {
            try
            {
                int sc = ran.Next(0, 5);
                string script = String.Format("document.getElementsByClassName(\"style-scope yt-chip-cloud-chip-renderer\")[{0}].click()", sc);
                driver.ExecuteScript(script);
                Thread.Sleep(TimeSpan.FromSeconds(ran.Next(5,10)));
                int cm = ran.Next(0, 2);
                if (cm == 0)
                {
                    AddStatus(this.index, "Scroll+ view trang chủ", 0);
                    ScrollToScroll(0);
                }
                else
                {
                    AddStatus(this.index, "Move+ view trang chủ", 0);
                    MovetoHomeVideos();
                }

                return true;

            }
            catch
            {
                AddStatus(this.index, "Chọn Topic trang chủ lỗi", 2);
            }
            return false;
        }


        /// <summary>
        ///  Lớp thực hiện việc tìm kiếm video
        /// </summary>
        public bool SearchKey(string keyword, string videoID)
        {
            bool result = false;
            try
            {

                int rn = ran.Next(0, 2);
                // nếu rn =0 thì bấm vào home trước khi thực hiện search
                if (rn == 0)
                {
                    var items = driver.FindElement(By.XPath("//ytd-topbar-logo-renderer//yt-icon"));
                    new Actions(driver).MoveToElement(items).Click().Build().Perform();
                    Thread.Sleep(TimeSpan.FromSeconds(ran.Next(5,10)));
                }

                // Tìm kiếm với nửa từ khóa hoặc tìm kiếm với đầy đủ từ khóa
                bool stop = false;
                int FullOrNot = ran.Next(0, 2);
            searchloop:
                // Thực hiện search
                // Lấy ra Key work
                string searchKeywork = FullOrNot == 0 ? keyword : keyword.Substring(0, keyword.Length - ran.Next(5, 10));
                var search = driver.FindElement(By.Name("search_query"));
                search.Clear();  // Làm sạch

                // Thực hiện việc nhập keywork
                Actions acts = new Actions(driver);
                acts.MoveToElement(search).Click().Pause(TimeSpan.FromSeconds(ran.Next(1, 3))).Build().Perform();

                for (int i = 0; i < searchKeywork.Length; i++)
                {
                    Thread.Sleep(ran.Next(100, 1000));
                    acts.SendKeys(searchKeywork[i].ToString()).Build().Perform();
                }
                Thread.Sleep(1000);
                // Bấm Enter để tìm kiếm video
                acts.SendKeys(Keys.Enter).Pause(TimeSpan.FromSeconds(1)).Build().Perform();
                Thread.Sleep(TimeSpan.FromSeconds(ran.Next(5,10)));


                int loop = 0;
                int found = 0;
                int titleorThubnail = 0;
                while (loop < 2)
                {
                    loop += 1;
                    titleorThubnail = ran.Next(0, 2);
                    AddStatus(this.index, "Đang tìm kiếm video!",0);
                    found = checkExistVideo(titleorThubnail, videoID);
                    Console.WriteLine(found);
                    if (found != 10000)
                    {
                        AddStatus(this.index, "Đã tìm thấy video!", 0);
                        break;
                    }
                    AddStatus(this.index, "Cuộn để tìm video!", 0);
                    new Actions(driver).ScrollByAmount(0, ran.Next(100, 400)).Pause(TimeSpan.FromSeconds(2)).ScrollByAmount(0, ran.Next(100, 400)).Pause(TimeSpan.FromSeconds(1)).Build().Perform();
                    new Actions(driver).SendKeys(Keys.End).Build().Perform();
                    Thread.Sleep(3000);
                }

                AddStatus(this.index, "Tìm lại với đầy đủ tiêu đề!", 0);
                if (found == 10000 && stop == false)
                {
                    stop = true;
                    FullOrNot = 0;
                    goto searchloop;
                }
                
                // Nếu mà không tìm thấy thì trả về 
                if (found == 10000)
                { return false; }


            
                var TT = titleorThubnail == 1 ? driver.FindElements(By.XPath("//ytd-video-renderer//ytd-thumbnail//a")) : driver.FindElements(By.XPath("//ytd-video-renderer//h3//a"));
                if (found <= 5) // TRường hợp nếu nó nằm trong top 5 thì thực hiện hành động cuộng cuộn rồi thực hiện click vào xem
                {
                    ScrollToScroll(1);

                    new Actions(driver).MoveToElement(TT[found + 1]).Pause(TimeSpan.FromSeconds(1)).MoveToElement(TT[found]).Pause(TimeSpan.FromSeconds(2)).Click().Build().Perform();
                }
                else // Trường hợp nếu mà nó nằm dưới top5 thì thực hiện cuộn lần lượt
                {
                    for (int i = 0; i < TT.Count; i++)
                    {
                        if (i == found)
                        {
                            if (i == TT.Count - 1)
                            {
                                new Actions(driver).MoveToElement(TT[i]).Pause(TimeSpan.FromSeconds(2)).Click().Build().Perform();
                                break;
                            }
                            else
                            {
                                new Actions(driver).MoveToElement(TT[i + 1]).Pause(TimeSpan.FromSeconds(1)).MoveToElement(TT[i]).Pause(TimeSpan.FromSeconds(2)).Click().Build().Perform();
                                break;
                            }

                        }
                        else
                        {
                            new Actions(driver).MoveToElement(TT[i]).Pause(TimeSpan.FromSeconds(1)).Build().Perform();
                        }

                    }
                }

                result = true;


            }
            catch
            {

            }
            return result;

        }

        /// <summary>
        ///  Lớp thực hiện việc di chuyển lần lượt qua các phần tử
        /// </summary>
        public void MoveToSearchVideo()
        {

        }

        /// <summary>
        /// Phương thức kiểm tra sau khi search có tìm thấy video không và index của video là gì
        /// search bằng title hoặc search bằng thumbnail
        /// </summary>
        /// <returns></returns>
        public int checkExistVideo(int type, string videoID)
        {
            int result = 10000;

            try
            {
                if (type == 0) // Tìm kiếm dựa vào title
                {
                    var items = driver.FindElements(By.XPath("//ytd-video-renderer//h3//a"));
                    for (int i = 0; i < items.Count; i++)
                    {
                        string urlVideo = "";
                        try
                        {
                            urlVideo = items[i].GetAttribute("href");
                            if (urlVideo.Contains(videoID))
                            {
                                return i;
                            }
                        }
                        catch
                        {

                        }
                    }


                }
                else
                { // Tìm kiếm dựa vào thumbnail

                    var items = driver.FindElements(By.XPath("//ytd-video-renderer//ytd-thumbnail//a"));
                    for (int i = 0; i < items.Count; i++)
                    {
                        string urlVideo = "";
                        try
                        {
                            urlVideo = items[i].GetAttribute("href");
                            if (urlVideo.Contains(videoID))
                            {
                                return i;
                            }
                        }
                        catch
                        {

                        }
                    }


                }


            }
            catch
            {

            }

            return result;
        }





        public void PrepareBeforeSearch()
        {
            try
            {
                var items = driver.FindElement(By.XPath("//ytd-topbar-logo-renderer//yt-icon"));
                action.MoveToElement(items).Click().Build().Perform();
                int choise = ran.Next(0, 4);
                switch (choise)
                {
                    case 0:
                        var topics = driver.FindElements(By.XPath("//iron-selector//yt-chip-cloud-chip-renderer//yt-formatted-string"));
                        int index = ran.Next(0, 5);
                        action.MoveToElement(topics[index]).Click().Build().Perform();
                        Thread.Sleep(100);
                        break;
                    case 1:
                        break;
                    default:
                        break;
                }


            }
            catch
            {

            }


        }

        public void SearchBar(string keyword)
        {
            try
            {
                var search = driver.FindElement(By.Name("search_query"));
                search.Clear();
                action.MoveToElement(search).Click().Build().Perform();
                Thread.Sleep(1000);
                for (int i = 0; i < keyword.Length; i++)
                {
                    Thread.Sleep(ran.Next(20, 60));
                    action.SendKeys(keyword[i].ToString()).Build().Perform();
                }
                Thread.Sleep(1000);
                action.SendKeys(Keys.Enter).Build().Perform();
            }
            catch
            {

            }

        }

        public void ChoiseVideoFromSearch(string ID)
        {
            int choise = ran.Next(0, 2);
            if (choise == 1)
            {
                clickByThumnail(ID);
            }
            else
            {
                clickByTitle(ID);
            }



        }

        public void clickByTitle(string ID)
        {
            try
            {
                // tìm kiếm danh sách phần tử
                var items = driver.FindElements(By.XPath("//ytd-video-renderer//h3//a"));
                foreach (var item in items)
                {
                    string urlVideo = item.GetAttribute("href");

                    // Thực hiện lướt tìm video nếu mà nó không có thì thực hiện cuộn
                    if (urlVideo.Contains(ID))
                    {
                        // action.ScrollToElement(item).Build().Perform();
                        Thread.Sleep(1000);
                        action.MoveToElement(item).Click().Build().Perform();
                        break;
                    }
                    else
                    {
                        action.ScrollToElement(item).Build().Perform();
                    }
                }
            }
            catch
            {

            }
        }


        public void scrollToElement()
        {
            IWebElement footer = driver.FindElement(By.TagName("footer"));
            int deltaY = footer.Location.Y;
            new Actions(driver)
                .ScrollByAmount(0, deltaY)
                .Perform();
            action.ScrollByAmount(0, 500).Pause(TimeSpan.FromSeconds(1)).ScrollByAmount(0, 2000).Build().Perform();
        }


        public void clickByThumnail(string ID)
        {
            try
            {
                // tìm kiếm danh sách phần tử
                var items = driver.FindElements(By.XPath("//ytd-video-renderer//ytd-thumbnail//a"));
                foreach (var item in items)
                {
                    string urlVideo = item.GetAttribute("href");

                    // Thực hiện lướt tìm video nếu mà nó không có thì thực hiện cuộn
                    if (urlVideo.Contains(ID))
                    {
                        // action.ScrollToElement(item).Build().Perform();
                        Thread.Sleep(1000);
                        action.MoveToElement(item).Click().Build().Perform();
                        break;
                    }
                    else
                    {
                        action.ScrollToElement(item).Build().Perform();
                    }
                }
            }
            catch
            {

            }
        }

    }
}
