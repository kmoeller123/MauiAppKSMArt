using KSMWebApi.Models;
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
    public class ArtObjectRestService
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;
        //string _urlAll = @"https://localhost:7196/ArtObjects/All";
        //string _urlGet = @"https://localhost:7196/ArtObjects/details";
        //string _urlCreate = @"https://localhost:7196/ArtObjects/Create";
        //string _urlUpdate = @"https://localhost:7196/ArtObjects/Update";
        //string _urlDelete = @"https://localhost:7196/ArtObjects/Delete";
        //string _urlTest = @"https://localhost:7196/ArtObjects/Test";

        string _urlAll = @"https://ksmartwebapi20241201155931.azurewebsites.net/ArtObjects/All";
        string _urlGet = @"https://ksmartwebapi20241201155931.azurewebsites.net/ArtObject/details";
        string _urlCreate = @"https://ksmartwebapi20241201155931.azurewebsites.net/ArtObjects/Create";
        string _urlUpdate = @"https://ksmartwebapi20241201155931.azurewebsites.net/ArtObjects/Update";
        string _urlDelete = @"https://ksmartwebapi20241201155931.azurewebsites.net/ArtObjects/Delete";
        string _urlTest = @"https://ksmartwebapi20241201155931.azurewebsites.net/ArtObjects/Test";

        List<ArtObject> Items = new List<ArtObject>();
        
        public ArtObjectRestService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public async Task<List<ArtObject>> GetDataAsync()
        {
            Items = new List<ArtObject>();

            Uri uri = new Uri(string.Format(_urlAll, string.Empty));
{

            };
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = JsonSerializer.Deserialize<List<ArtObject>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task<ArtObject> GetItemAsync(int id)
        {
            ArtObject Item = new ArtObject();

            Uri uri = new Uri(string.Format(_urlGet + @"/" + id.ToString(), string.Empty));
            {

            };
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Item = JsonSerializer.Deserialize<ArtObject>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Item;
        }

        public async Task<ArtObject> CreateArtObjectAsync(ArtObject item)
        {
            Uri uri = new Uri(string.Format(_urlCreate, string.Empty));
            
            try
            {
                string json = JsonSerializer.Serialize<ArtObject>(item, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage? response = null;
                
                response = await _client.PostAsync(uri, content);
                             

                if (response.IsSuccessStatusCode)
                {
                    string c = await response.Content.ReadAsStringAsync();
                    item = JsonSerializer.Deserialize<ArtObject>(c, _serializerOptions);

                    Debug.WriteLine(@"\tArtObject successfully Created.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return item;    
        }

        public async Task<ArtObject> UpdateArtObjectAsync(ArtObject item)
        {
            Uri uri = new Uri(string.Format(_urlUpdate, @"/" + item.Id));

            try
            {
                string json = JsonSerializer.Serialize<ArtObject>(item, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage? response = null;

                response = await _client.PutAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    string c = await response.Content.ReadAsStringAsync();
                    item = JsonSerializer.Deserialize<ArtObject>(c, _serializerOptions);

                    Debug.WriteLine(@"\tArtObject successfully Updated.");
                }
            }
            catch (Exception ex)
            {
                
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return item;
        }


        public async Task<string> DeleteArtObjectAsync(int id)
        {
           
           Uri uri = new Uri(string.Format(_urlDelete + @"/" + id.ToString(), string.Empty));

            HttpResponseMessage response = null;

            try
            {
                response = await _client.PostAsync(uri,null);

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

        public async Task<List<Image>> GetTestAsync()
        {   
            List<Image> images = new List<Image>();
            
            Uri uri = new Uri(string.Format(_urlTest, string.Empty));
            {

            };
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    images = JsonSerializer.Deserialize<List<Image>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return images;
        }


    }
}
