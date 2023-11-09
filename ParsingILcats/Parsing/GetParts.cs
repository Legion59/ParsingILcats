using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using ParsingILcats.Models;

namespace ParsingILcats.Parsing
{
    public static partial class HtmlParserExtensions
    {
        public static IEnumerable<PartsModel> GetParts(this HtmlParser htmlParser, string htmlContent, SubGroupModel subGroup, HtmlClient htmlClient)
        {
            var classNames = htmlParser.ParseDocument(htmlContent)
                                                    .QuerySelectorAll("tbody")
                                                    .FirstOrDefault()
                                                    .QuerySelectorAll("tr")
                                                    .Skip(1)
                                                    .Select(el => $"tr.{el.ClassName.Replace(' ', '.')}")
                                                    .ToHashSet();


            var parts = classNames.Select(el => htmlParser.ParseDocument(htmlContent).QuerySelectorAll(el))
                                  .Where(el => el.Length > 1)
                                  .SelectMany(el => CreateParts(el, subGroup));

            if (parts.Count() != 0)
            {
                var imageName = $"{subGroup.Group.Configuration.Car.Market.Code}-{subGroup.Group.Configuration.Car.Id}-{subGroup.Group.Configuration.ConfigurationName}-{subGroup.Group.Id}-{subGroup.Id}";
                var imageLink = htmlParser.ParseDocument(htmlContent)
                                          .QuerySelector("div.Image")
                                          .QuerySelector("img")
                                          .GetAttribute("src");

                htmlClient.SaveImage(imageLink, imageName);
            }

            return parts;

        }

        public static IEnumerable<PartsModel> CreateParts(IHtmlCollection<IElement> parts, SubGroupModel subGroup)
        {
            var treeCode = GetTreeCode(parts[0].TextContent);
            var treeName = GetTreeName(parts[0].TextContent);

            return parts.Skip(1)
                        .Select(el => new PartsModel
                        {
                            Code = GetPartCode(el),
                            Count = el.QuerySelector("div.count").TextContent,
                            Info = el.QuerySelector("div.usage").TextContent,
                            TreeCode = treeCode,
                            TreeName = treeName,
                            DateRange = el.QuerySelector("div.dateRange").TextContent,
                            SubGroup = subGroup
                        });
        }

        private static string GetPartCode(IElement element)
        {
            string partCode;

            if (element.QuerySelector("div.replaceNumber") == null)
            {
                partCode = element.QuerySelector("div.number").TextContent;
            }
            else
            {
                partCode = element.QuerySelector("div.replaceNumber").QuerySelector("a").TextContent;
            }

            return partCode;
        }
        private static string GetTreeCode(string str) => str.Split('\U000000A0').FirstOrDefault();
        private static string GetTreeName(string str) => str.Substring(str.IndexOf('\U000000A0')).Trim();
    }
}