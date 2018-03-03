using GimmeMovieSuggestionsAPI.Integrations.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GimmeMovieSuggestionsAPI.Integrations
{
    public static class UsersManagementIntegration
    { 
        public static UserDTO GetUserByEmail(string email)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://shielded-lowlands-27093.herokuapp.com");
                MediaTypeWithQualityHeaderValue contentType =  new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);

                HttpResponseMessage response = client.GetAsync($"/users/{email}").Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                UserDTO data = JsonConvert.DeserializeObject<List<UserDTO>>(stringData).FirstOrDefault();
                return data;
            }
        }
    }
}
