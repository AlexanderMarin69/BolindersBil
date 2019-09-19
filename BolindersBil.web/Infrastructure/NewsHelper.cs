using BolindersBil.web.Constants.News;
using BolindersBil.web.ViewModels.NewsModels;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.Infrastructure
{
    public class NewsHelper
    {
        //Create Dependency Injection
        private CustomAppSettings _appSettings;

        public NewsHelper(IOptions<CustomAppSettings> settings)
        {
            _appSettings = settings.Value;
        }

        public ArticlesResult GetNews()
        {
            var newsApiClient = new NewsApiClient(_appSettings.NewsApiKey, _appSettings.NewsApiUrl);

            var articlesResponse = newsApiClient.GetEverything(new EverythingRequestNews
            {
                Sources = { "the-new-york-times" },
                Q = "Trump",
                SortBy = SortBys.PublishedAt,
                Language = Languages.EN,
                From = new DateTime(2019, 09, 01)
            });

            return articlesResponse;
        }
    }
}
