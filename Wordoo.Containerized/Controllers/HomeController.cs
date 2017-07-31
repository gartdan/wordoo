using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WordCloudGenerator.Engine.CrossPlat;
using WordCloudGenerator.Engine.CrossPlat.ContentExtraction;
using System.Diagnostics;
using Wordoo.CrossPlat.Models;
using Wordoo.CrossPlat.Util;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Wordoo.Engine.CrossPlat;

namespace Wordoo.CrossPlat.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _env;

        public HomeController(ILogger<HomeController> logger, IHostingEnvironment env)
        {
            this._logger = logger;
            this._env = env;
        }

        
        public ActionResult Index()
        {
            var userAgent = Request?.Headers["User-Agent"].ToString();
            var hostAddress = Request?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            _logger.LogInformation($"Index - User Agent - {userAgent}, Address {hostAddress}");


            var json = TempData["model-todisplay"] as string;
            if (!string.IsNullOrEmpty(json)) 
            {
                var model = JsonConvert.DeserializeObject<HomeModel>(json);
                return View("Index", model);
            }

            return View("Index", new HomeModel());
        }


        //public async Task<ActionResult> PDF(string uri)
        //{

        //    try
        //    {
        //        var engine = GetEngine();
        //        var wordFrequencies = await engine.GenerateAsync(new Uri(uri));
        //        var model = new HomeModel() { Url = uri, WordDistribution = wordFrequencies };
        //        return new RazorPDF.PdfResult(model, "PDF");
        //    }
        //    catch (Exception ex)
        //    {
        //        Trace.TraceError($"Exception: {ex.ToString()}, Stack: {ex.StackTrace}, Message {ex.Message}");
        //        return View("Error");
        //    }

        //}

        [HttpPost]
        public async Task<ActionResult> Count(string url)
        {
            var userAgent = Request?.Headers["User-Agent"].ToString();
            var hostAddress = Request?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            _logger.LogInformation($"Generating Word Count for  {url} - User Agent - {userAgent}, Address {hostAddress}");

            if (url.Contains("invalid"))
                throw new ArgumentException("Invalid universalResourceLocator, cannot generate");
            var wordFrequencies = await GetEngine().GenerateAsync(new Uri(url));
            var model = new HomeModel() { WordDistribution = wordFrequencies, Url = url };
            var json = JsonConvert.SerializeObject(model);
            TempData["model-todisplay"] = json;
            return RedirectToAction("Index");
        }

        WordDistributionEngine GetEngine()
        {
            //var path = PlatformServices.Default.Application.ApplicationBasePath;
            var path = _env.ContentRootPath
                   + Path.DirectorySeparatorChar.ToString()
                   + "App_Data"
                   + Path.DirectorySeparatorChar.ToString()
                   + "Stopwords-Wordle.txt";

            var engine = new WordDistributionEngine(new ContentRetriever(),
                new List<IContentExtractor>() { new HtmlContentExtractor(), /**new PDFContentExtractor(), **/ new TextContentExtractor() },
                /** new NoiseWordsProvider(Microsoft.AspNet.Hosting.Server.MapPath("~/App_Data/Stopwords-Wordle.txt")), **/
                //new NoiseWordsProvider(Path.Combine(path, "App_Data/Stopwords-Wordle.txt")),
                //new NoiseWordsProvider(path),
                new StaticStringNoiseWordsProvider(),
                new WordDistributionSettings() { MinWordLength = 3 });
            return engine;
        }
    }
}
