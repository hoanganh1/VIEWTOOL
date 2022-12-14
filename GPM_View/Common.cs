using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPM_View
{
    class Common
    {
        /// <summary>
        ///  Đánh dấu khi Chương trình được khởi chạy
        /// </summary>
        public static bool Run_Flag = false;

        /// <summary>
        ///   Tạo random string
        /// </summary>
        /// <param name="lengText"></param>
        /// <returns></returns>
        public static string CreateRandomStringNumber(int lengText)
        {
            Random rd = new Random();
            string text = "";
            string text2 = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < lengText; i++)
            {
                text += text2[rd.Next(0, text2.Length)].ToString();
            }
            return text;
        }
    }
}
