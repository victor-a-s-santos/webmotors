using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WebMotors.Common
{
    public class ApiClient<T, TResourceIdentifier> : IDisposable where T : class
    {
        private bool disposed = false;
        private HttpClient _client;

        public ApiClient()
        {
            _client = ConfigureHttpClient();
        }

        protected virtual HttpClient ConfigureHttpClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://desafioonline.webmotors.com.br/api/");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("gzip"));
            _client.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("deflate"));
            return _client;
        }

        public async Task<IEnumerable<T>> GetManyAsync(TResourceIdentifier identifier)
        {
            string responseContent = string.Empty;
            try
            {
                using (HttpResponseMessage responseMessage = _client.GetAsync(identifier.ToString()).Result)
                {
                    var bytes = await responseMessage.Content.ReadAsByteArrayAsync();
                    responseContent = Encoding.UTF8.GetString(bytes);

                    responseMessage.EnsureSuccessStatusCode();
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return await responseMessage.Content.ReadAsAsync<IEnumerable<T>>();
                    }
                }
            }
            catch (HttpRequestException ex)
            {                
                    throw ex;
            }

            return null;
        }

        public async Task<IEnumerable<T>> PostManyAsync(TResourceIdentifier identifier, T model)
        {
            string responseContent = string.Empty;
            try
            {
                using (HttpResponseMessage responseMessage = _client.PostAsJsonAsync(identifier.ToString(), model).Result)
                {
                    var bytes = await responseMessage.Content.ReadAsByteArrayAsync();
                    responseContent = Encoding.UTF8.GetString(bytes);

                    responseMessage.EnsureSuccessStatusCode();

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return await responseMessage.Content.ReadAsAsync<IEnumerable<T>>();
                    }

                }
            }
            catch (HttpRequestException ex)
            {                
                    throw ex;
            }

            return null;
        }

        public async Task<T> GetAsync(TResourceIdentifier identifier)
        {
            string responseContent = string.Empty;
            try
            {
                using (HttpResponseMessage responseMessage = _client.GetAsync(identifier.ToString()).Result)
                {
                    var bytes = await responseMessage.Content.ReadAsByteArrayAsync();
                    responseContent = Encoding.UTF8.GetString(bytes);

                    responseMessage.EnsureSuccessStatusCode();

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return await responseMessage.Content.ReadAsAsync<T>();
                    }
                }
            }
            catch (HttpRequestException ex)
            {                
                    throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }

        public async Task<T> PostAsync(TResourceIdentifier identifier, T model)
        {
            string responseContent = string.Empty;
            try
            {
                using (HttpResponseMessage responseMessage = _client.PostAsJsonAsync(identifier.ToString(), model).Result)
                {
                    var bytes = await responseMessage.Content.ReadAsByteArrayAsync();
                    responseContent = Encoding.UTF8.GetString(bytes);

                    responseMessage.EnsureSuccessStatusCode();

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return await responseMessage.Content.ReadAsAsync<T>();
                    }
                }
            }
            catch (HttpRequestException ex)
            {                
                    throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }

        public async Task PutAsync(TResourceIdentifier identifier, T model)
        {
            var requestMessage = new HttpRequestMessage();
            var responseMessage = await _client.PutAsJsonAsync(identifier.ToString(), model);
        }

        public async Task DeleteAsync(TResourceIdentifier identifier)
        {
            var r = await _client.DeleteAsync(identifier.ToString());
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                if (_client != null)
                {
                    var hc = _client;
                    _client = null;
                    hc.Dispose();
                }
                disposed = true;
            }
        }

        #endregion IDisposable Members
    }
}
