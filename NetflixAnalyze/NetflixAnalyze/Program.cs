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
            Dictionary<int, Movie> trainingData = readfile.readMovieFiles();
            Dictionary<int, Movie> probeData = readfile.readProbeFile(trainingData.Count);
            Match.matchData(trainingData, probeData);
            //PreProcess.preProcessData(trainingData);
           // Dictionary<int, Movie> movies = readfile.readProbeFile();
            //PreProcess.prePostProcessData(movies, -1);
            Console.ReadKey();
        }
    }
}
