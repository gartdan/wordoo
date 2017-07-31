using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordCloudGenerator.Web.TagCloud;

namespace Wordoo.CrossPlat.Helpers
{
    public static class TagCloudExtensions
    {
        /// <summary>
        /// Creates tag cloud html from a provided list of string tags. 
        /// The more times a tag occures in the list, the larger weight it will get in the tag cloud.
        /// </summary>
        /// <param name="tags">A string list of tags</param>
        /// <param name="generationRules">A TagCloudGenerationRules object to decide how the cloud is generated.</param>
        /// <returns>A string containing the html markup of the tag cloud.</returns>
        public static HtmlString TagCloud(this IHtmlHelper htmlHelper, IEnumerable<string> tags, TagCloudGenerationRules generationRules)
        {
            return new HtmlString(new TagCloud(tags, generationRules).ToString());
        }

        /// <summary>
        /// Creates tag cloud html from a provided dictionary of string tags along with integer values indicating the weight of each tag. 
        /// This overload is suitable when you have a list of already weighted tags, i.e. from a database query result.
        /// </summary>
        /// <param name="weightedTags">A dictionary that takes a string for the tag text (as the dictionary key) and an integer for the tag weight (as the dictionary value).</param>
        /// <param name="generationRules">A TagCloudGenerationRules object to decide how the cloud is generated.</param>
        /// <returns>A string containing the html markup of the tag cloud.</returns>
        public static HtmlString TagCloud(this IHtmlHelper htmlHelper, IDictionary<string, int> weightedTags, TagCloudGenerationRules generationRules)
        {
            return new HtmlString(new TagCloud(weightedTags, generationRules).ToString());
        }

        /// <summary>
        /// Creates tag cloud html from a provided list of string tags along with integer values indicating the weight of each tag. 
        /// This overload is suitable when you have a list of already weighted tags, i.e. from a database query result.
        /// </summary>
        /// <param name="weightedTags">A list of KeyValuePair objects that take a string for the tag text and an integer for the weight of the tag.</param>
        /// <param name="generationRules">A TagCloudGenerationRules object to decide how the cloud is generated.</param>
        /// <returns>A string containing the html markup of the tag cloud.</returns>
        public static HtmlString TagCloud(this IHtmlHelper htmlHelper, IEnumerable<KeyValuePair<string, int>> weightedTags, TagCloudGenerationRules generationRules)
        {
            return new HtmlString(new TagCloud(weightedTags, generationRules).ToString());
        }


        /// <summary>
        /// Creates tag cloud html from a provided list of Tag objects.
        /// Use this overload to have full control over the content in the cloud.
        /// </summary>
        /// <param name="tags">Tag objects used to generate the tag cloud.</param>
        /// <returns>A string containing the html markup of the tag cloud.</returns>
        public static HtmlString TagCloud(this IHtmlHelper htmlHelper, IEnumerable<Tag> tags)
        {
            return new HtmlString(new TagCloud(tags).ToString());
        }
    }
}
