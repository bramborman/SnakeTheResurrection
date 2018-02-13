using Newtonsoft.Json;
using System.IO;

namespace SnakeTheResurrection.Utilities
{
    public static class FileHelper
    {
        public static bool SaveObject(object obj, string filePath)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                File.WriteAllText(filePath, JsonConvert.SerializeObject(obj), Constants.encoding);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static (T obj, bool success) LoadObject<T>(string filePath) where T : class, new()
        {
            bool success = true;
            T obj = null;

            if (File.Exists(filePath))
            {
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
            }
            
            return (obj ?? new T(), success);
        }
    }
}
