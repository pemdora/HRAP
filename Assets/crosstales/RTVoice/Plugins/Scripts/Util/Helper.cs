using UnityEngine;

namespace Crosstales.RTVoice.Util
{
    /// <summary>Various helper functions.</summary>
    public static class Helper
    {

        #region Variables

        private static readonly System.Text.RegularExpressions.Regex lineEndingsRegex = new System.Text.RegularExpressions.Regex(@"\r\n|\r|\n");
        //private static readonly Regex cleanStringRegex = new Regex(@"([^a-zA-Z0-9 ]|[ ]{2,})");
        private static readonly System.Text.RegularExpressions.Regex cleanSpacesRegex = new System.Text.RegularExpressions.Regex(@"\s+");
        private static readonly System.Text.RegularExpressions.Regex cleanTagsRegex = new System.Text.RegularExpressions.Regex(@"<.*?>");
        //private static readonly System.Text.RegularExpressions.Regex asciiOnlyRegex = new System.Text.RegularExpressions.Regex(@"[^\u0000-\u00FF]+");

        private const string WINDOWS_PATH_DELIMITER = @"\";
        private const string UNIX_PATH_DELIMITER = "/";

        #endregion


        #region Static properties

        /// <summary>Checks if an Internet connection is available.</summary>
        /// <returns>True if an Internet connection is available.</returns>
        public static bool isInternetAvailable
        {
            get
            {
#if CT_OC
                return OnlineCheck.OnlineCheck.isInternetAvailable;
#else
                return Application.internetReachability != NetworkReachability.NotReachable;
#endif
            }
        }

        /// <summary>Checks if the current platform is Windows.</summary>
        /// <returns>True if the current platform is Windows.</returns>
        public static bool isWindowsPlatform
        {
            get
            {
                return Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor;
            }
        }

        /// <summary>Checks if the current platform is macOS.</summary>
        /// <returns>True if the current platform is macOS.</returns>
        public static bool isMacOSPlatform
        {
            get
            {
#if UNITY_5_4_OR_NEWER
                return Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor;
#else
                return Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXDashboardPlayer;
#endif
            }
        }

        /// <summary>Checks if the current platform is Linux.</summary>
        /// <returns>True if the current platform is Linux.</returns>
        public static bool isLinuxPlatform
        {
            get
            {
#if UNITY_5_5_OR_NEWER
                    return Application.platform == RuntimePlatform.LinuxPlayer || Application.platform == RuntimePlatform.LinuxEditor;
#else
                return Application.platform == RuntimePlatform.LinuxPlayer;
#endif
            }
        }

        /// <summary>Checks if the current platform is Android.</summary>
        /// <returns>True if the current platform is Android.</returns>
        public static bool isAndroidPlatform
        {
            get
            {
                return Application.platform == RuntimePlatform.Android;
            }
        }

        /// <summary>Checks if the current platform is iOS.</summary>
        /// <returns>True if the current platform is iOS.</returns>
        public static bool isIOSPlatform
        {
            get
            {
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
                return Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS;
#else
                return Application.platform == RuntimePlatform.IPhonePlayer;
#endif
            }
        }

        /// <summary>Checks if the current platform is WSA.</summary>
        /// <returns>True if the current platform is WSA.</returns>
        public static bool isWSAPlatform
        {
            get
            {
                return Application.platform == RuntimePlatform.WSAPlayerARM ||
                    Application.platform == RuntimePlatform.WSAPlayerX86 ||
                    Application.platform == RuntimePlatform.WSAPlayerX64 ||
#if !UNITY_5_4_OR_NEWER
                    Application.platform == RuntimePlatform.WP8Player ||
#endif
#if !UNITY_5_5_OR_NEWER
                    Application.platform == RuntimePlatform.XBOX360 ||
#endif
                    Application.platform == RuntimePlatform.XboxOne;
            }
        }

        /// <summary>Checks if the current platform is WebGL.</summary>
        /// <returns>True if the current platform is WebGL.</returns>
        public static bool isWebGLPlatform
        {
            get
            {
                return Application.platform == RuntimePlatform.WebGLPlayer;
            }
        }

