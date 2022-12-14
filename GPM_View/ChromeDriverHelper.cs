using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPM_View
{
    class ChromeDriverHelper
    {
        public Process process {get; set;}


        public ChromeDriverHelper()
        {
            this.process = null;
        }

        /// <summary>
        ///  Khởi tạo nếu thành công thì thử khởi tạo lại.
        ///  Nếu không thành công thì
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        /// 

        public UndetectChromeDriver initDriver(JObject ob)
        {
            UndetectChromeDriver result = null;
            // Nếu trong trường hợp thất bại thì cố gắng thực hiện kết nối lại lần nữa.
            for(int i = 0; i < 3; i ++)
            {
                try
                {
                        string binaryLocation = Convert.ToString(ob["browser_location"]);
                        string debuggerAddress = Convert.ToString(ob["selenium_remote_debug_address"]);
                        FileInfo fileInfo = new FileInfo(Convert.ToString(ob["selenium_driver_location"]));
                        ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService(fileInfo.DirectoryName, fileInfo.Name);
                        chromeDriverService.HideCommandPromptWindow = true;
                        result = new UndetectChromeDriver(chromeDriverService, new ChromeOptions
                        {
                            Proxy = null,
                            BinaryLocation = binaryLocation,
                            DebuggerAddress = debuggerAddress
                        });
                        result.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds((double)30);
                        result.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes((double)180);
                    return result;
                }
                catch
                {
                    try
                    {
                        if(result != null)
                        {
                            result.Quit();
                        }
                    }
                    catch { }
                }
                
            }
           
            return null;
        }



        /// <summary>
        ///  Khởi chạy chrome thông quan GPM
        ///  nếu nó không thành công thì thực hiện gọi lại 2 lần để mở 
        /// </summary>
        /// <param name="api"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public JObject startChrome(GPMLoginAPI api, string ID,int port)
        {
            JObject result = null;
            try
            {
                for(int i = 0; i < 3; i ++)
                {
                    result = api.Start(ID, port, "");
                    if(result != null)
                    {
                        return result;
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                }
               
            }
            catch
            {
               
            }

            return null;
        }


        /// <summary>
        ///  Lấy ra Process của browser trong thường hợp nó không thể đóng được thông qua driver.
        /// </summary>
        /// <returns></returns>
        public bool GetProces(UndetectChromeDriver driver)
        {
            try
            {
                if (this.process !=null)
                {
                    return true;
                }
                string title = "";

                for(int i = 0; i< 10; i++)
                {
                    try
                    {
                        title = driver.CurrentWindowHandle;
                    }
                    catch
                    {
                        title = Common.CreateRandomStringNumber(15);
                    }
                    if(title != "")
                    {
                        for (int j = 0; j <30; j++)
                        {
                            driver.ExecuteScript("document.title='"+title.Trim()+"'");
                            Thread.Sleep(TimeSpan.FromSeconds(2));
                            IEnumerable<Process> processesByName = Process.GetProcessesByName("chrome");
                            foreach (var pro in processesByName)
                            {
                                if (pro.MainWindowTitle.Contains(title.Trim()))
                                {
                                    this.process = pro;
                                    return true;
                                }
                            }
                            Thread.Sleep(1000);
                        }
                    }
                }
            }
            catch
            {

            }

            return false;
        }
    }
}
