using SnakeTheResurrection.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SnakeTheResurrection.Data
{
    public sealed class AppData
    {
        private static readonly string filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $@"\{Constants.APP_NAME}\AppData.json";

        public static AppData Current { get; private set; }
        
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

            bool success = false;
            int attempts = 0;

            while (!success)
            {
                (Current, success) = FileHelper.LoadObject<AppData>(filePath);
                attempts++;

                if (!success)
                {
                    Symtext.WriteTitle("Error :[", -35);
                    Symtext.SetTextProperties();

                    const string LONGEST = " - closing programs that may be using this game's configuration file";
                    int cursorLeft = (Console.WindowWidth - Symtext.GetSymtextWidth(LONGEST)) / 2;

                    Symtext.CursorLeft = cursorLeft;
                    Symtext.WriteLine($"Couldn't load configuration file (attempts: {attempts})");
                    Symtext.CursorLeft = cursorLeft;
                    Symtext.WriteLine("You may try some of these to resolve this issue:");
                    Symtext.CursorLeft = cursorLeft;
                    Symtext.WriteLine(" - restarting this game");
                    Symtext.CursorLeft = cursorLeft;
                    Symtext.WriteLine(LONGEST);
                    Symtext.CursorLeft = cursorLeft;
                    Symtext.WriteLine(" - restarting your computer");
                    Symtext.WriteLine();
                    Symtext.WriteLine();

                    int result = new ListMenu()
                    {
                        Items = new List<MenuItem>()
                        {
                            new MenuItem("Try again"),
                            new MenuItem("Use default configuration"),
                            new MenuItem("Quit game")
                        }
                    }.GetResult();
                    
                    if (result == 1)
                    {
                        break;
                    }
                    else if (result == 2)
                    {
                        Program.FullExit(error: true);
                        return;
                    }
                }
            }

            Current.LastRunAppVersion = Assembly.GetExecutingAssembly().GetName().Version;
            Current.Save();
        }
    }
}
