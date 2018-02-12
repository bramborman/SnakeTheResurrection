using SnakeTheResurrection.Data;
using SnakeTheResurrection.Utilities;
using SnakeTheResurrection.Utilities.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SnakeTheResurrection
{
    public static class Program
    {
        private static int originalWindowLeft;
        private static int originalWindowTop;
        private static int originalWindowWidth;
        private static int originalWindowHeight;

        public static void Main(string[] args)
        {
            originalWindowLeft      = Console.WindowLeft;
            originalWindowTop       = Console.WindowTop; 
            originalWindowWidth     = Console.WindowWidth;
            originalWindowHeight    = Console.WindowHeight;

            Console.Title = Constants.APP_NAME;

            // Don't you dare try uncommenting this (ง⸟ᨎ⸟)ง
            // Console.InputEncoding   = Constants.encoding;
            // Console.OutputEncoding  = Constants.encoding;

            // Run the static constructor of Renderer
            RuntimeHelpers.RunClassConstructor(typeof(Renderer).TypeHandle);

            //UIPlayground();

            Window.Children.Add(new TextBlock()
            {
                Text = Constants.APP_SHORT_NAME,
                ForegroundColor = (Color)Constants.ACCENT_COLOR,
                FontSize = 25,
                HorizontalAlignment = Utilities.UI.HorizontalAlignment.Center,
                VerticalAlignment = Utilities.UI.VerticalAlignment.Center,
                Width = Symtext.GetSymtextWidth("Snakee", 25),
                Height = Symtext.GetCharHeight(25)
            });
            TextBlock prompt = new TextBlock()
            {
                Text = "Loading . . .",
                FontSize = 2,
                Margin = new Thickness(20),
                HorizontalAlignment = Utilities.UI.HorizontalAlignment.Right,
                VerticalAlignment = Utilities.UI.VerticalAlignment.Bottom,
                Width = Symtext.GetSymtextWidth("Loading . . .", 3),
                Height = Symtext.GetCharHeight(2)
            };
            Window.Children.Add(prompt);
            Window.Compositor.Run();

            AppData.Load();
            ProfileManager.LoadProfiles();
            
            int counter = 0;

            void BeforeRendering()
            {
                if (++counter == 17)
                {
                    counter = 0;
                    prompt.IsVisible = !prompt.IsVisible;
                }
            }
            
            Window.Compositor.AddToDispatchQueue(() =>
            {
                prompt.Text = "Press any key to continue . . .";
                prompt.HorizontalAlignment = Utilities.UI.HorizontalAlignment.Center;
                prompt.VerticalAlignment = Utilities.UI.VerticalAlignment.Center;
                prompt.Width = Symtext.GetSymtextWidth(prompt.Text + " . . .", prompt.FontSize);
                prompt.Margin = new Thickness(0, 150);
            });
            Window.Compositor.BeforeRendering += BeforeRendering;
            InputHelper.ClearInputBuffer();
            Console.ReadKey(true);
            Window.Compositor.BeforeRendering -= BeforeRendering;
            Window.Compositor.Stop();

            MainMenu();

#if DEBUG
            throw new Exception("Y u do dis ಠ_ಠ");
#else
            FullExit();
#endif
        }
        
        private static void UIPlayground()
        {
            Random r = new Random();
            //TextBlock e = new TextBlock()
            //{
            //    Text = "Hello World! ",
            //    ForegroundColor = Utilities.UI.Colors.Black,
            //    Width = 30,
            //    Height = 100,
            //    VerticalAlignment = Utilities.UI.VerticalAlignment.Stretch,
            //    HorizontalAlignment = Utilities.UI.HorizontalAlignment.Stretch,
            //    BackgroundColor = Utilities.UI.Colors.Cyan,
            //    BorderThickness = new Thickness(17),
            //    BorderColor = Utilities.UI.Colors.Red,
            //    Padding = new Thickness(5),
            //    Margin = new Thickness(7),
            //    FontSize = 3
            //};
            //
            //for (int i = 0; i < 1000; i++)
            //{
            //    e.Text += "Hello World! ";
            //}
            //
            //int framesCounter = 0;
            //
            //Window.Children.Add(e);
            //Window.Compositor.BeforeRendering += () =>
            //{
            //    if (framesCounter++ % 30 == 0)
            //    {
            //        try
            //        {
            //            e.Width = r.Next(1, Console.WindowWidth / 2);
            //            e.Height = r.Next(1, Console.WindowHeight / 2);
            //            e.HorizontalAlignment = (Utilities.UI.HorizontalAlignment)r.Next(0, 4);
            //            e.VerticalAlignment = (Utilities.UI.VerticalAlignment)r.Next(0, 4);
            //            e.BorderThickness = new Thickness(r.Next(0, Math.Min(Console.WindowWidth, Console.WindowHeight) / 4));
            //            e.BorderColor = (Color)r.Next(1, 16);
            //            e.FontSize = r.Next(1, e.Height / 7 / 2);
            //
            //            do
            //            {
            //                e.BackgroundColor = (Color)r.Next(1, 16);
            //            } while (e.BackgroundColor == e.BorderColor);
            //        }
            //        catch
            //        {
            //
            //        }
            //    }
            //};
            //TextBlock splashBlock = new TextBlock()
            //{
            //    Text = "Snake",
            //    TextWrapping = TextWrapping.NoWrap,
            //    FontSize = 15,
            //    ForegroundColor = (Color)Constants.ACCENT_COLOR,
            //    HorizontalAlignment = Utilities.UI.HorizontalAlignment.Stretch,
            //    VerticalAlignment = Utilities.UI.VerticalAlignment.Center,
            //    Height = 15 * 7,
            //    Margin = new Thickness(-Symtext.GetSymtextWidth("Snakee", 15), 0, 0, 0)
            //};
            //Window.Children.Add(splashBlock);
            //Window.Compositor.BeforeRendering += () =>
            //{
            //    splashBlock.Margin = new Thickness(splashBlock.Margin.Left + 1, splashBlock.Margin.Top, splashBlock.Margin.Right, splashBlock.Margin.Bottom);
            //};
            StackPanel stackPanel = new StackPanel()
            {
                BackgroundColor = Utilities.UI.Colors.Yellow,
                HorizontalAlignment = Utilities.UI.HorizontalAlignment.Stretch,
                VerticalAlignment = Utilities.UI.VerticalAlignment.Stretch
            };
            for (int i = 0; i < 10; i++)
            {
                StackPanel sp = new StackPanel()
                {
                    BackgroundColor = (Color)r.Next(1, 15),
                    HorizontalAlignment = Utilities.UI.HorizontalAlignment.Stretch,
                    Orientation = Orientation.Horizontal,
                    Height = 100
                };

                for (int j = 0; j < 5; j++)
                {
                    sp.Items.Add(new TextBlock()
                    {
                        Text = "Hello World! ",
                        ForegroundColor = Utilities.UI.Colors.Black,
                        Width = 200,
                        Height = 50,
                        BackgroundColor = Utilities.UI.Colors.Cyan,
                        BorderThickness = new Thickness(17),
                        BorderColor = Utilities.UI.Colors.Red,
                        Padding = new Thickness(10),
                        Margin = new Thickness(20),
                        FontSize = 3
                    });
                }

                stackPanel.Items.Add(sp);
            }
            //stackPanel.Items.Add(new TextBlock()
            //{
            //    Text = "Hello World 2! ",
            //    ForegroundColor = Utilities.UI.Colors.Black,
            //    Width = 30,
            //    Height = 100,
            //    HorizontalAlignment = Utilities.UI.HorizontalAlignment.Center,
            //    BackgroundColor = Utilities.UI.Colors.Blue,
            //    BorderThickness = new Thickness(17),
            //    BorderColor = Utilities.UI.Colors.Magenta,
            //    Padding = new Thickness(5),
            //    Margin = new Thickness(7),
            //    FontSize = 3
            //});
            //stackPanel.Items.Add(new TextBlock()
            //{
            //    Text = "Hello World 3! ",
            //    ForegroundColor = Utilities.UI.Colors.Black,
            //    Width = 300,
            //    Height = 100,
            //    HorizontalAlignment = Utilities.UI.HorizontalAlignment.Stretch,
            //    BackgroundColor = Utilities.UI.Colors.Green,
            //    BorderThickness = new Thickness(17),
            //    BorderColor = Utilities.UI.Colors.DarkBlue,
            //    Padding = new Thickness(5),
            //    Margin = new Thickness(7),
            //    FontSize = 5
            //});
            Window.Children.Add(stackPanel);
            Window.Compositor.Run();
            Console.ReadLine();
            Window.Compositor.Stop();
        }
        
        private static void MainMenu()
        {
            ListMenu mainMenu = new ListMenu
            {
                Items = new List<MenuItem>
                {
#pragma warning disable IDE0011 // Add braces
                    // I hope we're not filling the call stack using the while instead of calling the method in itself again to restart
                    new MenuItem("Singleplayer",    () => { while (Game.Play(false)) ; }    ),
                    new MenuItem("Multiplayer",     () => { while (Game.Play(true)) ; }     ),
                    new MenuItem("About",           About                                   ),
                    new MenuItem("Quit game",       () => FullExit()                        )
#pragma warning restore IDE0011 // Add braces
                }
            };

            ProfileManager.ShowProfileSelection();
            
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
                Items = new List<MenuItem>
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
                ProfileManager.SaveProfiles();
            }
            
            // Cleanup
            Renderer.Clear();
            Renderer.RenderFrame();

            DllImports.ConsoleFullscreen = false;

            Console.WindowLeft   = originalWindowLeft;
            Console.WindowTop    = originalWindowTop;
            Console.WindowWidth  = originalWindowWidth;
            Console.WindowHeight = originalWindowHeight;
            DllImports.SetFont("Consolas", 7, 14);

            Console.Clear();

            Environment.Exit(error ? 1 : 0);
        }
    }
}
