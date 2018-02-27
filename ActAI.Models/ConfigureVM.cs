using System;
using System.Collections.Generic;
using System.Text;

namespace ActAI.Models
{
    public class ConfigureVM
    {
        public List<Organisation> Organisations { get; set; }

        public List<SubOrganisation> SubOrganisations { get; set; }

        public List<Application> Applications { get; set; }

        public List<Theme> Themes { get; set; }

        public TrendAnalysisTheme trendAnalysisTheme { get; set; }
    }
}
