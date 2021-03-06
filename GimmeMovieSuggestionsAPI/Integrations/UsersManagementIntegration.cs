﻿using GimmeMovieSuggestionsAPI.Integrations.DTOs;
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
        private static string endpoint = "http://gimmeusers.us-west-2.elasticbeanstalk.com";

        public static UserDTO GetUserByEmail(string email)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(endpoint);
                MediaTypeWithQualityHeaderValue contentType =  new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);

                HttpResponseMessage response = client.GetAsync($"/users/{email}").Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                UserDTO data = JsonConvert.DeserializeObject<List<UserDTO>>(stringData).FirstOrDefault();
                return data;
            }
        }

        public static UserDTOs GetUsers()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(endpoint);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);

                HttpResponseMessage response = client.GetAsync($"/users/").Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                var data = new UserDTOs
                {
                    Users = JsonConvert.DeserializeObject<List<UserDTO>>(stringData)
                };

                return data;
            }
        }
    }
}
