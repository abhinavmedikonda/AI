using System;
using System.Collections.Generic;
using System.Text;
using ActAI.Models;

namespace ActAI.DataRepository.Interfaces
{
    public interface ITrendAnalysisThemeRepository
    {
        List<Organisation> Organisations();

        List<SubOrganisation> SubOrganisations();

        List<Application> Applications();

        List<TrendAnalysisTheme> Get();

        TrendAnalysisTheme Get(int id);

        Theme Insert(int Id, string Name);

        Theme Update(int Id, string Name);

        Theme Delete(int Id, string Name);
    }
}
