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
            Dictionary<int, Movie> movies = readfile.readProbeFile();
            Console.ReadKey();
        }
    }
}
