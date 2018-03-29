using GimmeMovieSuggestionsAPI.Integrations.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeMovieSuggestionsAPI.Utils
{
    public class GenresSynonims
    {
        private Dictionary<string, string> synonims;

        public GenresSynonims()
        {
            synonims = new Dictionary<string, string>();

            synonims.Add("medo", "terror");
            synonims.Add("medroso", "terror");
            synonims.Add("susto", "terror");
            synonims.Add("tenebro", "terror");
            synonims.Add("escuro", "terror");
            synonims.Add("escuridao", "terror");
            synonims.Add("chuvoso", "terror");
            synonims.Add("pavor", "terror");
            synonims.Add("assusta", "terror");
            synonims.Add("rir", "comedia"); 
            synonims.Add("risada", "comedia");
            synonims.Add("gargalhada", "comedia"); 
            synonims.Add("divertir", "comedia"); 
            synonims.Add("familia", "aventura");
            synonims.Add("heroi", "aventura");
            synonims.Add("chorar", "drama");
            synonims.Add("triste","drama");
            synonims.Add("tristeza", "drama");
            synonims.Add("amor", "romance");
            synonims.Add("beijo", "romance");
        }

        public string GetGenresBySynonims(string text)
        {
            var sentimentos = " ";

            foreach(var word in text.Split(' '))
            {
                var genreName = synonims.FirstOrDefault(x => x.Key.Equals(word, StringComparison.OrdinalIgnoreCase)).Value;
                if (!string.IsNullOrEmpty(genreName) && !sentimentos.Contains(genreName))
                    sentimentos = sentimentos + " " + genreName;
            }

            return sentimentos;
        }
    }
}
