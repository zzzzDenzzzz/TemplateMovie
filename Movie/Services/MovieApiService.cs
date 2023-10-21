

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Movie.Models;
using Movie.Options;
using System.Text.Json;

namespace Movie.Services
{
    public class MovieApiService : IMovieApiService
    {
        private readonly IMemoryCache memoryCache;

        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        private HttpClient httpClient { get; set; }

        public MovieApiService(IHttpClientFactory httpClientFactory, IOptions<MovieApiOptions> options,
            IMemoryCache memoryCache)
        {
            //BaseUrl = "https://omdbapi.com/";
            //ApiKey = "5b9b7798";

            BaseUrl = options.Value.BaseUrl;
            ApiKey = options.Value.ApiKey;

            httpClient = httpClientFactory.CreateClient();
            this.memoryCache = memoryCache;
        }

        public async Task<MovieApiResponse> SearchByTitleAsync(string title, int page = 1)
        {
            MovieApiResponse result;

            if (true)//tr && !memoryCache.TryGetValue(title.ToLower(),out result))
            {
                Console.WriteLine("REQUEST id");
                var response = await httpClient.GetAsync($"{BaseUrl}?s={title}&apikey={ApiKey}&page={page}");
                var json = await response.Content.ReadAsStringAsync();
                //result = JsonConvert.DeserializeObject<MovieApiResponse>(json);
                result = JsonSerializer.Deserialize<MovieApiResponse>(json);

                if (result.Response == "False")
                    throw new Exception(result.Error);


                var cacheTime = new MemoryCacheEntryOptions();
                cacheTime.SetAbsoluteExpiration(TimeSpan.FromDays(10));

                memoryCache.Set(title.ToLower(), result, cacheTime);
            }
            else
            {
                Console.WriteLine("Read Cache");
            }

            return result;
        }



        public async Task<Cinema> SearchByIdAsync(string id)
        {
            Cinema result;

            if (true)//!memoryCache.TryGetValue(id, out result))
            {
                Console.WriteLine("REQUEST id");
                var response = await httpClient.GetAsync($"{BaseUrl}?&apikey={ApiKey}&i={id}");
                var json = await response.Content.ReadAsStringAsync();
                //result = JsonConvert.DeserializeObject<Cinema>(json);
                result = JsonSerializer.Deserialize<Cinema>(json);
                if (result.Response == "False")
                    throw new Exception(result.Error);


                var cacheTime = new MemoryCacheEntryOptions();
                cacheTime.SetAbsoluteExpiration(TimeSpan.FromDays(10));

                memoryCache.Set(id, result, cacheTime);
            }
            else
            {
                Console.WriteLine("Cache read");
            }



            return result;
        }
    }
}
