using ActAI.Models;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ActAI.DataRepository
{
    public class NewPhraseRepository
    {
        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query
                ("DELETE FROM NewPhrase WHERE Id = @Id", new { Id = id });
            }
        }

        public List<NewPhrase> Get()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<NewPhrase>
                ("SELECT * FROM NewPhrase").ToList();
            }
        }

        public NewPhrase Get(int id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<NewPhrase>
                ("SELECT * FROM NewPhrase WHERE Id = @Id", new { Id = id }).First();
            }
        }

        public void Insert(NewPhrase newPhrase)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query
                ("INSERT INTO NewPhrase(Name, StopList) " +
                "VALUES(@Name, @StopList)", new { Name = newPhrase.Name, StopList = newPhrase.StopList });
            }
        }

        public void Update(NewPhrase newPhrase)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query<bool>
                ("UPDATE NewPhrase SET Name = @Name, StopList = @StopList " +
                "WHERE Id = @Id",
                new { Id = newPhrase.ID, Name = newPhrase.Name, StopList = newPhrase.StopList });
            }
        }
    }
}
