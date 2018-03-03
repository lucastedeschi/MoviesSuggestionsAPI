using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GimmeMovieSuggestionsAPI.Integrations.DTOs;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace GimmeMovieSuggestionsAPI.Integrations
{
    public static class YouTubeIntegration
    {
        private static string _key = "AIzaSyAWE9jdXkfviP2DJB7WIDcMdT7ONubiwKA";

        public static TrailerDTO GetTrailerUrl(string query)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _key,
                ApplicationName = "application/json"
            });

            var searchListRequest = youtubeService.Search.List("id,snippet");
            searchListRequest.Q = $"{query} trailer"; 
            searchListRequest.MaxResults = 1;

            var searchListResponse = searchListRequest.Execute();

            var trailer = new TrailerDTO();
            var searchResult = searchListResponse.Items.FirstOrDefault();

            if (searchResult != null)
            {
                trailer.Id = searchResult.Id.VideoId;
                trailer.Thumbnail = searchResult.Snippet.Thumbnails.High.Url;
                trailer.Url = $"https://www.youtube.com/watch?v={trailer.Id}";
                trailer.Title = searchResult.Snippet.Title;
            }

            return trailer;
        }
    }
}
