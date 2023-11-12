using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ParsingILcats.Models
{
    public class ConfigurationModel
    {
        public string ConfigurationCode { get; set; }
        public string DateRange { get; set; }
        public string LinkToGroupPage { get; set; }
        public List<SpecModel> Specs { get; set; } = new List<SpecModel>();

        public int CarId { get; set; }
        public CarModel Car { get; set; }

        public IEnumerable<GroupModel> Groups { get; set; }
    }

    public class SpecModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Value { get; set; }

        public int ConfigurationId { get; set; }
        [ForeignKey("ConfigurationId")]
        public ConfigurationModel Configuration { get; set; }
    }
}