using Newtonsoft.Json;
using NotifyPropertyChangedBase;
using SnakeTheResurrection.Utilities;
using System;

namespace SnakeTheResurrection.Data
{
    public sealed class AppData : NotifyPropertyChanged
    {
        private static readonly string filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $@"\{Constants.APP_NAME}\AppData.json";

        public static AppData Current { get; private set; }

        [JsonIgnore]
        public bool ShowLoadingError { get; set; }
        public int Counter
        {
            get { return (int)GetValue(); }
            set { SetValue(value); }
        }

        public AppData()
        {
            RegisterProperty(nameof(Counter), typeof(int), 0);
        }
        
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

            var loadObjectAsyncResult = FileHelper.LoadObject<AppData>(filePath);
            Current                   = loadObjectAsyncResult.Object;
            Current.ShowLoadingError  = !loadObjectAsyncResult.Success;

            Current.PropertyChanged += (sender, e) =>
            {
                Current.Save();
            };
        }
    }
}
