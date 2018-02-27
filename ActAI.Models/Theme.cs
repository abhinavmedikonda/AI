namespace ActAI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using Dapper;
    using System.Linq;

    [Table("Theme")]
    public class Theme
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual IList<Group> Groups { get; set; }

        public virtual Mapping Mapping { get; set; }
    }
}
