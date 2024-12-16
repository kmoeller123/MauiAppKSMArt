using MauiAppKSMArt.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiAppKSMArt.Services
{
    public class GenreRestService
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;

        //string _urlAll = @"https://localhost:7196/Genre/All";
        //string _urlGet = @"https://localhost:7196/Genre/details";
        //string _urlCreate = @"https://localhost:7196/Genre/Create";
        //string _urlUpdate = @"https://localhost:7196/Genre/Update";
        //string _urlDelete = @"https://localhost:7196/Genre/Delete";
        //string _urlTest = @"https://localhost:7196/Genre/Test";

        string _urlAll = @"https://ksmartwebapi20241201155931.azurewebsites.net/Genre/All";
        string _urlGet = @"https://ksmartwebapi20241201155931.azurewebsites.net/Genre/details";
        string _urlCreate = @"https://ksmartwebapi20241201155931.azurewebsites.net/Genre/Create";
        string _urlUpdate = @"https://ksmartwebapi20241201155931.azurewebsites.net/Genre/Update";
        string _urlDelete = @"https://ksmartwebapi20241201155931.azurewebsites.net/Genre/Delete";

        List<Genre> Items = new List<Genre>();

        public GenreRestService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public async Task<List<Genre>> GetDataAsync()
        {
            Items = new List<Genre>();

            Uri uri = new Uri(string.Format(_urlAll, string.Empty));
            {

            };
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = JsonSerializer.Deserialize<List<Genre>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task<Genre> GetItemAsync(int id)
        {
            Genre Item = new Genre();

            Uri uri = new Uri(string.Format(_urlGet + @"/" + id.ToString(), string.Empty));
            {

            };
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Item = JsonSerializer.Deserialize<Genre>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Item;
        }

        public async Task<Genre> CreateGenreAsync(Genre item)
        {
            Uri uri = new Uri(string.Format(_urlCreate, string.Empty));

            try
            {
                string json = JsonSerializer.Serialize<Genre>(item, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage? response = null;

                response = await _client.PostAsync(uri, content);


                if (response.IsSuccessStatusCode)
                {
                    string c = await response.Content.ReadAsStringAsync();
                    item = JsonSerializer.Deserialize<Genre>(c, _serializerOptions);

                    Debug.WriteLine(@"\tGenre successfully Created.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return item;
        }

        public async Task<Genre> UpdateGenreAsync(Genre item)
        {
            Uri uri = new Uri(string.Format(_urlUpdate, @"/" + item.Id));

            try
            {
                string json = JsonSerializer.Serialize<Genre>(item, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage? response = null;

                response = await _client.PutAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    string c = await response.Content.ReadAsStringAsync();
                    item = JsonSerializer.Deserialize<Genre>(c, _serializerOptions);

                    Debug.WriteLine(@"\tGenre successfully Updated.");
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return item;
        }


        public async Task<string> DeleteGenreAsync(int id)
        {

            Uri uri = new Uri(string.Format(_urlDelete + @"/" + id.ToString(), string.Empty));

            HttpResponseMessage response = null;

            try
            {
                response = await _client.PostAsync(uri, null);

                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"\tTodoItem successfully deleted.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            if (response == null) return @"\tERROR";
            return response.StatusCode.ToString();
        }

    }
}
