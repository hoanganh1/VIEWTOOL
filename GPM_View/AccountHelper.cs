using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPM_View
{
    class AccountHelper
    {
        public static JObject GetAccountFromList(string names, List<JObject> profiles)
        {
            if (profiles != null)
            {
                foreach (JObject profile in profiles)
                {
                    string name = Convert.ToString(profile["name"]);
                    string id = Convert.ToString(profile["id"]);
                    if (name.Contains(names.Trim()))
                    {
                        return profile;
                    }
                    Console.WriteLine($"ID: {id} | Name: {name}");
                }
            }
            return null;
        }
    }
}
