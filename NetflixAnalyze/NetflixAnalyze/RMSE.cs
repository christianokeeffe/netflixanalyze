using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetflixAnalyze
{
    class RMSE
    {
        public double calculateScores(Dictionary<int, Movie> probeData)
        {
            foreach(int movieID in probeData.Keys) 
            {
                foreach(int customerID in probeData[movieID].Ratings.Keys)
                {
                    //Predict score
                }
            }
            return 0;
        }
    }
}
