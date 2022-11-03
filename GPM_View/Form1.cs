﻿using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace GPM_View
{
    public partial class Form1 : Form
    {
        List<string> listComments;
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        List<account> lstAccount;
        List<string> lstProxy;
        int proxyNumber = 0;
        int numberRow = 0;
        Random random = new Random();
        private void btnEmail_Click(object sender, EventArgs e)
        {
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
        void createThread(int indexKichBan)
        {
            for (int i = 0; i < nbThread.Value; i++)
            {
                Thread st = new Thread(() =>
                {
                    run(indexKichBan);
                });
                st.Start();
                Thread.Sleep(500);
            }
        }
        void addStatus(int index, string Text)
        {
            dataGrid.Rows[index].Cells["status"].Value = Text;
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
                //ZFr60d CeoRYc
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
            try
            {
                addStatus(index, "------------starting-----------");
                GPMLoginAPI api = new GPMLoginAPI("http://" + APP_URL.Text);
                acton sts = new acton(act, api);
                JObject ojb = sts.getLst(act.email);
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
                        addStatus(index, "tạo profile thành công");
                        saveProfile(act, proxy);
                    }
                }
                else
                {
                    //đã có profile
                    id_profile = ojb["id"].ToString();
                }
                UndetectChromeDriver driver = null;
                try { driver = sts.openProfile(id_profile, index); }
                catch
                {
                   
                    addStatus(index, "Lỗi mở profile");
                    saveError(act, "Lỗi mở profile");
                    goto ketthuc;
            
                    
                }
                try { driver.Get(urlLogin); } catch { }
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
                                addStatus(index, "Lỗi captcha !");
                                driver.Quit();
                                driver.Close();
                                goto ketthuc;
                            }
                            goto lainha;
                        }

                        //Cảnh bảo lỗi
                    }
                }
                Thread.Sleep(TimeSpan.FromSeconds(3));
                //create account 
                addStatus(index, "Đã login mail !");
                clickDeSau(driver); Thread.Sleep(TimeSpan.FromSeconds(5));
                if (driver.Url.Contains("inoptions/recovery-options-collection"))
                {
                    driver.Navigate().GoToUrl(urlLogin);
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                }
                if (driver.Url.Contains("om/signin/v2/challenge/iap") || (driver.Url.Contains("gle.com/signin/rejected")))
                {
                    saveError(act, "Very phone");
                    addStatus(index, "very phone !");
                    driver.Quit();
                    driver.Close();
                    goto ketthuc;
                }
                if (driver.Url.Contains(".com/interstitials/birthday") || (driver.Url.Contains("ogle.com/web/chip")) || (driver.Url.Contains("/info/unknownerror")))
                {
                    addStatus(index, "Vui lòng thêm ngày sinh");
                    addbirthday(driver);
                }
                if ((driver.Url.Contains("m/signin/v2/identifier")) || (driver.Url.Contains("ccounts.google.com/speedbump/idvreenable")) || (driver.Url.Contains("m/signin/v2/disabled/explanation")))
                {
                    saveError(act, "Đăng nhập không thành công!");
                    addStatus(index, "Đăng nhập không thành công");
                    driver.Quit();
                    driver.Close();
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
                    addStatus(index, "Đăng nhập không thành công");
                    driver.Quit();
                    driver.Close();
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
                        addStatus(index, "Tìm kiếm theo key " + name);
                        active.searchKeyword(name);
                        Thread.Sleep(TimeSpan.FromSeconds(4));
                        //search
                        strKeyWork = Regex.Replace(strKeyWork, @"[^a-zA-Z0-9]", string.Empty);

                        var eleVideos = driver.FindElements(By.XPath("//div[@class='text-wrapper style-scope ytd-video-renderer']"));
                        for (int i = 0; i < eleVideos.Count; i++)
                        {
                            string data = eleVideos[i].Text;
                            string[] lines = data.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                            if (lines.Length < 4)
                            {
                                continue;
                            }
                            else
                            {
                                string strResult = Regex.Replace(lines[0], @"[^a-zA-Z0-9]", string.Empty);
                                if (strResult.Contains(strKeyWork) == false)
                                {
                                    continue;
                                }
                                if (lines[3].Contains(strTenChannel) == false)
                                {
                                    continue;
                                }
                            }

                            executorUseData.ExecuteScript("arguments[0].click()", eleVideos[i]);
                            

                            break;
                        }
                        
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

                        addStatus(index, "Reset video ve 0");
                        // Khởi tạo đối tượng thuộc Actions class
                        Actions action = new Actions(driver);

                        action.KeyDown(OpenQA.Selenium.Keys.NumberPad0);

                        action.SendKeys(OpenQA.Selenium.Keys.NumberPad0).Perform();
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(6);
                        Thread.Sleep(TimeSpan.FromSeconds(5));

                        int rand = random.Next(80, 99);

                        int sleepTime = rand * timevideo / 100;
                        addStatus(index, "Chuyen video sau : " + sleepTime + " s");
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
                                    addStatus(index, sleepCountDiv.ToString());
                                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(sleepTime + 2 - (j - 1) * 60);
                                    Thread.Sleep(TimeSpan.FromSeconds(sleepCountDiv));
                                }
                                else
                                {
                                    addStatus(index, "60s");
                                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1 * 60);
                                    Thread.Sleep(TimeSpan.FromMinutes(1));
                                    if ((j + 1) % 20 == 0)
                                    {
                                        int x = 100;
                                        driver.ExecuteScript("window.scrollTo(100," + (x * j + ")"));
                                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
                                        Thread.Sleep(TimeSpan.FromSeconds(2));
                                        addStatus(index, "2s");
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

                        addStatus(index, "Click Tim PLL");

                        driver.FindElement(By.XPath("//a[@class='yt-simple-endpoint style-scope ytd-video-owner-renderer']")).Click();

                        Thread.Sleep(10000);
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
                    lopPLL:
                        while (true)
                        {
                            videoNext += 1;
                            if (videoNext == 3)
                            {
                                if (sub.Checked)
                                {
                                    active.subVideo();
                                    
                                    
                                    Thread.Sleep(5000);
                                }
                            }
                            if (videoNext % 2 == 0)
                            {
                                active.likeVideo();
                                // comment
                                Thread.Sleep(6000);
                            }
                            Thread.Sleep(5000);

                            addStatus(index, "Reset video ve 0");
                            // Khởi tạo đối tượng thuộc Actions class
                            action = new Actions(driver);

                            action.KeyDown(OpenQA.Selenium.Keys.NumberPad0);

                            action.SendKeys(OpenQA.Selenium.Keys.NumberPad0).Perform();
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(6);
                            Thread.Sleep(TimeSpan.FromSeconds(5));

                            rand = random.Next(80, 99);

                            sleepTime = rand * timevideo / 100;
                            addStatus(index, "Chuyen video sau : " + sleepTime + " s");
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
                                        addStatus(index, sleepCountDiv.ToString());
                                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(sleepTime + 2 - (j - 1) * 60);
                                        Thread.Sleep(TimeSpan.FromSeconds(sleepCountDiv));
                                    }
                                    else
                                    {
                                        addStatus(index, "60s");
                                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1 * 60);
                                        Thread.Sleep(TimeSpan.FromMinutes(1));
                                        if ((j + 1) % 20 == 0)
                                        {
                                            int x = 100;
                                            driver.ExecuteScript("window.scrollTo(100," + (x * j + ")"));
                                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
                                            Thread.Sleep(TimeSpan.FromSeconds(2));
                                            addStatus(index, "2s");
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
                            goto lopPLL;
                            try
                            {
                                driver.Close();
                                driver.Quit();
                            }
                            catch (Exception e)
                            {
                                addStatus(index, e.Message);
                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {

                        if (ex.Message.ToString().Contains(" timed out after 60"))
                        {
                            goto vireyt;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                addStatus(index, ex.Message.ToString());
            }
        ketthuc:
            runNew(2);
            return;

        }
        int temp = 1;
        void run(int indexKichBan)
        {
            temp = indexKichBan;
            if (indexKichBan == 2)
            {
                kichban2();
            }
            else if(indexKichBan == 1)
            {
                kichban1();
            }
            else
            {
                kichban3();
            }
        }

        private void kichban1()
        {
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
            try
            {
                addStatus(index, "------------starting-----------");
                GPMLoginAPI api = new GPMLoginAPI("http://" + APP_URL.Text);
                acton sts = new acton(act, api);
                JObject ojb = sts.getLst(act.email);
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
                        addStatus(index, "tạo profile thành công");
                        saveProfile(act, proxy);
                    }
                }
                else
                {
                    //đã có profile
                    id_profile = ojb["id"].ToString();
                }
                UndetectChromeDriver driver = null;
                try { driver = sts.openProfile(id_profile, index); }
                catch
                {
                    addStatus(index, "Lỗi mở profile");
                    saveError(act, "Lỗi mở profile");
                    goto ketthuc;
                }
                try { driver.Get(urlLogin); } catch { }
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
                                addStatus(index, "Lỗi captcha !");
                                driver.Quit();
                                driver.Close();
                                goto ketthuc;
                            }
                            goto lainha;
                        }

                        //Cảnh bảo lỗi
                    }
                }
                Thread.Sleep(TimeSpan.FromSeconds(3));

                addStatus(index, "Đã login mail !");
                clickDeSau(driver); Thread.Sleep(TimeSpan.FromSeconds(5));
                if (driver.Url.Contains("inoptions/recovery-options-collection"))
                {
                    driver.Navigate().GoToUrl(urlLogin);
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                }
                if (driver.Url.Contains("om/signin/v2/challenge/iap") || (driver.Url.Contains("gle.com/signin/rejected")))
                {
                    saveError(act, "Very phone");
                    addStatus(index, "very phone !");
                    driver.Quit();
                    driver.Close();
                    goto ketthuc;
                }
                if (driver.Url.Contains(".com/interstitials/birthday") || (driver.Url.Contains("ogle.com/web/chip")) || (driver.Url.Contains("/info/unknownerror")))
                {
                    addStatus(index, "Vui lòng thêm ngày sinh");
                    addbirthday(driver);
                }
                if ((driver.Url.Contains("m/signin/v2/identifier")) || (driver.Url.Contains("ccounts.google.com/speedbump/idvreenable")) || (driver.Url.Contains("m/signin/v2/disabled/explanation")))
                {
                    saveError(act, "Đăng nhập không thành công!");
                    addStatus(index, "Đăng nhập không thành công");
                    driver.Quit();
                    driver.Close();
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
                    addStatus(index, "Đăng nhập không thành công");
                    driver.Quit();
                    driver.Close();
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
                        addStatus(index, "Tìm kiếm theo key " + name);
                        active.searchKeyword(name);
                        Thread.Sleep(TimeSpan.FromSeconds(4));
                        strKeyWork = Regex.Replace(strKeyWork, @"[^a-zA-Z0-9]", string.Empty);
                        //search
                        var eleVideos = driver.FindElements(By.XPath("//div[@class='text-wrapper style-scope ytd-video-renderer']"));
                        for (int i = 0; i < eleVideos.Count; i++)
                        {

                            string data = eleVideos[i].Text;
                            string[] lines = data.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                            if (lines.Length < 4)
                            {
                                continue;
                            }
                            else
                            {  
                                string strResult = Regex.Replace(lines[0], @"[^a-zA-Z0-9]", string.Empty);
                                
                                if (strResult.Contains(strKeyWork) == false)
                                {
                                    continue;
                                }
                                if (lines[3].Contains(strTenChannel) == false)
                                {
                                    continue;
                                }
                            }

                            executorUseData.ExecuteScript("arguments[0].click()", eleVideos[i]);

                            break;
                        }

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
                        addStatus(index, "Time Video " + timevideo);
                        Thread.Sleep(3000);
                        time.reset();

                        int rand = random.Next(80, 99);
                        for (int i = 0; i < iSoLanMoLink; i++)
                        {

                            addStatus(index, "Reset video ve 0");
                            // Khởi tạo đối tượng thuộc Actions class
                            Actions action = new Actions(driver);

                            action.KeyDown(OpenQA.Selenium.Keys.NumberPad0);

                            action.SendKeys(OpenQA.Selenium.Keys.NumberPad0).Perform();
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(6);
                            Thread.Sleep(TimeSpan.FromSeconds(5));

                            rand = random.Next(80, 99);

                            int sleepTime = rand * timevideo / 100;
                            addStatus(index, "Chuyen video sau : " + sleepTime + " s");
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(12);
                            Thread.Sleep(TimeSpan.FromSeconds(10));


                            if (iSoLanMoLink % 2 == 0)
                            {
                                active.subVideo();
                            }
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
                                        addStatus(index, sleepCountDiv.ToString());
                                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(sleepTime + 2 - (j - 1) * 60);
                                        Thread.Sleep(TimeSpan.FromSeconds(sleepCountDiv));
                                    }
                                    else
                                    {
                                        addStatus(index, "60s");
                                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1 * 60);
                                        Thread.Sleep(TimeSpan.FromMinutes(1));
                                        if ((j + 1) % 20 == 0)
                                        {
                                            int x = 100;
                                            driver.ExecuteScript("window.scrollTo(100," + (x * j + ")"));
                                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
                                            Thread.Sleep(TimeSpan.FromSeconds(2));
                                            addStatus(index, "2s");
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
                            //TEST VIEW

                            /* addStatus(index, "Sleep 30s");
                             driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                             Thread.Sleep(30 * 1000);*/

                            if (i != iSoLanMoLink - 1)
                            {
                                int x = 100;
                                driver.ExecuteScript("window.scrollTo(100," + (x * 2 + ")"));
                                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
                                Thread.Sleep(TimeSpan.FromSeconds(2));
                                addStatus(index, "2s");
                                driver.ExecuteScript("window.scrollTo({ top: 0, behavior: 'smooth' });");

                                addStatus(index, "Sleep 5s");
                                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5 + 2);
                                Thread.Sleep(TimeSpan.FromSeconds(5));
                                addStatus(index, "Sleep 5s1");
                                try
                                {
                                    driver.FindElement(By.XPath("/html/body/ytd-app/div[1]/ytd-page-manager/ytd-watch-flexy/div[5]/div[1]/div/div[2]/ytd-watch-metadata/div/div[3]/div[1]/div/ytd-text-inline-expander/div[1]/span[1]/yt-formatted-string/a[1]")).Click();
                                }
                                catch (Exception e)
                                {
                                    addStatus(index, "Sleep 5s1 ERROR" + e);
                                }
                                addStatus(index, "Sleep 5s2");

                                addStatus(index, "click description");
                                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5 + 2);
                                Thread.Sleep(TimeSpan.FromSeconds(5));
                            }

                        }
                        addStatus(index, "Xong");

                        try
                        {
                            driver.Close();
                            driver.Quit();
                        }
                        catch (Exception e)
                        {
                            addStatus(index, e.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToString().Contains(" timed out after 60"))
                        {
                            goto vireyt;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                addStatus(index, ex.Message.ToString());
            }
        ketthuc:
            runNew(1);
            return;
        }

        private void kichban3()
        {

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
            try
            {
                addStatus(index, "------------starting-----------");
                GPMLoginAPI api = new GPMLoginAPI("http://" + APP_URL.Text);
                acton sts = new acton(act, api);
                JObject ojb = sts.getLst(act.email);
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
                        addStatus(index, "tạo profile thành công");
                        saveProfile(act, proxy);
                    }
                }
                else
                {
                    //đã có profile
                    id_profile = ojb["id"].ToString();
                }
                UndetectChromeDriver driver = null;
                try { driver = sts.openProfile(id_profile, index); }
                catch
                {
                    saveError(act, "Lỗi mở profile");
                    addStatus(index, "Lỗi mở profile");
                    goto ketthuc;
                }
                try { driver.Get(urlLogin); } catch { }
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
                                addStatus(index, "Lỗi captcha !");
                                driver.Quit();
                                driver.Close();
                                goto ketthuc;
                            }
                            goto lainha;
                        }

                        //Cảnh bảo lỗi
                    }
                }
                Thread.Sleep(TimeSpan.FromSeconds(3));

                addStatus(index, "Đã login mail !");
                clickDeSau(driver); Thread.Sleep(TimeSpan.FromSeconds(5));
                if (driver.Url.Contains("inoptions/recovery-options-collection"))
                {
                    driver.Navigate().GoToUrl(urlLogin);
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                }
                if (driver.Url.Contains("om/signin/v2/challenge/iap") || (driver.Url.Contains("gle.com/signin/rejected")))
                {
                    saveError(act, "Very phone");
                    addStatus(index, "very phone !");
                    driver.Quit();
                    driver.Close();
                    goto ketthuc;
                }
                if (driver.Url.Contains(".com/interstitials/birthday") || (driver.Url.Contains("ogle.com/web/chip")) || (driver.Url.Contains("/info/unknownerror")))
                {
                    addStatus(index, "Vui lòng thêm ngày sinh");
                    addbirthday(driver);
                }
                if ((driver.Url.Contains("m/signin/v2/identifier")) || (driver.Url.Contains("ccounts.google.com/speedbump/idvreenable")) || (driver.Url.Contains("m/signin/v2/disabled/explanation")))
                {
                    saveError(act, "Đăng nhập không thành công!");
                    addStatus(index, "Đăng nhập không thành công");
                    driver.Quit();
                    driver.Close();
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
                    addStatus(index, "Đăng nhập không thành công");
                    driver.Quit();
                    driver.Close();
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
                        addStatus(index, "Tìm kiếm theo key " + name);
                        active.searchKeyword(name);
                        Thread.Sleep(TimeSpan.FromSeconds(4));
                        //search
                        strKeyWork = Regex.Replace(strKeyWork, @"[^a-zA-Z0-9]", string.Empty);

                        var eleVideos = driver.FindElements(By.XPath("//div[@class='text-wrapper style-scope ytd-video-renderer']"));
                        for (int i = 0; i < eleVideos.Count; i++)
                        {
                            string data = eleVideos[i].Text;
                            string[] lines = data.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                            if (lines.Length < 4)
                            {
                                continue;
                            }
                            else
                            {
                                string strResult = Regex.Replace(lines[0], @"[^a-zA-Z0-9]", string.Empty);
                                if (strResult.Contains(strKeyWork) == false)
                                {
                                    continue;
                                }
                                if (lines[3].Contains(strTenChannel) == false)
                                {
                                    continue;
                                }
                            }

                            executorUseData.ExecuteScript("arguments[0].click()", eleVideos[i]);

                            break;
                        }
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
                        addStatus(index, "Time Video " + timevideo);
                        Thread.Sleep(3000);
                        time.reset();

                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                        Thread.Sleep(TimeSpan.FromSeconds(5));

                        Actions action = new Actions(driver);

                        action.KeyDown(OpenQA.Selenium.Keys.NumberPad0);

                        action.SendKeys(OpenQA.Selenium.Keys.NumberPad0).Perform();

                        int rand = random.Next(80, 99);
                        int sleepTime = rand * timevideo / 100;
                        addStatus(index, "Chuyen video sau : " + sleepTime + " s");
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
                                    addStatus(index, sleepCountDiv.ToString());
                                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(sleepTime + 2 - (j - 1) * 60);
                                    Thread.Sleep(TimeSpan.FromSeconds(sleepCountDiv));
                                }
                                else
                                {
                                    addStatus(index, "60s");
                                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1 * 60);
                                    Thread.Sleep(TimeSpan.FromMinutes(1));
                                    if ((j + 1) % 20 == 0)
                                    {
                                        int x = 100;
                                        driver.ExecuteScript("window.scrollTo(100," + (x * j + ")"));
                                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
                                        Thread.Sleep(TimeSpan.FromSeconds(2));
                                        addStatus(index, "2s");
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
                        driver.FindElement(By.XPath("/html/body/ytd-app/div[1]/ytd-page-manager/ytd-watch-flexy/div[5]/div[1]/div/div[2]/ytd-watch-metadata/div/div[3]/div[1]/div/ytd-text-inline-expander/div[1]/span[1]/yt-formatted-string/a[1]")).Click();
                        addStatus(index, "Chuyển sang click PLL");
                        Thread.Sleep(10000);
                        // xem pll 
                        int videoNext = 0;
                        lopPLL:
                        while (true)
                        {
                            videoNext += 1;
                            if (videoNext == 3)
                            {
                                if (sub.Checked)
                                {
                                    active.subVideo();
                                    Thread.Sleep(5000);
                                }
                            }
                            if (videoNext % 2 == 0)

                            {
                                Thread.Sleep(6000);
                                active.likeVideo();
                                // comment
                                
                            }
                            Thread.Sleep(5000);

                            addStatus(index, "Reset video ve 0");
                            // Khởi tạo đối tượng thuộc Actions class
                            action = new Actions(driver);

                            action.KeyDown(OpenQA.Selenium.Keys.NumberPad0);

                            action.SendKeys(OpenQA.Selenium.Keys.NumberPad0).Perform();
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(6);
                            Thread.Sleep(TimeSpan.FromSeconds(5));

                            rand = random.Next(80, 99);

                            sleepTime = rand * timevideo / 100;
                            addStatus(index, "Chuyen video sau : " + sleepTime + " s");
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
                                        addStatus(index, sleepCountDiv.ToString());
                                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(sleepTime + 2 - (j - 1) * 60);
                                        Thread.Sleep(TimeSpan.FromSeconds(sleepCountDiv));
                                    }
                                    else
                                    {
                                        addStatus(index, "60s");
                                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1 * 60);
                                        Thread.Sleep(TimeSpan.FromMinutes(1));
                                        if ((j + 1) % 20 == 0)
                                        {
                                            int x = 100;
                                            driver.ExecuteScript("window.scrollTo(100," + (x * j + ")"));
                                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
                                            Thread.Sleep(TimeSpan.FromSeconds(2));
                                            addStatus(index, "2s");
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
                            Thread.Sleep(1000);
                            goto lopPLL;
                            try
                            {
                                driver.Close();
                                driver.Quit();
                            }
                            catch (Exception e)
                            {
                                addStatus(index, e.Message);
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        if (ex.Message.ToString().Contains(" timed out after 60"))
                        {
                            goto vireyt;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                addStatus(index, ex.Message.ToString());
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
            st.Start();
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
        }
        void savep()
        {
            try { File.WriteAllText("data\\phut.txt", countTimeAll.ToString()); } catch { Thread.Sleep(100); }
        }
        List<link> lstLink;

        private void btnStop_Click(object sender, EventArgs e)
        {
            clearchrome();
        }
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
        }
        bool checkopentab = true;

        private int iSoLuongEmail;
        private int iSoLuong;
        private int iSoLuongDangChay;
        private int iIndexDangChay;
        private string strKeyWork;
        private string strTenChannel;
        private int iSoLanMoLink = 0;



        private void btnStartKichBan1_Click(object sender, EventArgs e)
        {
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
            iIndexDangChay = 0;
            strKeyWork = txtKeyword.Text;
            strTenChannel = txtChannel.Text;
            iSoLanMoLink = int.Parse(txtSoLanMoLink.Text);


            Thread st = new Thread(() =>
            {
                createThread(1);
            });
            st.Start();


        }
        private void btnStartKichBan2_Click(object sender, EventArgs e)
        {
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
            iIndexDangChay = 0;
            strKeyWork = txtKeyword.Text;
            strTenChannel = txtChannel.Text;

            Thread st = new Thread(() =>
            {
                createThread(2);
            });
            st.Start();
        }

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

        private void button2_Click(object sender, EventArgs e)
        {
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
            iIndexDangChay = 0;
            strKeyWork = txtKeyword.Text;
            strTenChannel = txtChannel.Text;

            Thread st = new Thread(() =>
            {
                createThread(3);
            });
            st.Start();
        }
        

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void btnStop_Click_1(object sender, EventArgs e)
        {
            clearchrome();
        }

       /* private void button1_Click(object sender, EventArgs e)
        {

        }*/
        private void button1_Click(object sender, EventArgs e)
        {
            checkopentab = !checkopentab;
        }

        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }  
    }