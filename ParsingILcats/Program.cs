using AngleSharp.Html.Parser;
using ParsingILcats.Models;
using ParsingILcats.Parsing;
using ParsingILcats.Service;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ParsingILcats
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            HtmlClient htmlClient = new HtmlClient(new HttpClient());
            HtmlParser htmlParser = new HtmlParser();
            MonitoringProcessCollection monitoringProcessCollection = new MonitoringProcessCollection();

            string urlMainPage = "https://www.ilcats.ru";
            string urlMarketPage = "https://www.ilcats.ru/toyota/";

            Console.WriteLine("Brand: Toyota");

            var markets = htmlParser.GetMarkets(await htmlClient.GetHtmlContent(urlMarketPage), urlMainPage).ToList();
            Console.WriteLine($"Markets count: {markets.Count}");

            var allModels = monitoringProcessCollection.Car(markets, htmlParser, htmlClient);

            var allConfigurations = monitoringProcessCollection.Configuration(allModels, htmlParser, htmlClient);

            var allGroups = monitoringProcessCollection.Group(allConfigurations, htmlParser, htmlClient);

            var allSubGroups = monitoringProcessCollection.SubGroup(allGroups, htmlParser, htmlClient);

            var allParts = monitoringProcessCollection.Parts(allSubGroups, htmlParser, htmlClient);

            htmlClient.ShowRequestCount();
        }

        


        /*private List<U> CollectionProcess<T, U>(IEnumerable<T> collection,HtmlParser htmlParser ,HtmlClient htmlClient)
        {
            var result = new List<U>();

            foreach (var el in collection)
            {
                var a = typeof(T).Name switch
                {
                    "CarModel" => htmlParser.GetModels(htmlClient.GetHtmlContent(el.)),
                    "ConfigurationModel" => 2,
                    "GroupModel" => 3,
                    "SubGroupModel" => 4,
                    "PartsModel" => 5,
                    _ => null
                }; 
            }
        }*/
    }
}