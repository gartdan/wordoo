using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WordCloudGenerator.Engine.CrossPlat
{
    public class ContentRetriever : WordCloudGenerator.Engine.CrossPlat.IContentRetriever
    {
        public async Task<byte[]> GetAsync(Uri uri)
        {
            //TODO: Determine if HttpClient is cross-plat
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                return await response.Content.ReadAsByteArrayAsync();
            }
        }
    }
}