        /// <summary>Checks if the current platform is WebPlayer.</summary>
        /// <returns>True if the current platform is WebPlayer.</returns>
        public static bool isWebPlayerPlatform
        {
            get
            {
#if UNITY_5_4_OR_NEWER
                return false;
#else
                return Application.platform == RuntimePlatform.OSXWebPlayer || Application.platform == RuntimePlatform.WindowsWebPlayer;
#endif
            }
        }

        /// <summary>Checks if the current platform is Web (WebPlayer or WebGL).</summary>
        /// <returns>True if the current platform is Web (WebPlayer or WebGL).</returns>
        public static bool isWebPlatform
        {
            get
            {
                return isWebPlayerPlatform || isWebGLPlatform;
            }
        }

        /// <summary>Checks if the current platform is Windows-based (Windows standalone or WSA).</summary>
        /// <returns>True if the current platform is Windows-based (Windows standalone or WSA).</returns>
        public static bool isWindowsBasedPlatform
        {
            get
            {
                return isWindowsPlatform || isWSAPlatform;

                //return false;
            }
        }

        /// <summary>Checks if the current platform is Apple-based (macOS standalone or iOS).</summary>
        /// <returns>True if the current platform is Apple-based (macOS standalone or iOS).</returns>
        public static bool isAppleBasedPlatform
        {
            get
            {
                return isMacOSPlatform || isIOSPlatform;
            }
        }

        /// <summary>Checks if the current platform has built-in TTS.</summary>
        /// <returns>True if the current platform has built-in TTS.</returns>
        public static bool hasBuiltInTTS
        {
            get
            {
                return isWindowsPlatform || isMacOSPlatform || isAndroidPlatform || isIOSPlatform || isWSAPlatform;
            }
        }


        /// <summary>Checks if we are inside the Editor.</summary>
        /// <returns>True if we are inside the Editor.</returns>
        public static bool isEditor
        {
            get
            {
#if UNITY_5_5_OR_NEWER
                return Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.LinuxEditor;
#else
                return Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor;
#endif
            }
        }

        /// <summary>Checks if we are in Editor mode.</summary>
        /// <returns>True if in Editor mode.</returns>
        public static bool isEditorMode
        {
            get
            {
                return isEditor && !Application.isPlaying;
            }
        }

        /// <summary>The current provider type.</summary>
        /// <returns>Current provider type.</returns>
        public static Model.Enum.ProviderType CurrentProviderType
        {
            get
            {
                if (Speaker.isMaryMode)
                    return Model.Enum.ProviderType.MaryTTS;

                if (isWindowsPlatform)
                    return Model.Enum.ProviderType.Windows;

                if (isAndroidPlatform)
                    return Model.Enum.ProviderType.Android;

                if (isIOSPlatform)
                    return Model.Enum.ProviderType.iOS;

                if (isWSAPlatform)
                    return Model.Enum.ProviderType.Windows;

                //if (isMacOSPlatform)
                return Model.Enum.ProviderType.macOS;
            }
        }

        #endregion


        #region Static methods

#if !UNITY_WSA || UNITY_EDITOR
        /// <summary>HTTPS-certification callback.</summary>
        public static bool RemoteCertificateValidationCallback(System.Object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            bool isOk = true;

#if UNITY_5_4_OR_NEWER
        // If there are errors in the certificate chain, look at each error to determine the cause.
        if (sslPolicyErrors != System.Net.Security.SslPolicyErrors.None)
        {
        for (int i = 0; i < chain.ChainStatus.Length; i++)
        {
        if (chain.ChainStatus[i].Status != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.RevocationStatusUnknown)
        {
        chain.ChainPolicy.RevocationFlag = System.Security.Cryptography.X509Certificates.X509RevocationFlag.EntireChain;
        chain.ChainPolicy.RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.Online;
        chain.ChainPolicy.UrlRetrievalTimeout = new System.TimeSpan(0, 1, 0);
        chain.ChainPolicy.VerificationFlags = System.Security.Cryptography.X509Certificates.X509VerificationFlags.AllFlags;

        isOk = chain.Build((System.Security.Cryptography.X509Certificates.X509Certificate2)certificate);
        }
        }
        }
#endif

            return isOk;
        }
#endif

