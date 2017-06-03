﻿using Newtonsoft.Json;
using NotifyPropertyChangedBase;
using SnakeTheResurrection.Utilities;
using System;
using System.Collections.Generic;

namespace SnakeTheResurrection.Data
{
    public sealed class AppData : NotifyPropertyChanged
    {
        private static readonly string filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $@"\{Constants.APP_NAME}\AppData.json";

        public static AppData Current { get; private set; }

        [JsonIgnore]
        public bool ShowLoadingError { get; set; }
        public bool EnableDiagonalMovement
        {
            get { return (bool)GetValue(); }
            set { SetValue(value); }
        }
        public bool ForceGameBoardBorders
        {
            get { return (bool)GetValue(); }
            set { SetValue(value); }
        }
        
        public AppData()
        {
            RegisterProperty(nameof(EnableDiagonalMovement), typeof(bool), true);
            RegisterProperty(nameof(ForceGameBoardBorders), typeof(bool), false);
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

            FileHelper.LoadObjectAsyncResult<AppData> loadObjectAsyncResult = FileHelper.LoadObject<AppData>(filePath);
            Current                   = loadObjectAsyncResult.Object;
            Current.ShowLoadingError  = !loadObjectAsyncResult.Success;

            Current.PropertyChanged += (sender, e) =>
            {
                Current.Save();
            };
        }

        public List<string> TryParse(string[] args)
        {
            IsPropertyChangedEventInvokingEnabled = false;
            List<string> status = new List<string>();

            // We require pairs - arg & value
            if (args.Length % 2 != 0)
            {
                status.Add("Invalid arguments count.");
            }
            else
            {
                for (int i = 0; i < args.Length; i += 2)
                {
                    string arg = args[i];
                    string value = args[i + 1];

                    try
                    {
                        if (arg.StartsWith("-") || arg.StartsWith("/"))
                        {
                            arg = arg.Substring(1).ToLower();

                            // I may use reflection here, but would it be secure?
                            if (arg == nameof(EnableDiagonalMovement).ToLower())
                            {
                                EnableDiagonalMovement = TryGetBool(value);
                            }
                            else if (arg == nameof(ForceGameBoardBorders).ToLower())
                            {
                                ForceGameBoardBorders = TryGetBool(value);
                            }
                            else
                            {
                                throw new Exception();
                            }

                            status.Add($"Successfully set value '{value}' to property '{arg}'.");
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    catch
                    {
                        status.Add($"Unable to find property '{arg}' or assign value '{value}' into it.");
                    }
                }
            }

            Save();
            IsPropertyChangedEventInvokingEnabled = true;
            return status;
        }

        private bool TryGetBool(string value)
        {
            if (bool.TryParse(value, out bool result))
            {
                return result;
            }

            throw new Exception();
        }
    }
}
