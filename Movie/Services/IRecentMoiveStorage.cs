using Movie.Models;

namespace Movie.Services
{
    public interface IRecentMoiveStorage
    {

        void Add(Cinema cinema);
        IEnumerable<Cinema> GetRecent();
    }
}
