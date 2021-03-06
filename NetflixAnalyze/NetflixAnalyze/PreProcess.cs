﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetflixAnalyze
{
    class PreProcess
    {
        int pairCount = 0;
        double ratingSum = 0.0;
        Dictionary<int, Dictionary<int, double>> userRatings = new Dictionary<int, Dictionary<int, double>>();
        public PreProcess(Dictionary<int, Movie> inputDic)
        {
            
            // Loop over pairs with foreach
            foreach (KeyValuePair<int, Movie> movie in inputDic)
            {
                // Loop over pairs with foreach
                foreach (KeyValuePair<int, double> rating in movie.Value.Ratings)
                {
                    pairCount++;
                    ratingSum += rating.Value;
                    if (userRatings.ContainsKey(rating.Key))
                    {
                        userRatings[rating.Key].Add(movie.Key, rating.Value);
                    }
                    else
                    {
                        Dictionary<int, double> dic = new Dictionary<int, double>();
                        dic.Add(movie.Key, rating.Value);
                        userRatings.Add(rating.Key, dic);
                    }
                }
            }
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

                movie.Value.movieRatingSum = movieratingsum;
            }
        }

        public double postProcess(Movie movie, double rating, int userID)
        {
                double userRatingSum = 0.0;
                // Loop over pairs with foreach
                foreach (KeyValuePair<int, double> rat in userRatings[userID])
                {/////////////////OPTIMER//////////////
                    userRatingSum += rat.Value;
                }
                double part1 = (1 / (double)movie.Ratings.Count) * movie.movieRatingSum;
                double part2 = (1 / (double)userRatings[userID].Count) * userRatingSum;
                double part3 = (1 / (double)pairCount) * ratingSum;
                double result = rating + part1 + part2 - part3;
                return result;
            
        }
        public static Dictionary<int, Movie> prePostProcessData(Dictionary<int, Movie> inputDic, int factor)
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
                    ratingSum += rating.Value;
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
                    double test = movie.Value.Ratings[id];
                    double part1 = (1 / (double)movie.Value.Ratings.Count) * movieratingsum;
                    double part2 = (1 / (double)userRatings[id].Count) * userRatingSum;
                    double part3 =  (1/(double)pairCount)*ratingSum;
                    double result = test + factor * part1 + factor * part2 - factor * part3;
                    tempMovie.Ratings.Add(id, result); 
                }

                returnDic.Add(tempMovie.movieID,tempMovie);
            }
            return returnDic;
        }
    }
}
