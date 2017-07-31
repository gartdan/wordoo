using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordCloudGenerator.Engine.CrossPlat
{
    public class WordDistributionSettings
    {
        public int MinWordLength { get; set; }
        public string[] AdditionalNoiseWords { get; set; }

        public string[] AdditionalNoiseWords2 { get; set; }

    }
}
