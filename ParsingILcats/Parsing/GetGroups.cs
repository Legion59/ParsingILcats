using AngleSharp.Html.Parser;
using ParsingILcats.Models;

namespace ParsingILcats.Parsing
{
    public static partial class HtmlParserExtensions
    {
        public static IEnumerable<GroupModel> GetGroups(this HtmlParser htmlClient, string htmlContent, ConfigurationModel configuration)
        {
            return htmlClient.ParseDocument(htmlContent)
                             .QuerySelectorAll("div.name")
                             .Select((el, index) => new GroupModel
                             {
                                 Configuration = configuration,
                                 Id = index + 1,
                                 Name = el.QuerySelector("a").TextContent,
                                 LinkSubGroup = CreatSubGroupLink(index + 1, configuration)
                             });
        }

        private static string CreatSubGroupLink(int index, ConfigurationModel configuration)
        {
            string market = configuration.Car.Market.Code;
            string model = configuration.Car.Id;
            string modification = configuration.ConfigurationName;

            return $"https://www.ilcats.ru/toyota/?function=getSubGroups&market={market}&model={model}&modification={modification}&group={index + 1}";
        }
    }
}