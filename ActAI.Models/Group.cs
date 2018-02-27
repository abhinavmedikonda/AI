namespace ActAI.Models
{
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    [Table("Group")]
    public class Group
    {
        public int ID { get; set; }

        public int ThemeID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual Theme Theme { get; set; }

        public virtual IList<Phrase> Phrases { get; set; }
    }
}
