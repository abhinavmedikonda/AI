using ActAI.Models;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ActAI.DataRepository
{
    public class MappingRepository
    {
        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query
                ("DELETE FROM Mapping WHERE Id = @Id", new { Id = id });
            }
        }

        public Mapping GetByThemeID(int theThemeID)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<Mapping>
                ("SELECT * FROM Mapping WHERE ThemeID = @ThemeID", new { ThemeID = theThemeID }).First();
            }
        }

        public List<Mapping> Get()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<Mapping>
                ("SELECT * FROM Mapping").ToList();
            }
        }

        public Mapping Get(int id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<Mapping>
                ("SELECT * FROM Mapping WHERE Id = @Id", new { Id = id }).First();
            }
        }

        public void Insert(Mapping mapping)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query
                ("INSERT INTO Mapping(OrganisationID, SubOrganisationID, ApplicationID, ThemeID) " +
                "VALUES(@OrganisationID, @SubOrganisationID, @ApplicationID, @ThemeID)",
                new
                {
                    OrganisationID = mapping.OrganisationID,
                    SubOrganisationID = mapping.SubOrganisationID,
                    ApplicationID = mapping.ApplicationID,
                    ThemeID = mapping.ThemeID
                });
            }
        }

        public void Update(Mapping mapping)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query<bool>
                ("UPDATE Mapping SET OrganisationID = @OrganisationID, SubOrganisationID = @SubOrganisationID," +
                "ApplicationID = @ApplicationID, ThemeID = @ThemeID " +
                "WHERE Id = @Id",
                new
                {
                    Id = mapping.ID,
                    OrganisationID = mapping.OrganisationID,
                    SubOrganisationID = mapping.SubOrganisationID,
                    ApplicationID = mapping.ApplicationID,
                    ThemeID = mapping.ThemeID
                });
            }
        }
    }
}
