using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.Infrastructure
{
    public class CustomAppSettings
    {
        public CustomAppSettings()
        {
            //edit values in appsettings.json
            NewsApiUrl = "https://newsapi.org/v2/";
            NewsApiKey = "30ea7af35bcc4c969d83899450e6bd04";
            FormSmtpServer = "smtp.gmail.com";
            FormPort = 587;
            FormUserName = "mail.bolinderbil@gmail.com";
            FormPassWord = "bil12345";
        }

        public string NewsApiUrl { get; set; }
        public string NewsApiKey { get; set; }
        public string FormSmtpServer { get; set; }
        public int FormPort { get; set; }
        public string FormUserName { get; set; }
        public string FormPassWord { get; set; }
    }
}
