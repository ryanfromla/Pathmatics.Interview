using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Pathmatics.Interview
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            HttpClient client = new();
            //forgive the fact that it doesn't /technically/ take a text file.
            //I figure having a url may even be better depending on the use case...
            //Like this one! The file is stored on an accessible s3 blob, maybe it's updated periodically, who knows...
            //Code borrowed from https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-5.0
            HttpResponseMessage response = await client.GetAsync("https://s3.amazonaws.com/ym-hosting/tomtest/advertisers.txt");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            DuplicateFinder duplicateFinder = new();
            duplicateFinder.Items = new (responseBody.Split(Environment.NewLine));
            foreach (KeyValuePair<string, List<string>> item in duplicateFinder.GetDuplicates())
                Console.WriteLine(string.Concat(item.Key, ": ", string.Join(',', item.Value)));
        }
    }
}
