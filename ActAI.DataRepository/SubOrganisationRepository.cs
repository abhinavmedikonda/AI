using ActAI.Models;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ActAI.DataRepository
{
    public class SubOrganisationRepository
    {
        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query
                ("DELETE FROM SubOrganisation WHERE Id = @Id", new { Id = id });
            }
        }

        public List<SubOrganisation> Get()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<SubOrganisation>
                ("SELECT * FROM SubOrganisation").ToList();
            }
        }

        public SubOrganisation Get(int id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<SubOrganisation>
                ("SELECT * FROM SubOrganisation WHERE Id = @Id", new { Id = id }).First();
            }
        }

        public void Insert(SubOrganisation subOrganisation)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query
                ("INSERT INTO SubOrganisation(Name) " +
                "VALUES(@Name)", new { Name = subOrganisation.Name });
            }
        }

        public void Update(SubOrganisation subOrganisation)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query<bool>
                ("UPDATE SubOrganisation SET Name = @Name " +
                "WHERE Id = @Id",
                new { Id = subOrganisation.ID, Name = subOrganisation.Name });
            }
        }
    }
}
