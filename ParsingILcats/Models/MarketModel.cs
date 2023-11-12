namespace ParsingILcats.Models
{
    public class MarketModel
    {
        public string Code { get; set; }
        public string LinkCarModel { get; set; }

        public IEnumerable<CarModel> Cars { get; set; }
    }
}
