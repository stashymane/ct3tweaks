using System;
using System.IO;
using System.Linq;
using CommandLine;
using CT3Tweaks.Lib;

namespace CT3Tweaks.Cli
{
    static class Program
    {
        private static bool verboseMode;

        static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Options>(args);
            result.WithParsed(options =>
            {
                verboseMode = options.Verbose;
                if (!File.Exists(options.Path))
                {
                    Console.Out.WriteLine("File does not exist.");
                    return;
                }

                TweakLib lib;

                try
                {
                    lib = new TweakLib(options.Path);
                }
                catch
                {
                    //TODO restore backup and test then
                    Console.Out.WriteLine("Executable not supported or already modified.");
                    return;
                }

                if (options.Resolution.Any())
                {
                    var w = options.Resolution.First();
                    var h = options.Resolution.Last();
                    lib.Resolution = (w, h);
                    Verbose("Changed resolution to " + w + "x" + h);
                }

                if (options.Fps > 0)
                {
                    lib.Fps = (byte) options.Fps;
                    Verbose("Changed framerate to " + options.Fps);
                }

                if (options.Fov > 0)
                {
                    lib.Fov = options.Fov;
                    Verbose("Changed FOV to " + options.Fov);
                }

                if (options.ResetDisplayMode)
                {
                    try
                    {
                        if (options.ConfigPath == string.Empty)
                            lib.ResetDisplayMode();
                        else
                            TweakLib.ResetDisplayMode(options.ConfigPath);
                    }
                    catch (FileNotFoundException e)
                    {
                        Console.Out.WriteLine(e.Message);
                    }

                    Verbose("Reset display mode");
                }

                if (options.Restore)
                {
                    try
                    {
                        if (options.BackupPath == string.Empty)
                            lib.Restore();
                        else
                            lib.Restore(options.BackupPath);
                    }
                    catch (FileNotFoundException e)
                    {
                        Console.Out.WriteLine(e.Message);
                    }

                    Verbose("Restored original executable.");
                }

                if (options.Status)
                {
                    Console.Out.WriteLine("Resolution: " + lib.Resolution.ToString());
                    Console.Out.WriteLine("Framerate: " + lib.Fps);
                    Console.Out.WriteLine("Field of View: " + lib.Fov);
                }
            });
            result.WithNotParsed(errors => { });
        }

        private static void Verbose(string msg)
        {
            if (verboseMode)
                Console.Out.WriteLine(msg);
        }
    }
}
