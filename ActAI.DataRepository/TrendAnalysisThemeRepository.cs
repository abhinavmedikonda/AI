using System;
using Dapper;
using ActAI.DataRepository.Interfaces;
using ActAI.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Linq;

namespace ActAI.DataRepository
{
    public class TrendAnalysisThemeRepository : ITrendAnalysisThemeRepository
    {
        public List<Organisation> Organisations()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<Organisation>
                ("SELECT * FROM Organisation WHERE Name IS NOT NULL " +
                "ORDER BY Name").ToList();
            }
        }

        public List<SubOrganisation> SubOrganisations()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<SubOrganisation>
                ("SELECT * FROM SubOrganisation WHERE Name IS NOT NULL " +
                "ORDER BY Name").ToList();
            }
        }

        public List<Application> Applications()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<Application>
                ("SELECT * FROM Application WHERE Name IS NOT NULL " +
                "ORDER BY Name").ToList();
            }
        }

        public List<TrendAnalysisTheme> GetThemes(string organisation, string subOrganisation, string application)
        {
            string query = "select TrendThemeID, assignment_group_parent_parent, assignment_group_parent, configuration_item_application, TrendThemeGroup, " +
                                    "TrendKeyWordGroup, TrendKeyword from trendanalysis_themes (nolock) " +
                         "WHERE " + (string.IsNullOrEmpty(organisation) ? "assignment_group_parent_parent IS NULL " : "assignment_group_parent_parent = @Organisation ") +
                         "AND " + (string.IsNullOrEmpty(subOrganisation) ? "assignment_group_parent IS NULL " : "assignment_group_parent = @SubOrganisation ") +
                         "AND " + (string.IsNullOrEmpty(application) ? "configuration_item_application IS NULL " : "configuration_item_application = @Application ") +
                         "order by 1, 2, 3, 4, 5, 6 ";
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<TrendAnalysisTheme>
                (query, new { Organisation = organisation, SubOrganisation = subOrganisation, Application = application }).ToList();
            }
        }

        public List<TrendAnalysisTheme> Get()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<TrendAnalysisTheme>
                ("select TrendThemeID, assignment_group_parent_parent, assignment_group_parent, configuration_item_application, " +
                "TrendThemeGroup, TrendKeyWordGroup, TrendKeyword from trendanalysis_themes (nolock) order by 1, 2, 3, 4, 5, 6 ").ToList();
            }
        }

        public TrendAnalysisTheme Get(int id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<TrendAnalysisTheme>
                ("select TrendThemeID, assignment_group_parent_parent, assignment_group_parent, configuration_item_application, " +
                "TrendThemeGroup, TrendKeyWordGroup, TrendKeyword from trendanalysis_themes (nolock) " +
                "WHERE TrendThemeID = @id order by 1, 2, 3, 4, 5, 6 ", new { Id = id }).ToList().FirstOrDefault();
            }
        }

        public bool TrendExists(TrendAnalysisTheme trend)
        {
            string query = "SELECT CASE WHEN EXISTS (" +
                "select * from trendanalysis_themes (nolock) " +
                "WHERE " + (string.IsNullOrEmpty(trend.assignment_group_parent_parent) ? "assignment_group_parent_parent IS NULL " : "assignment_group_parent_parent = @Organisation ") +
                "AND " + (string.IsNullOrEmpty(trend.assignment_group_parent) ? "assignment_group_parent IS NULL " : "assignment_group_parent = @SubOrganisation ") +
                "AND " + (string.IsNullOrEmpty(trend.configuration_item_application) ? "configuration_item_application IS NULL " : "configuration_item_application = @Application ") +
                "AND " + (string.IsNullOrEmpty(trend.TrendThemeGroup) ? "TrendThemeGroup IS NULL " : "TrendThemeGroup = @Theme ") +
                "AND " + (string.IsNullOrEmpty(trend.TrendKeyWordGroup) ? "TrendKeyWordGroup IS NULL " : "TrendKeyWordGroup = @Group ") +
                "AND " + (string.IsNullOrEmpty(trend.TrendKeyword) ? "TrendKeyword IS NULL " : "TrendKeyword = @Keyword ") +
                ") THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END";

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<bool>
                (query, new
                {
                    Organisation = trend.assignment_group_parent_parent,
                    SubOrganisation = trend.assignment_group_parent,
                    Application = trend.configuration_item_application,
                    Theme = trend.TrendThemeGroup,
                    Group = trend.TrendKeyWordGroup,
                    Keyword = trend.TrendKeyword
                }).ToList().First();

            }
        }

        public bool NullKeywordExists(TrendAnalysisTheme trend)
        {
            string query = "SELECT CASE WHEN EXISTS (" +
                "select * from trendanalysis_themes (nolock) " +
                "WHERE " + (string.IsNullOrEmpty(trend.assignment_group_parent_parent) ? "assignment_group_parent_parent IS NULL " : "assignment_group_parent_parent = @Organisation ") +
                "AND " + (string.IsNullOrEmpty(trend.assignment_group_parent) ? "assignment_group_parent IS NULL " : "assignment_group_parent = @SubOrganisation ") +
                "AND " + (string.IsNullOrEmpty(trend.configuration_item_application) ? "configuration_item_application IS NULL " : "configuration_item_application = @Application ") +
                "AND " + (string.IsNullOrEmpty(trend.TrendThemeGroup) ? "TrendThemeGroup IS NULL " : "TrendThemeGroup = @Theme ") +
                "AND " + (string.IsNullOrEmpty(trend.TrendKeyWordGroup) ? "TrendKeyWordGroup IS NULL " : "TrendKeyWordGroup = @Group ") +
                "AND TrendKeyword IS NULL) " +
                "THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END";

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<bool>
                (query, new
                {
                    Organisation = trend.assignment_group_parent_parent,
                    SubOrganisation = trend.assignment_group_parent,
                    Application = trend.configuration_item_application,
                    Theme = trend.TrendThemeGroup,
                    Group = trend.TrendKeyWordGroup
                }).ToList().First();
            }
        }

        public void UpdateGroup(string group, string keyword)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query<bool>
                ("UPDATE trendanalysis_themes SET TrendKeywordGroup = @Group WHERE TrendKeyword = @Keyword",
                    new { Group = group, Keyword = keyword });
            }
        }

        public void UpdateTheme(string theme, string group)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query<bool>
                ("UPDATE trendanalysis_themes SET TrendThemeGroup = @Theme WHERE TrendKeywordGroup = @Group",
                    new { Theme = theme, Group = group });
            }
        }

        public int InsertTrend(TrendAnalysisTheme trend)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.QuerySingle<int>
                ("DECLARE @Id integer " +
                "SELECT @Id = MAX(TrendThemeID)+1 FROM TrendAnalysis_Themes " +
                "INSERT INTO trendanalysis_themes (TrendThemeID, assignment_group_parent_parent, assignment_group_parent, " +
                "configuration_item_application, TrendThemeGroup, TrendKeyWordGroup, TrendKeyword) " +
                "VALUES(@Id, @Organisation, " +
                "@SubOrganisation, @Application, @Theme, @Group, @Keyword) " +
                "SELECT @Id",
                new
                {
                    Organisation = trend.assignment_group_parent_parent,
                    SubOrganisation = trend.assignment_group_parent,
                    Application = trend.configuration_item_application,
                    Theme = trend.TrendThemeGroup,
                    Group = trend.TrendKeyWordGroup,
                    Keyword = trend.TrendKeyword
                });
            }
        }

        public bool NullThemeExists(TrendAnalysisTheme trend)
        {
            string query = "SELECT CASE WHEN EXISTS (" +
                "select * from trendanalysis_themes (nolock) " +
                "WHERE " + (string.IsNullOrEmpty(trend.assignment_group_parent_parent) ? "assignment_group_parent_parent IS NULL " : "assignment_group_parent_parent = @Organisation ") +
                "AND " + (string.IsNullOrEmpty(trend.assignment_group_parent) ? "assignment_group_parent IS NULL " : "assignment_group_parent = @SubOrganisation ") +
                "AND " + (string.IsNullOrEmpty(trend.configuration_item_application) ? "configuration_item_application IS NULL " : "configuration_item_application = @Application ") +
                "AND TrendThemeGroup IS NULL) " +
                "THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END";

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<bool>
                (query, new
                {
                    Organisation = trend.assignment_group_parent_parent,
                    SubOrganisation = trend.assignment_group_parent,
                    Application = trend.configuration_item_application
                }).ToList().First();
            }
        }

        public bool NullGroupExists(TrendAnalysisTheme trend)
        {
            string query = "SELECT CASE WHEN EXISTS (" +
                "select * from trendanalysis_themes (nolock) " +
                "WHERE " + (string.IsNullOrEmpty(trend.assignment_group_parent_parent) ? "assignment_group_parent_parent IS NULL " : "assignment_group_parent_parent = @Organisation ") +
                "AND " + (string.IsNullOrEmpty(trend.assignment_group_parent) ? "assignment_group_parent IS NULL " : "assignment_group_parent = @SubOrganisation ") +
                "AND " + (string.IsNullOrEmpty(trend.configuration_item_application) ? "configuration_item_application IS NULL " : "configuration_item_application = @Application ") +
                "AND TrendThemeGroup = @Theme " +
                "AND TrendKeyWordGroup IS NULL) " +
                "THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END";

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<bool>
                (query, new
                {
                    Organisation = trend.assignment_group_parent_parent,
                    SubOrganisation = trend.assignment_group_parent,
                    Application = trend.configuration_item_application,
                    Theme = trend.TrendThemeGroup
                }).ToList().First();
            }
        }

        public int UpdateNullTheme(TrendAnalysisTheme trend)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.QuerySingle<int>
                ("DECLARE @Id integer " +
                "SELECT @Id = TrendThemeID FROM TrendAnalysis_Themes " +
                "WHERE " + (string.IsNullOrEmpty(trend.assignment_group_parent_parent) ? "assignment_group_parent_parent IS NULL " : "assignment_group_parent_parent = @Organisation ") +
                "AND " + (string.IsNullOrEmpty(trend.assignment_group_parent) ? "assignment_group_parent IS NULL " : "assignment_group_parent = @SubOrganisation ") +
                "AND " + (string.IsNullOrEmpty(trend.configuration_item_application) ? "configuration_item_application IS NULL " : "configuration_item_application = @Application ") +
                "AND TrendThemeGroup IS NULL " +
                "UPDATE trendanalysis_themes SET TrendThemeGroup = @Theme WHERE TrendThemeID = @Id " +
                "SELECT @Id ",
                new
                {
                    Organisation = trend.assignment_group_parent_parent,
                    SubOrganisation = trend.assignment_group_parent,
                    Application = trend.configuration_item_application,
                    Theme = trend.TrendThemeGroup
                });
            }
        }

        public int UpdateNullGroup(TrendAnalysisTheme trend)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.QuerySingle<int>
                ("DECLARE @Id integer " +
                "SELECT @Id = TrendThemeID FROM TrendAnalysis_Themes " +
                "WHERE " + (string.IsNullOrEmpty(trend.assignment_group_parent_parent) ? "assignment_group_parent_parent IS NULL " : "assignment_group_parent_parent = @Organisation ") +
                "AND " + (string.IsNullOrEmpty(trend.assignment_group_parent) ? "assignment_group_parent IS NULL " : "assignment_group_parent = @SubOrganisation ") +
                "AND " + (string.IsNullOrEmpty(trend.configuration_item_application) ? "configuration_item_application IS NULL " : "configuration_item_application = @Application ") +
                "AND TrendThemeGroup = @Theme " +
                "AND TrendKeyWordGroup IS NULL " +
                "UPDATE trendanalysis_themes SET TrendKeyWordGroup = @Group WHERE TrendThemeID = @Id " +
                "SELECT @Id ",
                new
                {
                    Organisation = trend.assignment_group_parent_parent,
                    SubOrganisation = trend.assignment_group_parent,
                    Application = trend.configuration_item_application,
                    Theme = trend.TrendThemeGroup,
                    Group = trend.TrendKeyWordGroup
                });
            }
        }

        public int updateNullKeyword(TrendAnalysisTheme trend)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.QuerySingle<int>
                ("DECLARE @Id integer " +
                "SELECT @Id = TrendThemeID FROM TrendAnalysis_Themes " +
                "WHERE " + (string.IsNullOrEmpty(trend.assignment_group_parent_parent) ? "assignment_group_parent_parent IS NULL " : "assignment_group_parent_parent = @Organisation ") +
                "AND " + (string.IsNullOrEmpty(trend.assignment_group_parent) ? "assignment_group_parent IS NULL " : "assignment_group_parent = @SubOrganisation ") +
                "AND " + (string.IsNullOrEmpty(trend.configuration_item_application) ? "configuration_item_application IS NULL " : "configuration_item_application = @Application ") +
                "AND TrendThemeGroup = @Theme " +
                "AND TrendKeyWordGroup = @Group " +
                "AND TrendKeyword IS NULL " +
                "UPDATE trendanalysis_themes SET TrendKeyword = @Keyword WHERE TrendThemeID = @Id " +
                "SELECT @Id ",
                new
                {
                    Organisation = trend.assignment_group_parent_parent,
                    SubOrganisation = trend.assignment_group_parent,
                    Application = trend.configuration_item_application,
                    Theme = trend.TrendThemeGroup,
                    Group = trend.TrendKeyWordGroup,
                    Keyword = trend.TrendKeyword
                });
            }
        }

        public Theme Insert(int Id, string Name)
        {
            throw new NotImplementedException();
        }

        public Theme Update(int Id, string Name)
        {
            throw new NotImplementedException();
        }

        public Theme Delete(int Id, string Name)
        {
            throw new NotImplementedException();
        }
    }
}
