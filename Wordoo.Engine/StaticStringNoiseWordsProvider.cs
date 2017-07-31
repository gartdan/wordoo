using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordCloudGenerator.Engine.CrossPlat;

namespace Wordoo.Engine.CrossPlat
{
    public class StaticStringNoiseWordsProvider : INoiseWordsProvider
    {
        public static readonly string CommentIndicator = "---";
        public static string[] NoiseWords { get; private set; }

        public IEnumerable<string> GetNoiseWords()
        {
            var words = StopWords.Words.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None).ToList();
            return words.Where(x => !x.StartsWith(CommentIndicator)).ToArray();
        }
    }
}
