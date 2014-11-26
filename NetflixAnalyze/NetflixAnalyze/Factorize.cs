using ILNumerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetflixAnalyze
{
    class Factorize
    {
        public double[,] movieValues;
        public double[,] userValues;
        public List<int> idToUserIdList = new List<int>();
        public List<int> idToMovieIdList = new List<int>();
        public int k = 13;
        public Dictionary<int, Movie> ratingDic;

        public Factorize(Dictionary<int, Movie> inputDic)
        {
            // Loop over pairs with foreach
            foreach (int movieID in inputDic.Keys)
            {
                idToMovieIdList.Add(movieID);
                foreach (int userID in inputDic[movieID].Ratings.Keys)
                {
                    idToUserIdList.Add(userID);
                }
            }
            movieValues = new double[idToMovieIdList.Count, k];
            userValues = new double[idToUserIdList.Count, k];
            ratingDic = inputDic;
        }

        public void train()
        {

        }

        public double getRating(int movieID, int userID)
        {
            return ratingDic[movieID].Ratings[userID];
        }

        public static void matrix(double[,] inputMatrix)
        {
            ILInArray<double> ilArray = (ILInArray<double>)inputMatrix;
            int length = (int)Math.Sqrt(inputMatrix.Length);
            ILOutArray<double> outarray = (ILOutArray<double>)((ILArray<double>)new double[length, length]);
            ILRetArray<double> eigresult = ILMath.svd(ilArray, outarray);
        }
    }
}
