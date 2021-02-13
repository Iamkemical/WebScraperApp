using CsvHelper;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;

namespace WebScrapingApp
{
    public class Scrapper
    {
        private ObservableCollection<EntryModel> _entries = new ObservableCollection<EntryModel>();

        public ObservableCollection<EntryModel> Entries
        {
            get { return _entries; }
            set { _entries = value; }
        }

        public void ScrapeData(string page)
        {
            var web = new HtmlWeb();
            var doc = web.Load(page);

            var articles = doc.DocumentNode.SelectNodes("//*[@class='article-single']");

            foreach(var article in articles)
            {
                var header = HttpUtility.HtmlDecode(article.SelectSingleNode(".//li[@class = 'article-header']").InnerText);
                var description = HttpUtility.HtmlDecode(article.SelectSingleNode(".//li[@class = 'article-copy']").InnerText);
                Debug.Print($"Title: {header} \n Description: {description}");

                _entries.Add(new EntryModel { Title = header, Description = description });
            }
        }


        public void Export()
        {
            using(TextWriter tw = File.CreateText($"VanguardBusinessNews{DateTime.Now.ToFileTime()}.csv"))
            {
                using(var cw = new CsvWriter(tw, CultureInfo.InvariantCulture))
                {
                    foreach(var entry in Entries)
                    {
                        cw.WriteRecord(entry);
                        cw.NextRecord();
                    }
                }
            }
        }

        public void ScrapInfo(string page)
        {
            var web = new HtmlWeb();
            var doc = web.Load(page);

            var articles = doc.DocumentNode.SelectNodes("//*[@class='rtp-post-content']");

            foreach (var article in articles)
            {
                var header = HttpUtility.HtmlDecode(article.SelectSingleNode(".//h2[@class = 'entry-title']").InnerText);
                var description = article.ChildNodes.FindFirst("a");
                //var result = description.OuterHtml.Substring(9, 95);
                var link = description.GetAttributeValue("href", string.Empty);
                Debug.Print($"Title: {header} \nLink: {link}");

                _entries.Add(new EntryModel { Title = header, Description = link });
            }
        }
    }
}
