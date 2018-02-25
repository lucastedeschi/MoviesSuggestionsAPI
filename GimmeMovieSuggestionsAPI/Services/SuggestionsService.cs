using GimmeMovieSuggestionsAPI.Integrations;
using GimmeMovieSuggestionsAPI.Integrations.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeMovieSuggestionsAPI.Services
{
    public static class SuggestionsService
    {
        public static List<MovieDTO> ProccessSuggestionRequest(SuggestionRequest req)
        {
            req = PrepareSuggestionRequest(req);

            var genresIds = ProccessGenres(req.Audio);
            var periodGenres = ProccessPeriod(req.Period);
            var user = UsersManagementIntegration.GetUserByEmail(req.UserEmail);

            var movies = new List<MovieDTO>();
            var page = 1;
            var quantMovies = 20;
            while(movies.Count < quantMovies)
            {
                var moviesResult = TheMovieDbIntegration.GetMovies(genresIds, page, "true", "false").Results;
                if (moviesResult.Count == 0)
                    continue;

                int difference = quantMovies - movies.Count;
                moviesResult = moviesResult.Take(difference).ToList();

                movies.AddRange(FilterMoviesListByUser(moviesResult, user));
                page++;
            }

            movies = movies.OrderByDescending(x => x.Popularity).ToList();

            //procura na lista filmes que tenham o genero da hora, e reordena por isso

            return movies;
        }

        private static SuggestionRequest PrepareSuggestionRequest(SuggestionRequest req)
        {
            req.Audio = req.Audio.ToLower();
            return req;
        }

        private static string ProccessGenres(string audio)
        {
            audio = RemoveSpecialsChars(audio.ToLower());
            var genres = TheMovieDbIntegration.GetGenres();
            var genresIds = "";
            
            foreach (var genre in genres.Genres)
            {
                var genreNameLower = RemoveSpecialsChars(genre.Name.ToLower());
                if (audio.Contains(genreNameLower))
                {
                    if (genresIds.Equals(""))
                        genresIds = $"{genre.Id}";
                    else
                        genresIds = $"{genresIds},{genre.Id}";
                    audio = audio.Replace(genreNameLower, "");
                }     
            }

            return genresIds;
        }

        private static string ProccessPeriod(string period)
        {
            var result = "";
            return result;
        }

        private static List<MovieDTO> FilterMoviesListByUser(List<MovieDTO> movies, UserDTO user)
        {
            foreach (var item in user.Movies.Watched)
                movies.RemoveAll(x => x.Id == item.Id);

            foreach (var item in user.Movies.WatchLater)
                movies.RemoveAll(x => x.Id == item.Id);

            foreach (var item in user.Movies.BlackList)
                movies.RemoveAll(x => x.Id == item.Id);

            return movies;
        }

        public static string RemoveSpecialsChars(string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }
    }
}
