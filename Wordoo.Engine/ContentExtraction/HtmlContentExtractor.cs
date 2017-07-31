namespace WordCloudGenerator.Engine.CrossPlat.ContentExtraction
{
    public class HtmlContentExtractor : IContentExtractor
    {
        private HtmlToTextParser _parser;
        public ContentTypes SupportedContentType { get; private set; }

        public HtmlContentExtractor()
        {
            _parser = new HtmlToTextParser();
            SupportedContentType = ContentTypes.HTML;
        }

        public string ExtractContent(byte[] data)
        {
            var html = EncodingUtils.GetStringFromByteArray(data);
            return _parser.Parse(html);
        }

    }
}
