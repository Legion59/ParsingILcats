namespace ParsingILcats.Models
{
    public class GroupModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LinkSubGroup { get; set; }
        public ConfigurationModel Configuration { get; set; }

    }
}