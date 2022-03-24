# SpotifyExtractInfo
Extract Artist, Album, Track and playlist information to JSON and HTML


Query Spotify:

> SpotifyExtractInfo.exe Spotify [SpotifyToken]

Queries the Spotify API and stores the result as JSON files in the program directory.
You can get the Spotify token by going to the Spotify API documentation console, (f.ex. [Spotify API - Get current user's saved tracks](https://developer.spotify.com/console/get-current-user-saved-tracks/)), and clicking "Get Token".
You will need to create a token with the following scopes:

    user-library-read
    playlist-read-private
    user-follow-read

Note that the token is fairly short-lived.

Running this will create 4 files: Spotify-Albums.json, Spotify-Tracks.json, Spotify-Artists.json, and Spotify-Playlists.json. These contain the full data extracted from Spotify.


Create HTML:

> SpotifyExtractInfo.exe HTML

Creates HTML files from the above downloaded JSON files, and this can only be called after the above command.
The HTML files contains limited data from the JSON files, and are generated from templates in the Templates directory, enabling you to modify them to your liking.
