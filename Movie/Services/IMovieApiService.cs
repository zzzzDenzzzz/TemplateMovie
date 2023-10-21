using Movie.Models;

namespace Movie.Services
{
    public interface IMovieApiService
    {
        Task<MovieApiResponse> SearchByTitleAsync(string title,int page = 1);
        Task<Models.Cinema> SearchByIdAsync(string id);
    }
}