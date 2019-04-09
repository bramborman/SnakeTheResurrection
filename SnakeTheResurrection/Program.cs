using SnakeTheResurrection.Data;
using SnakeTheResurrection.Utilities;
using SnakeTheResurrection.Utilities.UI;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SnakeTheResurrection
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = Constants.APP_NAME;
            RuntimeHelpers.RunClassConstructor(typeof(Renderer).TypeHandle);

            Window.Children.Add(new TextBlock()
            {
                Text = Constants.APP_SHORT_NAME,
                ForegroundColor = (Color)Constants.ACCENT_COLOR,
                FontSize = 25,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = Symtext.GetSymtextWidth("Snake", 25, 25),
                Height = Symtext.GetCharHeight(25)
            });
            TextBlock prompt = new TextBlock()
            {
                Text = "Loading . . .",
                FontSize = 2,
                Margin = new Thickness(20),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Width = Symtext.GetSymtextWidth("Loading . . .", 2, 2),
                Height = Symtext.GetCharHeight(2)
            };
            Window.Children.Add(prompt);
            Window.Compositor.Start();

            AppData.Load();

            Stopwatch stopwatch = new Stopwatch();

            void BeforeRendering()
            {
                if (stopwatch.ElapsedMilliseconds > 850)
                {
                    stopwatch.Restart();
                    prompt.IsVisible = !prompt.IsVisible;
                }
            }
            
            Window.Compositor.AddToDispatchQueue(() =>
            {
                prompt.Text = "Press any key to continue . . .";
                prompt.HorizontalAlignment = HorizontalAlignment.Center;
                prompt.VerticalAlignment = VerticalAlignment.Center;
                prompt.Width = Symtext.GetSymtextWidth(prompt.Text, prompt.FontSize, prompt.CharacterSpacing);
                prompt.Margin = new Thickness(0, 150);
            });
            Window.Compositor.BeforeRendering += BeforeRendering;
            stopwatch.Start();
            InputHelper.ClearInputBuffer();
            Console.ReadKey(true);
            Window.Compositor.BeforeRendering -= BeforeRendering;
            Window.Compositor.Stop();
            stopwatch.Stop();

            MainMenu();

#if DEBUG
            throw new Exception("Y u do dis ಠ_ಠ");
#else
            FullExit();
#endif
        }
        
        private static void MainMenu()
        {
            ListMenu mainMenu = new ListMenu
            {
                Items = new[]
                {
#pragma warning disable IDE0011 // Add braces
                    new MenuItem("Singleplayer",    () => { while (Game.Play(false)) ; }    ),
                    new MenuItem("Multiplayer",     () => { while (Game.Play(true)) ; }     ),
                    new MenuItem("About",           About                                   ),
                    new MenuItem("Quit game",       () => FullExit()                        )
#pragma warning restore IDE0011 // Add braces
                }
            };
            
            while (true)
            {
                Renderer.Clear();
                Symtext.WriteTitle(Constants.APP_SHORT_NAME, 7);
                
                mainMenu.InvokeResult();
                InputHelper.ClearInputBuffer();
            }
        }

        private static void About()
        {
            bool goBack = false;

            ListMenu aboutMenu = new ListMenu
            {
                Items = new[]
                {
                    new MenuItem("GitHub repo", () => Process.Start("https://github.com/bramborman/SnakeTheResurrection")   ),
                    new MenuItem("Game font",   () => Process.Start("https://www.dafont.com/symtext.font")                  ),
                    new MenuItem("Back",        () => goBack = true                                                         )
                },
                SelectedIndex = 2
            };

            // Whole screen has to be rendered every time, because opening the link causes everything on screen to disappear
            do
            {
                Symtext.WriteTitle("About", 0);
                Symtext.SetCenteredTextProperties();

                Version gameVersion = AppData.Current.LastRunAppVersion;
                Symtext.WriteLine($"{Constants.APP_SHORT_NAME} v{gameVersion.Major}.{gameVersion.Minor}.{gameVersion.Build} '{Constants.APP_NAME_ADDITION}'");
                Symtext.WriteLine("© 2018 Marian Dolinsk§\n");

                aboutMenu.InvokeResult();
            } while (!goBack);
        }

        public static void FullExit(bool error = false)
        {
            if (!error)
            {
                AppData.Current.Save();
            }

            Environment.Exit(error ? 1 : 0);
        }
    }
}
