﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordCloudGenerator.Engine.CrossPlat
{
    public interface INoiseWordsProvider
    {
        System.Collections.Generic.IEnumerable<string> GetNoiseWords();
    }
}
