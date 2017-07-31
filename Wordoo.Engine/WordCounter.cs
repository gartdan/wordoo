using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordCloudGenerator.Engine.CrossPlat
{
    public class WordCounter : WordCloudGenerator.Engine.CrossPlat.IWordCounter
    {
        public INoiseWordsProvider NoiseWordsProvider { get; private set; }
        public IWordSplitter Splitter { get; private set; }

        public WordCounter(INoiseWordsProvider noiseWordsProvider)
        {
            this.NoiseWordsProvider = noiseWordsProvider;
            this.Splitter = WordSplitter.Default;
        }

        public Dictionary<string, int> CountWords(string content, int minWordLength = 2)
        {
            var counts = new Dictionary<string, int>();
            var words = this.Splitter.Split(content);
            var noiseWords = this.NoiseWordsProvider.GetNoiseWords();
            if (noiseWords != null)
            {
                words = words.Where(x => !this.NoiseWordsProvider.GetNoiseWords().Contains(x.ToLower())).ToArray();
            }
            words = words.Where(x => !IsNumber(x)).ToArray();
            words = words.Where(x => x.Length >= minWordLength).ToArray();
            counts = words.GroupBy(x => x.ToLower()).ToDictionary(g => g.Key, g => g.Count());
            return counts;
        }

        private bool IsNumber(string str)
        {
            double myNum = 0;
            return Double.TryParse(str, out myNum);
        }
    }
}
