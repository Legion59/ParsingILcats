namespace ParsingILcats.Models
{
    public class MarketModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string LinkCarModel { get; set; }

        public ICollection<CarModel> Cars { get; set; }
    }
}
