using MvcCorePersonajesSeriesAzure.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;

namespace MvcCorePersonajesSeriesAzure.Services
{
    public class PersonajesService
    {
        private string ApiUrl;
        private MediaTypeWithQualityHeaderValue Header;
        public PersonajesService(IConfiguration config)
        {
            this.ApiUrl = config.GetValue<string>("ApiUrls:ApiPersonajes");
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }


        public async Task<List<PersonajesSeries>> GetAllPersonajesAsync()
        {
            string request = "api/Personajes";
            return await this.CallApiAsync<List<PersonajesSeries>>(request);
        }

        public async Task<List<string>> GetSeriesAsync()
        {
            string request = "api/Personajes/Series";
            return await this.CallApiAsync<List<string>>(request);
        }

        public async Task<List<PersonajesSeries>> GetPersonajesSeriesAsync(string serie)
        {
            string request = "api/Personajes/PersonajesSeries/" + serie;
            return await this.CallApiAsync<List<PersonajesSeries>>(request);
        }

        public async Task<PersonajesSeries> FindPersonajeByIdAsync(int id)
        {
            string request = "api/Personajes/" + id;
            return await this.CallApiAsync<PersonajesSeries>(request);
        }

        public async Task<List<PersonajesSeries>> GetSeriesAsync(string serie)
        {
            string request = "api/Personajes/" + serie;
            return await this.CallApiAsync<List<PersonajesSeries>>(request);
        }

        public async Task InsertPersonajeAsync(PersonajesSeries personaje)
        {
            string request = "api/Personajes/InsertPersonaje";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string jsonModel = JsonConvert.SerializeObject(personaje);
                StringContent content =
                    new StringContent
                    (jsonModel, Encoding.UTF8, "application/json");
                await client.PostAsync(request, content);
            }
        }

        public async Task UpdatePersonajeAsync(PersonajesSeries personaje)
        {
            string request = "api/Personajes/UpdatePersonaje";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string jsonModel = JsonConvert.SerializeObject(personaje);
                StringContent content =
                    new StringContent
                    (jsonModel, Encoding.UTF8, "application/json");
                await client.PutAsync(request, content);
            }
        }

        public async Task DeletePersonajeAsync(int id)
        {
            string request = "api/Personajes/DeletePersonaje/" + id;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                await client.DeleteAsync(request);
            }
        }
    }
}
