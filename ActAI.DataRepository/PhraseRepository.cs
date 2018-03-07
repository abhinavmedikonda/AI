using ActAI.Models;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ActAI.DataRepository
{
    public class PhraseRepository
    {
        public List<Phrase> GetByGroupID(int theGroupID)
        {
            List<Phrase> phrases;

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                phrases = db.Query<Phrase>
                ("SELECT * FROM Phrase WHERE GroupID = @GroupID", new { GroupID = theGroupID }).ToList();
            }

            return this.GetReferanceData(phrases);
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query
                ("DELETE FROM Phrase WHERE Id = @Id", new { Id = id });
            }
        }

        public List<Phrase> Get()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<Phrase>
                ("SELECT * FROM Phrase").ToList();
            }
        }

        public Phrase Get(int id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<Phrase>
                ("SELECT * FROM Phrase WHERE Id = @Id", new { Id = id }).First();
            }
        }

        public int Insert(Phrase phrase)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<int>
                ("INSERT INTO Phrase(Name, GroupID) OUTPUT Inserted.ID " +
                "VALUES(@Name, @GroupID)", new { Name = phrase.Name, GroupID = phrase.GroupID }).First();
            }
        }

        public void Update(Phrase phrase)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query<bool>
                ("UPDATE Phrase SET Name = @Name, GroupID = @GroupID " +
                "WHERE Id = @Id",
                new { Id = phrase.ID, Name = phrase.Name, GroupID = phrase.GroupID });
            }
        }

        private List<Phrase> GetReferanceData(List<Phrase> thePhrases)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                foreach (Phrase phrase in thePhrases)
                {
                    phrase.Group = db.Query<Group>
                    ("SELECT * FROM [Group] WHERE ID = @ID ", new { ID = phrase.GroupID }).FirstOrDefault();
                }
            }

            return thePhrases;
        }
    }
}
