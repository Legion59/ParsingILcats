using AngleSharp.Html.Parser;
using ParsingILcats.Models;

namespace ParsingILcats.Parsing
{
    public static partial class HtmlParserExtensions
    {
        public static IEnumerable<BrandModel> GetBrands(this HtmlParser htmlParser, string htmlContent, string urlMainPage)
        {
            return htmlParser.ParseDocument(htmlContent)
                                    .GetElementsByClassName("CatalogGroup")
                                    .Take(2)
                                    .Select(x => x.InnerHtml)
                                    .SelectMany(group => GetBrandsList(group, htmlParser, urlMainPage))
                                    .ToList();
        }

        public static IEnumerable<BrandModel> GetBrandsList(string elements, HtmlParser parser, string urlMainPage)
        {
            return parser.ParseDocument(elements)
                         .QuerySelectorAll("a")
                         .Select(element => new BrandModel
                         {
                             Name = element.TextContent,
                             UrlToModelPage = urlMainPage + element.GetAttribute("href")
                         });
        }
    }
}