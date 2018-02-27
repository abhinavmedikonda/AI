using ActAI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActAI.DataRepository.Interfaces
{
    public interface ITrendAnalysisStopListRepository
    {
        List<TrendAnalysisStopList> Get();

        TrendAnalysisStopList Get(int Id);

        void Insert(string stopList);

        TrendAnalysisStopList Update(TrendAnalysisStopList stopList);

        void Delete(string stopList);
    }
}
