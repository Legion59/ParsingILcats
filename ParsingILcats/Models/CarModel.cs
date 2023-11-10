namespace ParsingILcats.Models
{
    public class CarModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string DateRange { get; set; }
        public string ModelCode { get; set; }
        public string LinkConfiguration { get; set; }

        public int MarketId { get; set; }
        public MarketModel Market { get; set; }

        public  ICollection<ConfigurationModel> Configurations { get; set; }
    }
}
