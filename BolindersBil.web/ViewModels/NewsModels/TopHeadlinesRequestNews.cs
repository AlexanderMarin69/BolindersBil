using BolindersBil.web.Constants.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.ViewModels.NewsModels
{
    public class TopHeadlinesRequestNews
    {       
        /// The keyword or phrase to search for. Boolean operators are supported.
        public string Q { get; set; }

        /// If you want to restrict the results to specific sources, add their Ids here. You can find source Ids with the /sources endpoint or on newsapi.org.
        public List<string> Sources = new List<string>();

        /// If you want to restrict the headlines to a specific news category, add these here.
        public Categories? Category { get; set; }

        /// The language to restrict articles to.
        public Languages? Language { get; set; }

        /// The country of the source to restrict articles to.
        public Countries? Country { get; set; }

        /// Each request returns a fixed amount of results. Page through them by increasing this.
        public int Page { get; set; }

        /// Set the max number of results to retrieve per request. The max is 100.
        public int PageSize { get; set; }
    }
}
