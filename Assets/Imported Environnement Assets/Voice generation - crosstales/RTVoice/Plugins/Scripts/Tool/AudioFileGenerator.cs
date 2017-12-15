using UnityEngine;
using NAudio.Wave;
using System;
using System.Linq;
using System.IO;

namespace Crosstales.RTVoice.Tool
{
    /// <summary>Process files with configured speeches.</summary>
    
    [HelpURL("https://crosstales.com/media/data/assets/rtvoice/api/class_crosstales_1_1_r_t_voice_1_1_tool_1_1_audio_file_generator.html")]
    public class AudioFileGenerator : MonoBehaviour
    {
        
        #region Variables

        /// <summary>Text files to generate.</summary>
        [Tooltip("Text files to generate.")]
        public TextAsset[] TextFiles;

        /// <summary>Are the specified file paths inside the Assets-folder (current project)? If this option is enabled, it prefixes the path with 'Application.dataPath'.</summary>
        [Tooltip("Are the specified file paths inside the Assets-folder (current project)? If this option is enabled, it prefixes the path with 'Application.dataPath'.")]
        public bool FileInsideAssets = true;

        /// <summary>Folder where audio files are created from folder Asset ex "Audio\RTgeneration".</summary>
        [Tooltip("Text files to generate.")]
        public string inFolder = @"/Audio/RTgeneration";
        public string outFolder = @"/Audio/RTgenerationSampled/";
        private static char[] splitChar = new char[] { ';' };

        #endregion


        #region MonoBehaviour methods

        public void Start()
        {
            Speaker.OnSpeakAudioGenerationComplete += onSpeakAudioGenerationComplete;
            Speaker.OnVoicesReady += onVoicesReady;
        }

        public void OnDestroy()
        {
            Speaker.OnSpeakAudioGenerationComplete -= onSpeakAudioGenerationComplete;
            Speaker.OnVoicesReady -= onVoicesReady;
        }

        #endregion


        #region Public methods

        /// <summary>Generate the audio files from the text files.</summary>
        public void Generate()
        {
            foreach (TextAsset textFile in TextFiles)
            {
                if (textFile != null)
                {
                    System.Collections.Generic.List<string> speeches = Util.Helper.SplitStringToLines(textFile.text);

                    foreach (string speech in speeches)
                    {

                        if (!speech.StartsWith("#"))
                        {
                            string[] args = speech.Split(splitChar, System.StringSplitOptions.RemoveEmptyEntries);

                            if (args.Length >= 2)
                            {

                                string text = args[0];

                                string outputFile = null;
                                if (FileInsideAssets)
                                {
                                    outputFile = Application.dataPath + @"/" + args[1];
                                }
                                else
                                {
                                    outputFile = args[1];
                                }

                                Model.Voice voice = null;
                                if (args.Length >= 3)
                                {
                                    voice = Speaker.VoiceForName(args[2]);
                                }

                                float rate = 1f;
                                if (args.Length >= 4)
                                {
                                    if (!float.TryParse(args[3], out rate))
                                    {
                                        Debug.LogWarning("Rate was invalid: " + speech);
                                    }
                                }

                                float pitch = 1f;
                                if (args.Length >= 5)
                                {
                                    if (!float.TryParse(args[4], out pitch))
                                    {
                                        Debug.LogWarning("Pitch was invalid: " + speech);
                                    }
                                }

                                float volume = 1f;
                                if (args.Length >= 6)
                                {
                                    if (!float.TryParse(args[5], out volume))
                                    {
                                        Debug.LogWarning("Volume was invalid: " + speech);
                                    }
                                }

                                if (Util.Helper.isEditorMode)
                                {
#if UNITY_EDITOR
                                    Speaker.GenerateInEditor(text, voice, rate, volume, pitch, outputFile);
#endif
                                }
                                else
                                {
                                    Speaker.Generate(text, outputFile, voice, rate, pitch, volume);
                                }
                            }
                            else
                            {
                                Debug.LogWarning("Invalid speech: " + speech);
                            }
                        }
                    }
                }
            }
            SamplingTo16k();
        }

        /// <summary>Generate the audio files sampled in 16kHz from the generated .wav files.</summary>
        public void SamplingTo16k()
        {
            Debug.Log("Sampling generated .wav files to 16kHz");
            int outRate = 16000;
            string outFile;
            DirectoryInfo dir = new DirectoryInfo(Application.dataPath+inFolder);
            FileInfo[] info = dir.GetFiles("*.wav");
            string[] fullNames = info.Select(f => f.FullName).ToArray();
            if (fullNames.Length==0)
            {
                Debug.Log("Generate a second time for sampling please");
            }
            foreach (string inFile in fullNames)
            {
                string name = Path.GetFileName(inFile);
                WaveFileReader reader = new NAudio.Wave.WaveFileReader(inFile);
                WaveFormat newFormat = new WaveFormat(outRate, 16, 1);
                WaveFormatConversionStream str = new WaveFormatConversionStream(newFormat, reader);
                outFile = Application.dataPath + outFolder + @"/" + name;
                try
                {
                    WaveFileWriter.CreateWaveFile(outFile, str);
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
                finally
                {
                    str.Close();
                }
            }

            Debug.Log("Conversion done");
        }

    #endregion


    #region Callbacks


    private void onVoicesReady()
    {
        Generate();
    }

    private void onSpeakAudioGenerationComplete(Model.Wrapper wrapper)
    {
        // Debug.Log("Speech generated: " + wrapper);
    }

    #endregion
    
}
}
// © 2017 crosstales LLC (https://www.crosstales.com)