using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WordCloudGenerator.Engine.CrossPlat.ContentExtraction;

namespace WordCloudGenerator.Engine.CrossPlat
{
    public class WordDistributionEngine
    {
        public IWordCounter Counter { get; private set; }
        public IContentRetriever ContentRetriever { get; private set; }
        public IEnumerable<IContentExtractor> ContentExtractors { get; private set; }
        public WordDistributionSettings Settings { get; set; }
        public INoiseWordsProvider NoiseWordsProvider { get; set; }

        public WordDistributionEngine(
            IContentRetriever contentRetriever,
            IEnumerable<IContentExtractor> contentExtractors,
            INoiseWordsProvider noiseWordsProvider,
            WordDistributionSettings settings)
        {
            this.ContentRetriever = contentRetriever;
            this.ContentExtractors = contentExtractors;
            this.NoiseWordsProvider = noiseWordsProvider;
            this.Counter = new WordCounter(this.NoiseWordsProvider);
            this.Settings = settings;
        }

        public async Task<IDictionary<string, int>> GenerateAsync(Uri uri)
        {
            var data = await this.ContentRetriever.GetAsync(uri);
            var contentType = DetermineContentType(uri);
            return Generate(data, contentType);
        }


        public IDictionary<string, int> Generate(string content)
        {
            return Counter.CountWords(content, Settings.MinWordLength);
        }

        public IDictionary<string, int> Generate(byte[] data, ContentTypes contentType)
        {
            var contentExtractor = GetContentExtractor(contentType);
            var content = contentExtractor.ExtractContent(data);
            return Generate(content);
        }

        public ContentTypes DetermineContentType(Uri uri)
        {
            string fileName = Path.GetFileName(uri.LocalPath);
            string extension = Path.GetExtension(uri.LocalPath);
            ContentTypes contentType = ContentTypes.None;
            switch (extension.ToLower())
            {
                case ".pdf":
                    contentType = ContentTypes.PDF;
                    break;
                case ".txt":
                case ".text":
                case ".csv":
                    contentType = ContentTypes.Text;
                    break;
                default:
                    contentType = ContentTypes.HTML;
                    break;

            }
            return contentType;
        }

        public IContentExtractor GetContentExtractor(ContentTypes contentType)
        {
            var extractor = this.ContentExtractors.Where(x => x.SupportedContentType == contentType).SingleOrDefault();
            if (extractor == null)
            {
                throw new InvalidOperationException("No extractor registered for content type:" + contentType.ToString());
            }
            return extractor;
        }



    }
}
