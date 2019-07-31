using HtmlAgilityApi.Data.Interfaces;
using HtmlAgilityApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace HtmlAgilityApi.Data.Repositories
{
  
    
    public class HaberRepository : IHaberRepository
    {
        public Uri url;
        public string html;
        string[] donanimHeader = new string[39];
        string[] donanimAuthor = new string[39];
        string[] donanimContent = new string[39];
        string[] donanimImage = new string[39];
        string[] donanimHaberLink = new string[39];
        int[] donanimFotoId = new int[39];
        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        public HaberRepository()
        {
            url = new Uri("https://www.donanimhaber.com/", UriKind.Absolute);
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            html = client.DownloadString(url);
            //HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            int counter = 0;
            for (int i = 0; i < 40; i++)
            {
                try
                {
                    //VeriAlDip(donanimHaberLink[i], "//*[@id='res-112646-1']/span/a", "href", listBox5); //Haber Image
                    //*[@id="kapsulDiv"]/div[1]/div[1]/div[1]/div[6]/div/a
                    donanimHeader[i] = doc.DocumentNode.SelectSingleNode("//*[@id='kapsulDiv']/div[1]/div[1]/div/div[" + (i + 1) + "]/div/div/a").InnerText;//Haber Header
                    donanimContent[i] = doc.DocumentNode.SelectSingleNode("//*[@id='kapsulDiv']/div[1]/div[1]/div/div[" + (i + 1) + "]/div/div/div[2]").InnerText;//Haber Content
                    donanimAuthor[i] = doc.DocumentNode.SelectSingleNode("//*[@id='kapsulDiv']/div[1]/div[1]/div/div[" + (i + 1) + "]/div/div/div[1]").InnerText;//Haber Author
                    donanimHaberLink[i] = VeriAlDipLink("https://www.donanimhaber.com/", "//*[@id='kapsulDiv']/div[1]/div[1]/div/div[" + (i + 1) + "]/div/div/a", "href"); //Haber Link
                    donanimFotoId[i] = VeriAlFotoId("https://www.donanimhaber.com/", "//*[@id='kapsulDiv']/div[1]/div[1]/div[1]/div[" + (i + 1) + "]/div/a", "data-id"); //Haber Foto id
                    donanimImage[i] = VeriAlFotoLink(donanimHaberLink[i], "//*[@id='res-" + donanimFotoId[i] + "-1']/span/a", "href"); //Haber Image
                    donanimHeader[i] = karakterTemizle(WebUtility.HtmlDecode(donanimHeader[i]));
                    donanimContent[i] = karakterTemizle(WebUtility.HtmlDecode(donanimContent[i]));
                    donanimAuthor[i] = karakterTemizle(donanimAuthor[i]);
                    donanimAuthor[i] = karakterTemizle(WebUtility.HtmlDecode(donanimAuthor[i]));
                    counter++;
                    haberler.Add(new Haber { Id = counter, Header = donanimHeader[i], Content = donanimContent[i], Author = donanimAuthor[i], NewsLink = donanimHaberLink[i], NewsPhotoId = donanimFotoId[i], NewsPhotoLink =donanimImage[i] });
                }
                catch (Exception)
                {

                }
            }
        }
        public string karakterTemizle(string gelen)
        {
            string trans = gelen;
            trans = Regex.Replace(trans, "\r\n", String.Empty);
            trans = trans.TrimEnd();
            trans = trans.TrimStart();
            return trans;
        }

        public string VeriAlDipLink(String Url, String XPath, String dip)
        {

            //Açıklama satırlarına alınma sebebi : Her seferinde aynı adresi gereksiz yere indirmesidir.
            //try
            //{
            //    url = new Uri(Url);
            //}
            //catch (UriFormatException)
            //{
            //    url = new Uri("https://www.donanimhaber.com");
            //}
            //catch (ArgumentNullException)
            //{
            //    url = new Uri("https://www.donanimhaber.com");
            //}

            //WebClient client = new WebClient();
            //client.Encoding = Encoding.UTF8;
            //try
            //{
            //    html = client.DownloadString(url);
            //}
            //catch (WebException)
            //{
            //    html = client.DownloadString(new Uri("https://www.donanimhaber.com"));
            //}

            //HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            //doc.LoadHtml(html);
            try
            {
                return "https://www.donanimhaber.com" + doc.DocumentNode.SelectSingleNode(XPath).Attributes[dip].Value;
            }
            catch (NullReferenceException)
            {
                    return "https://www.donanimhaber.com";
            }
            return "...";
        }
        public int VeriAlFotoId(String Url, String XPath, String dip)
        {
            //Açıklama satırlarına alınma sebebi : Her seferinde aynı adresi gereksiz yere indirmesidir.
            //try
            //{
            //    url = new Uri(Url);
            //}
            //catch (UriFormatException)
            //{

            //        url = new Uri("https://www.donanimhaber.com");

            //}
            //catch (ArgumentNullException)
            //{

            //        url = new Uri("https://www.donanimhaber.com");

            //}

            //WebClient client = new WebClient();
            //client.Encoding = Encoding.UTF8;
            //try
            //{
            //     html = client.DownloadString(url);
            //}
            //catch (WebException)
            //{
            //     html = client.DownloadString(new Uri("https://www.donanimhaber.com"));

            //}

            //HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            //doc.LoadHtml(html);
            try
            {
                return Convert.ToInt32(doc.DocumentNode.SelectSingleNode(XPath).Attributes[dip].Value);
            }
            catch (NullReferenceException)
            {
                    return 11111;
            }
        }
        public string VeriAlFotoLink(String Url, String XPath, String dip)
        {
            //Açıklama satırlarına ALINMAMA Sebebi : Her seferinde aynı adresi indirmemesi (Farklı adreslerden farklı veriler çektiği için)
            try
            {
                url = new Uri(Url);
            }
            catch (UriFormatException)
            {
                url = new Uri("https://www.donanimhaber.com");
            }
            catch (ArgumentNullException)
            {
                url = new Uri("https://www.donanimhaber.com");
            }
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            try
            {
                html = client.DownloadString(url);
            }
            catch (WebException)
            {
                html = client.DownloadString(new Uri("https://www.donanimhaber.com"));
            }

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            try
            {

                return doc.DocumentNode.SelectSingleNode(XPath).Attributes[dip].Value;

            }
            catch (Exception)
            {
                return "https://www.ekol.com/wp-content/uploads/2015/07/ekol-logotype.png";
            }
           
        }

        public List<Haber> haberler = new List<Haber>()
        {
            //new Haber {Id=1, Header="Haber başlığı 1", Content="İçerik 1", Author="Cihat PALA", NewsLink="1.haber.COM", NewsPhotoId=1121, NewsPhotoLink="1.1haber" },
            //new Haber {Id=2, Header="Haber başlığı 2", Content="İçerik 2", Author="Cihat PALA", NewsLink="2.haber.COM", NewsPhotoId=1122, NewsPhotoLink="2.2haber" },
            //new Haber {Id=3, Header="Haber başlığı 3", Content="İçerik 3", Author="Cihat PALA", NewsLink="3.haber.COM", NewsPhotoId=1123, NewsPhotoLink="3.3haber" },
            //new Haber {Id=4, Header="Haber başlığı 4", Content="İçerik 4", Author="Cihat PALA", NewsLink="4.haber.COM", NewsPhotoId=1124, NewsPhotoLink="4.4haber" }
        };

        public List<Haber> GetTumHaberler()
        {
            return haberler;
        }
        public Haber GetHaber(int id)
        {
            var haber = haberler.FirstOrDefault(x => x.Id == id);
            return haber;
        }
    }
}
