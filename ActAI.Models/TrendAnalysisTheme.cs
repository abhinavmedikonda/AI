using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ActAI.Models
{
    public class TrendAnalysisTheme
    {
        public int TrendThemeID { get; set; }

        [Column("assignment_group_parent_parent")]
        public string assignment_group_parent_parent { get; set; }

        [Column("assignment_group_parent")]
        public string assignment_group_parent { get; set; }

        [Column("configuration_item_application")]
        public string configuration_item_application { get; set; }

        public string TrendThemeGroup { get; set; }

        public string TrendKeyWordGroup { get; set; }

        public string TrendKeyword { get; set; }
    }
}
