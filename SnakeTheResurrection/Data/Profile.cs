namespace SnakeTheResurrection.Data
{
    public sealed class Profile
    {
        public string Name { get; set; }
        public short Color { get; set; }
        public SnakeControls SnakeControls { get; set; }

        public Profile()
        {
            SnakeControls = new SnakeControls();
        }
    }
}
