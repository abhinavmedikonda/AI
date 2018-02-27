namespace ActAI.Web.NewFolder1
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Phrase")]
    public class Phrase
    {
        public int ID { get; set; }

        public int? GroupID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Name { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual Group Group { get; set; }
    }
}
