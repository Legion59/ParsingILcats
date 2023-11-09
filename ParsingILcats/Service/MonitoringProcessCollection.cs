using AngleSharp.Html.Parser;
using ParsingILcats.Models;
using ParsingILcats.Parsing;

namespace ParsingILcats.Service
{
    public class MonitoringProcessCollection
    {


        public List<CarModel> Car(List<MarketModel> collection, HtmlParser htmlParser, HtmlClient htmlClient)
        {
            var result = new List<CarModel>();

            double progress;

            foreach (var market in collection)
            {
                result.AddRange(htmlParser.GetModels(htmlClient.GetHtmlContent(market.LinkCarModel).Result, market));

                progress = Math.Round(((double)collection.IndexOf(market) + 1) / collection.Count * 100, 3);

                WriteProgress(progress, typeof(CarModel).Name);
            }

            WriteResult(result.Count, typeof(CarModel).Name);

            return result;
        }

        public List<ConfigurationModel> Configuration(List<CarModel> collection, HtmlParser htmlParser, HtmlClient htmlClient)
        {
            var result = new List<ConfigurationModel>();

            double progress;

            foreach (var model in collection.Take(5))
            {
                result.AddRange(htmlParser.GetConfigurations(htmlClient.GetHtmlContent(model.LinkConfiguration).Result, model));

                progress = ((double)collection.IndexOf(model) + 1) / collection.Count * 100;

                WriteProgress(progress, typeof(ConfigurationModel).Name);
            }

            WriteResult(result.Count, typeof(ConfigurationModel).Name);

            return result;
        }

        public List<GroupModel> Group(List<ConfigurationModel> collection, HtmlParser htmlParser, HtmlClient htmlClient)
        {
            var result = new List<GroupModel>();

            double progress;

            foreach (var configuration in collection.Take(5))
            {
                result.AddRange(htmlParser.GetGroups(htmlClient.GetHtmlContent(configuration.LinkToGroupPage).Result, configuration));

                progress = ((double)collection.IndexOf(configuration) + 1) / collection.Count * 100;

                WriteProgress(progress, typeof(GroupModel).Name);
            }

            WriteResult(result.Count, typeof(GroupModel).Name);

            return result;
        }

        public List<SubGroupModel> SubGroup(List<GroupModel> collection, HtmlParser htmlParser, HtmlClient htmlClient)
        {
            var result = new List<SubGroupModel>();

            double progress;

            foreach (var group in collection.Take(5))
            {
                result.AddRange(htmlParser.GetSubGroups(htmlClient.GetHtmlContent(group.LinkSubGroup).Result, group));

                progress = ((double)collection.IndexOf(group) + 1) / collection.Count * 100;

                WriteProgress(progress, typeof(SubGroupModel).Name);
            }

            WriteResult(result.Count, typeof(SubGroupModel).Name);

            return result;
        }

        public List<PartsModel> Parts(List<SubGroupModel> collection, HtmlParser htmlParser, HtmlClient htmlClient)
        {
            var result = new List<PartsModel>();

            double progress;

            foreach (var subGroup in collection)
            {
                result.AddRange(htmlParser.GetParts(htmlClient.GetHtmlContent(subGroup.LinkToParts).Result, subGroup, htmlClient));

                progress = ((double)collection.IndexOf(subGroup) + 1) / collection.Count * 100;

                WriteProgress(progress, typeof(PartsModel).Name);
            }

            WriteResult(result.Count, typeof(PartsModel).Name);

            return result;
        }

        private void WriteProgress(double progress, string typeName)
        {
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);

            Console.Write($"Processing {typeName} collection: {Math.Round(progress, 3)}%");
            Console.SetCursorPosition(0, Console.CursorTop);
        }

        private void WriteResult(int result, string typeName)
        {
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.WriteLine($"{typeName} count: {result}");
        }
    }
}