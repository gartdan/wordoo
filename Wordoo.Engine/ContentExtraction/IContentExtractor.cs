namespace WordCloudGenerator.Engine.CrossPlat.ContentExtraction
{
    public interface IContentExtractor
    {
        string ExtractContent(byte[] data);
        ContentTypes SupportedContentType { get; }
    }
}
