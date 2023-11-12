namespace ParsingILcats.Models
{
    public class SubGroupModel
    {
        public int Id { get; set; }
        public string Index { get; set; }
        public string Name { get; set; }
        public string LinkToParts { get; set; }

        public int GroupId { get; set; }
        public GroupModel Group { get; set; }

        public IEnumerable<PartsModel> Parts { get; set; }
    }
}