        /// <summary>Format byte-value to Human-Readable-Form.</summary>
        /// <param name="bytes">Value in bytes</param>
        /// <returns>Formatted byte-value in Human-Readable-Form.</returns>
        public static string FormatBytesToHRF(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // show a single decimal place, and no space.
            return System.String.Format("{0:0.##} {1}", len, sizes[order]);
        }

        /// <summary>Cleans a given text to contain only letters or digits.</summary>
        /// <param name="text">Text to clean.</param>
        /// <param name="removeTags">Removes tags from text (default: true, optional).</param>
        /// <param name="clearSpaces">Clears multiple spaces from text (default: true, optional).</param>
        /// <param name="clearLineEndings">Clears line endings from text (default: true, optional).</param>
        /// <returns>Clean text with only letters and digits.</returns>
        public static string CleanText(string text, bool removeTags = true, bool clearSpaces = true, bool clearLineEndings = true)
        {
            string result = text;

            if (removeTags)
            {
                result = ClearTags(result);
            }

            if (clearSpaces)
            {
                result = ClearSpaces(result);
            }

            if (clearLineEndings)
            {
                result = ClearLineEndings(result);
            }

            //Debug.Log (result);

            return result;
        }

        /// <summary>Cleans a given text from tags.</summary>
        /// <param name="text">Text to clean.</param>
        /// <returns>Clean text without tags.</returns>
        public static string ClearTags(string text)
        {

            return cleanTagsRegex.Replace(text, string.Empty).Trim();
        }

        /// <summary>Cleans a given text from multiple spaces.</summary>
        /// <param name="text">Text to clean.</param>
        /// <returns>Clean text without multiple spaces.</returns>
        public static string ClearSpaces(string text)
        {

            return cleanSpacesRegex.Replace(text, " ").Trim();
        }

        /// <summary>Cleans a given text from line endings.</summary>
        /// <param name="text">Text to clean.</param>
        /// <returns>Clean text without line endings.</returns>
        public static string ClearLineEndings(string text)
        {

            return lineEndingsRegex.Replace(text, string.Empty).Trim();
        }

        /// <summary>Validates a given path and add missing slash.</summary>
        /// <param name="path">Path to validate</param>
        /// <returns>Valid path</returns>
        public static string ValidatePath(string path)
        {
            string result;

            if (isWindowsPlatform)
            {
                result = path.Replace('/', '\\');

                if (!result.EndsWith(WINDOWS_PATH_DELIMITER))
                {
                    result += WINDOWS_PATH_DELIMITER;
                }
            }
            else
            {
                result = path.Replace('\\', '/');

                if (!result.EndsWith(UNIX_PATH_DELIMITER))
                {
                    result += UNIX_PATH_DELIMITER;
                }
            }

            return result;
        }

        /// <summary>Cleans a given URL.
        /// <param name="url">URL to clean</param>
        /// <param name="removeProtocol">Remove the protocol, e.g. http:// (default: true, optional).</param>
        /// <param name="removeWWW">Remove www (default: true, optional).</param>
        /// <param name="removeSlash">Remove slash at the end (default: true, optional)</param>
        /// <returns>Clean URL</returns>
        public static string CleanUrl(string url, bool removeProtocol = true, bool removeWWW = true, bool removeSlash = true)
        {
            string result = url.Trim();

            if (!string.IsNullOrEmpty(url))
            {
                if (removeProtocol)
                {
                    result = result.Substring(result.IndexOf("//") + 2);
                }

                if (removeWWW)
                {
                    result = result.CTReplace("www.", string.Empty);
                }

                if (removeSlash && result.EndsWith(Constants.PATH_DELIMITER_UNIX))
                {
                    result = result.Substring(0, result.Length - 1);
                }

                /*
                if (urlTemp.StartsWith("http://"))
                {
                    result = urlTemp.Substring(7);
                }
                else if (urlTemp.StartsWith("https://"))
                {
                    result = urlTemp.Substring(8);
                }
                else
                {
                    result = urlTemp;
                }

                if (result.StartsWith("www."))
                {
                    result = result.Substring(4);
                }
                */
            }

            return result;
        }

