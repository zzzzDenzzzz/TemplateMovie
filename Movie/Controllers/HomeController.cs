using Microsoft.AspNetCore.Mvc;
using Movie.Models;
using Movie.Services;
using Movie.ViewModels;
using System.Diagnostics;

namespace Movie.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieApiService movieApiService;
        private readonly IRecentMoiveStorage recentMoiveStorage;

        public HomeController(IMovieApiService movieApiService,IRecentMoiveStorage recentMoiveStorage)
        {
            this.movieApiService = movieApiService;
            this.recentMoiveStorage = recentMoiveStorage;
        }

        public async Task<IActionResult> Index()
        {
            var result = recentMoiveStorage.GetRecent();
            return View(result);
        }


        public async Task<IActionResult> Movie(string id)
        {
            Cinema cinema = null;

            try
            {
                cinema = await movieApiService.SearchByIdAsync(id);
                recentMoiveStorage.Add(cinema);
            }
            catch (Exception ex)
            {

                ViewBag.errorMessages = ex.Message;
            }
            return View(cinema);
        }

        public async Task<IActionResult> MovieModal(string id)
        {
            Cinema cinema = null;

            try
            {
                cinema = await movieApiService.SearchByIdAsync(id);
                recentMoiveStorage.Add(cinema);
            }
            catch (Exception ex)
            {

                ViewBag.errorMessages = ex.Message;
            }
            return PartialView("_MovieModalPartial",cinema);
        }



        public async Task<IActionResult> SearchResult(string title, int page = 1, int countViewPage = 4)
        {
            SearchViewModel searchViewModel = new SearchViewModel();


            try
            {
                MovieApiResponse result = await movieApiService.SearchByTitleAsync(title, page); 
                searchViewModel.Movies = result.Cinemas; 
            }
            catch (Exception ex)
            {
                searchViewModel.Error = ex.Message;
            }

            return PartialView("_MovieListPartial", searchViewModel.Movies);
        }

        public async Task<IActionResult> Search(string title,int page = 1,int countViewPage = 4)
        {

            SearchViewModel searchViewModel = new();

            try
            {
                MovieApiResponse result = await movieApiService.SearchByTitleAsync(title, page);

                searchViewModel.Title = title;
                searchViewModel.CountViewPage = countViewPage;
                searchViewModel.Movies = result.Cinemas;
                searchViewModel.Response = result.Response;
                searchViewModel.Error = result.Error;
                searchViewModel.CurrentPage = page;
                searchViewModel.TotalResults = result.TotalResults;
                searchViewModel.TotalPages = (int)Math.Ceiling(result.TotalResults / 10.0);
            }
            catch (Exception ex)
            {
                searchViewModel.Error = ex.Message;
            }
             
            return View(searchViewModel);
        }

        public IActionResult Privacy()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}