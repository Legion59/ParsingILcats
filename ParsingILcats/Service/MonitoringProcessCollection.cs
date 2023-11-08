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

                Console.Write($"Processing {typeof(CarModel).Name} collection: {progress}%");
                Console.SetCursorPosition(0, Console.CursorTop);
            }

            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.WriteLine($"Models count: {result.Count}");

            return result;
        }

        public List<ConfigurationModel> Configuration(List<CarModel> collection, HtmlParser htmlParser, HtmlClient htmlClient)
        {
            var result = new List<ConfigurationModel>();

            double progress;

            foreach (var model in collection.GetRange(0, 10))
            {
                result.AddRange(htmlParser.GetConfigurations(htmlClient.GetHtmlContent(model.LinkConfiguration).Result, model));

                progress = ((double)collection.IndexOf(model) + 1) / collection.Count * 100;

                Console.Write($"Processing {typeof(ConfigurationModel).Name} collection: {Math.Round(progress, 3)}%");
                Console.SetCursorPosition(0, Console.CursorTop);
            }

            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.WriteLine($"Configurations count: {result.Count}");

            return result;
        }

        public List<GroupModel> Group(List<ConfigurationModel> collection, HtmlParser htmlParser, HtmlClient htmlClient)
        {
            var result = new List<GroupModel>();

            double progress;

            foreach (var configuration in collection.GetRange(0, 10))
            {
                result.AddRange(htmlParser.GetGroups(htmlClient.GetHtmlContent(configuration.LinkToGroupPage).Result, configuration));

                progress = ((double)collection.IndexOf(configuration) + 1) / collection.Count * 100;
                Console.Write($"Processing {typeof(GroupModel).Name} collection: {Math.Round(progress, 3)}%");
                Console.SetCursorPosition(0, Console.CursorTop);
            }

            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.WriteLine($"Groups count: {result.Count}");

            return result;
        }

        public List<SubGroupModel> SubGroup(List<GroupModel> collection, HtmlParser htmlParser, HtmlClient htmlClient)
        {
            var result = new List<SubGroupModel>();

            double progress;

            foreach (var group in collection.GetRange(0, 10))
            {
                result.AddRange(htmlParser.GetSubGroups(htmlClient.GetHtmlContent(group.LinkSubGroup).Result, group));

                progress = ((double)collection.IndexOf(group) + 1) / collection.Count * 100;
                Console.Write($"Processing {typeof(SubGroupModel).Name} collection: {Math.Round(progress, 3)}%");
                Console.SetCursorPosition(0, Console.CursorTop);
            }

            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.WriteLine($"Subgroups count: {result.Count}");

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
                Console.Write($"Processing {typeof(PartsModel).Name} collection: {Math.Round(progress, 3)}%");
                Console.SetCursorPosition(0, Console.CursorTop);
            }

            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.WriteLine($"Parts count: {result.Count}");

            return result;
        }
    }
}
