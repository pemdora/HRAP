#if !UNITY_WSA || UNITY_EDITOR

namespace Crosstales.RTVoice.Util
{
    /// <summary>Specialised WebClient.</summary>
    public class CTWebClient : System.Net.WebClient
    {
        /// <summary>
        /// Timeout in milliseconds
        /// </summary>
        public int Timeout { get; set; }

        public CTWebClient() : this(5000) { }

        public CTWebClient(int timeout)
        {
            this.Timeout = timeout;
        }

        protected override System.Net.WebRequest GetWebRequest(System.Uri uri)
        {
            System.Net.WebRequest request = base.GetWebRequest(uri);
            if (request != null)
            {
                request.Timeout = Timeout;
            }
            return request;
        }
    }
}

#endif
// © 2017 crosstales LLC (https://www.crosstales.com)