
namespace Pion.Domain
{
    public sealed class YouTubeLink
    {
        readonly string _url;
        readonly bool _isValid;

        public YouTubeLink(string url)
        {
            _url = url;
            _isValid = Validate();
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
        }

        public string Url
        {
            get
            {
                return _url;
            }
        }

        bool Validate()
        {
            return !string.IsNullOrWhiteSpace(Url) && Url.Contains("youtube.com");
        }
    }
}
