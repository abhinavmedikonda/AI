using ActAI.Models;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ActAI.DataRepository
{
    public class GroupRepository
    {
        public List<Group> GetByThemeID(int theThemeID)
        {
            List<Group> groups;

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                groups = db.Query<Group>
                ("SELECT * FROM [Group] WHERE ThemeID = @ThemeID", new { ThemeID = theThemeID }).ToList();
            }

            return this.GetReferanceData(groups);
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query
                ("DELETE FROM [Group] WHERE Id = @Id", new { Id = id });
            }
        }

        public List<Group> Get()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<Group>
                ("SELECT * FROM [Group]").ToList();
            }
        }

        public Group Get(int id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<Group>
                ("SELECT * FROM [Group] WHERE Id = @Id", new { Id = id }).First();
            }
        }

        public int Insert(Group group)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<int>
                ("INSERT INTO [Group](Name, ThemeID) OUTPUT Inserted.ID " +
                "VALUES(@Name, @ThemeID)", new { Name = group.Name, ThemeID = group.ThemeID }).First();
            }
        }

        public void Update(Group group)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query<bool>
                ("UPDATE [Group] SET Name = @Name " +
                "WHERE Id = @Id",
                new { Id = group.ID, Name = group.Name });
            }
        }

        private List<Group> GetReferanceData(List<Group> theGroups)
        {
            PhraseRepository phraseRepository = new PhraseRepository();
            ThemeRepository themeRepository = new ThemeRepository();

            foreach (Group group in theGroups)
            {
                group.Phrases = phraseRepository.GetByGroupID(group.ID);
                group.Theme = themeRepository.Get(group.ThemeID);
            }

            return theGroups;
        }
    }
}
