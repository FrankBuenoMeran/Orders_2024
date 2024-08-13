
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Orders.FrontEnd.Repositorios
{
    public class Repository : IRepository
    {
        private readonly HttpClient _httpClient;

        private JsonSerializerOptions _jsonDefaulOptions => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public Repository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseWrapper<T>> GetAsync<T>(string url)
        {
          var responseHttp= await _httpClient.GetAsync(url);

            if (responseHttp.IsSuccessStatusCode) 
            {   //Serialirar es cunado tenemos un objecto en memoria y lo queremos convertir a formato Json
                //Deserializar es por que la respuesta viene en strig
                //Deserializamos la respueta
                var response = await UnserializeAnswer<T>(responseHttp);//Control punto a qui UnserializeAnswer para que nos cree la firma del metodo
                return new HttpResponseWrapper<T>(response, false, responseHttp);
            }
            return new HttpResponseWrapper<T>(default, true, responseHttp);
        }

        public async Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T model)
        {
            var messageJson = JsonSerializer.Serialize(model);
            var messageContent = new StringContent(messageJson, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PostAsync(url, messageContent);
            return new HttpResponseWrapper<object>(null, responseHttp.IsSuccessStatusCode,responseHttp);
        }

        public async Task<HttpResponseWrapper<TActionResponse>> PostAsync<T, TActionResponse>(string url, T model)
        {
            var messageJson=JsonSerializer.Serialize(model);
            var messageContent = new StringContent(messageJson, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PostAsync(url, messageContent);

            if (responseHttp.IsSuccessStatusCode)
            {   //Serialirar es cunado tenemos un objecto en memoria y lo queremos convertir a formato Json
                //Deserializar es por que la respuesta viene en strig
                //Deserializamos la respueta
                var response = await UnserializeAnswer<TActionResponse>(responseHttp);//Control punto a qui UnserializeAnswer para que nos cree la firma del metodo
                return new HttpResponseWrapper<TActionResponse>(response, false, responseHttp);
            }
            return new HttpResponseWrapper<TActionResponse>(default, true, responseHttp);
        }

        private async Task<T> UnserializeAnswer<T>(HttpResponseMessage responseHttp)
        {
            var response = await responseHttp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(response, _jsonDefaulOptions)!;

        }
    }
}
