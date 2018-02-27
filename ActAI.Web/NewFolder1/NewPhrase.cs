namespace ActAI.Web.NewFolder1
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("NewPhrase")]
    public class NewPhrase
    {
        public int ID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Name { get; set; }

        public bool? StopList { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
