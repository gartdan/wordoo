using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordCloudGenerator.Engine.CrossPlat
{
    public static class EncodingUtils
    {
        public static string GetStringFromByteArray(byte[] data)
        {
            return System.Text.Encoding.GetEncoding(0).GetString(data);
        }
    }
}
