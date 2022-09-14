using ADFS_Plugin.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ADFS_Plugin.Helpers
{
    public class JsonFileReader
    {
        public UserAccountModel GetUser(string username, string domain)
        {
            string directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string filePath = directory + @"\Resources\user_accounts.json";

            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                List<UserAccountModel> userAccounts = JsonConvert.DeserializeObject<List<UserAccountModel>>(json);

                return userAccounts.FirstOrDefault(account => account.Username == username && account.Domain == domain);
            }
        }
    }
}
