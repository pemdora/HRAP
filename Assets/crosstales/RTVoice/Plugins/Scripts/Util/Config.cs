namespace Crosstales.RTVoice.Util
{
    /// <summary>Configuration for the asset.</summary>
    public static class Config
    {

        #region Changable variables

        /// <summary>Path to the asset inside the Unity project.</summary>
        public static string ASSET_PATH = "/crosstales/RTVoice/";

        /// <summary>Enable or disable debug logging for the asset.</summary>
        public static bool DEBUG = Constants.DEFAULT_DEBUG;

        /// <summary>Don't destroy the objects during scene switches.</summary>
        public static bool DONT_DESTROY_ON_LOAD = Constants.DEFAULT_DONT_DESTROY_ON_LOAD;

        /// <summary>Path to the generated audio files.</summary>
        public static string AUDIOFILE_PATH = Constants.DEFAULT_AUDIOFILE_PATH;

        /// <summary>Automatically delete the generated audio files.</summary>
        public static bool AUDIOFILE_AUTOMATIC_DELETE = Constants.DEFAULT_AUDIOFILE_AUTOMATIC_DELETE;
        //public static bool AUDIOFILE_AUTOMATIC_DELETE = false;

        /// <summary>Enforce 32bit versions of voices under Windows.</summary>
        public static bool ENFORCE_32BIT_WINDOWS = Constants.DEFAULT_ENFORCE_32BIT_WINDOWS;

        // Technical settings

        /// <summary>Location of the TTS-wrapper under Windows (stand-alone).</summary>
        public static string TTS_WINDOWS_BUILD = Constants.DEFAULT_TTS_WINDOWS_BUILD;

        /// <summary>Location of the TTS-system under MacOS.</summary>
        public static string TTS_MACOS = Constants.DEFAULT_TTS_MACOS;

        /// <summary>Is the configuration loaded?</summary>
        public static bool isLoaded = false;

        #endregion


        #region Properties

        /// <summary>Location of the TTS-wrapper under Windows (Editor).</summary>
        public static string TTS_WINDOWS_EDITOR
        {
            get
            {
                return ASSET_PATH + Constants.TTS_WINDOWS_SUBPATH;
            }
        }

        /// <summary>Location of the TTS-wrapper (32bit) under Windows (Editor).</summary>
        public static string TTS_WINDOWS_EDITOR_x86
        {
            get
            {
                return ASSET_PATH + Constants.TTS_WINDOWS_x86_SUBPATH;
            }
        }

        #endregion


        #region Public static methods

        /// <summary>Resets all changable variables to their default value.</summary>
        public static void Reset()
        {
            DEBUG = Constants.DEFAULT_DEBUG;
            DONT_DESTROY_ON_LOAD = Constants.DEFAULT_DONT_DESTROY_ON_LOAD;
            AUDIOFILE_PATH = Constants.DEFAULT_AUDIOFILE_PATH;
            AUDIOFILE_AUTOMATIC_DELETE = Constants.DEFAULT_AUDIOFILE_AUTOMATIC_DELETE;

            ENFORCE_32BIT_WINDOWS = Constants.DEFAULT_ENFORCE_32BIT_WINDOWS;
            TTS_WINDOWS_BUILD = Constants.DEFAULT_TTS_WINDOWS_BUILD;
            TTS_MACOS = Constants.DEFAULT_TTS_MACOS;
        }

        /// <summary>Loads all changable variables.</summary>
        public static void Load()
        {
            if (CTPlayerPrefs.HasKey(Constants.KEY_ASSET_PATH))
            {
                ASSET_PATH = CTPlayerPrefs.GetString(Constants.KEY_ASSET_PATH);
            }

            if (CTPlayerPrefs.HasKey(Constants.KEY_DEBUG))
            {
                DEBUG = CTPlayerPrefs.GetBool(Constants.KEY_DEBUG);
            }

            //if (CTPlayerPrefs.HasKey(Constants.KEY_DONT_DESTROY_ON_LOAD))
            //{
            //    DONT_DESTROY_ON_LOAD = CTPlayerPrefs.GetBool(Constants.KEY_DONT_DESTROY_ON_LOAD);
            //}

            if (CTPlayerPrefs.HasKey(Constants.KEY_AUDIOFILE_PATH))
            {
                AUDIOFILE_PATH = CTPlayerPrefs.GetString(Constants.KEY_AUDIOFILE_PATH);
            }

            if (CTPlayerPrefs.HasKey(Constants.KEY_AUDIOFILE_AUTOMATIC_DELETE))
            {
                AUDIOFILE_AUTOMATIC_DELETE = CTPlayerPrefs.GetBool(Constants.KEY_AUDIOFILE_AUTOMATIC_DELETE);
            }

            if (CTPlayerPrefs.HasKey(Constants.KEY_ENFORCE_32BIT_WINDOWS))
            {
                ENFORCE_32BIT_WINDOWS = CTPlayerPrefs.GetBool(Constants.KEY_ENFORCE_32BIT_WINDOWS);
            }

            //if (CTPlayerPrefs.HasKey(Constants.KEY_TTS_MACOS))
            //{
            //    TTS_MACOS = CTPlayerPrefs.GetString(Constants.KEY_TTS_MACOS);
            //}

            isLoaded = true;
        }

        /// <summary>Saves all changable variables.</summary>
        public static void Save()
        {
            CTPlayerPrefs.SetBool(Constants.KEY_DEBUG, DEBUG);
            //CTPlayerPrefs.SetBool(Constants.KEY_DONT_DESTROY_ON_LOAD, DONT_DESTROY_ON_LOAD);
            CTPlayerPrefs.SetString(Constants.KEY_AUDIOFILE_PATH, AUDIOFILE_PATH);
            CTPlayerPrefs.SetBool(Constants.KEY_AUDIOFILE_AUTOMATIC_DELETE, AUDIOFILE_AUTOMATIC_DELETE);
            CTPlayerPrefs.SetBool(Constants.KEY_ENFORCE_32BIT_WINDOWS, ENFORCE_32BIT_WINDOWS);
            //CTPlayerPrefs.SetString(Constants.KEY_TTS_MACOS, TTS_MACOS);

            CTPlayerPrefs.Save();
        }

        #endregion
    }
}
// © 2017 crosstales LLC (https://www.crosstales.com)