using ActAI.Models;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ActAI.DataRepository
{
    public class ApplicationRepository
    {
        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query
                ("DELETE FROM Application WHERE Id = @Id", new { Id = id });
            }
        }

        public List<Application> Get()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<Application>
                ("SELECT * FROM Application").ToList();
            }
        }

        public Application Get(int id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<Application>
                ("SELECT * FROM Application WHERE Id = @Id", new { Id = id }).First();
            }
        }

        public void Insert(Application application)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query
                ("INSERT INTO Application(Name) " +
                "VALUES(@Name)", new { Name = application.Name });
            }
        }

        public void Update(Application application)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query<bool>
                ("UPDATE Application SET Name = @Name " +
                "WHERE Id = @Id",
                new { Id = application.ID, Name = application.Name });
            }
        }
    }
}
