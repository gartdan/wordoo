using System;

namespace WordCloudGenerator.Engine.CrossPlat
{
    public interface IContentRetriever
    {
        System.Threading.Tasks.Task<byte[]> GetAsync(Uri uri);
    }
}
