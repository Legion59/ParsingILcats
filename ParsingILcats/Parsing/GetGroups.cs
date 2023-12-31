﻿using AngleSharp.Html.Parser;
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
                                 Index = index + 1,
                                 Name = el.QuerySelector("a").TextContent,
                                 LinkSubGroup = CreatSubGroupLink(index + 1, configuration),
                                 Configuration = configuration
                             });
        }

        private static string CreatSubGroupLink(int index, ConfigurationModel configuration)
        {
            string market = configuration.Car.Market.Code;
            string model = configuration.Car.ModelCode;
            string modification = configuration.ConfigurationCode;

            return $"https://www.ilcats.ru/toyota/?function=getSubGroups&market={market}&model={model}&modification={modification}&group={index + 1}";
        }
    }
}