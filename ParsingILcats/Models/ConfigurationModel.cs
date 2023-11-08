namespace ParsingILcats.Models
{
    public class ConfigurationModel
    {
        public string ConfigurationName { get; set; }
        public string DateRange { get; set; }
        public string LinkToGroupPage { get; set; }
        public List<string> Specs { get; set; } = new List<string>();

        public CarModel Car { get; set; }
    }
}