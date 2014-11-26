using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetflixAnalyze
{
    class RMSE
    {
        public List<Result> calculateScores(Dictionary<int, Movie> probeData)
        {
            List<Result> scores = new List<Result>();
            foreach(int movieID in probeData.Keys) 
            {
                foreach(int customerID in probeData[movieID].Ratings.Keys)
                {
                    double RMSE = 0;
                    //Predict score
                    Result tempResult = new Result(RMSE, movieID, customerID);
                    scores.Add(tempResult);
                }
            }
            return scores;
        }
    }
}
