using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActAI.Models;

namespace ActAI.DataRepository.Interfaces
{
    public interface ITrendAnalysisNewKeywordsListRepository
    {
        List<TrendAnalysisNewKeywordsList> Get();

        TrendAnalysisNewKeywordsList Get(int Id);

        void Insert(string newKeyword);

        TrendAnalysisNewKeywordsList Update(TrendAnalysisNewKeywordsList newKeywordsList);

        void Delete(string newKeyword);
    }
}
