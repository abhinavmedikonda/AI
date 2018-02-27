using ActAI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ActAI.DataRepository.Helper
{
    public class TrendAnalysisThemeRepositoryHelper
    {
        public List<Theme> TableToThemeEntity(List<TrendAnalysisTheme> trendAnalysisThemes)
        {
            List<Theme> themes = new List<Theme>();
            List<Group> groups = new List<Group>();
            List<Phrase> phrases = new List<Phrase>();

            foreach (TrendAnalysisTheme trendAnalysisTheme in trendAnalysisThemes)
            {
                if (!themes.Any(x => x.Name == trendAnalysisTheme.TrendThemeGroup) && !string.IsNullOrEmpty(trendAnalysisTheme.TrendThemeGroup))
                {
                    themes.Add(new Theme() { ID = trendAnalysisTheme.TrendThemeID, Name = trendAnalysisTheme.TrendThemeGroup });
                }

                groups = themes.Find(x => x.Name == trendAnalysisTheme.TrendThemeGroup)?.Groups.ToList();
                if (groups != null && !groups.Any(x => x.Name == trendAnalysisTheme.TrendKeyWordGroup) && !string.IsNullOrEmpty(trendAnalysisTheme.TrendKeyWordGroup))
                {
                    groups.Add(new Group() { ID = trendAnalysisTheme.TrendThemeID, Name = trendAnalysisTheme.TrendKeyWordGroup });
                }

                phrases = groups.Find(x => x.Name == trendAnalysisTheme.TrendKeyWordGroup)?.Phrases.ToList();
                if (phrases != null && !phrases.Any(x => x.Name == trendAnalysisTheme.TrendKeyword) && !string.IsNullOrEmpty(trendAnalysisTheme.TrendKeyword))
                {
                    phrases.Add(new Phrase() { ID = trendAnalysisTheme.TrendThemeID, Name = trendAnalysisTheme.TrendKeyword });
                }
            }

            return themes;
        }

        public int InsertTheme(TrendAnalysisTheme trendAnalysisTheme)
        {
            TrendAnalysisThemeRepository trendRepository = new TrendAnalysisThemeRepository();

            if (trendRepository.TrendExists(trendAnalysisTheme))
            {
                return 0;
            }

            if (trendRepository.NullThemeExists(trendAnalysisTheme))
            {
                return trendRepository.UpdateNullTheme(trendAnalysisTheme);
            }
            else
            {
                return trendRepository.InsertTrend(trendAnalysisTheme);
            }
        }

        public int InsertGroup(TrendAnalysisTheme trendAnalysisTheme)
        {
            TrendAnalysisThemeRepository trendRepository = new TrendAnalysisThemeRepository();

            if (trendRepository.TrendExists(trendAnalysisTheme))
            {
                return 0;
            }

            if (trendRepository.NullGroupExists(trendAnalysisTheme))
            {
                return trendRepository.UpdateNullGroup(trendAnalysisTheme);
            }
            else
            {
                return trendRepository.InsertTrend(trendAnalysisTheme);
            }
        }

        public int InsertKeyword(TrendAnalysisTheme trendAnalysisTheme)
        {
            TrendAnalysisThemeRepository trendRepository = new TrendAnalysisThemeRepository();

            if (trendRepository.TrendExists(trendAnalysisTheme))
            {
                return 0;
            }

            if (trendRepository.NullKeywordExists(trendAnalysisTheme))
            {
                return trendRepository.updateNullKeyword(trendAnalysisTheme);
            }
            else
            {
                return trendRepository.InsertTrend(trendAnalysisTheme);
            }
        }

        public int InsertKeyword(int id, string keyword)
        {
            TrendAnalysisThemeRepository trendRepository = new TrendAnalysisThemeRepository();

            TrendAnalysisTheme trendAnalysisTheme = trendRepository.Get(id);
            trendAnalysisTheme.TrendKeyword = keyword;

            return this.InsertKeyword(trendAnalysisTheme);
        }
    }
}
