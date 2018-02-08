namespace SnakeTheResurrection.Utilities.UI
{
    public class TextBlock : ContentElement
    {
        public string Text
        {
            get { return (string)GetValue(); }
            set { SetValue(value); }
        }
        public int FontSize
        {
            get { return (int)GetValue(); }
            set { SetValue(value); }
        }
        public Color ForegroundColor
        {
            get { return (Color)GetValue(); }
            set { SetValue(value); }
        }
        public TextWrapping TextWrapping
        {
            get { return (TextWrapping)GetValue(); }
            set { SetValue(value); }
        }
        public int CharacterSpacingRatio
        {
            get { return (int)GetValue(); }
            set { SetValue(value); }
        }
        public int LineSpacingRatio
        {
            get { return (int)GetValue(); }
            set { SetValue(value); }
        }
        
        public TextBlock()
        {
            RegisterProperty(nameof(Text), typeof(string), null);
            RegisterProperty(nameof(FontSize), typeof(int), 1);
            RegisterProperty(nameof(ForegroundColor), typeof(Color), Colors.White);
            RegisterProperty(nameof(TextWrapping), typeof(TextWrapping), default(TextWrapping));
            RegisterProperty(nameof(CharacterSpacingRatio), typeof(int), 1);
            RegisterProperty(nameof(LineSpacingRatio), typeof(int), 0);
        }

        private int GetCharacterSpacing()
        {
            return CharacterSpacingRatio * FontSize;
        }

        private int GetLineSpacing()
        {
            return LineSpacingRatio * FontSize;
        }

        public override bool Render()
        {
            if (!base.Render() || string.IsNullOrEmpty(Text))
            {
                return false;
            }

            Rectangle bounds = Measure();
            bounds = new Rectangle(
                bounds.X + Margin.Left + BorderThickness.Left + Padding.Left,
                bounds.Y + Margin.Top + BorderThickness.Top + Padding.Top,
                new Size(Width - Padding.Right, Height - Padding.Bottom));

            int x = bounds.X;
            int y = bounds.Y;
            int characterSpacing = GetCharacterSpacing();
            int lineSpacing = GetLineSpacing();
            int charHeight = Symtext.GetCharHeight(FontSize);
            
            for (int i = 0; i < Text.Length; i++)
            {
                if (Text[i] == '\n')
                {
                    GoToNewLine();
                    continue;
                }

                bool[,] renderedChar = Symtext.GetScaledBoolChar(Text[i], FontSize);
                int charWidth = renderedChar.GetLength(1);

                if (x + charWidth > bounds.Right)
                {
                    if (TextWrapping == TextWrapping.Wrap)
                    {
                        if (i != 0)
                        {
                            GoToNewLine();

                            if (y > bounds.Bottom)
                            {
                                break;
                            }
                        }
                    }
                    else if (TextWrapping == TextWrapping.NoWrap)
                    {
                        if (x > bounds.Right)
                        {
                            break;
                        }
                    }
                }

                if (x == bounds.X && Text[i] == ' ')
                {
                    continue;
                }

                Renderer.AddToBuffer(renderedChar, (short)ForegroundColor, x, y, bounds.Right, bounds.Bottom);
                x += charWidth + characterSpacing;


                void GoToNewLine()
                {
                    x = bounds.X;
                    y += charHeight + lineSpacing;
                }
            }

            return true;
        }
    }
}
