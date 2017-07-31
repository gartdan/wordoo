using System.IO;

namespace WordCloudGenerator.Engine.CrossPlat
{
    public class NoiseWordsFileReader : INoiseWordsFileReader
    {
        public string FilePath { get; private set; }
        public NoiseWordsFileReader(string filePath)
        {
            this.FilePath = filePath;
        }

        public string[] ReadFile()
        {
            return File.ReadAllLines(this.FilePath);
        }
    }
}
