using GimmeMovieSuggestionsAPI.Integrations;
using GimmeMovieSuggestionsAPI.Integrations.DTOs;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GimmeMovieSuggestionsAPI.Services
{
    public static class SuggestionsService
    {
        private static IHostingEnvironment _hostingEnvironment;

        public static List<MovieDTO> ProccessSuggestionRequest(SuggestionRequest req)
        {
            req = PrepareSuggestionRequest(req);
            var genresIds = ProccessGenresByText(req.Audio);

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

            return movies;
        }

        public static MovieDTO ProccessImFeelingLuckyRequest(SuggestionRequest req, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            var path = _hostingEnvironment.ContentRootPath + @"\AppData\TempMoviesBlacklist.txt";
            CleanTempMoviesBlacklist(path);

            var genresIds = ProccessGenresByTime(req.Time);

            var user = UsersManagementIntegration.GetUserByEmail(req.UserEmail);

            var movies = new List<MovieDTO>();
            var page = 1;
            var quantMovies = 1;

            while (movies.Count < quantMovies)
            {
                var moviesResult = TheMovieDbIntegration.GetMovies(genresIds, page, "true", "false").Results;
                if (moviesResult.Count == 0)
                    continue;

                int difference = quantMovies - movies.Count;
                moviesResult = moviesResult.Take(difference).ToList();

                movies.AddRange(FilterMoviesListByUser(moviesResult, user, true));
                page++;
            }

            var res = movies.FirstOrDefault();

            InsertTempMoviesBlacklist(req.UserEmail, res.Id, path);

            return res;
        }

        private static SuggestionRequest PrepareSuggestionRequest(SuggestionRequest req)
        {
            req.Audio = req.Audio.ToLower();
            return req;
        }

        private static string ProccessGenresByText(string text)
        {
            text = RemoveSpecialsChars(text.ToLower());
            var genres = TheMovieDbIntegration.GetGenres();
            var genresIds = "";
            
            foreach (var genre in genres.Genres)
            {
                var genreNameLower = RemoveSpecialsChars(genre.Name.ToLower());
                if (text.Contains(genreNameLower))
                {
                    if (genresIds.Equals(""))
                        genresIds = $"{genre.Id}";
                    else
                        genresIds = $"{genresIds},{genre.Id}";
                    text = text.Replace(genreNameLower, "");
                }     
            }

            return genresIds;
        }

        private static string ProccessGenresByTime(string time)
        {
            try
            {
                var genres = TheMovieDbIntegration.GetGenres();

                var timeSplitted = time.Split(":");
                var hour = Convert.ToInt32(timeSplitted[0]);
                var minutes = Convert.ToInt32(timeSplitted[1]);

                if ((hour >= 20 && hour <= 23) || (hour >= 0 && hour <= 6))
                    return ProccessGenresByText("Terror Suspense");

                if (hour > 6 && hour < 20)
                    return ProccessGenresByText("Ação Comedia Aventura Drama Romance");

                return "";
            } catch(Exception)
            {
                return "";
            }
        }

        private static List<MovieDTO> FilterMoviesListByUser(List<MovieDTO> movies, UserDTO user, bool isLucky = false)
        {
            foreach (var item in user.Movies.Watched)
                movies.RemoveAll(x => x.Id == item.Id);

            foreach (var item in user.Movies.WatchLater)
                movies.RemoveAll(x => x.Id == item.Id);

            foreach (var item in user.Movies.BlackList)
                movies.RemoveAll(x => x.Id == item.Id);

            if(isLucky)
            {
                var path = _hostingEnvironment.ContentRootPath + @"\AppData\TempMoviesBlacklist.txt";
                var tempMoviesBlacklist = GetTempMoviesBlacklistByUserEmail(user.Email, path);

                foreach (var item in tempMoviesBlacklist)
                    movies.RemoveAll(x => x.Id == item);
            }
            
            return movies;
        }

        private static string RemoveSpecialsChars(string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }

        private static List<int> GetTempMoviesBlacklistByUserEmail(string userEmail, string path)
        {
            var moviesToBlock = new List<int>();
            
            string[] lines = File.ReadAllLines(path); 

            foreach (string line in lines)
            {
                var lineSplitted = line.Split(";");
                var email = lineSplitted[0];
                var movieId = Convert.ToInt32(lineSplitted[1]);
                var date = Convert.ToDateTime(lineSplitted[2]);

                if (userEmail.Equals(email))
                    moviesToBlock.Add(movieId);
            }

            return moviesToBlock;
        }

        private static void CleanTempMoviesBlacklist(string path)
        {
            string[] lines = File.ReadAllLines(path);
            System.IO.File.WriteAllText(path, string.Empty);

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
            {
                foreach (string line in lines)
                {
                    var lineSplitted = line.Split(";");
                    var date = Convert.ToDateTime(lineSplitted[2]).AddDays(7);

                    if (date > DateTime.UtcNow)
                        file.WriteLine(line);
                }
            }
        }

        private static void InsertTempMoviesBlacklist(string userEmail, int movieId, string path)
        {
            string[] lines = File.ReadAllLines(path);

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
            {
                var line = $"{userEmail};{movieId};{DateTime.UtcNow}";
                file.WriteLine(line);
            }
        }
    }
}
