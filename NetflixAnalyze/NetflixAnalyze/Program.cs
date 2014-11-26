using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetflixAnalyze
{
    class Program
    {
        static void Main(string[] args)
        {
            readfile reader = new readfile();
            Dictionary<int, Movie> trainingData = reader.readMovieFiles();
            Dictionary<int, Movie> probeData = reader.readProbeFile(trainingData.Count);
            reader.matchData(trainingData, probeData);
            //PreProcess.preProcessData(trainingData);
           // Dictionary<int, Movie> movies = readfile.readProbeFile();
            Dictionary<int, Movie> movies = PreProcess.prePostProcessData(trainingData, -1);
            Factorize fac = new Factorize(movies);
            fac.train();
            RMSE scoreCalculater = new RMSE();
            scoreCalculater.calculateScores(probeData);
            Console.ReadKey();
        }
    }
}
