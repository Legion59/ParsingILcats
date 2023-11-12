namespace ParsingILcats.Models
{
    public class CarModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DateRange { get; set; }
        public string ModelCode { get; set; }
        public string LinkConfiguration { get; set; }

        public int MarketId { get; set; }
        public MarketModel Market { get; set; }

        public  IEnumerable<ConfigurationModel> Configurations { get; set; }
    }
}
