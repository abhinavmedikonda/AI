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
    public class TrendAnalysisStopListRepository : ITrendAnalysisStopListRepository
    {
        public void Delete(string stopList)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query
                ("DELETE FROM trendanalysis_StopList WHERE StopKeywords = @StopList", new { StopList = stopList });
            }
        }

        public List<TrendAnalysisStopList> Get()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                return db.Query<TrendAnalysisStopList>
                ("SELECT * FROM trendanalysis_StopList").ToList();
            }
        }

        public TrendAnalysisStopList Get(int Id)
        {
            throw new NotImplementedException();
        }

        public void Insert(string stopList)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ConnectionString))
            {
                db.Query
                ("INSERT INTO trendanalysis_StopList(StopKeywords) " +
                "VALUES(@StopList)", new { StopList = stopList });
            }
        }

        public TrendAnalysisStopList Update(TrendAnalysisStopList stopList)
        {
            throw new NotImplementedException();
        }
    }
}
