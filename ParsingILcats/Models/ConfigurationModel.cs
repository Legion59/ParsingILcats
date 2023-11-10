namespace ParsingILcats.Models
{
    public class ConfigurationModel
    {
        public int Id { get; set; }
        public string ConfigurationName { get; set; }
        public string DateRange { get; set; }
        public string LinkToGroupPage { get; set; }
        public List<string> Specs { get; set; } = new List<string>();

        public int CarId { get; set; }
        public CarModel Car { get; set; }

        public ICollection<GroupModel> Groups { get; set; }
    }
}