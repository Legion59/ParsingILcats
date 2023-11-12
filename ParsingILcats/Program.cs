using AngleSharp.Html.Parser;
using ParsingILcats.Database;
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

            ParsingDbContext parsingDbContext = new ParsingDbContext();

            string urlMainPage = "https://www.ilcats.ru";
            string urlMarketPage = "https://www.ilcats.ru/toyota/";


            Console.WriteLine("Brand: Toyota");

            var allMarkets = monitoringProcessCollection.Markets(urlMarketPage, urlMainPage, htmlParser, htmlClient);

            var allModels = monitoringProcessCollection.Car(await allMarkets, htmlParser, htmlClient);

            var allConfigurations = monitoringProcessCollection.Configuration(await allModels, htmlParser, htmlClient);

            var allGroups = monitoringProcessCollection.Group(await allConfigurations, htmlParser, htmlClient);

            var allSubGroups = monitoringProcessCollection.SubGroup(await allGroups, htmlParser, htmlClient);

            var allParts = monitoringProcessCollection.Parts(await allSubGroups, htmlParser, htmlClient);

            await parsingDbContext.AddRangeAsync(await allMarkets);
            await parsingDbContext.AddRangeAsync(await allModels);
            await parsingDbContext.AddRangeAsync(await allConfigurations);
            await parsingDbContext.AddRangeAsync(await allGroups);
            await parsingDbContext.AddRangeAsync(await allSubGroups);
            await parsingDbContext.AddRangeAsync(await allParts);

            await parsingDbContext.SaveChangesAsync();

            htmlClient.ShowRequestCount();
        }
    }
}