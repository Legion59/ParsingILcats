﻿namespace ParsingILcats.Models
{
    public class GroupModel
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public string LinkSubGroup { get; set; }

        public int ConfigurationId { get; set; }
        public ConfigurationModel Configuration { get; set; }

        public IEnumerable<SubGroupModel> SubGroups { get; set; }
    }
}