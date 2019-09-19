using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.Infrastructure
{
    public static class NewsApiExtension
    {
        public static string ShortenString(this string stringToShorten, int newLenght)
        {
            if (newLenght > stringToShorten.Length) return stringToShorten;

            int cutOfPoint = stringToShorten.IndexOf(" ", newLenght - 1);

            if (cutOfPoint <= 0)
                cutOfPoint = stringToShorten.Length;

            return stringToShorten.Substring(0, cutOfPoint);
        }
    }
}
