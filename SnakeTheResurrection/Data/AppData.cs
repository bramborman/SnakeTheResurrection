using Newtonsoft.Json;
using SnakeTheResurrection.Utilities;
using System;
using System.Reflection;

namespace SnakeTheResurrection.Data
{
    public sealed class AppData
    {
        private static readonly string filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $@"\{Constants.APP_NAME}\AppData.json";

        public static AppData Current { get; private set; }

        [JsonIgnore]
        public bool ShowLoadingError { get; set; }
        public Version LastRunAppVersion { get; set; }
        public bool ForceGameBoardBorders { get; set; }
        
        public void Save()
        {
            FileHelper.SaveObject(this, filePath);
        }
        
        public static void Load()
        {
#if DEBUG
            if (Current != null)
            {
                throw new Exception("You're not doing it right ;)");
            }
#endif

            FileHelper.LoadObjectAsyncResult<AppData> loadObjectAsyncResult = FileHelper.LoadObject<AppData>(filePath);
            Current                   = loadObjectAsyncResult.Object;
            Current.ShowLoadingError  = !loadObjectAsyncResult.Success;
            Current.LastRunAppVersion = Assembly.GetExecutingAssembly().GetName().Version;

            Current.Save();
        }
    }
}
