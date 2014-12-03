using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetflixAnalyze
{
    class RMSE
    {
        public double calculateScores(Dictionary<int, Movie> probeData, Dictionary<int, Movie> traningdata, Factorize fac)
        {
            double returnVal = 0;
            int reviewNumb = 0;
            PreProcess pp = new PreProcess(traningdata);
            foreach(int movieID in probeData.Keys) 
            {
                foreach(int customerID in probeData[movieID].Ratings.Keys)
                {
                    reviewNumb++;
                    try
                    {
                        returnVal += Math.Pow(pp.postProcess(probeData[movieID], fac.predictRating(movieID, customerID), customerID) - probeData[movieID].Ratings[customerID], 2);
                    }
                    catch(KeyNotFoundException e)
                    {
                        reviewNumb--;
                    }
                }
            }
            return Math.Sqrt(returnVal/reviewNumb);
        }
    }
}
