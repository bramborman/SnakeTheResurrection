using Newtonsoft.Json;
using System;
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
            try
            {
                string json = File.ReadAllText(filePath, Constants.encoding);
                T obj = null;

                if (!string.IsNullOrWhiteSpace(json))
                {
                    obj = JsonConvert.DeserializeObject<T>(json);
                }

                return (obj ?? new T(), true);
            }
            catch (Exception exception)
            {
                return (new T(), exception is FileNotFoundException);
            }
        }
    }
}
