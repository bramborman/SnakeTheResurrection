using Newtonsoft.Json;
using System.IO;

namespace SnakeTheResurrection.Utilities
{
    // Ported from my StorageFileHelper from UWPHelper - https://github.com/bramborman/UWPHelper/blob/master/UWPHelper/Utilities/StorageFileHelper.cs
    public static class FileHelper
    {
        public static bool SaveObject(object obj, string filePath)
        {
            ExceptionHelper.ValidateStringNotNullOrWhiteSpace(filePath, nameof(filePath));

            bool success    = true;
            string fileName = Path.GetFileName(filePath);

            try
            {
                string folderPath = filePath.Substring(0, filePath.Length - fileName.Length);

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

        public static LoadObjectAsyncResult<T> LoadObject<T>(string filePath) where T : class, new()
        {
            ExceptionHelper.ValidateStringNotNullOrWhiteSpace(filePath, nameof(filePath));
            
            if (!File.Exists(filePath))
            {
                return new LoadObjectAsyncResult<T>(new T(), true);
            }

            bool success    = true;
            T obj           = null;

            // Reading from the file could fail while the file is used by another proccess
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
            
            return new LoadObjectAsyncResult<T>(obj ?? new T(), success);
        }
        
        public sealed class LoadObjectAsyncResult<T> where T : class, new()
        {
            public T Object { get; }
            public bool Success { get; }

            public LoadObjectAsyncResult(T @object, bool success)
            {
                Object  = @object;
                Success = success;
            }
        }
    }
}
