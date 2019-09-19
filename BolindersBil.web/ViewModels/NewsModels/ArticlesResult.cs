using BolindersBil.web.Constants.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.ViewModels.NewsModels
{
    public class ArticlesResult
    {
        public Statuses Status { get; set; } //Constants/News - enum
        public Error Error { get; set; }
        public int TotalResults { get; set; }
        public List<Article> Articles { get; set; }
    }
}
