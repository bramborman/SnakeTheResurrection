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
        public int CharacterSpacing
        {
            get
            {
                return CharacterSpacingRatio * FontSize;
            }
        }
        public int LineSpacingRatio
        {
            get { return (int)GetValue(); }
            set { SetValue(value); }
        }
        public int LineSpacing
        {
            get
            {
                return LineSpacingRatio * FontSize;
            }
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
        
        public override Rectangle Render(in Rectangle bounds)
        {
            Rectangle area = base.Render(in bounds);

            if (area == Rectangle.Empty || string.IsNullOrEmpty(Text))
            {
                return area;
            }

            Rectangle textBounds = GetContentArea(area);
            int x = textBounds.X;
            int y = textBounds.Y;
            int characterSpacing = CharacterSpacing;
            int lineSpacing = LineSpacing;
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

                if (x + charWidth > textBounds.Right)
                {
                    if (TextWrapping == TextWrapping.Wrap)
                    {
                        if (i != 0)
                        {
                            GoToNewLine();

                            if (y > textBounds.Bottom)
                            {
                                break;
                            }
                        }
                    }
                    else if (TextWrapping == TextWrapping.NoWrap)
                    {
                        if (x > textBounds.Right)
                        {
                            break;
                        }
                    }
                }

                if (x == textBounds.X && Text[i] == ' ')
                {
                    continue;
                }

                Renderer.Safe.AddToBuffer(in renderedChar, ForegroundColor, in x, in y, in textBounds, in bounds);
                x += charWidth + characterSpacing;


                void GoToNewLine()
                {
                    x = textBounds.X;
                    y += charHeight + lineSpacing;
                }
            }

            return area;
        }
    }
}
