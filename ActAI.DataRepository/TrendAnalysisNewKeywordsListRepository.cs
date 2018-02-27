using ActAI.DataRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActAI.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

namespace ActAI.DataRepository
{
    public class TrendAnalysisNewKeywordsListRepository : ITrendAnalysisNewKeywordsListRepository
    {
        public void Delete(string newKeyword)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query
                ("DELETE FROM TrendAnalysis_NewKeywordsList WHERE TrendKeyword = @NewKeyword", new { NewKeyword = newKeyword });
            }
        }

        public List<TrendAnalysisNewKeywordsList> Get()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<TrendAnalysisNewKeywordsList>
                ("SELECT * FROM trendanalysis_NewKeywordsList").ToList();
            }
        }

        public TrendAnalysisNewKeywordsList Get(int Id)
        {
            throw new NotImplementedException();
        }

        public void Insert(string newKeyword)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query
                ("INSERT INTO trendanalysis_NewKeywordsList(TrendKeyword) " +
                "VALUES(@NewKeyword)", new { NewKeyword = newKeyword });
            }
        }

        public TrendAnalysisNewKeywordsList Update(TrendAnalysisNewKeywordsList newKeywordsList)
        {
            throw new NotImplementedException();
        }
    }
}
