using mersis_proje.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mersis_proje.Controllers
{
    public class LoginController : Controller
    {
        public  Login log = new Login();
        // GET: Login
        public ActionResult Login()
        {
            var client = new RestClient("https://mersis.gtb.gov.tr");

            var request = new RestRequest("/Portal/KullaniciIslemleri/Giris", Method.Get);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            // İlk olarak, bir GET isteği yaparak giriş sayfasının HTML içeriğini alıyoruz
            var getRequest = new RestRequest("/Portal/KullaniciIslemleri/Giris", Method.Get);
            RestResponse getResponse = client.Execute(getRequest);
            string htmlContent = getResponse.Content;

            // HTML içeriğini analiz ederek __RequestVerificationToken değerini elde ediyoruz
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(htmlContent);
            HtmlAgilityPack.HtmlNode verificationTokenNode = htmlDoc.DocumentNode.SelectSingleNode("//input[@name='__RequestVerificationToken']");
            string verificationToken = verificationTokenNode.GetAttributeValue("value", "");

            // İsteğe __RequestVerificationToken değerini ekliyoruz
            request.AddParameter("__RequestVerificationToken", verificationToken);
            request.AddParameter("KullaniciAdi", "suleymanhamurcu@gmail.com");
            request.AddParameter("Sifre", "123456");
            request.AddParameter("Captcha", "1");
            request.AddParameter("X-Requested-With", loginResponse.XRequestedWith);


            RestResponse response = client.Execute(request);

            // Yanıtı işleyerek __RequestVerificationToken değerini güncelliyoruz
            var loginResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<Login>(response.Content);
            log.__RequestVerificationToken = loginResponse.__RequestVerificationToken;

            return View();
        }
    }
}
