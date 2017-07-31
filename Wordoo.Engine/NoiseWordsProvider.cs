using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordCloudGenerator.Engine.CrossPlat
{
    public class NoiseWordsProvider : INoiseWordsProvider
    {
        public string FilePath { get; private set; }
        public static readonly string CommentIndicator = "---";
        public static string[] NoiseWords { get; private set; }
        public INoiseWordsFileReader Reader { get; private set; }

        public NoiseWordsProvider(string filePath) : this(new NoiseWordsFileReader(filePath))
        {
        }

        public NoiseWordsProvider(INoiseWordsFileReader reader)
        {
            this.Reader = reader;
            LoadNoiseWords();
        }

        private void LoadNoiseWords()
        {
            var noiseWords = this.Reader.ReadFile();
            NoiseWords = noiseWords.Where(x => !x.StartsWith(CommentIndicator)).ToArray();
        }

        public IEnumerable<string> GetNoiseWords()
        {
            return NoiseWords;
        }
    }
}
