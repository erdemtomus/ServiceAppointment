using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace TofasRandevu.Services.Base
{
    public class RestService
    {
        private string endPoint;
        private Uri uri;
        private string username;
        private string password;
        public RestService(string url, int? port, string endPoint, string username, string password)
        {
            this.endPoint = endPoint;
            this.uri = createUri(url, port);
            this.username = username;
            this.password = password;
        }

        private Uri createUri(string url, int? port)
        {
            var builder = new UriBuilder(url);
            if(port != null){
                builder.Port = (int)port;
            }
            return builder.Uri;
        }

        public Response<T> SendJsonRequest<T>(HttpMethod httpMethod, string methodUrl, object customerRequestObject)
        {
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", this.username, this.password)));
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MimeTypes.JSON));
                HttpRequestMessage request = new HttpRequestMessage(httpMethod, endPoint + methodUrl);
                request.Content = new StringContent(JsonConvert.SerializeObject(customerRequestObject, Formatting.Indented, new UnixDateTimeConverter()),
                                    Encoding.UTF8,
                                    MimeTypes.JSON);
                var response = client.SendAsync(request).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<T>(responseBody);
                    return new Response<T>
                    {
                        Code = response.StatusCode,
                        Data = data
                    };
                }
                else
                {
                    return new Response<T>
                    {
                        Code = response.StatusCode
                    };
                }
            }
        }
        public Response<T> SendRequest<T>(HttpMethod httpMethod, string methodUrl)
        {
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", this.username, this.password)));
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MimeTypes.JSON));
                var request = new HttpRequestMessage(httpMethod, endPoint + methodUrl);
                var response = client.SendAsync(request).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<T>(responseBody, new UnixDateTimeConverter());
                    return new Response<T>
                    {
                        Code = response.StatusCode,
                        Data = data
                    };
                }
                else
                {
                    return new Response<T>
                    {
                        Code = response.StatusCode
                    };
                }
            }
        }



    }
}