namespace ActAI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Organisation")]
    public class Organisation
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<Mapping> Mappings { get; set; }
    }
}