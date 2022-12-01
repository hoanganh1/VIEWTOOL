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

        
        public SuggestAutomation(UndetectChromeDriver driver)
        {
            this.driver = driver;
            this.action = new Actions(driver);
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
                int sc = ran.Next(0, 2);
                int type = !isYourChannel(channelID) ? 1 : 0;
                bool result = sc == 0 ? SuggestFromRight(channelNAme) : SuggestFromAnother(keyword, Idvideo, type);
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

            try
            {
                // Lấy tất cả những ID đang hiển thị 
                // Và tím kiếm ID đang
                var desCription = driver.FindElements(By.XPath("//iron-selector//yt-chip-cloud-chip-renderer//yt-formatted-string"));
               
                List<int> indexs = new List<int>();
                for (int i = 0; i < desCription.Count; i++)
                {
                    if (desCription[i].Displayed)
                    {
                        indexs.Add(i);
                        Console.WriteLine(desCription[i].GetAttribute("title"));
                    }
                    if (desCription[i].GetAttribute("title").Contains(channelName))
                    {
                        if (!indexs.Contains(i)) { indexs.Add(i); };
                    }

                }
                Thread.Sleep(1000);

                // Thực hiện ra xoát lần lượt từng Con đề xuất một
                // Nếu nó có tồn tại thì thực hiện click luôn

                foreach (int i in indexs)
                {
                    Actions act = new Actions(driver);
                    // Lặp lần lượt, nếu mà tìm thấy thì dừng, nếu không tìm thấy thì lập tức chuyển sang xem của thằng khác luôn
                    act.MoveToElement(desCription[indexs[i]]).ScrollByAmount(0, 20).MoveToElement(desCription[indexs[i]]).Click().Build().Perform();
                    Thread.Sleep(3000);
                    if (FindAndClick(channelName))
                    {
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {

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
        public bool FindAndClick(string channelName)
        {
            try
            {
                var desCription = driver.FindElements(By.XPath("//ytd-compact-video-renderer"));
                List<int> indexForFinding = new List<int>();
                for (int i = 0; i < desCription.Count; i++)
                {

                    var thubail = desCription[i].FindElement(By.TagName("yt-formatted-string"));
                    if (thubail.Text.Trim().Contains(channelName))
                    {
                        indexForFinding.Add(i);
                    }
                    Console.WriteLine(thubail.Text);
                }
                Thread.Sleep(1000);

                // Thực hiện cuộn đến vị trí được chọn
                int indexToRun = ran.Next(0, indexForFinding.Count);
                for (int i = 0; i <= indexForFinding[indexToRun]; i++)
                {
                    Actions act = new Actions(driver);
                    if (i == indexForFinding[indexToRun])
                    {
                        // click ngẫu nhiên vào thumb hoặc là hình ảnh
                        var titleOrthubm = ran.Next(0, 2) == 0 ? desCription[i].FindElement(By.TagName("ytd-thumbnail")) : desCription[i].FindElement(By.Id("video-title"));
                        act.MoveToElement(titleOrthubm).ScrollByAmount(0, 200).MoveToElement(titleOrthubm).Pause(TimeSpan.FromSeconds(2)).Click().Build().Perform();
                        return true;
                    }
                    else
                    {
                        act.ScrollByAmount(0, ran.Next(50, 300)).Build().Perform();
                    }
                }

            }
            catch
            {

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
                    int rn = ran.Next(0, 2);
                    // nếu rn =0 thì bấm vào home trước khi thực hiện search
                    if (rn == 0)
                    {
                        var items = driver.FindElement(By.XPath("//ytd-topbar-logo-renderer//yt-icon"));
                        Actions act = new Actions(driver);
                        act.MoveToElement(items).Click().Build().Perform();
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                    }
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
                    var titles = driver.FindElements(By.XPath("//ytd-video-renderer//h3//a"));
                    
                    int choose = ran.Next(0, titles.Count);
                    for (int i = 0; i < titles.Count; i++)
                    {
                        Actions actr = new Actions(driver);
                        if (choose == i)
                        {
                            actr.MoveToElement(titles[i]).Pause(TimeSpan.FromSeconds(1)).ScrollByAmount(0, 200).MoveToElement(titles[i]).Pause(TimeSpan.FromSeconds(2)).Click().Build().Perform();
                            break;
                        }
                        else
                        {
                            actr.ScrollByAmount(0, ran.Next(50, 300)).Build().Perform();
                        }
                    }
                    // Đợi để xem video của họ
                    Thread.Sleep(TimeSpan.FromSeconds(ran.Next(30, 80)));

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
                Actions de = new Actions(driver);
                foreach (var i in desCription)
                {
                    if (i.GetAttribute("href").Contains(videoID))
                    {
                        de.MoveToElement(i).Pause(TimeSpan.FromSeconds(1)).ScrollByAmount(0, 100).MoveToElement(i).Pause(TimeSpan.FromSeconds(2)).Click().Build().Perform();
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

            }

            return result;
        }
    }
}
