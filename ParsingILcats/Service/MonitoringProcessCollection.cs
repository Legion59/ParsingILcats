using AngleSharp.Html.Parser;
using Microsoft.IdentityModel.Tokens;
using ParsingILcats.Models;
using ParsingILcats.Parsing;

namespace ParsingILcats.Service
{
    public class MonitoringProcessCollection
    {
        public async Task<List<MarketModel>> Markets(string urlMarket, string urlMainPage, HtmlParser htmlParser, HtmlClient htmlClient)
        {
            var result = htmlParser.GetMarkets(await htmlClient.GetHtmlContent(urlMarket), urlMainPage).Take(1).ToList();

            Console.WriteLine($"Markets count: {result.Count}");

            return result;
        }

        public async Task<List<CarModel>> Car(List<MarketModel> collection, HtmlParser htmlParser, HtmlClient htmlClient)
        {
            var result = new List<CarModel>();
            double progress;

            foreach (var market in collection)
            {
                var cars = htmlParser.GetModels(await htmlClient.GetHtmlContent(market.LinkCarModel), market).Take(1).ToList();

                market.Cars = cars;
                result.AddRange(cars);

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

            foreach (var model in collection)
            {
                var conf = htmlParser.GetConfigurations(await htmlClient.GetHtmlContent(model.LinkConfiguration), model).Take(1).ToList();

                model.Configurations = conf;
                result.AddRange(conf);

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

            foreach (var configuration in collection)
            {
                var grops = htmlParser.GetGroups(await htmlClient.GetHtmlContent(configuration.LinkToGroupPage), configuration).ToList();

                configuration.Groups = grops;
                result.AddRange(grops);

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

            foreach (var group in collection)
            {
                var subGrops = htmlParser.GetSubGroups(await htmlClient.GetHtmlContent(group.LinkSubGroup), group).ToList();

                group.SubGroups = subGrops;
                result.AddRange(subGrops);

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

                var parts = (await htmlParser.GetParts(await htmlClient.GetHtmlContent(subGroup.LinkToParts), subGroup, htmlClient)).ToList();

                if (!parts.IsNullOrEmpty())
                {
                    subGroup.Parts = parts;
                    result.AddRange(parts);
                }

                progress = ((double)collection.IndexOf(subGroup) + 1) / collection.Count * 100;
                WriteProgress(progress, typeof(PartsModel).Name);
            }

            foreach(var part in collection.Where(el => el.Parts == null).ToList())
            {
                collection.Remove(part);
            }

            WriteResult(collection.Count, typeof(SubGroupModel).Name);
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