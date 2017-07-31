using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordCloudGenerator.Engine.CrossPlat
{
    public interface IWordCounter
    {
        System.Collections.Generic.Dictionary<string, int> CountWords(string content, int minWordLength = 2);
    }
}
