using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace mersis_proje.Models
{
    public class Login
    {
        public string __RequestVerificationToken { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Captcha { get; set; }
        public string requestedwith { get; set; }
}

}