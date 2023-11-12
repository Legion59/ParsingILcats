using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using ParsingILcats.Models;

namespace ParsingILcats.Parsing
{
    public static partial class HtmlParserExtensions
    {
        public static IEnumerable<CarModel> GetModels(this HtmlParser htmlParser, string htmlContent, MarketModel market)
        {
            return htmlParser.ParseDocument(htmlContent)
                             .All
                             .Where(el => el.ClassName == "List" && el.ParentElement.ClassName == "List Multilist")
                             .SelectMany(el => GetModelInfo(el, htmlParser, market));
        }

        private static IEnumerable<CarModel> GetModelInfo(IElement infoHtml, HtmlParser htmlParser, MarketModel market)
        {
            string carName = infoHtml.QuerySelector("div.name").TextContent;

            return htmlParser.ParseDocument(infoHtml.InnerHtml)
                             .All
                             .Where(el => el.ClassName == "List" && el.Children.HasClass("id"))
                             .Select(el => new CarModel
                             {
                                 Market = market,
                                 Id = el.QuerySelector("div.id").TextContent,
                                 Name = carName,
                                 DateRange = el.QuerySelector("div.dateRange").TextContent,
                                 ModelCode = el.QuerySelector("div.modelCode").TextContent,
                                 LinkConfiguration = $"https://www.ilcats.ru/toyota/?function=getComplectations&market={market.Code}&model={el.QuerySelector("div.id").TextContent}"
                             });
        }
    }
}