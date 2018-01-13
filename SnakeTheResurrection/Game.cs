﻿using SnakeTheResurrection.Data;
using SnakeTheResurrection.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace SnakeTheResurrection
{
    public static class Game
    {
        public const int BLOCK_SIZE = 5;

        public static int gameBoardLeft;
        public static int gameBoardTop;
        public static int gameBoardRight;
        public static int gameBoardBottom;
        public static int gameBoardWidth;
        public static int gameBoardHeight;

        public static bool borderlessMode;
        public static int delay;
        private static int playerCount = 1;

        public static bool Play(bool multiplayer)
        {
            InputHelper.ClearCache();
            Berry.Reset();
            Snake.Reset();
            Renderer.ClearBuffer();

            if (!GetGameSettings(multiplayer))
            {
                return false;
            }

            Renderer.ClearBuffer();
            CreateGameBoard();
                
            for (int i = 0; i < playerCount; i++)
            {
                Profile profile = null;

                //TODO: Load real profiles here
                switch (i)
                {
                    case 0:
                        profile = ProfileManager.CurrentProfile;
                        break;
                        
                    case 1:
                        profile = new Profile
                        {
                            Name = "Frogpanda",
                            Color = ConsoleColor.Cyan
                        };
                        profile.SnakeControls.Left  = ConsoleKey.A;
                        profile.SnakeControls.Up    = ConsoleKey.W;
                        profile.SnakeControls.Right = ConsoleKey.D;
                        profile.SnakeControls.Down  = ConsoleKey.S;

                        break;

                    case 2:
                        profile = new Profile
                        {
                            Name = "Strawberryraspberry",
                            Color = ConsoleColor.Magenta
                        };
                        profile.SnakeControls.Left  = ConsoleKey.NumPad4;
                        profile.SnakeControls.Up    = ConsoleKey.NumPad8;
                        profile.SnakeControls.Right = ConsoleKey.NumPad6;
                        profile.SnakeControls.Down  = ConsoleKey.NumPad5;

                        break;

                    case 3:
                        profile = new Profile
                        {
                            Name = "Lifeescape",
                            Color = ConsoleColor.Yellow
                        };
                        profile.SnakeControls.Left  = ConsoleKey.J;
                        profile.SnakeControls.Up    = ConsoleKey.I;
                        profile.SnakeControls.Right = ConsoleKey.L;
                        profile.SnakeControls.Down  = ConsoleKey.K;

                        break;
                }

                new Snake(profile, i, playerCount);
                new Berry(10);
            }

            Stopwatch stopwatch = new Stopwatch();
            Renderer.RenderFrame();

            while (Snake.current.Count != 0)
            {
                stopwatch.Restart();

                foreach (Snake snake in Snake.current)
                {
                    snake.Update();
                }

                foreach (Snake snake in Snake.current)
                {
                    snake.LateUpdate();
                }

                foreach (Berry berry in Berry.current)
                {
                    berry.Update();
                }

                Renderer.DisplayFrame();

                if (InputHelper.WasKeyPressed(ConsoleKey.Escape))
                {
                    InputHelper.ClearInputBuffer();

                    switch (PauseMenu())
                    {
                        case MenuResult.Restart:
                            return true;
                            
                        case MenuResult.MainMenu:
                            return false;
                            
                        case MenuResult.QuitGame:
                            Program.FullExit();
                            break;
                    }
                }
#if DEBUG
#pragma warning disable IDE0011 // Add braces
                else if (InputHelper.WasKeyPressed(ConsoleKey.Pause))
                {
                    while (Console.ReadKey(true).Key != ConsoleKey.Pause) ;
                }
#pragma warning restore IDE0011 // Add braces
#endif

                InputHelper.ClearCache();
                stopwatch.Stop();
                
                int currentDelay = Math.Max(0, delay - stopwatch.Elapsed.Milliseconds);

                if (currentDelay != 0)
                {
                    InputHelper.StartCaching();
                    Thread.Sleep(currentDelay);
                    InputHelper.StopCaching();
                }
            }

            return false;
        }

        private static void CreateGameBoard()
        {
            int windowWidthOverlap  = Console.WindowWidth % BLOCK_SIZE;
            int windowHeightOverlap = Console.WindowHeight % BLOCK_SIZE;
            
            if (windowWidthOverlap >= 1 || windowHeightOverlap >= 1 || AppData.Current.ForceGameBoardBorders)
            {
                windowWidthOverlap  += BLOCK_SIZE * 2;
                windowHeightOverlap += BLOCK_SIZE * 2;
            }

            gameBoardLeft                   = (int)Math.Round(windowWidthOverlap / 2.0);
            gameBoardTop                    = (int)Math.Round(windowHeightOverlap / 2.0);

            int gameBoardBorderRightSize    = windowWidthOverlap - gameBoardLeft;
            int gameBoardBorderBottomSize   = windowHeightOverlap - gameBoardTop;

            gameBoardRight                  = Console.WindowWidth - gameBoardBorderRightSize;
            gameBoardBottom                 = Console.WindowHeight - gameBoardBorderBottomSize;

            //TODO: Status bar
            // gameBoardTop                    += gameBoardTop >= 1 ? BLOCK_SIZE : (BLOCK_SIZE * 2);

            Renderer.AddToBuffer(Constants.ACCENT_COLOR_DARK, 0, 0, gameBoardLeft, Console.WindowHeight);
            Renderer.AddToBuffer(Constants.ACCENT_COLOR_DARK, gameBoardRight, 0, gameBoardBorderRightSize, Console.WindowHeight);

            Renderer.AddToBuffer(Constants.ACCENT_COLOR_DARK, 0, 0, Console.WindowWidth, gameBoardTop);
            Renderer.AddToBuffer(Constants.ACCENT_COLOR_DARK, 0, gameBoardBottom, Console.WindowWidth, gameBoardBorderBottomSize);

            gameBoardWidth  = gameBoardRight - gameBoardLeft;
            gameBoardHeight = gameBoardBottom - gameBoardTop;
        }

        private static bool GetGameSettings(bool multiplayer)
        {
            for (int i = 0; ; i++)
            {
                // Not using switch to be able to use continue and break
                if (i == 0)
                {
                    bool? getGameModeOutput = GetGameMode();

                    if (getGameModeOutput == null)
                    {
                        return false;
                    }
                    else
                    {
                        borderlessMode = getGameModeOutput.Value;
                    }
                }
                else if (i == 1)
                {
                    int? getDelayOutput = GetDelay();

                    if (getDelayOutput == null)
                    {
                        i -= 2;
                        continue;
                    }
                    else
                    {
                        delay = getDelayOutput.Value;
                    }
                }
                else if (i == 2)
                {
                    if (multiplayer)
                    {
                        int? getPlayerCountOutput = GetPlayerCount();

                        if (getPlayerCountOutput == null)
                        {
                            i -= 2;
                            continue;
                        }

                        playerCount = getPlayerCountOutput.Value;
                    }
                }
                else
                {
                    break;
                }
            }

            return true;
        }

        private static bool? GetGameMode()
        {
            bool? output = null;

            Symtext.WriteTitle("Mode", 7);
            new ListMenu
            {
                Items = new List<MenuItem>
                {
                    new MenuItem("Classic",     () => output = false    ),
                    new MenuItem("Borderless",  () => output = true     ),
                    new MenuItem(null,          null                    ),
                    new MenuItem("Back",        () => output = null     )
                }
            }.InvokeResult();

            return output;
        }

        private static int? GetDelay()
        {
            int? output = null;

            Symtext.WriteTitle("Level", 0);
            new ListMenu
            {
                Items = new List<MenuItem>
                {
                    new MenuItem("Easy",    () => output = 200  ),
                    new MenuItem("Medium",  () => output = 50   ),
                    new MenuItem("Hard",    () => output = 30   ),
                    new MenuItem(null,      null                ),
                    new MenuItem("Back",    () => output = null )
                },
                SelectedIndex = 1
            }.InvokeResult();

            return output;
        }

        private static int? GetPlayerCount()
        {
            //TODO: Selection UI
            return 2;
        }

        private static MenuResult PauseMenu()
        {
            object gameBufferKey    = Renderer.BackupBuffer();
            MenuResult output       = default;
            
            Symtext.WriteTitle("Pause", 7);
            new ListMenu
            {
                Items = new List<MenuItem>
                {
                    new MenuItem("Continue",    () => output = MenuResult.Continue  ),
                    new MenuItem("Restart",     () => output = MenuResult.Restart   ),
                    new MenuItem("Main menu",   () => output = MenuResult.MainMenu  ),
                    new MenuItem("Quit game",   () => output = MenuResult.QuitGame  )
                }
            }.InvokeResult();

            Renderer.RestoreBuffer(gameBufferKey);
            return output;
        }

        private enum MenuResult
        {
            Continue,
            Restart,
            MainMenu,
            QuitGame
        }
    }
}
