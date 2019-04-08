using Octokit;
using Semver;
using System;
using System.Diagnostics;
using System.Reflection;

namespace ct3tweaks
{
    class Updater
    {
        static GitHubClient client = new GitHubClient(new ProductHeaderValue("ct3tweaks"));

        public static async void Check()
        {
            if (DateTime.Now > Properties.Settings.Default.NextCheck)
            {
                try
                {
                    Properties.Settings.Default.NextCheck = DateTime.Now;
                    Release release = (await client.Repository.Release.GetAll("stashymane", "ct3tweaks"))[0];
                    String latest = release.TagName.Replace("v", "");
                    String current = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
                    if (SemVersion.Parse(latest) > SemVersion.Parse(current))
                        new UpdateNotifier(latest, release.HtmlUrl).ShowDialog();
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine("Failed to check for updates: " + e.Message);
                }
            }
        }
    }
}
