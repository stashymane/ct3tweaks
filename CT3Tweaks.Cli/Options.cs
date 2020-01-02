using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CommandLine;

namespace CT3Tweaks.Cli
{
    class Options
    {
        [Value(0, MetaName = "Path", Required = true, HelpText = "Path to CT3.exe.")]
        public string Path { get; set; }

        [Option('r', "resolution", Separator = 'x', Min = 2, Max = 2, HelpText = "Changes resolution (in pixels separated by an 'x').")]
        public IEnumerable<uint> Resolution { get; set; }

        [Option('f', "fps", HelpText = "Changes game framerate.")]
        public int Fps { get; set; }

        [Option('v', "fov", HelpText = "Changes field of view in degrees.")]
        public float Fov { get; set; }

        [Option(HelpText = "Path to where the original game file will be backed up to or restored from.")]
        public string BackupPath { get; set; }

        [Option(HelpText = "Restores backed up game file.")]
        public bool Restore { get; set; }

        [Option(HelpText = "Resets display mode in the game's configuration file.")]
        public bool ResetDisplayMode { get; set; }

        [Option(HelpText = "The path to the game's configuration file (TAXI3.CFG).")]
        public string ConfigPath { get; set; }

        [Option(HelpText = "Display executable status after modification.", Default = false)]
        public bool Status { get; set; }

        [Option(HelpText = "Enable verbose mode.", Default = false)]
        public bool Verbose { get; set; }
    }
}
