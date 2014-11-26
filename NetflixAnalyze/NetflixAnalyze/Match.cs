using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetflixAnalyze
{
    class Match
    {
        public static void matchData(Dictionary<int, Movie> trainingData, Dictionary<int, Movie> probeData)
        {
            List<int> movieIDs = new List<int>();
            List<int> customerIDs = new List<int>();
            foreach (int movieID in probeData.Keys)
            {
                for (int i = 0; i < probeData[movieID].Ratings.Count; i++)
                {
                    int customerID = probeData[movieID].Ratings.Keys.ElementAt(i);
                    probeData[movieID].Ratings[customerID] = trainingData[movieID].Ratings[customerID];
                    movieIDs.Add(movieID);
                    customerIDs.Add(customerID);
                }
            }
            for(int i = 0; i < movieIDs.Count; i++)
            {
                trainingData[movieIDs[i]].Ratings.Remove(customerIDs[i]);
            }
        }
    }
}
