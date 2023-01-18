using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesApp.Filters;
using MoviesApp.Models;
using MoviesApp.Services;
using MoviesApp.Services.Dto;
using MoviesApp.ViewModels;

namespace MoviesApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IMovieService _service;

        public MoviesController(ILogger<HomeController> logger, IMapper mapper, IMovieService service)
        {
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }

        // GET: Movies
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var movies = _mapper.Map<IEnumerable<MovieDto>, IEnumerable<Movie>>(_service.GetAllMovies());
            return View(_mapper.Map<IEnumerable<Movie>,IEnumerable<MovieViewModel>>(movies));
        }

        // GET: Movies/Details/5
        [HttpGet]
        [Authorize]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<Movie>(_service.GetMovie((int)id));

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<MovieViewModel>(viewModel));
        }

        // GET: Movies/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [EnsureReleaseDateBeforeNow]
        public IActionResult Create([Bind("Title,ReleaseDate,Genre,Price")] InputMovieViewModel inputModel)
        {
            if (ModelState.IsValid)
            {
                _service.AddMovie(_mapper.Map<MovieDto>(_mapper.Map<Movie>(inputModel)));
                return RedirectToAction(nameof(Index));
            }

            return View(inputModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        // GET: Movies/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editModel = _mapper.Map<Movie>(_service.GetMovie((int)id));

            if (editModel == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<EditMovieViewModel>(editModel));
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id, [Bind("Title,ReleaseDate,Genre,Price")] EditMovieViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                var movie = _mapper.Map<Movie>(editModel);
                movie.Id = id;

                var result = _service.UpdateMovie(_mapper.Map<MovieDto>(movie)); 

                if (result == null)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(editModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        // GET: Movies/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deleteModel = _mapper.Map<Movie>(_service.GetMovie((int)id));

            if (deleteModel == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<DeleteMovieViewModel>(deleteModel) );
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = _service.DeleteMovie(id);
            if (movie == null)
            {
                return NotFound();
            }

            _logger.LogTrace($"Movie with id {movie.Id} has been deleted!");
            return RedirectToAction(nameof(Index));
        }
    }
}