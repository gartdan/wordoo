using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wordoo.CrossPlat.Models
{
    
    public class HomeModel
    {
        public string Url { get; set; }

        public string FirstName { get; set; }

        public string Text { get; set; }
        public IDictionary<string, int> WordDistribution { get; set; }
    }
}
