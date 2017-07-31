using HtmlAgilityPack;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WordCloudGenerator.Engine.CrossPlat
{
    public class HtmlToTextParser
    {
        static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);
        static string Space = " ";

        public string Parse(string html)
        {
            var sb = new StringBuilder();
            var document = new HtmlDocument();
            document.LoadHtml(html);
            document.DocumentNode.Descendants()
                .Where(n => n.Name == "script" || n.Name == "style" || n.Name == "#comment")
                .ToList()
                .ForEach(x => x.Remove());

            document.DocumentNode.DescendantsAndSelf()
                .ToList()
                .ForEach(x =>
                {
                    if (!x.HasChildNodes)
                    {
                        string text = x.InnerText;
                        if (!string.IsNullOrEmpty(text))
                            sb.AppendLine(text.Trim() + Space);
                    }
                });
            var strippedOfTags = _htmlRegex.Replace(sb.ToString(), string.Empty);
            strippedOfTags = strippedOfTags.Replace("\r\n", string.Empty);
            strippedOfTags = System.Net.WebUtility.HtmlDecode(strippedOfTags);
            return strippedOfTags;
        }
    }
}
