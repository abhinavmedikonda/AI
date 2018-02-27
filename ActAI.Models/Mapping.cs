namespace ActAI.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Mapping")]
    public class Mapping
    {
        public int ID { get; set; }

        public int? OrganisationID { get; set; }

        public int? SubOrganisationID { get; set; }

        public int? ApplicationID { get; set; }

        public int ThemeID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual Application Application { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual SubOrganisation SubOrganisation { get; set; }

        public virtual Theme Theme { get; set; }
    }
}
