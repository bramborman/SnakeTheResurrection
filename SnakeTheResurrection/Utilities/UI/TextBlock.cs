namespace SnakeTheResurrection.Utilities.UI
{
    public class TextBlock : UIElement
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
            if (!base.Render() || Text == null || Text == "")
            {
                return false;
            }

            bool[][,] renderedChars = Symtext.Render(Text, FontSize);
            Rectangle bounds = Measure();
            int x = bounds.X;
            int y = bounds.Y;
            int charHeight = renderedChars[0].GetLength(0);
            int characterSpacing = GetCharacterSpacing();
            int lineSpacing = GetLineSpacing();
            int lastSpaceCharIndex = -1;

            for (int i = 0; i < Text.Length; i++)
            {
                if (Text[i] == ' ')
                {
                    lastSpaceCharIndex = i;
                }

                bool[,] renderedChar = renderedChars[i];
                int charWidth = renderedChar.GetLength(1);

                if (x + charWidth > bounds.Right)
                {
                    if (i == 0)
                    {
                        break;
                    }

                    x = bounds.X;
                    y += charHeight + lineSpacing;
                }

                Renderer.AddToBuffer(renderedChar, (short)ForegroundColor, x, y);
                x += charWidth;
            }

            return true;
        }
    }
}
