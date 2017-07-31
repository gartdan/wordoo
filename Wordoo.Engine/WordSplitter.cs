using System;

namespace WordCloudGenerator.Engine.CrossPlat
{
    public class WordSplitter : WordCloudGenerator.Engine.CrossPlat.IWordSplitter
    {
        private static WordSplitter _default = new WordSplitter();
        public static WordSplitter Default
        {
            get { return _default; }
        }

        public string[] Split(string content)
        {
            return content.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',', '\'', '-', '_', '/', '\u000A', '"', '|', '}', '{', ']', '[', '»', '’', '‘', '`', ')', '(', '°', '#', '$', '“' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
