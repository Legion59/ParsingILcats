using AngleSharp.Html.Parser;
using ParsingILcats.Service;

namespace ParsingILcats
{
    public class Program
    {
        static async Task Main()
        {
            HtmlClient htmlClient = new HtmlClient(new HttpClient());
            HtmlParser htmlParser = new HtmlParser();
            MonitoringProcessCollection monitoringProcessCollection = new MonitoringProcessCollection();

            string urlMainPage = "https://www.ilcats.ru";
            string urlMarketPage = "https://www.ilcats.ru/toyota/";

            Console.WriteLine("Brand: Toyota");

            var allMarkets = monitoringProcessCollection.Markets(urlMarketPage, urlMainPage, htmlParser, htmlClient);

            var allModels = monitoringProcessCollection.Car(await allMarkets, htmlParser, htmlClient);

            var allConfigurations = monitoringProcessCollection.Configuration(await allModels, htmlParser, htmlClient);

            var allGroups = monitoringProcessCollection.Group(await allConfigurations, htmlParser, htmlClient);

            var allSubGroups = monitoringProcessCollection.SubGroup(await allGroups, htmlParser, htmlClient);

            var allParts = monitoringProcessCollection.Parts(await allSubGroups, htmlParser, htmlClient);

            Console.WriteLine(allParts.Result.Count);



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