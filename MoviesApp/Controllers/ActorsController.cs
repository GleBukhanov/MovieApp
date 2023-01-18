using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesApp.Data;
using MoviesApp.Filters;
using MoviesApp.Models;
using MoviesApp.ViewModels;

namespace MoviesApp.Controllers;

public class ActorsController:Controller
{
    private readonly MoviesContext _context;
    private readonly ILogger<HomeController> _logger;
    private readonly IMapper _mapper;
    
    public ActorsController(MoviesContext context, ILogger<HomeController> logger,IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Authorize]
    public IActionResult Index()
    {
        var actors = _mapper.Map<IEnumerable<Actor>, IEnumerable<ActorViewModel>>(_context.Actors.ToList());
        return View(actors);
    }
    [Authorize]
    [HttpGet]
    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var viewModel = _mapper.Map<ActorViewModel>(_context.Actors.FirstOrDefault(a => a.Id == id));

            
        if (viewModel == null)
        {
            return NotFound();
        }

        return View(viewModel);
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActorValidation]
    [Authorize(Roles = "Admin")]
    public IActionResult Create([Bind("FirstName,LastName,Birthday")] InputActorViewModel inputModel)
    {
        if (ModelState.IsValid)
        {
            _context.Add(_mapper.Map<Actor>(inputModel));
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        return View(inputModel);
    }
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var editModel = _mapper.Map<EditActorViewModel>(_context.Actors.FirstOrDefault(a => a.Id == id));
        
            
        if (editModel == null)
        {
            return NotFound();
        }
        return View(editModel);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActorValidation]
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int id, [Bind("FirstName,LastName,Birthday")] EditActorViewModel editModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var actor = _mapper.Map<Actor>(editModel);
                actor.Id = id;
                _context.Update(actor);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (!ActorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(editModel);
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var deleteModel = _mapper.Map<DeleteActorViewModel>(_context.Actors.FirstOrDefault(a => a.Id == id));
            
        if (deleteModel == null)
        {
            return NotFound();
        }

        return View(deleteModel);
    }
    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteConfirmed(int id)
    {
        var actor = _context.Actors.Find(id);
        _context.Actors.Remove(actor);
        _context.SaveChanges();
        _logger.LogError($"Actor with id {actor.Id} has been deleted!");
        return RedirectToAction(nameof(Index));
    }
    

    private bool ActorExists(int id)
    {
        return _context.Actors.Any(a => a.Id == id);
    }
}