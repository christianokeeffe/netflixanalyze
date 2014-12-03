using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NetflixAnalyze
{
    class SaveLoad
    {
        public void SerializeDictionary(Dictionary<int, int> dictionary, Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(dictionary.Count);
            foreach (var kvp in dictionary)
            {
                writer.Write(kvp.Key);
                writer.Write(kvp.Value);
            }
            writer.Flush();
        }

        public void SerializeArray(double[,] array, Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(array.GetLength(0));
            writer.Write(array.GetLength(1));
            for(int i = 0; i < array.GetLength(0); i++)
            {
                for (int n = 0; n < array.GetLength(1); n++)
                { 
                    writer.Write(array[i, n]);
                }
            }
            writer.Flush();
        }

        public double[,] DeserializeArray(Stream stream)
        {
            BinaryReader reader = new BinaryReader(stream);
            int height = reader.ReadInt32();
            int length = reader.ReadInt32();

            double[,] array = new double[height, length];
            for (int i = 0; i < height; i++)
            {
                for (int n = 0; n < length; n++)
                {
                    array[i, n] = reader.ReadDouble();
                }
            }
                return array;
        }

        public Dictionary<int, int> DeserializeDictionary(Stream stream)
        {
            BinaryReader reader = new BinaryReader(stream);
            int count = reader.ReadInt32();
            var dictionary = new Dictionary<int, int>(count);
            for (int n = 0; n < count; n++)
            {
                var key = reader.ReadInt32();
                var value = reader.ReadInt32();
                dictionary.Add(key, value);
            }
            return dictionary;
        }

    }
}
