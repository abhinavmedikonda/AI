namespace ActAI.Models
{
    using Dapper;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    [Table("Phrase")]
    public class Phrase
    {
        public int ID { get; set; }

        public int GroupID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Name { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual Group Group { get; set; }
    }
}
