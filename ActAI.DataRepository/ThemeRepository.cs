using ActAI.Models;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ActAI.DataRepository
{
    public class ThemeRepository
    {
        public List<Theme> DropDownSelect(int organisationID, int subOrganisationID, int applicationID)
        {
            List<Theme> themes;

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                string query = "SELECT * FROM Theme " +
                "WHERE ID IN (SELECT ThemeID FROM Mapping " +
                "WHERE " + (organisationID == 0 ? "OrganisationID IS NULL " : "OrganisationID = @OrganisationID ") +
                "AND " + (subOrganisationID == 0 ? "SubOrganisationID IS NULL " : "SubOrganisationID = @SubOrganisationID ") +
                "AND " + (applicationID == 0 ? "ApplicationID IS NULL " : "ApplicationID = @ApplicationID ") + ") ";
                var param = new
                {
                    OrganisationID = organisationID,
                    SubOrganisationID = subOrganisationID,
                    ApplicationID = applicationID
                };

                themes = db.Query<Theme>(query, param).ToList();
            }

            return this.GetReferanceData(themes);
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query
                ("DELETE FROM Theme WHERE Id = @Id", new { Id = id });
            }
        }

        public List<Theme> Get()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<Theme>
                ("SELECT * FROM Theme").ToList();
            }
        }

        public Theme Get(int id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<Theme>
                ("SELECT * FROM Theme WHERE Id = @Id", new { Id = id }).First();
            }
        }

        public int Insert(Theme theme)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<int>
                ("INSERT INTO Theme(Name) OUTPUT Inserted.ID " +
                "VALUES(@Name)", new { Name = theme.Name }).First();
            }
        }

        public void Update(Theme theme)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query<bool>
                ("UPDATE Theme SET Name = @Name " +
                "WHERE Id = @Id",
                new { Id = theme.ID, Name = theme.Name });
            }
        }

        private List<Theme> GetReferanceData(List<Theme> theThemes)
        {
            GroupRepository groupRepository = new GroupRepository();
            MappingRepository mappingRepository = new MappingRepository();

            foreach (Theme theme in theThemes)
            {
                theme.Groups = groupRepository.GetByThemeID(theme.ID);
                theme.Mapping = mappingRepository.GetByThemeID(theme.ID);
            }

            return theThemes;
        }
    }
}
