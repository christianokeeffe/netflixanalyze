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
        public void Serialize(Dictionary<int, int> dictionary, Stream stream)
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

        public Dictionary<int, int> Deserialize(Stream stream)
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
