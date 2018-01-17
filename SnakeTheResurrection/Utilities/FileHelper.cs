using Newtonsoft.Json;
using System.IO;

namespace SnakeTheResurrection.Utilities
{
    public static class FileHelper
    {
        public static bool SaveObject(object obj, string filePath)
        {
            ExceptionHelper.ValidateStringNotNullOrWhiteSpace(filePath, nameof(filePath));

            bool success = true;

            try
            {
                string folderPath = Path.GetDirectoryName(filePath);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                
                File.WriteAllText(filePath, JsonConvert.SerializeObject(obj), Constants.encoding);
            }
            catch
            {
                success = false;
            }
            
            return success;
        }

        public static (T obj, bool success) LoadObject<T>(string filePath) where T : class, new()
        {
            ExceptionHelper.ValidateStringNotNullOrWhiteSpace(filePath, nameof(filePath));
            
            if (!File.Exists(filePath))
            {
                return (new T(), true);
            }

            bool success = true;
            T obj = null;

            // Reading from the file could fail if the file is used by another proccess
            try
            {
                string json = File.ReadAllText(filePath, Constants.encoding);

                if (!string.IsNullOrWhiteSpace(json))
                {
                    obj = JsonConvert.DeserializeObject<T>(json);
                }
            }
            catch
            {
                success = false;
            }
            
            return (obj ?? new T(), success);
        }
    }
}
