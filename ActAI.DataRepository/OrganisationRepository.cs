using ActAI.Models;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ActAI.DataRepository
{
    public class OrganisationRepository
    {
        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query
                ("DELETE FROM Organisation WHERE Id = @Id", new { Id = id });
            }
        }

        public List<Organisation> Get()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<Organisation>
                ("SELECT * FROM Organisation").ToList();
            }
        }

        public Organisation Get(int id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<Organisation>
                ("SELECT * FROM Organisation WHERE Id = @Id", new { Id = id }).First();
            }
        }

        public void Insert(Organisation organisation)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query
                ("INSERT INTO Organisation(Name) " +
                "VALUES(@Name)", new { Name = organisation.Name });
            }
        }

        public void Update(Organisation organisation)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query<bool>
                ("UPDATE Organisation SET Name = @Name " +
                "WHERE Id = @Id",
                new { Id = organisation.ID, Name = organisation.Name });
            }
        }
    }
}
