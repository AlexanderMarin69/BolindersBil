using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.Constants.News
{
    public enum SortBys
    {   /// <summary>
        /// Sort by publisher popularity
        /// </summary>
        Popularity,
        /// <summary>
        /// Sort by article publish date (newest first)
        /// </summary>
        PublishedAt,
        /// <summary>
        /// Sort by relevancy to the Q param
        /// </summary>
        Relevancy
    }
}
