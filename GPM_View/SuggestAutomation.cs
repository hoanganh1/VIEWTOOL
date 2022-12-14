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
    class SuggestAutomation
    {
        Random ran = new Random();
        public UndetectChromeDriver driver { get; set; }

        public Actions action { get; set; }

        public int index { get; set; }

        public delegate void CallbackEventHandler(int index, string status, int type);
        public event CallbackEventHandler AddStatus;
        public SuggestAutomation(UndetectChromeDriver driver, int index)
        {
            this.driver = driver;
            this.action = new Actions(driver);
            this.index = index;
        }

        /// <summary>
        /// type được truyền bên ngoài nếu video mới không phải video cũ
        /// </summary>
        /// <param name="channelNAme"></param>
        /// <param name="keyword"></param>
        /// <param name="Idvideo"></param>
        /// <param name="type"></param>
        public void run(string channelNAme,string channelID,string keyword, string Idvideo)
        {
            try
            {
                if(!driver.Url.Contains("watch?v"))
                {
                    SuggestFromAnother(keyword, Idvideo, 0);
                    Thread.Sleep(3000);
                    return;
                }


                int sc = ran.Next(0, 2);
                int dk = ran.Next(0, 2);
                bool result = false;
                // Nếu không phải thì = 1
                // nếu đúng channel thì =0 ;
                int type = !isYourChannel(channelID) ? 1 : 0;
                if(type == 0) // Nếu là kênh của mình thì sẽ random 2 trường hợp
                { // Đề xuất bên phải hoặc là đề xuất từ video người khác
                    // đề xuất từ video người khác thì có 2 kiểu
                    // Kiểu 0 là tìm kiếm rồi đề xuất
                    // kiểu 1 là đề xuất trực tiếp bằng các add link vào
                    result = sc == 0 ? SuggestFromRight(channelNAme) : SuggestFromAnother(keyword, Idvideo, 0);
                }
                else
                {
                    
                    result = SuggestFromAnother(keyword, Idvideo, dk);
                }
             
                Thread.Sleep(3000);
                if (result == false) // Nếu một trong 2 cái thất bại thì lập tức gọi thằng thứ 2 luôn để add video vào trực tiếp luôn
                {
                    SuggestFromAnother(keyword, Idvideo, 1);
                    Thread.Sleep(3000);
                }
            }
            catch
            {

            }


        }

        /// <summary>
        /// Lớp thực hiện tìm kiếm video đề xuất của chính nó từ bên phải đề xuất của chính nó
        /// </summary>
        public bool SuggestFromRight(string channelName)
        {
            AddStatus(this.index, "Tìm video đề xuất bên phải", 0);
            try
            {
                // Lấy các topic đang hiển thị
                // và lấy topic mà có kênh của kênh
                var desCription = driver.FindElements(By.XPath("//iron-selector//yt-chip-cloud-chip-renderer//yt-formatted-string"));

                int muc = 0;
                foreach (var i in desCription)
                {
                    if (i.Text.Trim() != "")
                    {
                        Console.WriteLine(i.Text);
                        new Actions(driver).MoveToElement(i).Click().Build().Perform();
                        Thread.Sleep(TimeSpan.FromSeconds(new Random().Next(5, 10)));
                        if (FindAndClick(channelName, muc))
                        { return true; }

                    }
                    muc += 1;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                AddStatus(this.index, "Tìm video bên phải thất bại", 1);
            }

            return false;

        }


        /// <summary>
        ///  Phương thức thực hiện tìm kiếm Video đề xuất phía bên phải của màn hình
        ///  // Nếu tìm thấy và click thành công thì trả về true
        ///  // nếu khong thì trả về False
        /// </summary>
        /// <param name="channelName"></param>
        /// <returns></returns>
        public bool FindAndClick(string channelName, int idx)
        {
            try
            {
                AddStatus(this.index, "Đang tìm kiếm mục " + idx, 0);

                // Tìm một loạt để chọn ra danh sách video cần tìm
                var desCription = driver.FindElements(By.XPath("//ytd-compact-video-renderer"));
                List<int> indexForFinding = new List<int>();
                for (int i = 0; i < desCription.Count; i++)
                {

                    var thubail = desCription[i].FindElement(By.TagName("yt-formatted-string"));
                    if (thubail.Text.Trim().Contains(channelName))
                    {
                        indexForFinding.Add(i);
                    }
                }
                Thread.Sleep(1000);
                
                if(indexForFinding.Count ==0)
                {
                    AddStatus(this.index, "Không tìm thấy video mục " + idx, 0);
                    return false;
                }

                // Thực hiện cuộn đến vị trí được chọn
                int indexToRun = ran.Next(0, indexForFinding.Count);
                for (int i = 0; i <= indexForFinding[indexToRun]; i++)
                {
                    if (i == indexForFinding[indexToRun])
                    {
                        // click ngẫu nhiên vào thumb hoặc là hình ảnh
                        var titleOrthubm = ran.Next(0, 2) == 0 ? desCription[i].FindElement(By.TagName("ytd-thumbnail")) : desCription[i].FindElement(By.Id("video-title"));
                        new Actions(driver).MoveToElement(titleOrthubm).ScrollByAmount(0, 200).MoveToElement(titleOrthubm).Pause(TimeSpan.FromSeconds(2)).Click().Build().Perform();
                        return true;
                    }
                    else
                    {
                        new Actions(driver).ScrollByAmount(0, ran.Next(50, 300)).Build().Perform();
                    }
                }

            }
            catch
            {
                AddStatus(this.index, "Lỗi tìm video tìm kiếm mục " + idx, 0);
            }
            return false;
        }


        /// <summary>
        /// Xử lý đề xuất từ video của người khác sang video của mình
        /// type = 0 thì thực hiện tìm kiếm thoe từ khóa chỉnh định
        /// type =1  thì add đề xuất trực tiếp từ video mà mình đang xem.
        /// </summary>
        public bool SuggestFromAnother(string searchKeywork, string videoID, int type)
        {
            try
            {
                if (type == 0)
                {
                    try
                    {
                        AddStatus(this.index, "Tìm kiếm + đề xuất đang chạy!", 0);
                        int rn = ran.Next(0, 2);

                        if (rn == 0)// nếu rn =0 thì bấm vào home trước khi thực hiện search
                        {
                            var items = driver.FindElement(By.XPath("//ytd-topbar-logo-renderer//yt-icon"));
                            new Actions(driver).MoveToElement(items).Click().Build().Perform();
                            Thread.Sleep(TimeSpan.FromSeconds(3));
                        }

                        AddStatus(this.index, "Random ngẫu nhiên các từ khóa tìm kiếm!", 0);
                        var search = driver.FindElement(By.Name("search_query"));
                        search.Clear();
                        Actions acts = new Actions(driver);
                        acts.MoveToElement(search).Click().Pause(TimeSpan.FromSeconds(ran.Next(1, 3))).Build().Perform();

                        for (int i = 0; i < searchKeywork.Length; i++)
                        {
                            Thread.Sleep(ran.Next(100, 1000));
                            acts.SendKeys(searchKeywork[i].ToString()).Build().Perform();
                        }
                        Thread.Sleep(1000);
                        acts.SendKeys(Keys.Enter).Pause(TimeSpan.FromSeconds(1)).Build().Perform();
                        Thread.Sleep(3000);

                        AddStatus(this.index, "Chọn ngẫu nhiên video để xem!", 0);
                        var titles = driver.FindElements(By.XPath("//ytd-video-renderer//h3//a"));
                        int choose = ran.Next(0, titles.Count);
                        for (int i = 0; i < titles.Count; i++)
                        {
                            if (choose == i)
                            {
                                new Actions(driver).MoveToElement(titles[i]).Pause(TimeSpan.FromSeconds(1)).ScrollByAmount(0, 200).MoveToElement(titles[i]).Pause(TimeSpan.FromSeconds(2)).Click().Build().Perform();
                                break;
                            }
                            else
                            {
                                new Actions(driver).ScrollByAmount(0, ran.Next(50, 300)).Build().Perform();
                            }
                        }

                        int timeWait = ran.Next(30, 80);
                        AddStatus(this.index, "Xem ngẫu nhiên " + timeWait + " giây!", 0);
                        Thread.Sleep(TimeSpan.FromSeconds(timeWait));
                    }
                    catch
                    {
                        AddStatus(this.index, "Tìm kiếm + đề xuất Lỗi!", 1);
                    }
                    

                }
                List<string> urlIfFalse = new List<string>() { "https://www.youtube.com/watch?v=GJLHlBRqKAI", "https://www.youtube.com/watch?v=r6zIGXun57U", "https://www.youtube.com/watch?v=hheZSOCEpwA" };

                if(!driver.Url.Contains("watch?v"))
                {
                    driver.Get(urlIfFalse[ran.Next(0, urlIfFalse.Count)]);
                    Thread.Sleep(3000);
                }

                // Thực hiện chèn video của mình vào trong mô tả của họ

                try // Nếu có nút hiển thị thêm thì bấm, còn không thì add nó vào
                {
                    driver.ExecuteScript("document.querySelector(\"tp-yt-paper-button#expand\").click();");
                    Thread.Sleep(1000);
                    driver.ExecuteScript("var html = '<a class=\"yt-simple-endpoint style-scope yt-formatted-string\" spellcheck=\"false\" href=\"/watch?v=" + videoID + "&t=0s\" dir=\"auto\">https://www.youtube.com/watch?v=" + videoID + "</a><br>';" +
                                        "  var element = document.querySelector(\"#description-inline-expander\");" +
                                        "  element.insertAdjacentHTML('afterbegin',html);");
                }
                catch
                {
                    driver.ExecuteScript("var html = '<a class=\"yt-simple-endpoint style-scope yt-formatted-string\" spellcheck=\"false\" href=\"/watch?v=" + videoID + "&t=0s\" dir=\"auto\">https://www.youtube.com/watch?v=" + videoID + "</a><br>';" +
                                        " var elements = document.querySelectorAll(\"#description > yt-formatted-string\");" +
                                        " var element = elements[elements.length- 1];" +
                                        " element.insertAdjacentHTML( 'afterbegin', html );");
                }

                Thread.Sleep(5000);
                // Thực hiện tìm kiếm và bấm vào video đề xuất để thực hiện
                var desCription = driver.FindElements(By.XPath("//ytd-text-inline-expander//a"));
                foreach (var i in desCription)
                {
                    if (i.GetAttribute("href").Contains(videoID))
                    {
                        new Actions(driver).MoveToElement(i).Pause(TimeSpan.FromSeconds(1)).ScrollByAmount(0, 100).MoveToElement(i).Pause(TimeSpan.FromSeconds(2)).Click().Build().Perform();
                        return true;
                    }

                }



            }
            catch
            {

            }
            return false;
        }




        public bool isYourChannel(string channelList)
        {
            bool result = false;

            try
            {
                var item = driver.FindElement(By.XPath("//ytd-video-owner-renderer//a"));
                string currentChannel = item.GetAttribute("href").Split('@')[1].Trim();
                if (channelList.Contains(currentChannel))
                {
                    return true;
                }
            }
            catch
            {
                AddStatus(this.index, "Lỗi kiểm tra channel!", 2);
            }

            return result;
        }
    }
}
