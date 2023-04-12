using MinVideoEditor.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MinVideoEditor.Workers
{
    public class SpeedConfig : IConfig
    {
        
        public SpeedConfig(double min, double max)
        {
            Min = min; Max = max;
        }
        public double Min { get; set; }
        public double Max { get; set; }
    }

    public class SoundConfig
    {

        public SoundConfig(bool isMaleInsert, bool isFemaleInsert, bool isNoisyInsert)
        {
            IsMaleInsert = isMaleInsert;
            IsFemaleInsert = isFemaleInsert;
            IsNoisyInsert = isNoisyInsert;
        }
        public bool IsMaleInsert { set; get; }
        public bool IsFemaleInsert { set; get; }
        public bool IsNoisyInsert { set; get; }
    }

    public class ConcatConfig
    {

        public ConcatConfig(List<string> startVideos, List<string> endVideos) {
            StartVideos = startVideos;
            EndVideos = endVideos;
        }
        public List<string> StartVideos { get; set; }
        public List<string> EndVideos { set; get; }
    }
    public class Config
    { 
        public Config() {
            SpeedConfig = null;
            SoundConfig = null;
            ConcatConfig = null;
        }
        public Config(bool isRandomConfig, int amount, SpeedConfig speedConfig = null, SoundConfig soundConfig = null, ConcatConfig concatConfig = null) {
            VideoPaths = new string[] { };
            IsRandomConfig = isRandomConfig;
            Amount = amount;
            SpeedConfig = speedConfig;
            SoundConfig = soundConfig;
            ConcatConfig = concatConfig;
        }

        public string LastOpenPath { get; set; }

        public string[] VideoPaths { get; set; }

        public bool IsRandomConfig { get; set; }
        public int Amount { get; set; }

        public SpeedConfig SpeedConfig { get; set; }
        public SoundConfig SoundConfig { get; set; }
        public ConcatConfig ConcatConfig { get; set; }

        public void LoadFromFile() {
            // Get the currently executing assembly
            Assembly assembly = Assembly.GetExecutingAssembly();

            // Get the path of the assembly file
            string path = assembly.Location;
            string directory = Path.GetDirectoryName(path);

            string executedPath = directory==null?"":directory;

            try
            {
                if (File.Exists(executedPath + "/config.json"))
                {
                    string json = "";
                    // Create a new StreamReader object with the file path
                    using (StreamReader reader = new StreamReader(executedPath + "/config.json"))
                    {
                        // Read the entire file as a string
                        json = reader.ReadToEnd();
                    }
                    Config config = JsonConvert.DeserializeObject<Config>(json);
                    if (config != null)
                    {
                        LastOpenPath = config.LastOpenPath;
                        VideoPaths = config.VideoPaths;
                        IsRandomConfig = config.IsRandomConfig;
                        Amount = config.Amount;
                        SpeedConfig = config.SpeedConfig;
                        SoundConfig = config.SoundConfig;
                        ConcatConfig = config.ConcatConfig;
                        return;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            LastOpenPath = @"C:\";
            VideoPaths = new string[] { };
            IsRandomConfig = true;
            Amount = 0;
            SaveToFile();
        }

        public void SaveToFile() {
            string json = JsonConvert.SerializeObject(this);
            // Get the currently executing assembly
            Assembly assembly = Assembly.GetExecutingAssembly();

            // Get the path of the assembly file
            string path = assembly.Location;
            string directory = Path.GetDirectoryName(path);

            string executedPath = directory == null ? "" : directory;
            // Create a new StreamWriter object with the file path
            using (StreamWriter writer = new StreamWriter(executedPath + "/config.json")) 
            {
                // Write text to the file
                writer.WriteLine(json);

                // Flush the buffer to ensure all the text is written to the file
                writer.Flush();
            }
        }
    }
}
