#if DEBUG
using System;

namespace SnakeTheResurrection.Utilities.UI
{
    public static class Playground
    {
        private static readonly Random r = new Random();

        public static void ScreenSaver(string text = Constants.APP_SHORT_NAME, int fontSize = 15)
        {
            TextBlock textBlock = new TextBlock()
            {
                Text = text,
                TextWrapping = TextWrapping.NoWrap,
                FontSize = fontSize,
                ForegroundColor = (Color)Constants.ACCENT_COLOR
            };

            int symtextWidth = Symtext.GetSymtextWidth(textBlock.Text, textBlock.FontSize, textBlock.CharacterSpacing);
            int symtextHeight = Symtext.GetCharHeight(textBlock.FontSize);
            textBlock.Width = symtextWidth;
            textBlock.Height = symtextHeight;
            textBlock.Padding = new Thickness(0, -textBlock.FontSize);
            textBlock.Margin = new Thickness(
                (Window.Width - Symtext.GetSymtextWidth("Snake", 15, 15)) / 2,
                (Window.Height - Symtext.GetCharHeight(15) - 30) / 2,
                0,
                0);

            bool goRight = true;
            bool goDown = true;
            StartStop(textBlock, BeforeRendering);


            void BeforeRendering()
            {
                int left = textBlock.Margin.Left + (goRight ? 1 : -1);
                int top = textBlock.Margin.Top + (goDown ? 1 : -1);

                if (left <= 0 || left + textBlock.ActualWidth >= Window.Width)
                {
                    goRight = !goRight;
                }

                if (top <= 0 || top + textBlock.ActualHeight >= Window.Height)
                {
                    goDown = !goDown;
                }

                textBlock.Margin = new Thickness(left, top, textBlock.Margin.Right, textBlock.Margin.Bottom);
            };
        }

        public static void StackPanelTest()
        {
            StackPanel stackPanel = new StackPanel()
            {
                BackgroundColor = Colors.Yellow,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            for (int i = 0; i < 10; i++)
            {
                StackPanel sp = new StackPanel()
                {
                    BackgroundColor = (Color)r.Next(1, 16),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Orientation = Orientation.Horizontal,
                    Height = 100
                };
                stackPanel.Items.Add(sp);

                for (int j = 0; j < 5; j++)
                {
                    TextBlock t = new TextBlock()
                    {
                        Text = "Hello World! ",
                        Margin = new Thickness(r.Next(0, 20), r.Next(0, 20), r.Next(0, 20), r.Next(0, 20)),
                        Padding = new Thickness(r.Next(0, 20), r.Next(0, 20), r.Next(0, 20), r.Next(0, 20)),
                        BorderThickness = new Thickness(r.Next(0, 20), r.Next(0, 20), r.Next(0, 20), r.Next(0, 20)),
                        FontSize = r.Next(1, 10),
                        TextWrapping = (TextWrapping)r.Next(0, 1),
                        VerticalAlignment = (VerticalAlignment)r.Next(0, 4),
                        HorizontalAlignment = (HorizontalAlignment)r.Next(0, 4),
                        Width = r.Next(20, 150),
                        Height = r.Next(20, 150),
                        CharacterSpacingRatio = r.Next(1, 3),
                        LineSpacingRatio = r.Next(1, 3)
                    };

                    do
                    {
                        t.BackgroundColor = (Color)r.Next(0, 16);
                    } while (t.BackgroundColor == sp.BackgroundColor);

                    do
                    {
                        t.BorderColor = (Color)r.Next(0, 16);
                    } while (t.BackgroundColor == t.BorderColor);

                    do
                    {
                        t.ForegroundColor = (Color)r.Next(0, 16);
                    } while (t.BackgroundColor == t.ForegroundColor);

                    sp.Items.Add(t);
                }
            }

            StartStop(stackPanel);
        }

        public static void TextBlockTest()
        {
            TextBlock textBlock = new TextBlock()
            {
                Text = "Hello World! ",
                ForegroundColor = Colors.Black,
                Width = 30,
                Height = 100,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                BackgroundColor = Colors.Cyan,
                BorderThickness = new Thickness(17),
                BorderColor = Colors.Red,
                Padding = new Thickness(5),
                Margin = new Thickness(7),
                FontSize = 3
            };

            for (int i = 0; i < 1000; i++)
            {
                textBlock.Text += "Hello World! ";
            }

            int framesCounter = 0;
            StartStop(textBlock, BeforeRendering);


            void BeforeRendering()
            {
                if (framesCounter++ % 30 == 0)
                {
                    try
                    {
                        textBlock.Width = r.Next(1, Console.WindowWidth / 2);
                        textBlock.Height = r.Next(1, Console.WindowHeight / 2);
                        textBlock.HorizontalAlignment = (HorizontalAlignment)r.Next(0, 4);
                        textBlock.VerticalAlignment = (VerticalAlignment)r.Next(0, 4);
                        textBlock.BorderThickness = new Thickness(r.Next(0, Math.Min(Console.WindowWidth, Console.WindowHeight) / 4));
                        textBlock.BorderColor = (Color)r.Next(1, 16);
                        textBlock.FontSize = r.Next(1, textBlock.Height / 7 / 2);

                        do
                        {
                            textBlock.BackgroundColor = (Color)r.Next(1, 16);
                        } while (textBlock.BackgroundColor == textBlock.BorderColor);
                    }
                    catch
                    {

                    }
                }
            };
        }

        private static void StartStop(UIElement child = null, Action beforeRendering = null)
        {
            if (child != null)
            {
                Window.Children.Add(child);
            }

            Window.Compositor.BeforeRendering += beforeRendering;
            Window.Compositor.Start();
            Console.ReadLine();
            Window.Compositor.Stop();
            Window.Children.Clear();
            Window.Compositor.BeforeRendering -= beforeRendering;
        }
    }
}
#endif
