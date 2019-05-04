using System.IO;

namespace ct3tweaks
{
    class ProfileManager
    {
        private static string directory;
        private static Profile currentProfile;

        public static Profile CurrentProfile
        {
            get
            {
                return currentProfile;
            }
            set
            {
                ProfileChange?.Invoke(currentProfile, value);
                currentProfile = value;
            }
        }

        public static event OnProfileChange ProfileChange;
        public delegate void OnProfileChange(Profile past, Profile present);

        public static event OnDirectoryChange DirectoryChange;
        public delegate void OnDirectoryChange(string past, string present);

        public static string Directory
        {
            get
            {
                return directory;
            }
            set
            {
                if (value.EndsWith("/CT3.exe"))
                    value.Replace("/CT3.exe", "");
                if (File.Exists(value + "/CT3.exe"))
                {
                    DirectoryChange?.Invoke(directory, value);
                    directory = value;
                } else
                    throw new FileNotFoundException();
            }
        }

        public static string ExecutablePath
        {
            get
            {
                return directory + "/CT3.exe";
            }
        }

        public static string ConfigPath
        {
            get
            {
                return directory + "/TAXI3.CFG";
            }
        }

        public static string BackupExecutablePath
        {
            get
            {
                return directory + "/CT3.original.exe";
            }
        }

        public static void Apply()
        {
            if (currentProfile != null)
            {
                TweakLib.ChangeResolution(ExecutablePath, currentProfile.Resolution);
                TweakLib.ChangeFramerate(ExecutablePath, currentProfile.Framerate);
                TweakLib.ChangeFOV(ExecutablePath, currentProfile.FOV);
            }
        }
    }
}
