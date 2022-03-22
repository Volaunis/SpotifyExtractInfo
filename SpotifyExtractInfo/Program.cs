using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SpotifyExtractInfo.Business;
using SpotifyExtractInfo.Helpers;
using SpotifyExtractInfo.Models;
using SpotifyExtractInfo.Service;

namespace SpotifyExtractInfo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }

            var token = "";
            if (args.Length > 1)
                token = args[1];

            var processType = (ProcessTypeEnum)Enum.Parse(typeof(ProcessTypeEnum), args[0]);

            var serviceProviderCollection = new ServiceCollection();
            AddInjections(serviceProviderCollection);
            var serviceProvider = serviceProviderCollection.BuildServiceProvider();
            var spotifyExtractInfoService = serviceProvider.GetService<ISpotifyExtractInfoService>();
            await spotifyExtractInfoService.Process(processType, token);
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Query Spotify:");
            Console.WriteLine("");
            Console.WriteLine("> SpotifyExtractInfo.exe Spotify [SpotifyToken]");
            Console.WriteLine("");
            Console.WriteLine("Queries the Spotify API and stores the result as JSON files in the program directory.");
            Console.WriteLine("You can get the Spotify token by going to the Spotify API documentation console, (f.ex. https://developer.spotify.com/console/get-current-user-saved-tracks/), and clicking \"Get Token\".");
            Console.WriteLine("You will need to create a token with the following scopes:");
            Console.WriteLine("");
            Console.WriteLine("user-library-read");
            Console.WriteLine("playlist-read-private");
            Console.WriteLine("user-follow-read");
            Console.WriteLine("");
            Console.WriteLine("Note that the token is fairly short-lived.");
            Console.WriteLine("");
            Console.WriteLine("Running this will create 4 files: Spotify-Albums.json, Spotify-Tracks.json, Spotify-Artists.json, and Spotify-Playlists.json. These contain the full data extracted from Spotify.");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Create HTML:");
            Console.WriteLine("");
            Console.WriteLine("> SpotifyExtractInfo.exe HTML");
            Console.WriteLine("");
            Console.WriteLine("Creates HTML files from the above downloaded JSON files, and this can only be called after the above command.");
            Console.WriteLine("The HTML files contains limited data from the JSON files, and are generated from templates in the Templates directory, enabling you to modify them to your liking.");
        }

        private static void AddInjections(ServiceCollection serviceProviderCollection)
        {
            // Service
            serviceProviderCollection.AddSingleton<ISpotifyExtractInfoService, SpotifyExtractInfoService>();

            // Business
            serviceProviderCollection.AddSingleton<IArtistBusiness, ArtistBusiness>();
            serviceProviderCollection.AddSingleton<ITrackBusiness, TrackBusiness>();
            serviceProviderCollection.AddSingleton<IAlbumBusiness, AlbumBusiness>();
            serviceProviderCollection.AddSingleton<IPlaylistBusiness, PlaylistBusiness>();
            serviceProviderCollection.AddSingleton<IFileStoreBusiness, FileStoreBusiness>();

            // Helpers
            serviceProviderCollection.AddSingleton<IHttpHelper, HttpHelper>();
            serviceProviderCollection.AddSingleton<IFileHelper, FileHelper>();
            serviceProviderCollection.AddSingleton<ITemplateHelper, TemplateHelper>();
        }
    }
}