        /// <summary>Split the given text to lines and return it as list.</summary>
        /// <param name="text">Complete text fragment</param>
        /// <returns>Splitted lines as array</returns>
        public static System.Collections.Generic.List<string> SplitStringToLines(string text)
        {
            System.Collections.Generic.List<string> result = new System.Collections.Generic.List<string>();

            if (string.IsNullOrEmpty(text))
            {
                Debug.LogWarning("Parameter 'text' is null or empty!" + System.Environment.NewLine + "=> 'SplitStringToLines()' will return an empty string list.");
            }
            else
            {
                string[] lines = lineEndingsRegex.Split(text);

                for (int ii = 0; ii < lines.Length; ii++)
                {
                    if (!System.String.IsNullOrEmpty(lines[ii]))
                    {
                        result.Add(lines[ii]);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Generate nice HSV colors.
        /// Based on https://gist.github.com/rje/6206099
        /// </summary>
        /// <param name="h">Hue</param>
        /// <param name="s">Saturation</param>
        /// <param name="v">Value</param>
        /// <param name="a">Alpha (optional)</param>
        /// <returns>True if the current platform is supported.</returns>
        public static Color HSVToRGB(float h, float s, float v, float a = 1f)
        {
            if (s == 0)
            {
                return new Color(v, v, v, a);
            }

            float hue = h / 60f;
            int sector = Mathf.FloorToInt(hue);
            float fact = hue - sector;
            float p = v * (1f - s);
            float q = v * (1f - s * fact);
            float t = v * (1f - s * (1f - fact));

            switch (sector)
            {
                case 0:
                    return new Color(v, t, p, a);
                case 1:
                    return new Color(q, v, p, a);
                case 2:
                    return new Color(p, v, t, a);
                case 3:
                    return new Color(p, q, v, a);
                case 4:
                    return new Color(t, p, v, a);
                default:
                    return new Color(v, p, q, a);
            }
        }

        /// <summary>Marks the current word or all spoken words from a given text array.</summary>
        /// <param name="speechTextArray">Array with all text fragments</param>
        /// <param name="wordIndex">Current word index</param>
        /// <param name="markAllSpokenWords">Mark the spoken words (default: false, optional)</param>
        /// <param name="markPrefix">Prefix for every marked word (default: green, optional)</param>
        /// <param name="markPostfix">Postfix for every marked word (default: green, optional)</param>
        /// <returns>Marked current word or all spoken words.</returns>
        public static string MarkSpokenText(string[] speechTextArray, int wordIndex, bool markAllSpokenWords = false, string markPrefix = "<color=green><b>", string markPostfix = "</b></color>")
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (speechTextArray == null)
            {
                Debug.LogWarning("The given 'speechTextArray' is null!");
            }
            else
            {
                if (wordIndex < 0 || wordIndex > speechTextArray.Length - 1)
                {
                    Debug.LogWarning("The given 'wordIndex' is invalid: " + wordIndex);
                }
                else
                {
                    for (int ii = 0; ii < wordIndex; ii++)
                    {

                        if (markAllSpokenWords)
                            sb.Append(markPrefix);
                        sb.Append(speechTextArray[ii]);
                        if (markAllSpokenWords)
                            sb.Append(markPostfix);
                        sb.Append(" ");
                    }

                    sb.Append(markPrefix);
                    sb.Append(speechTextArray[wordIndex]);
                    sb.Append(markPostfix);
                    sb.Append(" ");

                    for (int ii = wordIndex + 1; ii < speechTextArray.Length; ii++)
                    {
                        sb.Append(speechTextArray[ii]);
                        sb.Append(" ");
                    }
                }
            }

            return sb.ToString();
        }

        #endregion

    }
}
// © 2015-2017 crosstales LLC (https://www.crosstales.com)