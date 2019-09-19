using BolindersBil.web.Constants.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.ViewModels.NewsModels
{
    public class EverythingRequestNews
    {
     /// The keyword or phrase to search for. Boolean operators are supported.
        public string Q { get; set; }

        /// If you want to restrict the search to specific sources, add their Ids here. You can find source Ids with the /sources endpoint or on newsapi.org.
        public List<string> Sources = new List<string>();

        /// If you want to restrict the search to specific web domains, add these here. Example: nytimes.com.
        public List<string> Domains = new List<string>();

        /// The earliest date to retrieve articles from. Note that how far back you can go is constrained by your plan type. See newsapi.org/pricing for plan details.
        public DateTime? From { get; set; }

        /// The latest date to retrieve articles from.
        public DateTime? To { get; set; }

        /// The language to restrict articles to.
        public Languages? Language { get; set; }

        /// How should the results be sorted? Relevancy = articles relevant to the Q param come first. PublishedAt = most recent articles come first. Publisher = popular publishers come first.
        public SortBys? SortBy { get; set; }

        /// Each request returns a fixed amount of results. Page through them by increasing this.
        public int Page { get; set; }

        /// Set the max number of results to retrieve per request. The max is 100.
        public int PageSize { get; set; }
    }
}
