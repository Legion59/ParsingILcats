using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using ParsingILcats.Models;

namespace ParsingILcats.Parsing
{
    public static partial class HtmlParserExtensions
    {
        public static IEnumerable<SubGroupModel> GetSubGroups(this HtmlParser htmlClient, string htmlContent, GroupModel group)
        {
            return htmlClient.ParseDocument(htmlContent)
                             .All
                             .Where(el => el.ClassName == "List" && el.Children.HasClass("image"))
                             .Select(el => new SubGroupModel
                             {
                                 Index = GetSubId(el.QuerySelector("img").GetAttribute("alt")),
                                 Name = el.QuerySelector("div.name").TextContent,
                                 LinkToParts = CreatePartsLink(el, group),
                                 Group = group
                             });
        }

        private static string GetSubId(string str) => str.Split(' ')[1];

        private static string CreatePartsLink(IElement element, GroupModel groupModel)
        {
            string market = groupModel.Configuration.Car.Market.Code;
            string model = groupModel.Configuration.Car.ModelCode;
            string modification = groupModel.Configuration.ConfigurationCode;
            int group = groupModel.Index;

            return $"https://www.ilcats.ru/toyota/?function=getParts&market={market}&model={model}&modification={modification}&group={group}&subgroup={GetSubId(element.QuerySelector("img").GetAttribute("alt"))}";

        }
    }
}