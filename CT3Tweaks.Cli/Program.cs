using System;
using CommandLine;
using CT3Tweaks.Lib;

namespace CT3Tweaks.Cli
{
    static class Program
    {
        static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Options>(args);
            result.WithParsed(options => { });
            result.WithNotParsed(errors => { });
        }
    }
}
