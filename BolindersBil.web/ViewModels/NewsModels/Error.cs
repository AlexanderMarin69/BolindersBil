using BolindersBil.web.Constants.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.ViewModels.NewsModels
{
    public class Error
    {
        public ErrorCodes Code { get; set; } //Constants/News - enum
        public string Message { get; set; }
    }
}
