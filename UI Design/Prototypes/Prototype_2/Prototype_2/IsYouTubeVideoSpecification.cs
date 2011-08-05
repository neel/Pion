using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype_2
{
    public sealed class IsYouTubeVideoSpecification
    {
        public IsYouTubeVideoSpecification()
        {
        }
        
        public bool IsSatisfiedBy(string potentialYouTubeUrl)
        {
            if (string.IsNullOrWhiteSpace(potentialYouTubeUrl))
            {
                return false;
            }

            return potentialYouTubeUrl.Contains("youtube.com");
        }
    }
}
