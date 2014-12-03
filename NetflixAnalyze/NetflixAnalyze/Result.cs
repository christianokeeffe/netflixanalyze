using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetflixAnalyze
{
    class Result
    {
        double score;
        int movieID;
        int customerID;
 
        public Result(double Score, int MovieID, int CustomerID)
        {
            score = Score;
            movieID = MovieID;
            customerID = CustomerID;
        }
    }
}
