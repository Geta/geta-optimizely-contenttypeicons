using System;
using System.IO;
using System.Threading.Tasks;
using Octokit;

namespace Geta.Optimizely.ContentTypeIcons.EnumGenerator
{
    public static class GithubDownloader
    {
        public static async Task<Stream> DownloadLatestReleaseAsync(string owner, string repo)
        {
            var gitHubClient = new GitHubClient(new ProductHeaderValue("Geta.Optimizely.ContentTypeIcons.EnumGenerator"));
            var latestRelease = await gitHubClient.Repository.Release.GetLatest(owner, repo);

            Console.WriteLine(
                "The latest release is tagged at {0} and is named {1}",
                latestRelease.TagName,
                latestRelease.Name);

            var response = await gitHubClient.Connection.Get<object>(new Uri(latestRelease.ZipballUrl), TimeSpan.FromMinutes(1));
            var bytes = (byte[])response.HttpResponse.Body;

            return new MemoryStream(bytes);
        }
    }
}
