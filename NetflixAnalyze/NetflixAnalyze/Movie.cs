using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetflixAnalyze
{
    class Movie
    {
        public int movieID;
        public Dictionary<int, double> Ratings = new Dictionary<int, double>();
    }
}
