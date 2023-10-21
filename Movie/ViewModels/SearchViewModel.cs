﻿using Movie.Models;

namespace Movie.ViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<Cinema> Movies { get; set; }
        public string Title { get; set; }
        public int CurrentPage { get; set; }
        public int CountViewPage { get; set; }
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
        public string Response { get; set; }
        public string Error { get; set; }

    }
}
