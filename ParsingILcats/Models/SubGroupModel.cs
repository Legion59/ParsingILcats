namespace ParsingILcats.Models
{
    public class SubGroupModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LinkToParts { get; set; }
        public GroupModel Group { get; set; }
    }
}
