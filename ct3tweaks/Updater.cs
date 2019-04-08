using Octokit;
using Semver;
using System;
using System.Reflection;

namespace ct3tweaks
{
    class Updater
    {
        static GitHubClient client = new GitHubClient(new ProductHeaderValue("ct3tweaks"));

        public static async void Check()
        {
            if (DateTime.Now.Subtract(Properties.Settings.Default.LastUpdateCheck).TotalDays > 7)
            {
                try
                {
                    Properties.Settings.Default.LastUpdateCheck = DateTime.Now;
                    Release latest = await client.Repository.Release.GetLatest("stashymane", "ct3tweaks");

                    if (SemVersion.Parse(latest.TagName) > SemVersion.Parse(Assembly.GetExecutingAssembly().GetName().Version.ToString()))
                        new UpdateNotifier(latest.TagName, latest.Url).ShowDialog();
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine(e.StackTrace);
                }
            }
        }
    }
}
