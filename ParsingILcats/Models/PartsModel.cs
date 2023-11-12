namespace ParsingILcats.Models
{
    public class PartsModel
    {
        public string Code { get; set; }
        public string Count { get; set; }
        public string Info { get; set; }
        public string TreeCode { get; set; }
        public string TreeName { get; set; }
        public string DateRange { get; set; }
        public string PictureName { get; set; }

        public int SubGroupId { get; set; }
        public SubGroupModel SubGroup { get; set; }
    }
}