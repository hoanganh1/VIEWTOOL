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
    class HomeAutomation
    {
        Random ran = new Random();
        public List<string> channelList { get; set; }
        public UndetectChromeDriver driver { get; set; }

        public Actions action { get; set; }

        public HomeAutomation(UndetectChromeDriver driver, List<string> channels)
        {
            this.driver = driver;
            this.action = new Actions(driver);
            this.channelList = channels;
        }

        public void Run()
        {
            // Kiểm tra xem video đang xem có phải nằm trong danh sách channel của bạn không
            // Nếu không thì thực hiện tìm đến channel đó và kiểm tra xem đã đăng kí video hay chưa.
            bool isYour = isYourChannel(channelList);

            if (!isYour)
            {
                driver.Navigate().GoToUrl("https://www.youtube.com/@" + channelList[ran.Next(0, channelList.Count)]);
                Thread.Sleep(TimeSpan.FromSeconds(3));
            }
            // Kiểm tra xem đã đăng kí hay chưa, nếu chưa thì thực hiện đăng kí
            CheckAndSubscriber();
            Thread.Sleep(2000);
            int c = ran.Next(0, 2);
            bool RnC = c == 0 ? SubscriberProcess() : HomeProcess();
            if ((RnC == false))
            {
                if (c == 1)
                {
                    SubscriberProcess();
                }

            }





        }

        /// <summary>
        ///  Thực hiện sử lý bấm vào subscribe để xem video
        /// </summary>
        /// <returns></returns>
        public bool SubscriberProcess()
        {
            try
            {
                // Click vào nút home để quay lại màn hình chính
                var itemsHome = driver.FindElement(By.XPath("//ytd-topbar-logo-renderer//yt-icon"));
                action.MoveToElement(itemsHome).Click().Build().Perform();
                Thread.Sleep(TimeSpan.FromSeconds(ran.Next(2, 5)));

                // Click vào nút đăng ký
                var items = driver.FindElements(By.XPath("//ytd-guide-entry-renderer//a"));
                foreach (var item in items)
                {
                    var channel = item.GetAttribute("href");
                    if (channel != null)
                    {
                        if (channel.Contains("subscriptions"))
                        {
                            Console.WriteLine(channel);
                            action.MoveToElement(item).Pause(TimeSpan.FromSeconds(1)).Click().Build().Perform();
                        }
                    }

                }

                Thread.Sleep(TimeSpan.FromSeconds(ran.Next(2, 5)));
                /// Chọn chế độ xem video
                // flow = 1 là xem dạng danh sách
                // flow =2 là xem dạng lưới.
                int oneOrTwo = ran.Next(0, 2);
                string typeS = oneOrTwo == 0 ? "subscriptions?flow=1" : "subscriptions?flow=2";
                bool foundFlow = false;
                var simple = driver.FindElements(By.XPath("//ytd-button-renderer//yt-button-shape//a"));
                foreach (var i in simple)
                {
                    string title = i.GetAttribute("href");
                    Console.WriteLine(title);
                    if (title.Contains(typeS))
                    {
                        foundFlow = true;
                        action.MoveToElement(i).Click().Build().Perform();
                        Thread.Sleep(3000);
                        break;
                    }

                }
                // Nếu trong trường hợp mà không thì thấy video thì thực hiện làm gì đó.
                if (!foundFlow)
                {
                    driver.Navigate().GoToUrl("https://www.youtube.com/feed/" + typeS);
                }

                // Tìm kiếm ngẫu nhiên video dạng grid
                if (oneOrTwo == 0)
                {
                    var gridVideo = driver.FindElements(By.XPath("//ytd-grid-renderer//ytd-grid-video-renderer//ytd-thumbnail"));
                    int chooseVideo = ran.Next(0, gridVideo.Count);
                    Console.WriteLine(chooseVideo);
                    for (int i = 0; i < gridVideo.Count; i++)
                    {
                        if (i == chooseVideo)
                        {
                            Console.WriteLine("vào video");
                            action.MoveToElement(gridVideo[i]).Pause(TimeSpan.FromSeconds(2)).ScrollByAmount(0, 100).Pause(TimeSpan.FromSeconds(2)).MoveToElement(gridVideo[i]).Click().Build().Perform();
                            return true;
                            break;
                        }
                        else
                        {
                            action.ScrollByAmount(0, ran.Next(20, 50)).Build().Perform();
                        }
                    }

                }
                else
                {
                    string titileOrThumbail = ran.Next(0, 2) == 0 ? "//ytd-video-renderer//h3//a" : "//ytd-video-renderer//ytd-thumbnail";
                    var items1 = driver.FindElements(By.XPath(titileOrThumbail));
                    int chooseVideo = ran.Next(0, items1.Count);
                    Console.WriteLine(chooseVideo);
                    for (int i = 0; i < items1.Count; i++)
                    {
                        if (i == chooseVideo)
                        {
                            action.MoveToElement(items1[i]).Pause(TimeSpan.FromSeconds(2)).ScrollByAmount(0, 100).Pause(TimeSpan.FromSeconds(2)).MoveToElement(items1[i]).Click().Build().Perform();
                            return true;
                            break;
                        }
                        else
                        {
                            action.ScrollByAmount(0, ran.Next(50, 200)).Build().Perform();
                        }
                    }

                }




            }
            catch
            {

            }
            return false;
        }


        /// <summary>
        /// Kiểm tra xử lý Màn hình home nếu video có tồn tại ở màn hình home thì thực hiện click vào nó
        /// dừn tìm kiếm khi lặp lại 2 lần.
        /// </summary>
        /// <returns></returns>
        public bool HomeProcess()
        {
            try
            {
                bool stop = false;
            findAgain:
                var items = driver.FindElement(By.XPath("//ytd-topbar-logo-renderer//yt-icon"));
                action.MoveToElement(items).Click().Build().Perform();
                Thread.Sleep(TimeSpan.FromSeconds(ran.Next(2, 5)));
                // Kiểm tra xem trong trang chủ có Kênh của mình không
                // Nếu có thì clikc() nếu không lặp lại 1 lần
                bool stopRepeat = false;
            repeat: // Lặp lại tìm kiếm khi load thêm đối tượng
                var items1 = driver.FindElements(By.XPath("//ytd-rich-item-renderer//ytd-rich-grid-media"));
                foreach (var item in items1)
                {
                    var íchannel = item.FindElement(By.Id("avatar-link"));
                    var channel = íchannel.GetAttribute("href");
                    // Console.WriteLine(channel);
                    if (channel != null)
                    {
                        if (channel.Contains("https://www.youtube.com/@"))
                        {
                            string chnlID = channel.Split('@')[1].Trim();
                            if (channelList.Contains(chnlID))
                            {
                                var s = item.FindElement(By.TagName("ytd-thumbnail"));
                                action.MoveToElement(s).Build().Perform();
                                Thread.Sleep(TimeSpan.FromSeconds(1));
                                action.ScrollByAmount(0, 100).Build().Perform();
                                Thread.Sleep(TimeSpan.FromSeconds(2));
                                action.MoveToElement(s).Pause(TimeSpan.FromSeconds(2)).Click().Build().Perform();
                                return true;
                                break;
                            }
                            else
                            {
                                action.ScrollByAmount(0, ran.Next(20, 200)).Build().Perform();
                            }

                        }

                    }
                }
                // Nếu =0 thì tìm kiếm tiếp
                // bằng 1 thì load lại trang chủ và thực hiện tìm kiếm.
                if (0 == 0 && stopRepeat == false)
                {
                    stopRepeat = true;
                    stop = true;
                    goto repeat;
                }

                if (!stop)
                {
                    stop = true;
                    goto findAgain;
                }
            }
            catch
            {

            }
            return false;
        }

        /// <summary>
        ///  Kiểm tra xem nếu video đã đăng kí chưa
        ///  // Nếu chưa thì thực hiện đăng kí
        /// </summary>
        /// <returns></returns>
        public void CheckAndSubscriber()
        {


            try
            {
                var hehe = driver.ExecuteScript("return document.getElementsByTagName(\"tp-yt-paper-button\")[0].getAttribute('subscribed');");
                if (hehe == null)
                {
                    driver.ExecuteScript("return document.getElementsByTagName(\"tp-yt-paper-button\")[0].click();");
                }

            }
            catch
            {

            }

        }



        /// <summary>
        ///  Kiểm tra xem có phải kênh mình đang chạy hay không!
        /// </summary>
        /// <param name="channelList"></param>
        /// <returns></returns>
        public bool isYourChannel(List<string> chanelList)
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
