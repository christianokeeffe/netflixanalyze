using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetflixAnalyze
{
    class PreProcess
    {
        public static void preProcessData(Dictionary<int, Movie> inputDic)
        {
            int pairCount = 0;
            double ratingSum = 0.0;
            Dictionary<int, Dictionary<int, double>> userRatings = new Dictionary<int, Dictionary<int, double>>();
            // Loop over pairs with foreach
            foreach (KeyValuePair<int, Movie> movie in inputDic)
            {
                // Loop over pairs with foreach
                foreach (KeyValuePair<int, double> rating in movie.Value.Ratings)
                {
                    pairCount++;
                    ratingSum = rating.Value;
                    if(userRatings.ContainsKey(rating.Key))
                    {
                        userRatings[rating.Key].Add(movie.Key, rating.Value);
                    }
                    else
                    {
                        Dictionary<int, double> dic = new Dictionary<int,double>();
                        dic.Add(movie.Key, rating.Value);
                        userRatings.Add(rating.Key,dic);
                    }
                }
            }
            Dictionary<int, Movie> returnDic = new Dictionary<int, Movie>();
            foreach (KeyValuePair<int, Movie> movie in inputDic)
            {
                Movie tempMovie = new Movie();
                tempMovie.movieID = movie.Key;
                double movieratingsum = 0.0;

                // Loop over pairs with foreach
                foreach (int id in movie.Value.Ratings.Keys)
                {
                    movieratingsum += movie.Value.Ratings[id];
                }

                // Loop over pairs with foreach
                foreach (int id in movie.Value.Ratings.Keys)
                {
                    double userRatingSum = 0.0;
                    // Loop over pairs with foreach
                    foreach (KeyValuePair<int, double> rating in userRatings[id])
                    {/////////////////OPTIMER//////////////
                        userRatingSum += rating.Value;
                    }

                    tempMovie.Ratings.Add(id, movie.Value.Ratings[id] - (1/(double)movie.Value.Ratings.Count)*movieratingsum - (1/(double)userRatings[id].Count)*userRatingSum - ((double)pairCount)*ratingSum); 
                }

                returnDic.Add(tempMovie.movieID,tempMovie);
            }
        }
    }
}
