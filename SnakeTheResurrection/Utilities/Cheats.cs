using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeTheResurrection.Utilities
{
    public static class Cheats
    {
        public enum CheatCode
        {
            Pony
        }

        private static readonly Dictionary<CheatCode, bool> cheatCodeInfo = new Dictionary<CheatCode, bool>
        {
            { CheatCode.Pony, false }
        };

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

                    Task.Run(async () =>
                    {
                        Symtext.FontSize = 1;
                        Symtext.SetCursorPosition(0, 0);
                        Symtext.BackgroundColor = ConsoleColor.Gray;
                        Symtext.ForegroundColor = ConsoleColor.Black;

                        string message = $" Cheat {(cheatCodeInfo[currentCode] ? "" : "de")}activated ";
                        Symtext.Write(message);
                        Renderer.RenderFrame();

                        await Task.Delay(TimeSpan.FromSeconds(5));
                        Renderer.RemoveFromBuffer(0, 0, Symtext.CharHeight, Symtext.GetSymtextWidth(message));
                        Renderer.RenderFrame();
                    });
                }
            }

            return pressedKeyInfo;
        }
    }
}
