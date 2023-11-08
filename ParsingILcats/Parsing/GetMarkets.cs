using AngleSharp.Html.Parser;
using ParsingILcats.Models;

namespace ParsingILcats.Parsing
{
    public static partial class HtmlParserExtensions
    {
        public static IEnumerable<MarketModel> GetMarkets(this HtmlParser htmlParser, string htmlContent, string urlMain)
        {
            return htmlParser.ParseDocument(htmlContent)
                             .QuerySelectorAll("div.name")
                             .Select(el => new MarketModel
                             {
                                 Code = GetContryCode(el.QuerySelector("a").GetAttribute("href")),
                                 LinkCarModel =  urlMain + el.QuerySelector("a").GetAttribute("href")
                             });
        }

        private static string GetContryCode(string str) => str.Substring(str.Length - 2);
    }
}
