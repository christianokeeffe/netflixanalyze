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
        public static void readMovieFiles()
        {
            var txtFiles = Directory.EnumerateFiles(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\download\\training_set", "*.txt");
            foreach (string currentFile in txtFiles)
            {
                int i = 0;
                string line;
                System.IO.StreamReader file = new System.IO.StreamReader(currentFile);
                while ((line = file.ReadLine()) != null)
                {
                    if(i == 0)
                    {

                        i++;
                    }
                    else
                    {
                        
                    }
                }

                file.Close();
            }
            Console.WriteLine("Movie Files Loaded!");
        }

        public static void readProbeFile()
        {
            Dictionary<int, Movie> movies = new Dictionary<int, Movie>();
            string line;
            int tempID = -1;
            Movie tempMovie = null;
            System.IO.StreamReader file = new System.IO.StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\download\\probe.txt");
            while ((line = file.ReadLine()) != null)
            {
                if(line.Contains(":"))
                {
                    tempID = int.Parse(line.Replace(":", ""));
                    tempMovie = new Movie();
                    movies.Add(tempID, tempMovie);
                }
                else
                {
                    tempMovie.Ratings.Add(int.Parse(line), -1); 
                }
            }

            file.Close();

            Console.WriteLine("Probe File Loaded!");
        }
    }
}
