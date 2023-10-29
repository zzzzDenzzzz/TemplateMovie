using Movie.Options;
using Movie.Services;

namespace Movie.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMovieService(this IServiceCollection services, Action<MovieApiOptions> options)
        {
            services.AddScoped<IMovieApiService, MovieApiService>();
            services.Configure(options);
            return services;
        }
    }
}
