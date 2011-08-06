using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Collections.Specialized;

namespace Prototype_3
{
    public sealed class UrlExtractorStrategy
    {
        public UrlExtractorStrategy()
        {
        }

        public string Extract(string flashVariables)
        {
            SteeringString steeringWheel = new SteeringString(flashVariables);

            List<string> urls = new List<string>();

            while (true)
            {
                try
                {
                    steeringWheel.MoveForwardTo("videoplayback");
                    steeringWheel.MoveBackwardsTo("url%3D");
                    steeringWheel.SetStartMarker();

                    try
                    {
                        steeringWheel.MoveForwardTo("%2Curl%3D");
                    }
                    catch (StringMarkingNotFoundException)
                    {
                        steeringWheel.MoveForwardTo("&amp");
                    }

                    steeringWheel.SetEndMarker();

                    string firstURl = steeringWheel.GetMarkedRange();

                    string unescapedUrl = Uri.UnescapeDataString(firstURl);

                    Regex itagRegex = new Regex(@"&?itag=\d+", RegexOptions.IgnoreCase);

                    string usableUrl = itagRegex.Replace(unescapedUrl, string.Empty);

                    NameValueCollection ho = HttpUtility.ParseQueryString(usableUrl);

                    urls.Add(Uri.UnescapeDataString(usableUrl));
                }
                catch(StringMarkingNotFoundException)
                { 
                    break; 
                }
            }

            return urls.Where(value => value.Contains("flv")).FirstOrDefault();
        }
    }
}
