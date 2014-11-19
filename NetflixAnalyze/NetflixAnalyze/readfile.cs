using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetflixAnalyze
{
    class readfile
    {
        public static Dictionary<int, Movie> readMovieFiles(Dictionary<int, Movie> movieDictionary)
        {
            int i = 0;
            var txtFiles = Directory.EnumerateFiles(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\download\\training_set", "*.txt");
            foreach (string currentFile in txtFiles)
            {
                i++;
                if(i % 500 == 0)
                {
                    Console.WriteLine(i);
                }
                System.IO.StreamReader file = new System.IO.StreamReader(currentFile);
                Movie tempMovie = null;
                bool movieFound = false;
                double tempRating;
                while (!file.EndOfStream)
                {
                    string line = file.ReadLine();
                    if(line.Contains(":"))
                    {
                        if (movieDictionary.TryGetValue(int.Parse(line.Replace(":", "")), out tempMovie))
                        {
                            movieFound = true;
                        }
                    }
                    else if (movieFound)
                    {
                        string[] tempLine = line.Split(',');
                        int customer = int.Parse(tempLine[0]);
                        int rating = int.Parse(tempLine[1]);
                        if (tempMovie.Ratings.TryGetValue(customer, out tempRating))
                        {
                            tempMovie.Ratings[customer] = (double)rating;
                        }
                    }
                }

                file.Close();
            }
            Console.WriteLine("Movie Files Loaded!");
            return movieDictionary;
        }

        public static Dictionary<int, Movie> readProbeFile()
        {
            int max = int.Parse(Console.ReadLine());
            int counter = 0;
            Dictionary<int, Movie> movies = new Dictionary<int, Movie>();
            int tempID = -1;
            Movie tempMovie = null;
            System.IO.StreamReader file = new System.IO.StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\download\\probe.txt");
            while (!file.EndOfStream && counter < max)
            {
                string line = file.ReadLine();
                if(line.Contains(":"))
                {
                    tempID = int.Parse(line.Replace(":", ""));
                    tempMovie = new Movie();
                    tempMovie.movieID = tempID;
                    movies.Add(tempID, tempMovie);
                    counter++;
                }
                else if (line != "")
                {
                    tempMovie.Ratings.Add(int.Parse(line), -1.0); 
                }
            }

            file.Close();

            Console.WriteLine("Probe File Loaded!");
            return readMovieFiles(movies);
        }
    }
}
