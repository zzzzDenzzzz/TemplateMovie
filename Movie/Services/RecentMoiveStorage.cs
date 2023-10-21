using Movie.Models;
using System.Collections.Concurrent;

namespace Movie.Services
{
    public class RecentMoiveStorage : IRecentMoiveStorage
    {
        private ConcurrentQueue<Cinema> Cinemas = new ConcurrentQueue<Cinema>();
        public void Add(Cinema cinema)
        {

            var result = Cinemas.FirstOrDefault(x => x.imdbID == cinema.imdbID);
            if (result==null)
            {

                Cinemas.Enqueue(cinema); 
            }
           

            if (Cinemas.Count>6)
                Cinemas.TryDequeue(out Cinema trash);


          
        }

        public IEnumerable<Cinema> GetRecent()
        {
            return Cinemas.Take(6);
        }
    }
}
