using EU.CqrXs.Crypt;
using EU.CqrXs.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace EU.CqrXs.Gui.Helper
{
    public class Settings
    {
        private static readonly Lazy<Settings> _instance = new Lazy<Settings>(() => new Settings());

        public bool WarnOnEmptyPipe { get; set; }

        public bool WarnOnDoubleZipping { get; set; }

        public bool VerifyEncryptionSha512 { get; set; }

        public bool VerifyEncryptionOneEightBytes { get; set; }

        public bool CreatePipeSettingFromFileName { get; set; }

        public bool AutomaticallySaveToTemp { get; set; }

        public bool SaveSettingsToJson { get; set; }

        public SystemColorMode ColorMode { get; set; }

        public FormMode LastForm { get; set; }


        [JsonIgnore]
        public Settings Instance { get => _instance.Value; }

        public Settings()
        {
            WarnOnEmptyPipe = false;
            WarnOnDoubleZipping = false;
            VerifyEncryptionSha512 = true;
            VerifyEncryptionOneEightBytes = false;
            CreatePipeSettingFromFileName = false;
            AutomaticallySaveToTemp = true;
            SaveSettingsToJson = false;
            ColorMode = SystemColorMode.System;
            LastForm = FormMode.Complex;

        }

        #region static members Load() Save(Settings? settings)

        /// <summary>
        /// loads json serialized Settings data string from 
        /// <see cref="Program.ProgDirPazh"/> + <see cref="Constants.JSON_SETTINGS_FILE"/>
        /// and deserialize it to singleton instance <see cref="Settings"/> of <seealso cref="Lazy{Settings}"/>
        /// </summary>
        /// <param name="jsonFileName">fileName of serialized json</param>
        /// <returns>singelton <see cref="CqrSettings.Instance"/></returns>
        public static Settings Load(string jsonFileName = null)
        {
            string settingsJsonString = string.Empty;
            Settings settings = null;
            jsonFileName = jsonFileName ?? Path.Combine(Program.ProgDirPazh, Constants.JSON_SETTINGS_FILE);
            try
            {
                if (File.Exists(jsonFileName))
                {
                    settingsJsonString = File.ReadAllText(jsonFileName);
                    settings = JsonConvert.DeserializeObject<Settings>(settingsJsonString);
                }
            }
            catch (Exception ex)
            {
                CException.SetLastException(ex);
            }

            if (settings != null)
            {
                _instance.Value.WarnOnEmptyPipe = settings.WarnOnEmptyPipe;
                _instance.Value.WarnOnDoubleZipping = settings.WarnOnDoubleZipping;
                _instance.Value.VerifyEncryptionSha512 = settings.VerifyEncryptionSha512;
                _instance.Value.VerifyEncryptionOneEightBytes = settings.VerifyEncryptionOneEightBytes;
                _instance.Value.CreatePipeSettingFromFileName = settings.CreatePipeSettingFromFileName;
                _instance.Value.AutomaticallySaveToTemp = settings.AutomaticallySaveToTemp;
                _instance.Value.SaveSettingsToJson = settings.SaveSettingsToJson;
                _instance.Value.ColorMode = settings.ColorMode;
                _instance.Value.LastForm = settings.LastForm;
            
            }

            return _instance.Value;
        }


        /// <summary>
        /// json serializes <see cref="Settings"/> and 
        /// saves json serialized data string to 
        /// <see cref="Program.ProgDirPazh"/> + <see cref="Constants.JSON_SETTINGS_FILE"/>
        /// </summary>
        /// <param name="Settings">settings to save</param>
        /// <param name="jsonFileName">filename, where writing serialized json</param>
        /// <returns>true on successfully save</returns>
        public static bool Save(Settings settings = null, string jsonFileName = null)
        {
            settings = settings ?? Settings._instance.Value;
            jsonFileName = jsonFileName ?? Path.Combine(Program.ProgDirPazh, Constants.JSON_SETTINGS_FILE);
            try
            {
                string saveString = JsonConvert.SerializeObject(settings);
                File.WriteAllText(jsonFileName, saveString);
            }
            catch (Exception ex)
            {
                CException.SetLastException(ex);
                return false;
            }

            return true;
        }

        #endregion static members Load() Save(Settings? settings)


    }
}
