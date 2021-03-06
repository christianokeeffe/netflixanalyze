﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetflixAnalyze
{
    class readfile
    {
        public Dictionary<int, Movie> readMovieFiles()
        {
            Console.WriteLine("How many movies files do you wanna load?:");
            int max = int.Parse(Console.ReadLine());
            int counter = 0;
            Dictionary<int, Movie> movieData = new Dictionary<int, Movie>();
            int i = 0;
            var txtFiles = Directory.EnumerateFiles(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\nf_prize_dataset\\training_set", "*.txt");
            foreach (string currentFile in txtFiles)
            {
                i++;
                if (i % 500 == 0)
                {
                    Console.WriteLine(i);
                }
                if (counter < max)
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(currentFile);
                    Movie tempMovie = new Movie();
                    while (!file.EndOfStream)
                    {
                        string line = file.ReadLine();
                        if (line.Contains(":"))
                        {
                            tempMovie.movieID = int.Parse(line.Replace(":", ""));
                        }
                        else
                        {
                            string[] tempLine = line.Split(',');
                            int customer = int.Parse(tempLine[0]);
                            int rating = int.Parse(tempLine[1]);
                            tempMovie.Ratings.Add(customer, (double)rating);
                        }
                    }
                    movieData.Add(tempMovie.movieID, tempMovie);
                    file.Close();
                    counter++;
                }
            }
            Console.WriteLine("Movie Files Loaded!");
            return movieData;
        }

        public Dictionary<int, Movie> readProbeFile(int maxID)
        {
            Dictionary<int, Movie> probeData = new Dictionary<int, Movie>();
            int tempID = -1;
            bool included = false;
            Movie tempMovie = null;
            System.IO.StreamReader file = new System.IO.StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\nf_prize_dataset\\probe.txt");
            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                if(line.Contains(":"))
                {
                    tempID = int.Parse(line.Replace(":", ""));
                    if(tempID <= maxID)
                    {
                        included = true;
                        tempMovie = new Movie();
                        tempMovie.movieID = tempID;
                        probeData.Add(tempID, tempMovie);
                    }
                    else
                    {
                        included = false;
                    }
                }
                else if (line != "" && included)
                {
                    tempMovie.Ratings.Add(int.Parse(line), -1.0); 
                }
            }

            file.Close();

            Console.WriteLine("Probe File Loaded!");
            return probeData;
        }

        public void matchData(Dictionary<int, Movie> trainingData, Dictionary<int, Movie> probeData)
        {
            List<int> movieIDs = new List<int>();
            List<int> customerIDs = new List<int>();
            foreach (int movieID in probeData.Keys)
            {
                for (int i = 0; i < probeData[movieID].Ratings.Count; i++)
                {
                    int customerID = probeData[movieID].Ratings.Keys.ElementAt(i);
                    probeData[movieID].Ratings[customerID] = trainingData[movieID].Ratings[customerID];
                    movieIDs.Add(movieID);
                    customerIDs.Add(customerID);
                }
            }
            for (int i = 0; i < movieIDs.Count; i++)
            {
                trainingData[movieIDs[i]].Ratings.Remove(customerIDs[i]);
            }
        }
    }
}
