using GPM_View.Controller;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace GPM_View
{
    public partial class Form1 : Form
    {
        List<string> listComments = new List<string>();
        List<string> listChannel = new List<string>();
        List<string> listIDAndKey = new List<string>();
        List<string> listKeywordForSuggest = new List<string>();
        List<JObject> profiles;
        List<account> lstAccount; // danh sách tài khoản mà đã nhập vào
        List<bool> listRunning = new List<bool>(); // Danh sách các thread đang chạy!
        List<int> listOfPort = new List<int>();
        int numberRow = 0; // index của rows
        bool repeat = true;
        List<int> listRandomView = new List<int>();
        int limitView = 0;
        int countThread = 0;

        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }
       
        List<string> lstProxy;
       // List<string> Listcomments;
        int proxyNumber = 0;
       
        Random random = new Random();
        private void btnEmail_Click(object sender, EventArgs e)
        {
            checkopentab = true;
            lstAccount = new List<account>();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            if (ofd.FileName.Length > 0)
            {
                var lines = File.ReadAllLines(ofd.FileName);
                int num = 0;
                foreach (var line in lines)
                {
                    account account = new account();
                    account.stt = num; num += 1;
                    account.email = line.Split('\t')[0].Trim();
                    account.password = line.Split('\t')[1].Trim();
                    account.mail_kp = line.Split('\t')[2].Trim();
                    account.status = "";
                    lstAccount.Add(account);
                }
            }
            dataGrid.DataSource = lstAccount;
            dataGrid.Columns[0].Width = 50;
            dataGrid.Columns[1].Width = 100;
            dataGrid.Columns[2].Width = 70;
            dataGrid.Columns[3].Width = 50;
            dataGrid.Columns["status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            numberRow = 0;
        }
        void loadProxy()
        {
            lstProxy = new List<string>();
            var item = File.ReadAllLines("data\\proxy.txt");
            foreach (string line in item)
            {
                lstProxy.Add(line.Trim());
            }
        }
        void saveError(account mails, string error)
        {
            for (int i = 0; i < 1000; i++)
            {
                try { File.AppendAllText("Error.txt", mails.email + "|" + mails.password + "|" + mails.mail_kp + "|" + error + "\r\n"); break; } catch { Thread.Sleep(100); }
            }
        }
        void resave(string proxy)
        {
            lstProxy.Remove(proxy);
            try
            {
                File.WriteAllText("data\\proxy.txt", String.Join("\r\n", lstProxy));
            }
            catch { }
        }
        List<Thread> lsThread = new List<Thread>();

     


        



        void createThread(int indexKichBan)
        {
            for (int i = 0; i < nbThread.Value; i++)
            {
                Thread st = new Thread(() =>
                {
                    run(indexKichBan);
                });
                st.IsBackground = true;
                st.Start();
                lsThread.Add(st);
                Thread.Sleep(1000);
            }
        }
     
        bool clickDeSau(UndetectChromeDriver driver)
        {
            try
            {
                driver.ExecuteScript("document.getElementsByClassName('VfPpkd-Jh9lGc')[0].click()"); return true;
            }
            catch
            {
                ClickXacMinh(driver);
                return false;
            }
        }
        bool ClickXacMinh(UndetectChromeDriver driver)
        {
            try
            {
              
                driver.ExecuteScript("document.getElementsByClassName('ZFr60d CeoRYc')[1].click()"); return true;
            }
            catch
            {
                return false;

            }
        }
        void addbirthday(UndetectChromeDriver driver)
        {
            driver.Navigate().GoToUrl(urlLogin);
            Thread.Sleep(7000);
        }
        List<string> list = new List<string>();

        string urlLogin = "https://accounts.google.com/signin/v2/identifier?service=lbc&passive=1209600&continue=https%3A%2F%2Fbusiness.google.com%2F%3FskipPagesList%3D1%26skipLandingPage%3Dtrue%26hl%3Den%26gmbsrc%3Dus-en-z-z-z-gmb-l-z-d~mhp-hom_sig-u&followup=https%3A%2F%2Fbusiness.google.com%2F%3FskipPagesList%3D1%26skipLandingPage%3Dtrue%26hl%3Den%26gmbsrc%3Dus-en-z-z-z-gmb-l-z-d~mhp-hom_sig-u&hl=en&flowName=GlifWebSignIn&flowEntry=ServiceLogin";

        void kichban2()
        {
            int dem_kb2 = 0;
            int index = numberRow;
            numberRow += 1;
            if (index >= dataGrid.Rows.Count)
            {
                return;
            }
            if (!checkopentab)
            {
                return;
            }
            account act = lstAccount[index];
            UndetectChromeDriver driver = null;
            try
            {
                //addStatus(index, "starting");
                GPMLoginAPI api = new GPMLoginAPI("http://" + APP_URL.Text);
                acton sts = new acton(act, api);
                Thread.Sleep(1000);
                JObject ojb = sts.getLst(act.email, profiles);
                string id_profile = "";
                if (ojb == null)
                {
                    int prox = proxyNumber;
                    loadProxy();
                    proxyNumber += 1;
                    if (prox >= lstProxy.Count)
                    {
                        proxyNumber = 0;
                        prox = proxyNumber;
                    }
                    string proxy = lstProxy[prox];
                    resave(proxy);
                 
                    ojb = api.Create(act.email, proxy, true);
                    if (ojb != null)
                    {
                        //Tạo thành công
                        id_profile = ojb["profile_id"].ToString();
                        //addStatus(index, "tạo profile thành công");
                        saveProfile(act, proxy);
                    }
                }
                else
                {
                    //đã có profile
                    id_profile = ojb["id"].ToString();
                }
                Thread.Sleep(1000);
                bool lockWasTaken = false;
                var temp = obj;
                try
                {
                    Monitor.Enter(temp, ref lockWasTaken);
                    //addStatus(index, "đang mở profile");
                    try { driver = sts.openProfile(id_profile, index); }
                    catch
                    {
                       // addStatus(index, "Lỗi mở profile");
                        saveError(act, "Lỗi mở profile");
                        goto ketthuc;
                    }
                }
                finally
                {
                    if (lockWasTaken)
                    {
                        Monitor.Exit(temp);
                    }
                }
                while (dem_kb2 < nbThread.Value - 1)
                {
                    dem_kb2++;
                    Thread.Sleep(2000);
                }
                //addStatus(index, "truy cập google");
                try { driver.Get(urlLogin); } catch {

                    //addStatus(index, "Lỗi truy cập google");
                    driver.Close();
                    driver.Dispose();
                    driver.Quit();
                    goto ketthuc;}

                Thread.Sleep(2000);
               
                if (driver.Url.Contains("business.google.com/create/new"))
                {
                    goto searchz;
                }
                login st = new login(driver, act); int demnha = 0;
                string Error = string.Empty;
            lainha:
                if (st.Nanial(urlLogin))
                {
                    if (!st.StartLogin(out Error))
                    {
                        if (Error == "captcha")
                        {
                            demnha += 1;
                            if (demnha == 7)
                            {
                                //addStatus(index, "Lỗi captcha !");
                                driver.Close();
                                driver.Dispose();
                                driver.Quit();
                                goto ketthuc;
                            }
                            goto lainha;
                        }

                        //Cảnh bảo lỗi
                    }
                }
                Thread.Sleep(TimeSpan.FromSeconds(3));
                //create account 
                //addStatus(index, "Đã login mail !");
                clickDeSau(driver); Thread.Sleep(TimeSpan.FromSeconds(5));
                if (driver.Url.Contains("inoptions/recovery-options-collection"))
                {
                    driver.Navigate().GoToUrl(urlLogin);
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                }
                if (driver.Url.Contains("om/signin/v2/challenge/iap") || (driver.Url.Contains("gle.com/signin/rejected")))
                {
                    saveError(act, "Very phone");
                    //addStatus(index, "very phone !");
                    driver.Close();
                    driver.Dispose();
                    driver.Quit();
                    goto ketthuc;
                }
                if (driver.Url.Contains(".com/interstitials/birthday") || (driver.Url.Contains("ogle.com/web/chip")) || (driver.Url.Contains("/info/unknownerror")))
                {
                    //addStatus(index, "Vui lòng thêm ngày sinh");
                    addbirthday(driver);
                }
                if ((driver.Url.Contains("m/signin/v2/identifier")) || (driver.Url.Contains("ccounts.google.com/speedbump/idvreenable")) || (driver.Url.Contains("m/signin/v2/disabled/explanation")))
                {
                    saveError(act, "Đăng nhập không thành công!");
                    //addStatus(index, "Đăng nhập không thành công");
                    driver.Close();
                    driver.Dispose();
                    driver.Quit();
                    goto ketthuc;
                }
                if (driver.Url.Contains("ogle.com/web/chip"))
                {
                    addbirthday(driver);
                }
            searchz:
                if (driver.Url.Contains("/signin/v2/challenge/pwd"))
                {
                    saveError(act, "Đăng nhập không thành công");
                    //addStatus(index, "Đăng nhập không thành công");
                    driver.Close();
                    driver.Dispose();
                    driver.Quit();
                    goto ketthuc;
                }
                else
                {
                    acYoutube active = new acYoutube(driver);
                    active.gotoHome();
                    Thread.Sleep(2000);
                vireyt:
                    try
                    { 
                        IJavaScriptExecutor executorUseData = driver;
                        string name = txtKeyword.Text;
                        //addStatus(index, "Tìm kiếm theo key " + name);
                        active.searchKeyword(name);
                        Thread.Sleep(TimeSpan.FromSeconds(4));

                        var items = driver.FindElements(By.XPath("//ytd-video-renderer//ytd-thumbnail//a"));
                        foreach (var item in items)
                        {
                            string urlVideo = item.GetAttribute("href");
                            if (urlVideo.Contains(strTenChannel))
                            {
                                executorUseData.ExecuteScript("arguments[0].click()", item);
                                break;
                            }
                        }
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                        countTime time = new countTime();

                        int timevideo = 0;
                        for (int i = 0; i < 300; i++)
                        {
                            if (i % 50 == 0)
                            {
                                driver.Navigate().Refresh();
                                Thread.Sleep(2000);
                            }
                            timevideo = active.getTimeVideo();
                            if (timevideo != 0)
                            {
                                break;
                            }
                            Thread.Sleep(1500);
                        }
                        time.reset();
                        Thread.Sleep(TimeSpan.FromSeconds(30));
                        //addStatus(index, "Reset video ve 0");
                        // Khởi tạo đối tượng thuộc Actions class
                        Actions action = new Actions(driver);

                        action.KeyDown(OpenQA.Selenium.Keys.NumberPad0);

                        action.SendKeys(OpenQA.Selenium.Keys.NumberPad0).Perform();
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(6);
                        Thread.Sleep(TimeSpan.FromSeconds(5));

                        int rand = rand = random.Next(80, 99);
                        int sleepTime = rand * timevideo / 100;
                        //addStatus(index, "Chuyen video sau : " + sleepTime + " s");
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(12);
                        Thread.Sleep(TimeSpan.FromSeconds(10));

                        //Sleep 1min
                        //Scroll after 20min
                        if (sleepTime > 1 * 60)
                        {
                            int sleepCount = sleepTime / (60);
                            int sleepCountDiv = sleepTime % 60;
                            if (sleepCountDiv > 0)
                            {
                                sleepCount += 1;
                            }
                            for (int j = 0; j < sleepCount; j++)
                            {
                                if (j == sleepCount - 1)
                                {
                                    //addStatus(index, sleepCountDiv.ToString());
                                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(sleepTime + 2 - (j - 1) * 60);
                                    Thread.Sleep(TimeSpan.FromSeconds(sleepCountDiv));
                                }
                                else
                                {
                                    //addStatus(index, "đang xem video mồi");
                                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1 * 60);
                                    Thread.Sleep(TimeSpan.FromMinutes(1));
                                    if ((j + 1) % 20 == 0)
                                    {
                                        int x = 100;
                                        driver.ExecuteScript("window.scrollTo(100," + (x * j + ")"));
                                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
                                        Thread.Sleep(TimeSpan.FromSeconds(2));
                                        //addStatus(index, "2s");
                                        driver.ExecuteScript("window.scrollTo({ top: 0, behavior: 'smooth' });");
                                    }

                                }
                            }
                        }
                        else
                        {
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(sleepTime + 2);
                            Thread.Sleep(TimeSpan.FromSeconds(sleepTime));
                        }
                      
                        Thread.Sleep(5000);

                        //addStatus(index, "Click Tim PLL");

                        driver.FindElement(By.XPath("//a[@class='yt-simple-endpoint style-scope ytd-video-owner-renderer']")).Click();

                        Thread.Sleep(10000);
                        
                        //
                        driver.FindElement(By.XPath("//*[@id='tabsContent']/tp-yt-paper-tab[3]")).Click();

                        Thread.Sleep(10000);
                        int str = Convert.ToInt32(driver.ExecuteScript("var t = document.getElementsByClassName('ytd-playlist-thumbnail').length; return t;"));
                        if (str > 0)
                        {
                            int t = random.Next(0, str);
                            driver.ExecuteScript("document.getElementsByClassName('ytd-playlist-thumbnail')[" + t + "].click()");
                        }
                        else
                        {
                            driver.FindElement(By.XPath("//*[@id='tabsContent']/tp-yt-paper-tab[4]")).Click();
                            Thread.Sleep(10000);
                            str = Convert.ToInt32(driver.ExecuteScript("var t = document.getElementsByClassName('ytd-playlist-thumbnail').length; return t;"));
                            int t = random.Next(0, str);
                            driver.ExecuteScript("document.getElementsByClassName('ytd-playlist-thumbnail')[" + t + "].click()");
                        }

                        Thread.Sleep(10000);
                        int videoNext = 0;

                       for(int i = 0; i < 1000; i++) {

                            videoNext += 1;
                            bool is_comment = false;

                            for (int k = 0; k < 300; k++)
                            {
                                if (k % 50 == 0)
                                {
                                    driver.Navigate().Refresh();
                                    Thread.Sleep(2000);
                                }
                                timevideo = active.getTimeVideo();
                                if (timevideo != 0)
                                {
                                    break;
                                }
                                Thread.Sleep(1500);
                            }

                            int rand_video_sub= random.Next(3, 8); // random khoang time sub channel
                            if (videoNext == rand_video_sub)
                            {
                                if (sub.Checked)
                                {

                                    try { active.subVideo(); }
                                    catch
                                    {
                                        continue;
                                    }
                                }
                            }
                            Thread.Sleep(5000);

                            //addStatus(index, "Reset video ve 0");
                            // Khởi tạo đối tượng thuộc Actions class
                            action = new Actions(driver);

                            action.KeyDown(OpenQA.Selenium.Keys.NumberPad0);

                            action.SendKeys(OpenQA.Selenium.Keys.NumberPad0).Perform();
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(6);
                            Thread.Sleep(TimeSpan.FromSeconds(5));

                            rand = random.Next(80, 99);

                            sleepTime = rand * timevideo / 100;
                            //addStatus(index, "Chuyen video sau : " + sleepTime + " s");
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(12);
                            Thread.Sleep(TimeSpan.FromSeconds(10));

                            //Sleep 1min
                            //Scroll after 20min
                            if (sleepTime > 1 * 60)
                            {
                                int sleepCount = sleepTime / (60);
                                int sleepCountDiv = sleepTime % 60;
                                if (sleepCountDiv > 0)
                                {
                                    sleepCount += 1;
                                }
                                int rand_cmt_time = random.Next(2, sleepCount);// random khoang time comment don vi phut

                                for (int j = 0; j < sleepCount; j++)
                                {
                                    if (j == sleepCount - 1)
                                    {
                                        //addStatus(index, sleepCountDiv.ToString());
                                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(sleepTime + 2 - (j - 1) * 60);
                                        Thread.Sleep(TimeSpan.FromSeconds(sleepCountDiv));
                                    }
                                    else
                                    {
                                        //addStatus(index, "Đang xem video PLL");
                                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1 * 60);
                                        Thread.Sleep(TimeSpan.FromMinutes(1));
                                        if ((j + 1) % 20 == 0)
                                        {
                                            int x = 100;
                                            driver.ExecuteScript("window.scrollTo(100," + (x * j + ")"));
                                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
                                            Thread.Sleep(TimeSpan.FromSeconds(2));
                                            //addStatus(index, "2s");
                                            driver.ExecuteScript("window.scrollTo({ top: 0, behavior: 'smooth' });");
                                        }
                                        if (is_comment == false)
                                        {
                                            int rand_video_cmt = random.Next(2, 6);
                                            Thread.Sleep(10000);
                                            if (videoNext % rand_video_cmt == 0)
                                            {
                                                if (j == rand_cmt_time)
                                                {
                                                    if (listComments.Count > 0)
                                                    {
                                                        try
                                                        {
                                                            int randomCommend = random.Next(listComments.Count);
                                                            active.goToComment(listComments[randomCommend]);
                                                            is_comment = true;
                                                            Thread.Sleep(6000);
                                                        }
                                                        catch
                                                        {
                                                            continue;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(sleepTime + 2);
                                Thread.Sleep(TimeSpan.FromSeconds(sleepTime));
                            }
                            int tr = random.Next(0, 2);
                            if (videoNext == 1)
                            {
                                tr = 1;
                            }
                            //addStatus(index, "Click Video Next");
                            if (tr == 0)
                            {
                                try { driver.ExecuteScript("document.getElementsByClassName('ytp-prev-button ytp-button')[0].click()"); }
                                catch
                                {
                                    Thread.Sleep(1000);
                                }
                                //Back
                            }
                            else
                            {
                                //forword
                                try { driver.ExecuteScript("document.getElementsByClassName('ytp-next-button ytp-button')[0].click()"); }
                                catch
                                {
                                    Thread.Sleep(1000);
                                }
                            }
                            Thread.Sleep(10000);
                            if(i == 999)
                            {
                                //addStatus(index, "xem hết pll");
                                driver.Close();
                                driver.Dispose();
                                driver.Quit();
                                return;
                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        //addStatus(index, "timecout access youtube");
                        driver.Close();
                        driver.Dispose();
                        driver.Quit();
                        goto ketthuc;
                    }
                }

            }
            catch (Exception ex)
            {
                //addStatus(index, ex.Message.ToString());
                goto ketthuc;
            }
        ketthuc:
            runNew(2);
            return;
        }
        bool flag_start = true;
        
       
        void run(int indexKichBan)
        {
            if(flag_start == true)
            {
                GPMLoginAPI api = new GPMLoginAPI("http://" + apiUrl.Text.Trim());
                profiles = api.GetProfiles();
            }
            flag_start = false;

            KichbanMix();
        }

        /// <summary>
        ///  Lấy ra index // nếu mà đến cuối của kịch bản thì làm gì
        /// </summary>
        /// <param name="a"></param>
        public void KichbanMix(int a=-1)
        {
            
            int dem_kb1 = 0;
            int index = 0;
            if (a > -1)
            {
                index = a;
            }else
            {
                index = numberRow;
            }
            
            numberRow += 1;
            if (index >= dataGrid.Rows.Count) // Nếu mà chạy đến cái cuối cùng rồi thì lặp lại kịch bản
            {
                numberRow = 0;
                index = numberRow;
                numberRow += 1;
            }
            account act = lstAccount[index]; // Lấy ra account
            UndetectChromeDriver driver = null;
            try
            {
                // Lấy thong tin profile
                //addStatus(index, "starting");
                GPMLoginAPI api = new GPMLoginAPI("http://" + apiUrl.Text.Trim());
                acton sts = new acton(act, api);
                Thread.Sleep(1000);
                JObject ojb = sts.getLst(act.email, profiles); // Lấy ra thông số của trình duyệt
                string id_profile = "";
                if (ojb == null)
                {
                    goto ketthuc;
                }
                else
                {
                    //đã có profile
                    id_profile = ojb["id"].ToString();
                }
                Thread.Sleep(1000);
                bool lockWasTaken = false;
                var temp = obj;
                try
                {
                    Monitor.Enter(temp, ref lockWasTaken);
                    //addStatus(index, "đang mở profile");
                    try { driver = sts.openProfile(id_profile, index); } // Mở thông số lên
                    catch
                    {
                        //addStatus(index, "Lỗi mở profile");
                        saveError(act, "Lỗi mở profile");
                        goto ketthuc;
                    }
                }
                finally
                {
                    if (lockWasTaken)
                    {
                        Monitor.Exit(temp);
                    }
                }
                while (dem_kb1 < nbThread.Value - 1)
                {
                    dem_kb1++;
                    Thread.Sleep(2000);
                }
                //addStatus(index, "Kiểm tra đăng nhập");
                Thread.Sleep(2000);
                LoginAutomation login = new LoginAutomation(driver);
                if(!login.Login(act.email, act.password,act.mail_kp))
                {
                    //addStatus(index, "Lỗi Login!");
                    driver.Close();
                    driver.Dispose();
                    driver.Quit();
                    goto ketthuc;
                }
                Thread.Sleep(3000);
                //addStatus(index, "Try cập vào youtube");
                driver.Get("https://www.youtube.com/");
                Thread.Sleep(TimeSpan.FromSeconds(random.Next(3, 5)));

                SearchAutomation search = new SearchAutomation(driver,index);
                VideoAutomation video = new VideoAutomation(driver, index);
                HomeAutomation home = new HomeAutomation(driver, listChannel, index);
                SuggestAutomation suggest = new SuggestAutomation(driver, index);
            searchAgain:
                //addStatus(index, "Khởi chạy Tìm kiếm lần đầu tuy cập");
                int idexSearch = random.Next(0, listIDAndKey.Count);
                string[] idandKey = listIDAndKey[idexSearch].ToString().Split(';');
                if(!search.Run(0,idandKey[1],idandKey[0]))
                {
                    //addStatus(index, "Thực hiện tìm kiếm lần nữa!");
                    goto searchAgain;
                }
                //addStatus(index, "Đã tìm thấy video!");
                Thread.Sleep(3000);
                //addStatus(index, "Bắt đầu xem video!");
                video._Comment = listComments[random.Next(0, listComments.Count)].ToString();
                video._from = (int)numFrom.Value;
                video._to = (int)numFrom.Value;
                video._isLike = true;
                video._isComment = true;
                video._waitTimeEnd = (int)waitTimeEnd.Value;
                video._waitTimeStart = (int)waitTimeStart.Value;
                video.Run();
                //addStatus(index, "Đã xem xong video");
                Thread.Sleep(3000);
                while (true)
                {
                    int iView = random.Next(0, 3);
                    switch(iView)
                    {
                        case 0:
                            //addStatus(index, "Đang chạy view trang chủ!");
                            home.Run();
                            break;
                        case 1:
                            int idexSearch1 = random.Next(0, listIDAndKey.Count);
                            string[] idandKey1 = listIDAndKey[idexSearch].ToString().Split(';');
                            //addStatus(index, "Đang chạy view tìm kiếm!");
                            search.Run(1, idandKey1[1], idandKey1[0]);
                            break;
                        case 2:
                            //addStatus(index, "Đang chạy view đề xuất");
                            int idexSearch2 = random.Next(0, listIDAndKey.Count);
                            string[] idandKey2 = listIDAndKey[idexSearch].ToString().Split(';');
                            suggest.run(txtChannelName.Text.Trim(), txtChannelID.Text.Trim(), listKeywordForSuggest[random.Next(0, listKeywordForSuggest.Count)], idandKey2[0]);
                            break;
                        default:
                            break;

                    }
                    Thread.Sleep(3000);
                    //addStatus(index, "Bắt đầu xem video!");
                    video._Comment = listComments[random.Next(0, listComments.Count)].ToString();
                    video._from = (int)numFrom.Value;
                    video._to = (int)numFrom.Value;
                    video._isLike = true;
                    video._isComment = true;
                    video.Run();
                    //addStatus(index, "Đã xem xong video");
                    Thread.Sleep(2000);

                    // randome View

                }

                //addStatus(index, "Done!");
                try
                {
                    driver.Close();
                }
                catch
                {

                }
                try
                {
                    driver.Quit();
                }
                catch
                {

                }
                try
                {
                    driver.Dispose();
                }
                catch
                {

                }
            
              
                goto ketthuc;

            }
            catch
            {
                //addStatus(index, "Ngoại lệ view");
                try
                {
                    driver.Close();
                }
                catch
                {

                }
                try
                {
                    driver.Quit();
                }
                catch
                {

                }
                try
                {
                    driver.Dispose();
                }
                catch
                {

                }


                goto ketthuc;
            }
        ketthuc:
            KichbanMix(index);
            return;
        }



        private static readonly Object obj = new Object();

        private void kichban1()
        {
            int dem_kb1 = 0;
            int index = numberRow;
            numberRow += 1;
            if (index >= dataGrid.Rows.Count)
            {
                return;
            }
            if (!checkopentab)
            {
                return;
            }
            account act = lstAccount[index];
            UndetectChromeDriver driver = null;
            try
            {
                //addStatus(index, "starting");
                GPMLoginAPI api = new GPMLoginAPI("http://" + APP_URL.Text);
                acton sts = new acton(act, api);
                Thread.Sleep(1000);
                JObject ojb = sts.getLst(act.email, profiles); // Lấy ra thông số của trình duyệt
                string id_profile = ""; 
                if (ojb == null)
                {
                   
                }
                else
                {
                    //đã có profile
                    id_profile = ojb["id"].ToString();
                }
                Thread.Sleep(1000);
               
                bool lockWasTaken = false;
                var temp = obj;
                try
                {
                    Monitor.Enter(temp, ref lockWasTaken);
                    //addStatus(index, "đang mở profile");
                    try { driver = sts.openProfile(id_profile, index); } // Mở thông số lên
                    catch
                    {
                        //addStatus(index, "Lỗi mở profile");
                        saveError(act, "Lỗi mở profile");
                        goto ketthuc;
                    }
                }
                finally
                {
                    if (lockWasTaken)
                    {
                        Monitor.Exit(temp);
                    }
                }
                while (dem_kb1 < nbThread.Value - 1)
                {
                    dem_kb1++;
                    Thread.Sleep(2000);
                }
                //addStatus(index, "truy cập google");

                try { driver.Get(urlLogin); } 
                
                catch {
                    //addStatus(index, "Lỗi truy cập google !");
                    driver.Close();
                    driver.Dispose();
                    driver.Quit();
                    goto ketthuc;
                }

                if (driver.Url.Contains("business.google.com/create/new"))
                {
                    goto searchz;
                }
                login st = new login(driver, act); int demnha = 0;
                string Error = string.Empty;
            lainha:
                if (st.Nanial(urlLogin))
                {
                    if (!st.StartLogin(out Error))
                    {
                        if (Error == "captcha")
                        {
                            demnha += 1;
                            if (demnha == 7)
                            {
                                //addStatus(index, "Lỗi captcha !");
                                driver.Close();
                                driver.Dispose();
                                driver.Quit();
                                goto ketthuc;
                            }
                            goto lainha;
                        }

                        //Cảnh bảo lỗi
                    }
                }
                Thread.Sleep(TimeSpan.FromSeconds(3));

                //addStatus(index, "Đã login mail !");
                clickDeSau(driver); Thread.Sleep(TimeSpan.FromSeconds(5));
                if (driver.Url.Contains("inoptions/recovery-options-collection"))
                {
                    driver.Navigate().GoToUrl(urlLogin);
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                }
                if (driver.Url.Contains("om/signin/v2/challenge/iap") || (driver.Url.Contains("gle.com/signin/rejected")))
                {
                    saveError(act, "Very phone");
                    //addStatus(index, "very phone !");
                    driver.Close();
                    driver.Dispose();
                    driver.Quit();
                    goto ketthuc;
                }
                if (driver.Url.Contains(".com/interstitials/birthday") || (driver.Url.Contains("ogle.com/web/chip")) || (driver.Url.Contains("/info/unknownerror")))
                {
                    //addStatus(index, "Vui lòng thêm ngày sinh");
                    addbirthday(driver);
                }
                if ((driver.Url.Contains("m/signin/v2/identifier")) || (driver.Url.Contains("ccounts.google.com/speedbump/idvreenable")) || (driver.Url.Contains("m/signin/v2/disabled/explanation")))
                {
                    saveError(act, "Đăng nhập không thành công!");
                    //addStatus(index, "Đăng nhập không thành công");
                    driver.Close();
                    driver.Dispose();
                    driver.Quit();
                    goto ketthuc;
                }
                if (driver.Url.Contains("ogle.com/web/chip"))
                {
                    addbirthday(driver);
                }
            searchz:
                if (driver.Url.Contains("/signin/v2/challenge/pwd"))
                {
                    saveError(act, "Đăng nhập không thành công");
                    //addStatus(index, "Đăng nhập không thành công");
                    driver.Close();
                    driver.Dispose();
                    driver.Quit();
                    goto ketthuc;
                }
                else
                {
                    acYoutube active = new acYoutube(driver);
                    active.gotoHome();
                    Thread.Sleep(2000);
                vireyt:
                    try
                    {
                        IJavaScriptExecutor executorUseData = driver;
                        string name = txtKeyword.Text;
                        //addStatus(index, "Tìm kiếm theo key " + name);
                        active.searchKeyword(name);
                        Thread.Sleep(TimeSpan.FromSeconds(4));

                        var items = driver.FindElements(By.XPath("//ytd-video-renderer//ytd-thumbnail//a"));
                        foreach (var item in items)
                        {
                            string urlVideo = item.GetAttribute("href");
                            if (urlVideo.Contains(strTenChannel))
                            {
                                executorUseData.ExecuteScript("arguments[0].click()", item);
                                break;
                            }
                        }
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                        

                        int rand = random.Next(80, 99);
                        int timevideo = 0;

                        for (int i = 0; i < iSoLanMoLink; i++)
                        {
                            bool is_comment = false;
                            int rand_num_cmt = random.Next(2, 5); // random comment o moi lan click link
                            countTime time = new countTime();

                            for (int k = 0; k < 300; k++)
                            {
                                if (k % 50 == 0)
                                {
                                    driver.Navigate().Refresh();
                                    Thread.Sleep(2000);
                                }
                                timevideo = active.getTimeVideo();
                                if (timevideo != 0)
                                {
                                    break;
                                }
                                Thread.Sleep(1500);
                            }
                          //  addStatus(index, "Time Video " + timevideo);
                            Thread.Sleep(3000);
                            time.reset();

                            Thread.Sleep(TimeSpan.FromSeconds(30));
                            //addStatus(index, "Reset video ve 0");
                            // Khởi tạo đối tượng thuộc Actions class
                            Actions action = new Actions(driver);

                            action.KeyDown(OpenQA.Selenium.Keys.NumberPad0);

                            action.SendKeys(OpenQA.Selenium.Keys.NumberPad0).Perform();
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(6);
                            Thread.Sleep(TimeSpan.FromSeconds(5));

                            rand = random.Next(80, 99);

                            int sleepTime = rand * timevideo / 100;
                            //addStatus(index, "Chuyen video sau : " + sleepTime + " s");
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(12);
                            Thread.Sleep(TimeSpan.FromSeconds(10));
                            if (sub.Checked)
                            {

                                try { active.subVideo(); }
                                catch
                                {
                                    continue;
                                }
                            Thread.Sleep(5000);
                            if (sleepTime > 1 * 60)
                            {
                                int sleepCount = sleepTime / (60);
                                int sleepCountDiv = sleepTime % 60;
                                if (sleepCountDiv > 0)
                                {
                                    sleepCount += 1;
                                }
                                for (int j = 0; j < sleepCount; j++)
                                {
                                    if (j == sleepCount - 1)
                                    {
                                        //addStatus(index, sleepCountDiv.ToString());
                                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(sleepTime + 2 - (j - 1) * 60);
                                        Thread.Sleep(TimeSpan.FromSeconds(sleepCountDiv));
                                    }
                                    else
                                    {
                                        if(i > 0)
                                        {
                                            //addStatus(index, "đang xem video lần click thứ" + i);
                                        }
                                        
                                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1 * 60);
                                        Thread.Sleep(TimeSpan.FromMinutes(1));
                                        if ((j + 1) % 20 == 0)
                                        {
                                            int x = 100;
                                            driver.ExecuteScript("window.scrollTo(100," + (x * j + ")"));
                                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
                                            Thread.Sleep(TimeSpan.FromSeconds(2));
                                            //addStatus(index, "2s");
                                            driver.ExecuteScript("window.scrollTo({ top: 0, behavior: 'smooth' });");

                                        }
                                        if(is_comment == false)
                                            {
                                                if (i % rand_num_cmt == 0) // comment tai lan click Des random
                                                {
                                                    int rand_commenTime = random.Next(1, sleepCount); //comment tai so phut random
                                                    if(j == rand_commenTime)
                                                    {
                                                        if (listComments.Count > 0)
                                                        {
                                                            try
                                                            {
                                                                int randomCommend = random.Next(listComments.Count);
                                                                active.goToComment(listComments[randomCommend]);
                                                                is_comment = true;
                                                                Thread.Sleep(6000);
                                                            }
                                                            catch
                                                            {
                                                                continue;
                                                            }
                                                        }
                                                    }
                                                    
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(sleepTime + 2);
                                Thread.Sleep(TimeSpan.FromSeconds(sleepTime));

                            }
                            if (i != iSoLanMoLink - 1)
                            {
                                int x = 100;
                                driver.ExecuteScript("window.scrollTo(100," + (x * 2 + ")"));
                                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
                                Thread.Sleep(TimeSpan.FromSeconds(2));
                                //addStatus(index, "2s");
                                driver.ExecuteScript("window.scrollTo({ top: 0, behavior: 'smooth' });");

                                //addStatus(index, "Sleep 5s");
                                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5 + 2);
                                Thread.Sleep(TimeSpan.FromSeconds(5));
                                //addStatus(index, "Sleep 5s1");
                                try
                                {
                                    /* driver.FindElement(By.XPath("/html/body/ytd-app/div[1]/ytd-page-manager/ytd-watch-flexy/div[5]/div[1]/div/div[2]/ytd-watch-metadata/div/div[3]/div[1]/div/ytd-text-inline-expander/div[1]/span[1]/yt-formatted-string/a[1]")).Click();*/
                                    var eleDes = driver.FindElement(By.XPath("//div[@id='description']"));
                                    executorUseData.ExecuteScript("arguments[0].click()", eleDes);
                                    Thread.Sleep(2000);
                                    string contentDes = driver.FindElement(By.XPath("//div[@id='description']")).Text;
                                    string[] lines = contentDes.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                                    string linkDes = lines.FirstOrDefault(t => t.StartsWith("http", StringComparison.OrdinalIgnoreCase));

                                    if (string.IsNullOrWhiteSpace(linkDes))
                                    {
                                        iSoLuongDangChay--;
                                        //addStatus(index, "Xong");
                                        try
                                        {
                                            driver.Close();
                                            driver.Quit();
                                        }
                                        catch
                                        {
                                            driver.Navigate().GoToUrl(linkDes);
                                        }
                                    }
                                    else
                                    {
                                        //addStatus(index, "Chạy link ở des: " + i);
                                        driver.Navigate().GoToUrl(linkDes);
                                    }
                                }
                                catch (Exception e)
                                {
                                    //addStatus(index, "Sleep 5s1 ERROR" + e);
                                }
                                //addStatus(index, "Sleep 5s2");

                                //addStatus(index, "click description");
                                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5 + 2);
                                Thread.Sleep(TimeSpan.FromSeconds(5));
                            }
                        }
                        //addStatus(index, "Xong");
                        driver.Close();
                        driver.Dispose();
                        driver.Quit();
                    }
                    catch (Exception ex) // catch 1
                    {
                        //addStatus(index, "timeout access youtube ");
                        driver.Close();
                        driver.Dispose();
                        driver.Quit();
                        goto ketthuc;
                    }
                }
        }catch (Exception ex) // catch 1
            {
                //addStatus(index, ex.Message.ToString());
                goto ketthuc;
            }
        ketthuc:
            runNew(1);
            return;
        }

        private void kichban3()
        {
            int dem_kb3 = 0;
            int index = numberRow;
            numberRow += 1;
            if (index >= dataGrid.Rows.Count)
            {
                return;
            }
            if (!checkopentab)
            {
                return;
            }
            account act = lstAccount[index];

            UndetectChromeDriver driver = null;

            try
            {
                //addStatus(index, "starting");
                Console.WriteLine(APP_URL.Text);
                GPMLoginAPI api = new GPMLoginAPI("http://" + "127.0.0.1:19955");
                acton sts = new acton(act, api);
                
                Thread.Sleep(1000);
                JObject ojb = sts.getLst(act.email, profiles);
                string id_profile = "";
                if (ojb == null)
                {
                    int prox = proxyNumber;
                    loadProxy();
                    proxyNumber += 1;
                    if (prox >= lstProxy.Count)
                    {
                        proxyNumber = 0;
                        prox = proxyNumber;
                    }
                    string proxy = lstProxy[prox];
                    resave(proxy);
                    
                    ojb = api.Create(act.email, proxy, true);
                    if (ojb != null)
                    {
                        //Tạo thành công
                        id_profile = ojb["profile_id"].ToString();
                        //addStatus(index, "tạo profile thành công");
                        saveProfile(act, proxy);
                    }
                }
                else
                {
                    //đã có profile
                    id_profile = ojb["id"].ToString();
                }
                Thread.Sleep(1000);

                bool lockWasTaken = false;
                var temp = obj;
                try
                {
                    Monitor.Enter(temp, ref lockWasTaken);
                    //addStatus(index, "đang mở profile");
                    try { driver = sts.openProfile(id_profile, index); }
                    catch
                    {
                        //addStatus(index, "Lỗi mở profile");
                        saveError(act, "Lỗi mở profile");
                        goto ketthuc;
                    }
                }
                finally
                {
                    if (lockWasTaken)
                    {
                        Monitor.Exit(temp);
                    }
                }
                while (dem_kb3 < nbThread.Value - 1)
                {
                    dem_kb3++;
                    Thread.Sleep(2000);
                }
                //addStatus(index, "truy cập google");

                try { driver.Get(urlLogin); } 

                catch {
                    //addStatus(index, "Lỗi truy cập google !");
                    driver.Close();
                    driver.Dispose();
                    driver.Quit();
                    goto ketthuc;
                }
                Thread.Sleep(2000);
                
                if (driver.Url.Contains("business.google.com/create/new"))
                {
                    goto searchz;
                }
                login st = new login(driver, act); int demnha = 0;
                string Error = string.Empty;
            lainha:
                if (st.Nanial(urlLogin))
                {
                    if (!st.StartLogin(out Error))
                    {
                        if (Error == "captcha")
                        {
                            demnha += 1;
                            if (demnha == 7)
                            {
                                //addStatus(index, "Lỗi captcha !");
                                driver.Close();
                                driver.Dispose();
                                driver.Quit();
                                goto ketthuc;
                            }
                            goto lainha;
                        }

                        //Cảnh bảo lỗi
                    }
                }
                Thread.Sleep(TimeSpan.FromSeconds(3));

                //addStatus(index, "Đã login mail !");
                clickDeSau(driver); Thread.Sleep(TimeSpan.FromSeconds(5));
                if (driver.Url.Contains("inoptions/recovery-options-collection"))
                {
                    driver.Navigate().GoToUrl(urlLogin);
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                }
                if (driver.Url.Contains("om/signin/v2/challenge/iap") || (driver.Url.Contains("gle.com/signin/rejected")))
                {
                    saveError(act, "Very phone");
                    //addStatus(index, "very phone !");
                    driver.Close();
                    driver.Dispose();
                    driver.Quit();
                    goto ketthuc;
                }
                if (driver.Url.Contains(".com/interstitials/birthday") || (driver.Url.Contains("ogle.com/web/chip")) || (driver.Url.Contains("/info/unknownerror")))
                {
                    //addStatus(index, "Vui lòng thêm ngày sinh");
                    addbirthday(driver);
                }
                if ((driver.Url.Contains("m/signin/v2/identifier")) || (driver.Url.Contains("ccounts.google.com/speedbump/idvreenable")) || (driver.Url.Contains("m/signin/v2/disabled/explanation")))
                {
                    saveError(act, "Đăng nhập không thành công!");
                    ////addStatus(index, "Đăng nhập không thành công");
                    driver.Close();
                    driver.Dispose();
                    driver.Quit();
                    goto ketthuc;
                }
                if (driver.Url.Contains("ogle.com/web/chip"))
                {
                    addbirthday(driver);
                }
            searchz:
                if (driver.Url.Contains("/signin/v2/challenge/pwd"))
                {
                    saveError(act, "Đăng nhập không thành công");
                    //addStatus(index, "Đăng nhập không thành công");
                    driver.Close();
                    driver.Dispose();
                    driver.Quit();
                    goto ketthuc;
                }
                else
                {
                    acYoutube active = new acYoutube(driver);
                    active.gotoHome();
                    Thread.Sleep(2000);
                vireyt:
                    try
                    {
                        IJavaScriptExecutor executorUseData = driver;
                        string name = txtKeyword.Text;
                        //addStatus(index, "Tìm kiếm theo key " + name);
                        active.searchKeyword(name);
                        Thread.Sleep(TimeSpan.FromSeconds(4));

                        var items = driver.FindElements(By.XPath("//ytd-video-renderer//ytd-thumbnail//a"));
                        foreach (var item in items)
                        {
                            string urlVideo = item.GetAttribute("href");
                            if (urlVideo.Contains(strTenChannel))
                            {
                                executorUseData.ExecuteScript("arguments[0].click()", item);
                                break;
                            }
                        }
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                        countTime time = new countTime();

                        int timevideo = 0;
                        for (int i = 0; i < 300; i++)
                        {
                            if (i % 50 == 0)
                            {
                                driver.Navigate().Refresh();
                                Thread.Sleep(2000);
                            }
                            timevideo = active.getTimeVideo();
                            if (timevideo != 0)
                            {
                                break;
                            }
                            Thread.Sleep(1500);
                        }
                        ////addStatus(index, "Time Video " + timevideo);
                       
                        time.reset();
               
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                        Thread.Sleep(TimeSpan.FromSeconds(30));
                        //addStatus(index, "reset video time về 0");

                        Actions action = new Actions(driver);

                        action.KeyDown(OpenQA.Selenium.Keys.NumberPad0);

                        action.SendKeys(OpenQA.Selenium.Keys.NumberPad0).Perform();

                        int rand = random.Next(80, 99);
                        int sleepTime = rand * timevideo / 100;
                        //addStatus(index, "Chuyen video sau : " + sleepTime + " s");
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(12);
                        Thread.Sleep(TimeSpan.FromSeconds(10));

                        //Sleep 1min
                        //Scroll after 20min
                        if (sleepTime > 1 * 60)
                        {
                            int sleepCount = sleepTime / (60);
                            int sleepCountDiv = sleepTime % 60;
                            if (sleepCountDiv > 0)
                            {
                                sleepCount += 1;
                            }
                            for (int j = 0; j < sleepCount; j++)
                            {
                                if (j == sleepCount - 1)
                                {
                                    //addStatus(index, sleepCountDiv.ToString());
                                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(sleepTime + 2 - (j - 1) * 60);
                                    Thread.Sleep(TimeSpan.FromSeconds(sleepCountDiv));
                                }
                                else
                                {
                                    //addStatus(index, "60s");
                                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1 * 60);
                                    Thread.Sleep(TimeSpan.FromMinutes(1));
                                    if ((j + 1) % 20 == 0)
                                    {
                                        int x = 100;
                                        driver.ExecuteScript("window.scrollTo(100," + (x * j + ")"));
                                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
                                        Thread.Sleep(TimeSpan.FromSeconds(2));
                                        //addStatus(index, "2s");
                                        driver.ExecuteScript("window.scrollTo({ top: 0, behavior: 'smooth' });");

                                    }

                                }
                            }
                        }
                        else
                        {
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(sleepTime + 2);
                            Thread.Sleep(TimeSpan.FromSeconds(sleepTime));

                        }
                        Thread.Sleep(5000);
                        /* driver.FindElement(By.XPath("/html/body/ytd-app/div[1]/ytd-page-manager/ytd-watch-flexy/div[5]/div[1]/div/div[2]/ytd-watch-metadata/div/div[3]/div[1]/div/ytd-text-inline-expander/div[1]/span[1]/yt-formatted-string/a[1]")).Click();*/

                        var eleDes = driver.FindElement(By.XPath("//div[@id='description']"));
                        executorUseData.ExecuteScript("arguments[0].click()", eleDes);
                        Thread.Sleep(2000);
                        string contentDes = driver.FindElement(By.XPath("//div[@id='description']")).Text;
                        string[] lines = contentDes.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                        string linkDes = lines.FirstOrDefault(t => t.StartsWith("http", StringComparison.OrdinalIgnoreCase));

                        if (string.IsNullOrWhiteSpace(linkDes))
                        {
                            iSoLuongDangChay--;
                            //addStatus(index, "Xong");
                            try
                            {
                                driver.Close();
                                driver.Quit();
                                driver.Dispose();
                            }
                            catch
                            {
                                driver.Navigate().GoToUrl(linkDes);
                            }
                        }
                        else
                        {
                            //addStatus(index, "Chuyển sang click PLL");
                            driver.Navigate().GoToUrl(linkDes);
                        }
                        
                        Thread.Sleep(10000);
                        // xem pll 
                        int videoNext = 0;

                        for (int i = 0; i < 1000; i++)
                        {
                            bool is_comment = false;
                            videoNext += 1;

                            for (int k = 0; k < 300; k++)
                            {
                                if (k % 50 == 0)
                                {
                                    driver.Navigate().Refresh();
                                    Thread.Sleep(2000);
                                }
                                timevideo = active.getTimeVideo();
                                if (timevideo != 0)
                                {
                                    break;
                                }
                            } 
                                int rand_video_next = random.Next(3, 8);
                            if (videoNext == rand_video_next)
                            {
                                if (sub.Checked)
                                {
                                    try { active.subVideo(); }
                                    catch
                                    {
                                        continue;
                                    }
                                }
                            }
                            //addStatus(index, "Reset video ve 0");
                            // Khởi tạo đối tượng thuộc Actions class
                            action = new Actions(driver);

                            action.KeyDown(OpenQA.Selenium.Keys.NumberPad0);

                            action.SendKeys(OpenQA.Selenium.Keys.NumberPad0).Perform();
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(6);
                            Thread.Sleep(TimeSpan.FromSeconds(5));

                            rand = random.Next(80, 99);

                            sleepTime = rand * timevideo / 100;
                            ////addStatus(index, "Chuyen video sau : " + sleepTime + " s");
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(12);
                            Thread.Sleep(TimeSpan.FromSeconds(10));

                            //Sleep 1min
                            //Scroll after 20min
                            if (sleepTime > 1 * 60)
                            {
                                int sleepCount = sleepTime / (60);
                                int sleepCountDiv = sleepTime % 60;
                                if (sleepCountDiv > 0)
                                {
                                    sleepCount += 1;
                                }
                                int rand_cmt_time = random.Next(2, sleepCount);
                                for (int j = 0; j < sleepCount; j++)
                                {
                                    if (j == sleepCount - 1)
                                    {
                                        ///addStatus(index, sleepCountDiv.ToString());
                                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(sleepTime + 2 - (j - 1) * 60);
                                        Thread.Sleep(TimeSpan.FromSeconds(sleepCountDiv));
                                    }
                                    else
                                    {
                                        //addStatus(index, "60s");
                                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1 * 60);
                                        Thread.Sleep(TimeSpan.FromMinutes(1));
                                        if ((j + 1) % 20 == 0)
                                        {
                                            int x = 100;
                                            driver.ExecuteScript("window.scrollTo(100," + (x * j + ")"));
                                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
                                            Thread.Sleep(TimeSpan.FromSeconds(2));
                                            //addStatus(index, "2s");
                                            driver.ExecuteScript("window.scrollTo({ top: 0, behavior: 'smooth' });");
                                        }
                                        if (is_comment == false)
                                        {
                                            int rand_video_cmt = random.Next(2, 6);
                                            Thread.Sleep(10000);
                                            if (videoNext % rand_video_cmt == 0)
                                            {
                                                if (j == rand_cmt_time)
                                                {
                                                    if (listComments.Count > 0)
                                                    {
                                                        try
                                                        {
                                                            int randomCommend = random.Next(listComments.Count);
                                                            active.goToComment(listComments[randomCommend]);
                                                            is_comment = true;
                                                            Thread.Sleep(6000);
                                                        }
                                                        catch
                                                        {
                                                            continue;
                                                        }
                                                    }
                                                }

                                            }

                                        }
                                    }
                                }
                            }
                            else
                            {
                                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(sleepTime + 2);
                                Thread.Sleep(TimeSpan.FromSeconds(sleepTime));
                            }
                            int tr = random.Next(0, 2);
                            if (videoNext == 1)
                            {
                                tr = 1;
                            }
                            if (tr == 0)
                            {
                                try { driver.ExecuteScript("document.getElementsByClassName('ytp-prev-button ytp-button')[0].click()"); }
                                catch
                                {
                                    Thread.Sleep(1000);
                                }
                                //Back
                            }
                            else
                            {
                                //forword
                                try { driver.ExecuteScript("document.getElementsByClassName('ytp-next-button ytp-button')[0].click()"); }
                                catch
                                {
                                    Thread.Sleep(1000);
                                }
                            }
                            Thread.Sleep(10000);
                            if (i == 999)
                            {
                                //addStatus(index, "xem hết pll");
                                driver.Close();
                                driver.Dispose();
                                driver.Quit();
                                return;
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        //addStatus(index, "error time out access youtube");
                        driver.Close();
                        driver.Dispose();
                        driver.Quit();
                        goto ketthuc;
                        
                    }
                }

            }
            catch (Exception ex)
            {
                //addStatus(index, ex.Message.ToString());
                goto ketthuc;
            }
        ketthuc:
            runNew(3);
            return;
        }
        void saveProfile(account taikhoan, string proxy = "")
        {
            while (true)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(proxy))
                    {
                        File.AppendAllText("data\\info_profile.txt", taikhoan.email + "|" + taikhoan.password + "|" + taikhoan.mail_kp + "\r\n");
                    }
                    else
                    {
                        File.AppendAllText("data\\info_profile.txt", taikhoan.email + "|" + taikhoan.password + "|" + taikhoan.mail_kp + "|" + proxy + "\r\n");
                    }
                    break;
                }
                catch
                {
                    Thread.Sleep(100);
                }
            }
        }
        void runNew(int indexKichBan)
        {
            Thread st = new Thread(() =>
            {
                run(indexKichBan);
            });
            st.IsBackground = true;
            st.Start();
            lsThread.Add(st);
        }
        void clickGotit(UndetectChromeDriver driver)
        {
            try { driver.FindElement(By.XPath("//yt-upsell-dialog-renderer//tp-yt-paper-button//yt-formatted-string")).Click(); } catch { }
        }
        int countTimeAll = 0;
        void addLink(string link, int time)
        {

        }
        void save()
        {

        }
        string ConvertListToString()
        {
            string data = "";
            foreach (var item in lstLink.ToList())
            {
                data += item.link_yt + "|" + item.count + "\r\n";
            }
            return data;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {

                var lines = File.ReadAllLines("config\\config.txt");

            }
            catch
            {
                MessageBox.Show("Config file not found!");
            }
        }
        void savep()
        {
            try { File.WriteAllText("data\\phut.txt", countTimeAll.ToString()); } catch { Thread.Sleep(100); }
        }
        List<link> lstLink;

        void clearchrome()
        {

           Process[] chromeDriverProcesses = Process.GetProcessesByName("gpmdriver.exe");
            foreach (var chromeDriverProcess in chromeDriverProcesses)
            {
                try { chromeDriverProcess.Kill(); } catch { }
            }
            Process[] chromed = Process.GetProcessesByName("chrome");
            foreach (var chrome in chromed)
            {
                try { chrome.Kill(); } catch { }
            }

            Process.Start("taskkill", "/F /IM gpmdriver.exe");
            Process.Start("taskkill", "/F /IM chromedriver.exe");

            flag_view_dx = false;
            flag_view_dx = false;
            flag_view_dx = false;
        }
        bool checkopentab = true;

        private int iSoLuongEmail;
        private int iSoLuong;
        private int iSoLuongDangChay;
        private string strKeyWork;
        private string strTenChannel;
        private string strChannelID;
        private int iSoLanMoLink = 0;


        private void btnSelectCommentFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text file (*.txt)|*.txt";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    listComments = File.ReadAllLines(openFileDialog.FileName).ToList();
                }
            }
        }
        

        private void btnStop_Click_1(object sender, EventArgs e)
        {
            Common.Run_Flag = true;
            clearchrome();
            foreach(Thread t in lsThread)
            {
                t.Abort();
            }
            lsThread = new List<Thread>();
        }

        bool flag_view_dx = false;
        bool flag_view_pll = false;
        bool flag_viewPll_video = false;
        private void btnViewDX_CheckedChanged(object sender, EventArgs e)
        {
            flag_view_dx = true;
        }

        private void btnPLL_CheckedChanged_1(object sender, EventArgs e)
        {
            flag_view_pll = true;
        }

        private void btnPLL_Video_CheckedChanged(object sender, EventArgs e)
        {
            flag_viewPll_video = true;
        }

        private void btnWait_Click(object sender, EventArgs e)
        {
            checkopentab = !checkopentab;
        }




        public void ScriptAll()
        {

        }

















        /// <summary>
        ///  YÊu cầu nhập đầy đủ thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click_1(object sender, EventArgs e)
        {
            
            // Dừng nếu không nhập đủ thông tin
            if (listIDAndKey.Count == 0 || lstAccount.Count == 0 || listComments.Count == 0 || listKeywordForSuggest.Count == 0 || txtChannelName.Text.Trim() == "" || txtChannelID.Text.Trim() == "" || apiUrl.Text.Trim() == "")
            {
                MessageBox.Show("Nhập đầy đủ thông tin");
                return;
            }

            getTypeView();
            limitView = random.Next(Convert.ToInt32(numForStart.Value), Convert.ToInt32(numForEnd.Value));
            Common.Run_Flag = true;

            listChannel = new List<string>();
            listChannel.Add(txtChannelID.Text.Trim());
            // Điều chỉnh số luồng phù hợp với số lượng emails hiện có.
            iSoLuongDangChay = 0;
            iSoLuongEmail = dataGrid.Rows.Count;

            if (iSoLuongEmail == 0)
            {
                return;
            }
            if (iSoLuongEmail < nbThread.Value)
            {
                nbThread.Value = iSoLuongEmail;
            }
            iSoLuong = (int)nbThread.Value;
            
            // Khởi tạo số lượng phần tử đang chạy.
            listRunning = Enumerable.Repeat(false, iSoLuongEmail).ToList();
            listOfPort = Enumerable.Repeat(0, iSoLuongEmail).ToList();

            // Lấy giá trị từ phía giao diện
            strKeyWork = txtKeyword.Text.Trim();
            strTenChannel = txtChannelName.Text.Trim();
            strChannelID = txtChannelID.Text.Trim();

            // Thực hiện gọi luồng.
            Thread st = new Thread(() =>
            {
                Create_Thread();
            });
            st.IsBackground = true;
            st.Start();
            lsThread.Add(st);

            
        }

        public void getTypeView()
        {
            listRandomView = new List<int>();
            if (ckbHome.Checked)
            {
                listRandomView.Add(0);
            }
            if (ckbSearch.Checked)
            {
                listRandomView.Add(1);
            }
            if (ckbSuggest.Checked)
            {
                listRandomView.Add(2);
            }

            if (ckbRepeat.Checked)
            {
                repeat = true;
            }
            else
            {
                repeat = false;
            }
        }



        /// <summary>
        ///  Khởi tạo luồng
        /// </summary>
        public void Create_Thread()
        {
            // Lấy danh sách profile hiện có
            GPMLoginAPI api = new GPMLoginAPI("http://" + apiUrl.Text.Trim());
            profiles = api.GetProfiles();
            if (profiles.Count == 0)
            {
                return;
            }


            // Thực hiện vòng lặp call 
            for (int i = 0; i < nbThread.Value; i++)
            {
                Thread st = new Thread(() =>
                {
                    loopScript();
                });
                st.IsBackground = true;
                st.Start();
                lsThread.Add(st);
                Thread.Sleep(2000);
            }
        }


        /// <summary>
        ///  Thực hiện vòng lặp khi một account đc tạo xong.
        ///  // Nếu mà lặp thì thực hiện lặp còn nếu không lặp thì thực hiện không lặp
        /// </summary>
        public void loopScript()
        {
            while (true)
            {
                int index = 0;
                if (Common.Run_Flag == false)
                {
                    return;
                }
                if (repeat)
                {
                    if (numberRow >= iSoLuongEmail)
                    {
                        numberRow = 0;
                    }
                    
                    index = checkIndex();
                    numberRow += 1;
                }
                else
                {
                    if(numberRow >= iSoLuongEmail)
                    {
                        return;
                    }
                
                    index = numberRow;
                    numberRow += 1;
                }
                    
                ScriptForAll(index);
            }
        }


        /// <summary>
        ///  Lấy ra những Account đang chưa  chạy!
        /// </summary>
        /// <returns></returns>
        public int checkIndex()
        {
            for(int i =0; i< (dataGrid.Rows.Count*2); i ++)
            {
                if (Common.Run_Flag == false)
                {
                    return 0;
                }
                if (listRunning[numberRow] == false)
                {
                    return numberRow;
                }
                numberRow = numberRow +1 >= dataGrid.Rows.Count ? 0: numberRow + 1;
            }
            return 0;
        }

        /// <summary>
        ///  Cập nhật trạng thái cho giao diện
        ///  0: là tốt
        ///  1: cảnh báo:
        ///  2: là lỗi
        /// </summary>
        /// <param name="index"></param>
        /// <param name="Text"></param>
        void addStatus(int index, string Text, int type)
        {
            try
            {
                switch (type)
                {
                    case 0:
                        dataGrid.Rows[index].Cells["status"].Style.ForeColor = Color.Green;
                        break;
                    case 1:
                        dataGrid.Rows[index].Cells["status"].Style.ForeColor = Color.Yellow;
                        break;
                    case 2:
                        dataGrid.Rows[index].Cells["status"].Style.ForeColor = Color.Red;
                        break;
                    default: break;

                }
                dataGrid.Rows[index].Cells["status"].Value = Text;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
           
        }


        /// <summary>
        ///  Thực hiện kịch bản cho tất cả
        /// </summary>
        public void ScriptForAll(int ind)
        {
            int index = ind;
            listRunning[index] = true;
            bool runok = false;
            
            try
            {
                // Lấy ra index của hàng mình muốn chạy
                

                // Lấy ra tài khoản muốn chạy
                account account = lstAccount[index]; // Lấy ra account
                

                addStatus(index, "Lấy thông tin tài khoản!", 0);
                string profileID = "";
                JObject ojb = AccountHelper.GetAccountFromList(account.email, profiles);
                if (ojb == null)
                {
                    addStatus(index, "Tài khoản chưa được khởi tạo!", 1);
                    return;
                }
                else
                {
                    addStatus(index, "Đã tìm thấy tài khoản!", 0);
                    profileID = ojb["id"].ToString();
                }
                Thread.Sleep(1000);
                addStatus(index, "Mở chrome browser!", 0);
                Thread.Sleep(1000);
                UndetectChromeDriver driver = null;
                ChromeDriverHelper chromeHelper = new ChromeDriverHelper();
                JObject ob = null;
                
                try
                {
                    GPMLoginAPI api = new GPMLoginAPI("http://" + apiUrl.Text.Trim());
                    ob = chromeHelper.startChrome(api, profileID,listOfPort[index]);
                    if (ob == null)
                    {
                        addStatus(index, "Không thể mở chrome profile từ GPM!", 2);
                        return; // Không mở đc thì return luôn
                    }
                    
                    
                    driver = chromeHelper.initDriver(ob);
                    if (driver == null)
                    {
                        addStatus(index, "Khởi tạo ChromeDriver thất bại!", 2);
                        goto endProcess; // trong trường hợp mà không kết nối đc driver thì gọi endprocess để đóng hết những PID đang mở cổng tương ứng
                    }
                    addStatus(index, "Lấy Process trình duyệt!", 0);
                    bool getPro = chromeHelper.GetProces(driver);
                    if (!getPro)
                    {
                        addStatus(index, "Không thể lấy Process trình duyệt!", 2);
                        goto endProcess; // Không lấy đc process thì đóng hết các cổng tương ứng
                    }
                    addStatus(index, "Thay đổi kích thước trình duyệt!", 0);
                    if (!ChangePositionAndSize(driver, index))
                    {
                        addStatus(index, "Thay đổi kích thước Thất bại!", 2);
                        goto endProcess; // 
                    };
                    Thread.Sleep(2000);
                    runok = true;
                    countThread += 1;

                    while (countThread < iSoLuong)
                    {
                        Thread.Sleep(3000);
                    }

                    Thread.Sleep(TimeSpan.FromSeconds(random.Next(5, 20)));

                    addStatus(index, "login Email!", 0);
                    // Triểm tra xem tài khoản đã được login hay chưa
                    LoginAutomation login = new LoginAutomation(driver);
                    if (!login.Login(account.email, account.password, account.mail_kp))
                    {
                        addStatus(index, "Login không thành công!", 2);
                        goto endProcess;
                    }
                    addStatus(index, "Email đã login!", 0);

                    Thread.Sleep(TimeSpan.FromSeconds(2));

                    addStatus(index, "Try cập youtube!", 0);
                    // Bắt đầu thực hiện chạy view youtube!
                    driver.Get("https://www.youtube.com/");
                    Thread.Sleep(TimeSpan.FromSeconds(random.Next(8, 15)));

                    SearchAutomation search = new SearchAutomation(driver, index);
                    search.AddStatus += new SearchAutomation.CallbackEventHandler(addStatus);
                    VideoAutomation video = new VideoAutomation(driver, index);
                    video.AddStatus += new VideoAutomation.CallbackEventHandler(addStatus);
                    HomeAutomation home = new HomeAutomation(driver, listChannel, index);
                    home.AddStatus += new HomeAutomation.CallbackEventHandler(addStatus);
                    SuggestAutomation suggest = new SuggestAutomation(driver, index);
                    suggest.AddStatus += new SuggestAutomation.CallbackEventHandler(addStatus);
                    int numOfSearches = 1;
                    // Thực hiện tìm kiếm với các trường hợp khác nhau.
                    while (true)
                    {
                        addStatus(index, "Tìm kiếm lần đầu!", 0);
                        int idexSearch = random.Next(0, listIDAndKey.Count);
                        string[] idandKey = listIDAndKey[idexSearch].ToString().Split(';');
                        if (!search.Run(0, idandKey[1], idandKey[0]))
                        {
                            addStatus(index, "Tìm kiếm lại lần " + numOfSearches + "!", 0);
                            numOfSearches += 1;
                        }
                        else
                        {

                            Thread.Sleep(3000);
                            addStatus(index, "Bắt đầu xem video!", 0);
                            video._Comment = listComments[random.Next(0, listComments.Count)].ToString();
                            video._from = (int)numFrom.Value;
                            video._to = (int)numFrom.Value;
                            video._isLike = true;
                            video._isComment = true;
                            video._waitTimeEnd = (int)waitTimeEnd.Value;
                            video._waitTimeStart = (int)waitTimeStart.Value;
                            video.Run();
                            addStatus(index, "Đã xem xong video!", 0);
                            Thread.Sleep(3000);
                            break;
                        }

                    }
                    addStatus(index, "Ngẫu nhiên View!", 0);

                    // Bắt đầu thực hiện Random ngẫu nhiên video
                    int countLimite = 0;

                    while (true)
                    {
                        if(countLimite >= limitView)
                        {
                            break;
                        }
                        countLimite += 1;


                        int iView = random.Next(0,listRandomView.Count);
                        int idexSearch = random.Next(0, listIDAndKey.Count);
                        switch (iView)
                        {
                            case 0:
                                addStatus(index, "Đang chạy view Trang chủ!", 0);
                                home.Run();
                                break;
                            case 1:
                                int idexSearch1 = random.Next(0, listIDAndKey.Count);
                                string[] idandKey1 = listIDAndKey[idexSearch].ToString().Split(';');
                                addStatus(index, "Đang chạy view tìm kiếm!", 0);
                                search.Run(1, idandKey1[1], idandKey1[0]);
                                break;
                            case 2:
                                addStatus(index, "Đang chạy view đề xuất!", 0);
                                int idexSearch2 = random.Next(0, listIDAndKey.Count);
                                string[] idandKey2 = listIDAndKey[idexSearch].ToString().Split(';');
                                suggest.run(txtChannelName.Text.Trim(), txtChannelID.Text.Trim(), listKeywordForSuggest[random.Next(0, listKeywordForSuggest.Count)], idandKey2[0]);
                                break;
                            default:
                                break;

                        }
                        Thread.Sleep(3000);
                        addStatus(index, "Bắt đầu xem video!", 0);
                        video._Comment = listComments[random.Next(0, listComments.Count)].ToString();
                        video._from = (int)numFrom.Value;
                        video._to = (int)numFrom.Value;
                        video._isLike = true;
                        video._isComment = true;
                        video.Run();
                        addStatus(index, "Đã xem xong video!", 0);
                        Thread.Sleep(2000);



                    }


                endProcess:
                    
                    try
                    {
                        addStatus(index, "Đóng Chromedriver!", 0);
                        driver.Close();
                        driver.Quit();
                        listOfPort[index] = 0;
                    }
                    catch
                    {
                        addStatus(index, "Đóng Chromedriver thất bại!", 2);
                    }

                    // Kiểm tra xem browser đã đóng chưa. nếu chưa đóng thì thực hiện đóng.
                    try
                    {

                        if (chromeHelper.process == null)
                        {
                            addStatus(index, "Đóng tất cả PID từ port !", 0);
                            killAllPID(Convert.ToString(ob["selenium_remote_debug_address"]).Trim());
                        }
                        else
                        {
                            if (!chromeHelper.process.HasExited)
                            {
                                listOfPort[index] = Convert.ToInt32(ob["selenium_remote_debug_address"].ToString().Trim().Split(':')[1]);
                            }
                        }

                    }
                    catch
                    {
                        addStatus(index, "Đóng PID/Process thất bại !", 2);
                    }

                    addStatus(index, "Đã Đóng chrome!!", 0);
                    Thread.Sleep(1000);

                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    try
                    {
                        addStatus(index, "Đóng Chromedriver!", 0);
                        driver.Close();
                        driver.Quit();
                        listOfPort[index] = 0;
                    }
                    catch
                    {
                        addStatus(index, "Đóng Chromedriver thất bại!", 2);
                    }

                    // Kiểm tra xem browser đã đóng chưa. nếu chưa đóng thì thực hiện đóng.
                    try
                    {

                        if (chromeHelper.process == null)
                        {
                            addStatus(index, "Đóng tất cả PID từ port !", 0);
                            killAllPID(Convert.ToString(ob["selenium_remote_debug_address"]).Trim());
                        }
                        else
                        {
                            if (!chromeHelper.process.HasExited)
                            {
                                listOfPort[index] = Convert.ToInt32(ob["selenium_remote_debug_address"].ToString().Trim().Split(':')[1]);
                            }
                        }

                    }
                    catch
                    {
                        addStatus(index, "Đóng PID/Process thất bại !", 2);
                    }
                }
            }
            catch
            {
            }
            listRunning[index] = false;
            if(runok)
            {
                countThread -= 1;
            }
            


        }



        /// <summary>
        ///  Kill toàn bộ PID đang hoạt động với Port
        /// </summary>
        /// <param name="port"></param>
        public void killAllPID(string port)
        {
            List<string> listPID = getPID(port);
            for(int i =0; i< listPID.Count; i ++)
            {
                killPID(listPID[i]);
            }
        }



        /// <summary>
        ///  Lấy toàn bộ PID đang sử dụng Port để thực hiện đóng thông qua PID
        /// </summary>
        /// <returns></returns>
        public static List<string> getPID(string port)
        {
            List<string> listPID = new List<string>();
            try
            {
                using (Process p = new Process())
                {
                    ProcessStartInfo ps = new ProcessStartInfo();
                    ps.Arguments = "/C netstat -ano | findStr "+port;
                    ps.FileName = "cmd.exe";
                    ps.UseShellExecute = false;
                    ps.WindowStyle = ProcessWindowStyle.Hidden;
                    ps.RedirectStandardInput = true;
                    ps.RedirectStandardOutput = true;
                    ps.RedirectStandardError = true;
                    p.StartInfo = ps;
                    p.Start();

                    StreamReader stdOutput = p.StandardOutput;
                    StreamReader stdError = p.StandardError;

                    string content = stdOutput.ReadToEnd() + stdError.ReadToEnd();
                    string exitStatus = p.ExitCode.ToString();

                    string[] rows = Regex.Split(content, "\r\n");
                    foreach (string row in rows)
                    {
                        if (row.Trim() != "")
                        {
                            string[] tokens = Regex.Split(row.Trim(), "\\s+");
                            if (!listPID.Contains(tokens[tokens.Length - 1].Trim()))
                            {
                                listPID.Add(tokens[tokens.Length - 1].Trim());
                            }


                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return listPID;
        }



        /// <summary>
        ///  Lấy toàn bộ PID đang sử dụng Port để thực hiện đóng thông qua PID
        /// </summary>
        /// <returns></returns>
        public void killPID(string pid)
        {
            try
            {
                using (Process p = new Process())
                {
                    ProcessStartInfo ps = new ProcessStartInfo();
                    ps.Arguments = "/C taskkill /F /PID " + pid;
                    ps.FileName = "cmd.exe";
                    ps.UseShellExecute = false;
                    ps.WindowStyle = ProcessWindowStyle.Hidden;
                    ps.RedirectStandardInput = true;
                    ps.RedirectStandardOutput = true;
                    ps.RedirectStandardError = true;
                    p.StartInfo = ps;
                    p.Start();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }








        /// <summary>
        ///  Thay đổi kích thước trình duyệt
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="index"></param>
        public bool ChangePositionAndSize(UndetectChromeDriver driver, int index)
        {
            try
            {
                int z_index = index % 300;
                driver.Manage().Window.Position = new Point(50 * Convert.ToInt32(z_index / 25), 35 * Convert.ToInt32(z_index % 25));
                Thread.Sleep(TimeSpan.FromSeconds(1));
                driver.Manage().Window.Size = new Size(1200, 800);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }




        private void sub_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void OpenComment_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            if (ofd.FileName.Length > 0)
            {
               
                listComments = File.ReadAllLines(ofd.FileName).ToList();
                richComments.Lines = listComments.ToArray();

                //int num = 0;
                //foreach (var line in lines)
                //{

                //    Listcomments.Add(line);
                //}
            }
           
        }

        private void btnIDandSearch_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            if (ofd.FileName.Length > 0)
            {

                listIDAndKey = File.ReadAllLines(ofd.FileName).ToList();
                richIDAndSearch.Lines = listIDAndKey.ToArray();

                //int num = 0;
                //foreach (var line in lines)
                //{

                //    Listcomments.Add(line);
                //}
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            if (ofd.FileName.Length > 0)
            {

                listKeywordForSuggest = File.ReadAllLines(ofd.FileName).ToList();
                richKeySuggest.Lines = listKeywordForSuggest.ToArray();

                //int num = 0;
                //foreach (var line in lines)
                //{

                //    Listcomments.Add(line);
                //}
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }  
    }
