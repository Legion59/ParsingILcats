using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using ParsingILcats.Models;

namespace ParsingILcats.Parsing
{
    public static partial class HtmlParserExtensions
    {
        public static IEnumerable<ConfigurationModel> GetConfigurations(this HtmlParser htmlParser, string htmlContent, CarModel car)
        {
            return htmlParser.ParseDocument(htmlContent)
                             .QuerySelectorAll("table")
                             .FirstOrDefault()
                             .QuerySelectorAll("tr")
                             .Skip(1)
                             .Select(el => new ConfigurationModel
                             {
                                 ConfigurationCode = el.QuerySelector("a").TextContent,
                                 DateRange = el.QuerySelector("div.dateRange").TextContent,
                                 LinkToGroupPage = CreatGroupLink(el, car),
                                 Specs = el.QuerySelectorAll("td")
                                                .Skip(2)
                                                .Select(el => new SpecModel
                                                {
                                                    Value = el.TextContent
                                                })
                                                .Where(el => !string.IsNullOrEmpty(el.Value))
                                                .ToList(),
                                 Car = car
                             });
        }

        private static string CreatGroupLink(IElement element, CarModel car)
        {
            string market = car.Market.Code;
            string model = car.Id;

            return $"https://www.ilcats.ru/toyota/?function=getGroups&market={market}&model={model}&modification={element.QuerySelector("a").TextContent}";
        }
    }
}