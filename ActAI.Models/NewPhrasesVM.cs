using System;
using System.Collections.Generic;
using System.Text;

namespace ActAI.Models
{
    public class NewPhrasesVM
    {
        public List<Organisation> Organisations { get; set; }

        public List<SubOrganisation> SubOrganisations { get; set; }

        public List<Application> Applications { get; set; }

        public List<Theme> Themes { get; set; }

        //public TrendAnalysisTheme trendAnalysisTheme { get; set; }

        public List<NewPhrase> NewPhrases { get; set; }

        public List<NewPhrase> StopLists { get; set; }
    }
}
