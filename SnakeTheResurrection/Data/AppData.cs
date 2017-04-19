using Newtonsoft.Json;
using NotifyPropertyChangedBase;
using System;
using System.IO;

namespace SnakeTheResurrection.Data
{
    public sealed class AppData : NotifyPropertyChanged
    {
        private const string FILE = "AppData.json";

        private static readonly string folderPath   = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $@"\{Constants.APP_NAME}\";
        private static readonly string filePath     = folderPath + FILE;

        public static AppData Current { get; private set; }

        public void Save()
        {
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                File.WriteAllText(filePath, JsonConvert.SerializeObject(this), Constants.encoding);
            }
            catch
            {

            }
        }

        public static void Load()
        {
#if DEBUG
            if (Current != null)
            {
                throw new Exception("You're not doing it right ;)");
            }
#endif

            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath, Constants.encoding);
                    Current = string.IsNullOrWhiteSpace(json) ? new AppData() : JsonConvert.DeserializeObject<AppData>(json);
                }
                else
                {
                    Current = new AppData();
                }
            }
            catch
            {

            }
        }
    }
}
