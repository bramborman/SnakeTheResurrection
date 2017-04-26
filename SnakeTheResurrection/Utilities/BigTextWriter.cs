using System;
using System.Text;

namespace SnakeTheResurrection.Utilities
{
    public static class BigTextWriter
    {
        public static void Write(string text)
        {
            Write(text, Console.ForegroundColor);
        }

        public static void Write(string text, ConsoleColor foregroundColor)
        {
            text = text.ToUpper();

            // centering is assigned to 4 because of four spaces on left and on right side
            // strlen(text) - 1 = count of spaces between each letters
            // int centering = 4 + strlen(text) - 1;
            int centering = 3 + text.Length;
            
            // Rewrite str as uppercase and set centering
            foreach (char ch in text)
            {
                if (ch == 'M' || ch == 'N' || ch == 'Q' || ch == 'T' || ch == 'W' || ch == 'X' || ch == 'Y')
                {
                    centering += 8;
                }
                else if (ch == 'V' || ch == '0' || (ch >= '2' && ch <= '9'))
                {
                    centering += 6;
                }
                else if (ch == '1')
                {
                    centering += 4;
                }
                else if (ch == 'I' || ch == ' ')
                {
                    centering += 2;
                }
                else if (ch == '!')
                {
                    centering++;
                }
                else
                {
                    centering += 7;
                }
            }

            StringBuilder str = new StringBuilder();
    
            for (int i = 0; i < 5; i++)
            {
                str.Append(new string(' ', 5 - i));
        
                for (int j = 0; j < text.Length; j++)
                {
                    switch(text[j])
                    {
                        case ' ':
                            str.Append("  ");
                            break;
                    
                        case 'A':
                            if (i == 0 || i == 2)
                            {
                                str.Append("///////");
                            }
                            else
                            {
                                str.Append("//   //");
                            }
                    
                            break;
                    
                        case 'B':
                            if (i == 1 || i == 3)
                            {
                                str.Append("//   //");
                            }
                            else
                            {
                                str.Append("////// ");
                            }
                    
                            break;
                    
                        case 'C':
                            if (i == 0 || i == 4)
                            {
                                str.Append("///////");
                            }
                            else
                            {
                                str.Append("//     ");
                            }
                    
                            break;
                    
                        case 'D':
                            if (i == 0 || i == 4)
                            {
                                str.Append("////// ");
                            }
                            else
                            {
                                str.Append("//   //");
                            }
                    
                            break;
                    
                        case 'E':
                            if (i == 0 || i == 4)
                            {
                                str.Append("///////");
                            }
                            else if (i == 2)
                            {
                                str.Append("/////  ");
                            }
                            else
                            {
                                str.Append("//     ");
                            }
                    
                            break;
                    
                        case 'F':
                            if (i == 0)
                            {
                                str.Append("///////");
                            }
                            else if (i == 2)
                            {
                                str.Append("/////  ");
                            }
                            else
                            {
                                str.Append("//     ");
                            }
                    
                            break;
                    
                        case 'G':
                            if (i == 1)
                            {
                                str.Append("//     ");
                            }
                            else if (i == 2)
                            {
                                str.Append("// ////");
                            }
                            else if (i == 3)
                            {
                                str.Append("//   //");
                            }
                            else
                            {
                                str.Append("///////");
                            }
                    
                            break;
                    
                        case 'H':
                            if (i != 2)
                            {
                                str.Append("//   //");
                            }
                            else
                            {
                                str.Append("///////");
                            }
                    
                            break;
                    
                        case 'I':
                            str.Append("//");
                            break;
                    
                        case 'J':
                            if (i < 3)
                            {
                                str.Append("     //");
                            }
                            else if (i == 3)
                            {
                                str.Append("//   //");
                            }
                            else
                            {
                                str.Append("///////");
                            }
                    
                            break;
                    
                        case 'K':
                            if (i == 0 || i == 4)
                            {
                                str.Append("//   //");
                            }
                            else if (i == 2)
                            {
                                str.Append("///    ");
                            }
                            else
                            {
                                str.Append("// //  ");
                            }
                    
                            break;
                    
                        case 'L':
                            if (i < 4)
                            {
                                str.Append("//     ");
                            }
                            else
                            {
                                str.Append("///////");
                            }
                    
                            break;
                    
                        case 'M':
                            if (i == 1)
                            {
                                str.Append("///  ///");
                            }
                            else if (i == 2)
                            {
                                str.Append("// // //");
                            }
                            else
                            {
                                str.Append("//    //");
                            }
                    
                            break;
                    
                        case 'N':
                            if (i == 1)
                            {
                                str.Append("///   //");
                            }
                            else if (i == 2)
                            {
                                str.Append("// // //");
                            }
                            else if (i == 3)
                            {
                                str.Append("//   ///");
                            }
                            else
                            {
                                str.Append("//    //");
                            }
                    
                            break;
                    
                        case 'O':
                            if (i == 0 || i == 4)
                            {
                                str.Append("///////");
                            }
                            else
                            {
                                str.Append("//   //");
                            }
                    
                            break;
                    
                        case 'P':
                            if (i == 0 || i == 2)
                            {
                                str.Append("///////");
                            }
                            else if (i == 1)
                            {
                                str.Append("//   //");
                            }
                            else
                            {
                                str.Append("//     ");
                            }
                    
                            break;
                    
                        case 'Q':
                            if (i == 0)
                            {
                                str.Append("/////// ");
                            }
                            else if (i == 3)
                            {
                                str.Append("//  /// ");
                            }
                            else if (i == 4)
                            {
                                str.Append("////////");
                            }
                            else
                            {
                                str.Append("//   // ");
                            }
                    
                            break;
                    
                        case 'R':
                            if (i == 0)
                            {
                                str.Append("///////");
                            }
                            else if (i == 2)
                            {
                                str.Append("////// ");
                            }
                            else
                            {
                                str.Append("//   //");
                            }
                    
                            break;
                    
                        case 'S':
                            if (i == 1)
                            {
                                str.Append("//     ");
                            }
                            else if (i == 3)
                            {
                                str.Append("     //");
                            }
                            else
                            {
                                str.Append("///////");
                            }
                    
                            break;
                    
                        case 'T':
                            if (i != 0)
                            {
                                str.Append("   //   ");
                            }
                            else
                            {
                                str.Append("////////");
                            }
                    
                            break;
                    
                        case 'U':
                            if (i != 4)
                            {
                                str.Append("//   //");
                            }
                            else
                            {
                                str.Append("///////");
                            }
                    
                            break;
                    
                        case 'V':
                            if (i != 4)
                            {
                                str.Append("//  //");
                            }
                            else
                            {
                                str.Append("  //  ");
                            }
                    
                            break;
                    
                        case 'W':
                            if (i == 0)
                            {
                                str.Append("//    //");
                            }
                            else if (i == 4)
                            {
                                str.Append(" //  // ");
                            }
                            else
                            {
                                str.Append("// // //");
                            }
                    
                            break;
                    
                        case 'X':
                            if (i == 0 || i == 4)
                            {
                                str.Append("//    //");
                            }
                            else if (i == 2)
                            {
                                str.Append("   //   ");
                            }
                            else
                            {
                                str.Append(" //  // ");
                            }
                    
                            break;
                    
                        case 'Y':
                            if (i == 0)
                            {
                                str.Append("//    //");
                            }
                            else if (i == 1)
                            {
                                str.Append(" //  // ");
                            }
                            else
                            {
                                str.Append("   //   ");
                            }
                    
                            break;
                    
                        case 'Z':
                            if (i == 1)
                            {
                                str.Append("     //");
                            }
                            else if (i == 2)
                            {
                                str.Append("  ///  ");
                            }
                            else if (i == 3)
                            {
                                str.Append("//     ");
                            }
                            else
                            {
                                str.Append("///////");
                            }
                    
                            break;
                    
                        case '1':
                            if (i == 0)
                            {
                                str.Append("////");
                            }
                            else
                            {
                                str.Append("  //");
                            }
                    
                            break;
                    
                        case '2':
                            if (i == 0 || i == 4)
                            {
                                str.Append("//////");
                            }
                            else if (i == 1)
                            {
                                str.Append("    //");
                            }
                            else if (i == 2)
                            {
                                str.Append("  //  ");
                            }
                            else
                            {
                                str.Append("//    ");
                            }
                    
                            break;
                    
                        case '3':
                            if (i == 0 || i == 4)
                            {
                                str.Append("//////");
                            }
                            else if (i == 2)
                            {
                                str.Append(" /////");
                            }
                            else
                            {
                                str.Append("    //");
                            }
                    
                            break;
                    
                        case '4':
                            if (i == 0 || i == 1)
                            {
                                str.Append("//  //");
                            }
                            else if (i == 2)
                            {
                                str.Append("//////");
                            }
                            else
                            {
                                str.Append("    //");
                            }
                    
                            break;
                    
                        case '5':
                            if (i == 3)
                            {
                                str.Append("    //");
                            }
                            else if (i == 1)
                            {
                                str.Append("//    ");
                            }
                            else
                            {
                                str.Append("//////");
                            }
                    
                            break;
                    
                        case '6':
                            if (i == 3)
                            {
                                str.Append("//  //");
                            }
                            else if (i == 1)
                            {
                                str.Append("//    ");
                            }
                            else
                            {
                                str.Append("//////");
                            }
                    
                            break;
                    
                        case '7':
                            if (i == 0)
                            {
                                str.Append("//////");
                            }
                            else if (i == 1)
                            {
                                str.Append("    //");
                            }
                            else if (i == 2)
                            {
                                str.Append("   // ");
                            }
                            else if (i == 3)
                            {
                                str.Append("  //  ");
                            }
                            else
                            {
                                str.Append(" //   ");
                            }
                    
                            break;
                    
                        case '8':
                            if (i % 2 == 0)
                            {
                                str.Append("//////");
                            }
                            else
                            {
                                str.Append("//  //");
                            }
                    
                            break;
                    
                        case '9':
                            if (i % 2 == 0)
                            {
                                str.Append("//////");
                            }
                            else if (i == 1)
                            {
                                str.Append("//  //");
                            }
                            else
                            {
                                str.Append("    //");
                            }
                    
                            break;
                    
                        case '0':
                            if (i == 0 || i == 4)
                            {
                                str.Append("//////");
                            }
                            else
                            {
                                str.Append("//  //");
                            }
                    
                            break;
                    
                        case '!':
                            if (i == 3)
                            {
                                str.Append("  ");
                            }
                            else
                            {
                                str.Append("//");
                            }
                    
                            break;
                    
                        default:
                            str.Append("!ERROR!");
                            break;
                    }
            
                    if (j < text.Length - 1)
                    {
                        str.Append(' ');
                    }
                }
        
                str.Append('\n');
            }

            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            
            foreach (string line in str.ToString().Split('\n'))
            {
                Console.CursorLeft = (Console.WindowWidth - centering) / 2;
                Console.WriteLine(line);
            }

            Console.ForegroundColor = originalForegroundColor;
        }
    }
}
