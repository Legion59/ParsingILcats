using AngleSharp.Html.Parser;
using ParsingILcats.Models;
using ParsingILcats.Parsing;

namespace ParsingILcats.Service
{
    public class MonitoringProcessCollection
    {
        public async Task<List<CarModel>> Car(List<MarketModel> collection, HtmlParser htmlParser, HtmlClient htmlClient)
        {
            var result = new List<CarModel>();
            double progress;

            foreach (var market in collection.Take(5))
            {
                result.AddRange(htmlParser.GetModels(await htmlClient.GetHtmlContent(market.LinkCarModel), market));

                progress = Math.Round(((double)collection.IndexOf(market) + 1) / collection.Count * 100, 3);

                WriteProgress(progress, typeof(CarModel).Name);
            }

            WriteResult(result.Count, typeof(CarModel).Name);

            return result;
        }

        public async Task<List<ConfigurationModel>> Configuration(List<CarModel> collection, HtmlParser htmlParser, HtmlClient htmlClient)
        {
            var result = new List<ConfigurationModel>();
            double progress;

            foreach (var model in collection.Take(5))
            {
                result.AddRange(htmlParser.GetConfigurations(await htmlClient.GetHtmlContent(model.LinkConfiguration), model));

                progress = ((double)collection.IndexOf(model) + 1) / collection.Count * 100;

                WriteProgress(progress, typeof(ConfigurationModel).Name);
            }

            WriteResult(result.Count, typeof(ConfigurationModel).Name);

            return result;
        }

        public async Task<List<GroupModel>> Group(List<ConfigurationModel> collection, HtmlParser htmlParser, HtmlClient htmlClient)
        {
            var result = new List<GroupModel>();
            double progress;

            foreach (var configuration in collection.Take(5))
            {
                result.AddRange(htmlParser.GetGroups(await htmlClient.GetHtmlContent(configuration.LinkToGroupPage), configuration));

                progress = ((double)collection.IndexOf(configuration) + 1) / collection.Count * 100;

                WriteProgress(progress, typeof(GroupModel).Name);
            }

            WriteResult(result.Count, typeof(GroupModel).Name);

            return result;
        }

        public async Task<List<SubGroupModel>> SubGroup(List<GroupModel> collection, HtmlParser htmlParser, HtmlClient htmlClient)
        {
            var result = new List<SubGroupModel>();
            double progress;

            foreach (var group in collection.Take(5))
            {
                result.AddRange(htmlParser.GetSubGroups(await htmlClient.GetHtmlContent(group.LinkSubGroup), group));

                progress = ((double)collection.IndexOf(group) + 1) / collection.Count * 100;

                WriteProgress(progress, typeof(SubGroupModel).Name);
            }

            WriteResult(result.Count, typeof(SubGroupModel).Name);

            return result;
        }

        public async Task<List<PartsModel>> Parts(List<SubGroupModel> collection, HtmlParser htmlParser, HtmlClient htmlClient)
        {
            var result = new List<PartsModel>();
            double progress;

            foreach (var subGroup in collection)
            {
                result.AddRange(await htmlParser.GetParts(await htmlClient.GetHtmlContent(subGroup.LinkToParts), subGroup, htmlClient));

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