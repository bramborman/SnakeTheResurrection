﻿using SnakeTheResurrection.Data;
using System;
using System.Runtime.CompilerServices;

namespace SnakeTheResurrection
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.Title           = Constants.APP_NAME;
            Console.CursorVisible   = false;

            Console.InputEncoding   = Constants.encoding;
            Console.OutputEncoding  = Constants.encoding;
            Console.ForegroundColor = Constants.FOREGROUND_COLOR;

            AppData.Load();
            ProfileManager.LoadProfiles();

            Console.Write("Look at dis ᕕ(⪦╭͜ʖ╮⪧)ᕗ");
            Console.ReadKey();

            MainMenu.Show();

#if DEBUG
            throw new Exception("Y u do dis ಠ_ಠ");
#else
            Exit();
#endif
        }

        public static void Exit([CallerMemberName]string callerMemberName = null)
        {
            AppData.Current.Save();
            ProfileManager.SaveProfiles();
            Environment.Exit(callerMemberName == nameof(Main) ? 1 : 0);
        }
    }
}
