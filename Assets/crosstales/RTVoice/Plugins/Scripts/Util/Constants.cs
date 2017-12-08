using UnityEngine;

namespace Crosstales.RTVoice.Util
{
    /// <summary>Collected constants of very general utility for the asset.</summary>
    public static class Constants
    {

        #region Constant variables

        /// <summary>Is PRO-version?</summary>
        public static readonly bool isPro = true;

        /// <summary>Name of the asset.</summary>
        public const string ASSET_NAME = "RTVoice PRO"; //PRO
        //public const string ASSET_NAME = "RTVoice"; //DLL

        /// <summary>Version of the asset.</summary>
        public const string ASSET_VERSION = "2.9.1";

        /// <summary>Build number of the asset.</summary>
        public const int ASSET_BUILD = 291;

        /// <summary>Create date of the asset (YYYY, MM, DD).</summary>
        public static readonly System.DateTime ASSET_CREATED = new System.DateTime(2015, 4, 29);

        /// <summary>Change date of the asset (YYYY, MM, DD).</summary>
        public static readonly System.DateTime ASSET_CHANGED = new System.DateTime(2017, 11, 23);

        /// <summary>Author of the asset.</summary>
        public const string ASSET_AUTHOR = "crosstales LLC";

        /// <summary>URL of the asset author.</summary>
        public const string ASSET_AUTHOR_URL = "https://www.crosstales.com";

        /// <summary>URL of the crosstales assets in UAS.</summary>
        public const string ASSET_CT_URL = "https://www.assetstore.unity3d.com/#!/list/42213-crosstales?aid=1011lNGT&pubref=" + ASSET_NAME; // crosstales list

        /// <summary>URL of the PRO asset in UAS.</summary>
        public const string ASSET_PRO_URL = "https://www.assetstore.unity3d.com/#!/content/41068?aid=1011lNGT&pubref=" + ASSET_NAME;

        /// <summary>URL of the 3rd party assets in UAS.</summary>
        public const string ASSET_3P_URL = "https://www.assetstore.unity3d.com/#!/list/42209-rt-voice-friends?aid=1011lNGT&pubref=" + ASSET_NAME; // RTV&Friends list

        /// <summary>URL for update-checks of the asset</summary>
        public const string ASSET_UPDATE_CHECK_URL = "https://www.crosstales.com/media/assets/rtvoice_versions.txt";

        /// <summary>Contact to the owner of the asset.</summary>
        public const string ASSET_CONTACT = "rtvoice@crosstales.com";

        /// <summary>URL of the asset manual.</summary>
        public const string ASSET_MANUAL_URL = "https://www.crosstales.com/media/data/assets/rtvoice/RTVoice-doc.pdf";

        /// <summary>URL of the asset API.</summary>
        public const string ASSET_API_URL = "http://goo.gl/6w4Fy0"; // checked: 18.11.2017
        //public const string ASSET_API_URL = "http://www.crosstales.com/en/assets/rtvoice/api/";

        /// <summary>URL of the asset forum.</summary>
        public const string ASSET_FORUM_URL = "http://goo.gl/Z6MZMl"; // checked: 18.11.2017
        //public const string ASSET_FORUM_URL = "http://forum.unity3d.com/threads/rt-voice-run-time-text-to-speech-solution.340046/";

        /// <summary>URL of the asset in crosstales.</summary>
        public const string ASSET_WEB_URL = "https://www.crosstales.com/en/portfolio/rtvoice/";

        /// <summary>URL of the promotion video of the asset (Youtube).</summary>
        public const string ASSET_VIDEO_PROMO = "https://youtu.be/iVhTWDLY7g8?list=PLgtonIOr6Tb41XTMeeZ836tjHlKgOO84S";

        /// <summary>URL of the tutorial video of the asset (Youtube).</summary>
        public const string ASSET_VIDEO_TUTORIAL = "https://youtu.be/OJyVgCmX3wU?list=PLgtonIOr6Tb41XTMeeZ836tjHlKgOO84S";

        /// <summary>URL of the crosstales Facebook-profile.</summary>
        public const string ASSET_SOCIAL_FACEBOOK = "https://www.facebook.com/crosstales/";

        /// <summary>URL of the crosstales Twitter-profile.</summary>
        public const string ASSET_SOCIAL_TWITTER = "https://twitter.com/crosstales";

        /// <summary>URL of the crosstales Youtube-profile.</summary>
        public const string ASSET_SOCIAL_YOUTUBE = "https://www.youtube.com/c/Crosstales";

        /// <summary>URL of the crosstales LinkedIn-profile.</summary>
        public const string ASSET_SOCIAL_LINKEDIN = "https://www.linkedin.com/company/crosstales";

        /// <summary>URL of the crosstales XING-profile.</summary>
        public const string ASSET_SOCIAL_XING = "https://www.xing.com/companies/crosstales";

        /// <summary>URL of the 3rd party asset "PlayMaker".</summary>
        public const string ASSET_3P_PLAYMAKER = "https://www.assetstore.unity3d.com/#!/content/368?aid=1011lNGT&pubref=" + ASSET_NAME;

        /// <summary>URL of the 3rd party asset "Adventure Creator".</summary>
        public const string ASSET_3P_ADVENTURE_CREATOR = "https://www.assetstore.unity3d.com/#!/content/11896?aid=1011lNGT&pubref=" + ASSET_NAME;

        /// <summary>URL of the 3rd party asset "Cinema Director".</summary>
        public const string ASSET_3P_CINEMA_DIRECTOR = "https://www.assetstore.unity3d.com/#!/content/19779?aid=1011lNGT&pubref=" + ASSET_NAME;

