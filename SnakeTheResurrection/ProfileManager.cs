using SnakeTheResurrection.Data;
using SnakeTheResurrection.Utilities;
using System.Collections.Generic;

namespace SnakeTheResurrection
{
    public static class ProfileManager
    {
        private static readonly List<Profile> profiles = new List<Profile>();
        
        public static Profile CurrentProfile { get; private set; }

        public static void ShowProfileSelection()
        {
            if (profiles.Count < 1)
            {
                CreateNewProfile();
            }

            //TODO: UI for profile selection
            CurrentProfile = profiles[0];
        }

        private static void CreateNewProfile()
        {
            Profile newProfile = new Profile
            {
                Name = "Pandafrog",
                Color = FastColors.Green
            };

            //TODO: UI for customization

            profiles.Add(newProfile);
        }

        public static void SaveProfiles()
        {
            //TODO: Save profiles
        }

        public static void LoadProfiles()
        {
            //TODO: Load profiles
        }
    }
}
