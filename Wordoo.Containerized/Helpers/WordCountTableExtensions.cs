using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.WebEncoders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Encodings.Web;

namespace Wordoo.CrossPlat.Helpers
{
    public static class DistributionTableExtensions
    {
        public static HtmlString DistributionTable(this IHtmlHelper helper,
            string name,
            IDictionary<string, int> distribution,
            int limit,
            string itemHeaderText,
            string countHeaderText,
            object htmlAttributes)
        {
            return DistributionTable(helper, name, distribution, limit, itemHeaderText,
                countHeaderText,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static HtmlString DistributionTable(this IHtmlHelper helper,
            string name,
            IDictionary<string, int> distribution,
            int limit,
            string itemHeaderText,
            string countHeaderText,
            IDictionary<string, object> htmlAttributes)
        {
            var sb = new StringBuilder();
            TagBuilder tagBuilder = new TagBuilder("table");
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("name", name);
            tagBuilder.MergeAttribute("id", name);
            BuildHeaderRow(sb, itemHeaderText, countHeaderText);
            BuildTable(sb, distribution.OrderByDescending(x => x.Value).Take(limit).ToList());
            tagBuilder.InnerHtml.AppendHtml(sb.ToString());
            var sb2 = new StringBuilder();
            tagBuilder.WriteTo(new StringWriter(sb2), HtmlEncoder.Default);
            return new HtmlString(sb2.ToString());
        }

        private static void BuildTable(StringBuilder sb, List<KeyValuePair<string, int>> items)
        {
            items.ForEach(x =>
            {
                sb.AppendLine("\t<tr>\n");
                sb.AppendFormat("\t\t<td>{0}</td>", x.Key);
                sb.AppendFormat("\t\t<td>{0}</td>", x.Value);
                sb.AppendLine("\t</tr>\n");
            });
        }

        private static void BuildHeaderRow(StringBuilder sb, string wordHeader, string countHeader)
        {
            sb.AppendLine("\t<tr>\n");
            sb.AppendFormat("\t\t<th>{0}</th>\n", wordHeader);
            sb.AppendFormat("\t\t<th>{0}</th>\n", countHeader);
            sb.AppendLine("\t</tr>\n");


        }
    }
}