        /// <summary>URL of the 3rd party asset "Dialogue System".</summary>
        public const string ASSET_3P_DIALOG_SYSTEM = "https://www.assetstore.unity3d.com/#!/content/11672?aid=1011lNGT&pubref=" + ASSET_NAME;

        /// <summary>URL of the 3rd party asset "Localized Dialogs".</summary>
        public const string ASSET_3P_LOCALIZED_DIALOGS = "https://www.assetstore.unity3d.com/#!/content/5020?aid=1011lNGT&pubref=" + ASSET_NAME;

        /// <summary>URL of the 3rd party asset "LipSync Pro".</summary>
        public const string ASSET_3P_LIPSYNC = "https://www.assetstore.unity3d.com/#!/content/32117?aid=1011lNGT&pubref=" + ASSET_NAME;

        /// <summary>URL of the 3rd party asset "NPC Chat".</summary>
        public const string ASSET_3P_NPC_CHAT = "https://www.assetstore.unity3d.com/#!/content/9723?aid=1011lNGT&pubref=" + ASSET_NAME;

        /// <summary>URL of the 3rd party asset "Quest System Pro".</summary>
        public const string ASSET_3P_QUEST_SYSTEM = "https://www.assetstore.unity3d.com/#!/content/63460?aid=1011lNGT&pubref=" + ASSET_NAME;

        /// <summary>URL of the 3rd party asset "SALSA".</summary>
        public const string ASSET_3P_SALSA = "https://www.assetstore.unity3d.com/#!/content/16944?aid=1011lNGT&pubref=" + ASSET_NAME;

        /// <summary>URL of the 3rd party asset "SLATE".</summary>
        public const string ASSET_3P_SLATE = "https://www.assetstore.unity3d.com/#!/content/56558?aid=1011lNGT&pubref=" + ASSET_NAME;

        /// <summary>URL of the 3rd party asset "THE Dialogue Engine".</summary>
        public const string ASSET_3P_DIALOGUE_ENGINE = "https://www.assetstore.unity3d.com/#!/content/42467?aid=1011lNGT&pubref=" + ASSET_NAME;

        /// <summary>URL of the 3rd party asset "uSequencer".</summary>
        public const string ASSET_3P_USEQUENCER = "https://www.assetstore.unity3d.com/#!/content/3666?aid=1011lNGT&pubref=" + ASSET_NAME;

        /// <summary>Path delimiter for Windows.</summary>
        public const string PATH_DELIMITER_WINDOWS = @"\";

        /// <summary>Path delimiter for Unix.</summary>
        public const string PATH_DELIMITER_UNIX = "/";

        // Keys for the configuration of the asset
        public const string KEY_PREFIX = "RTVOICE_CFG_";
        public const string KEY_ASSET_PATH = KEY_PREFIX + "ASSET_PATH";
        public const string KEY_DEBUG = KEY_PREFIX + "DEBUG";
        //public const string KEY_DONT_DESTROY_ON_LOAD = KEY_PREFIX + "DONT_DESTROY_ON_LOAD";
        public const string KEY_AUDIOFILE_PATH = KEY_PREFIX + "AUDIOFILE_PATH";
        public const string KEY_AUDIOFILE_AUTOMATIC_DELETE = KEY_PREFIX + "AUDIOFILE_AUTOMATIC_DELETE";

        public const string KEY_ENFORCE_32BIT_WINDOWS = KEY_PREFIX + "ENFORCE_32BIT_WINDOWS";
        //public const string KEY_TTS_MACOS = KEY_PREFIX + "TTS_MACOS";

        // Default values
        public const bool DEFAULT_DEBUG = false;
        public const bool DEFAULT_DONT_DESTROY_ON_LOAD = true;

        public static readonly string DEFAULT_AUDIOFILE_PATH = Application.temporaryCachePath;

        public const bool DEFAULT_AUDIOFILE_AUTOMATIC_DELETE = true;

        public const bool DEFAULT_ENFORCE_32BIT_WINDOWS = false;
        public const string DEFAULT_TTS_WINDOWS_BUILD = @"/RTVoiceTTSWrapper.exe";
        public const string DEFAULT_TTS_MACOS = "say";
        public const int DEFAULT_TTS_KILL_TIME = 7000;

        #endregion


        #region Changable variables

        /// <summary>Development debug logging for the asset.</summary>
        public static bool DEV_DEBUG = false;

        // Technical settings

        /// <summary>Sub-path to the TTS-wrapper under Windows (Editor).</summary>
        public static string TTS_WINDOWS_SUBPATH = "Plugins/Windows/RTVoiceTTSWrapper.exe";

        /// <summary>Sub-path to the TTS-wrapper (32bit) under Windows (Editor).</summary>
        public static string TTS_WINDOWS_x86_SUBPATH = "Plugins/Windows/RTVoiceTTSWrapper_x86.exe";

        // Text fragments for the asset
        public static string TEXT_TOSTRING_START = " {";
        public static string TEXT_TOSTRING_END = "}";
        public static string TEXT_TOSTRING_DELIMITER = "', ";
        public static string TEXT_TOSTRING_DELIMITER_END = "'";

        // Prefixes for URLs and paths
        public static string PREFIX_HTTP = "http://";
        public static string PREFIX_HTTPS = "https://";
        //public static string PREFIX_FILE = "file://";

        /// <summary>RTVoice prefab scene name.</summary>
        public const string RTVOICE_SCENE_OBJECT_NAME = "RTVoice";

        #endregion


        #region Properties

        public static string PREFIX_FILE
        { //TODO verify!
            get
            {
                if (Helper.isWindowsPlatform)
                {
                    return "file:///";
                }
                else
                {
                    return "file://";
                }
            }
        }

        #endregion
    }
}
// © 2015-2017 crosstales LLC (https://www.crosstales.com)