using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeTheResurrection.Utilities
{
    public static class Cheats
    {
        public enum CheatCode
        {
            Nothing
        }

        private static readonly Dictionary<CheatCode, bool> cheatCodeInfo = new Dictionary<CheatCode, bool>
        {
            { CheatCode.Nothing, false }
        };

        private static CancellationTokenSource previousCts;

        public static ReadOnlyDictionary<CheatCode, bool> CheatCodeInfo
        {
            get
            {
                return new ReadOnlyDictionary<CheatCode, bool>(cheatCodeInfo);
            }
        }

        public static ConsoleKeyInfo ValidateCheat(ConsoleKeyInfo pressedKeyInfo)
        {
            if (char.IsLetter(pressedKeyInfo.KeyChar))
            {
                // We don't currently support more cheats starting with the same letter
                string currentCheatCode = Enum.GetNames(typeof(CheatCode)).FirstOrDefault(c => char.ToLower(c[0]) == char.ToLower(pressedKeyInfo.KeyChar));

                if (currentCheatCode != null)
                {
                    for (int i = 1; i < currentCheatCode.Length; i++)
                    {
                        pressedKeyInfo = Console.ReadKey(true);

                        if (char.ToLower(currentCheatCode[i]) != char.ToLower(pressedKeyInfo.KeyChar))
                        {
                            return pressedKeyInfo;
                        }
                    }

                    CheatCode currentCode = (CheatCode)Enum.Parse(typeof(CheatCode), currentCheatCode);
                    cheatCodeInfo[currentCode] = !cheatCodeInfo[currentCode];

                    CancellationTokenSource currentCts = new CancellationTokenSource();

                    Task.Run(async () =>
                    {
                        const string CHEAT_ACTIVATED_MESSAGE    = " Cheat activated ";
                        const string CHEAT_DEACTIVATED_MESSAGE  = " Cheat deactivated ";

                        previousCts?.Cancel();

                        lock (Symtext.SyncRoot)
                        {
                            Symtext.SetCursorPosition(0, 0);
                            Symtext.FontSize            = 1;
                            Symtext.BackgroundColor     = ConsoleColor.Gray;
                            Symtext.ForegroundColor     = ConsoleColor.Black;
                            Symtext.HorizontalAlignment = HorizontalAlignment.None;
                            Symtext.VerticalAlignment   = VerticalAlignment.None;

                            Renderer.RemoveFromBuffer(0, 0, Symtext.CharHeight, Symtext.GetSymtextWidth(CHEAT_DEACTIVATED_MESSAGE));
                            Symtext.Write(cheatCodeInfo[currentCode] ? CHEAT_ACTIVATED_MESSAGE : CHEAT_DEACTIVATED_MESSAGE);
                            Renderer.RenderFrame();
                        }

                        await Task.Delay(TimeSpan.FromSeconds(5));

                        if (!currentCts.IsCancellationRequested)
                        {
                            lock (Symtext.SyncRoot)
                            {
                                Renderer.RemoveFromBuffer(0, 0, Symtext.CharHeight, Symtext.GetSymtextWidth(CHEAT_DEACTIVATED_MESSAGE));
                                Renderer.RenderFrame();
                            }
                        }
                    }, currentCts.Token);

                    previousCts = currentCts;
                }
            }

            return pressedKeyInfo;
        }
    }
}
