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
        public double lrate = 0.001;
        public Dictionary<int, Movie> ratingDic;

        public Factorize(Dictionary<int, Movie> inputDic)
        {
            // Loop over pairs with foreach
            foreach (int movieID in inputDic.Keys)
            {
                idToMovieIdList.Add(movieID);
                foreach (int userID in inputDic[movieID].Ratings.Keys)
                {
                    if (!idToUserIdList.Contains(userID))
                    {
                        idToUserIdList.Add(userID);
                    }
                }
            }
            movieValues = new double[idToMovieIdList.Count, k];
            userValues = new double[idToUserIdList.Count, k];
            ratingDic = inputDic;
            for(int i = 0; i < idToMovieIdList.Count; i++)
            {
                for(int j = 0; j < k; j++)
                {
                    movieValues[i, j] = 0.1;
                }
            }
            for (int i = 0; i < idToUserIdList.Count; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    userValues[i, j] = 0.1;
                }
            }
        }

        public void train()
        {
            Console.WriteLine("Started training");
            int i = 0;
            while(i < 10)
            {
                foreach (int movieID in ratingDic.Keys)
                {
                    foreach (int userID in ratingDic[movieID].Ratings.Keys)
                    {
                        trainMovieUserLink(movieID, userID, i);
                    }
                }
            }
            i = 0;
            Console.WriteLine("Press Y to continue, N to stop training");
            Console.WriteLine("Waiting for input for 10 seconds...");

            DateTime start = DateTime.Now;

            bool gotKey = false;

            while ((DateTime.Now - start).TotalSeconds < 10)
            {
                if (Console.KeyAvailable)
                {
                    gotKey = true;
                    break;
                }
            }

            if (gotKey)
            {
                string s = Console.ReadLine();
                if (s == "Y" || s == "y")
                {
                    train();
                }
            }
            else
            {
                train();
            }
        }

        private void trainMovieUserLink(int movieID, int userID, int feature)
        {
            double err = lrate * (getRating(movieID, userID) - predictRating(movieID, userID));
            double uv = userValues[userID, feature];
            userValues[userID, feature] += err * movieValues[movieID, feature];
            movieValues[movieID, feature] += err * uv;

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
