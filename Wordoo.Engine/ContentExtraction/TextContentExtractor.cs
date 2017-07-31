namespace WordCloudGenerator.Engine.CrossPlat.ContentExtraction
{
    public class TextContentExtractor : IContentExtractor
    {
        public TextContentExtractor()
        {
        }

        public string ExtractContent(byte[] data)
        {
            return EncodingUtils.GetStringFromByteArray(data);
        }



        public ContentTypes SupportedContentType
        {
            get { return ContentTypes.Text; }
        }
    }
}
