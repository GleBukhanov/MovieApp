using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesApp.Data;
using MoviesApp.Models;
using MoviesApp.ViewModels;

namespace MoviesApp.Controllers;

public class ActorsController:Controller
{
    private readonly MoviesContext _context;
    private readonly ILogger<HomeController> _logger;
    
    public ActorsController(MoviesContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return View(_context.Actors.Select(a => new ActorViewModel
        {
            Id = a.Id,
            FirstName = a.FirstName,
            LastName= a.LastName,
            Birthday = a.Birthday
        }).ToList());
    }
    
    [HttpGet]
    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var viewModel = _context.Actors.Where(a => a.Id == id).Select(a => new ActorViewModel
        {
            Id = a.Id,
            FirstName = a.FirstName,
            LastName = a.LastName,
            Birthday = a.Birthday
        }).FirstOrDefault();

            
        if (viewModel == null)
        {
            return NotFound();
        }

        return View(viewModel);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("FirstName,LastName,Birthday")] InputActorViewModel inputModel)
    {
        if (ModelState.IsValid)
        {
            _context.Add(new Actor
            {
                FirstName = inputModel.FirstName,
                LastName = inputModel.LastName,
                Birthday = inputModel.Birthday
            });
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        return View(inputModel);
    }
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var editModel = _context.Actors.Where(a => a.Id == id).Select(a => new EditActorViewModel
        {
            FirstName = a.FirstName,
            LastName = a.LastName,
            Birthday = a.Birthday
        }).FirstOrDefault();
            
        if (editModel == null)
        {
            return NotFound();
        }
            
        return View(editModel);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("FirstName,LastName,Birthday")] EditActorViewModel editModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var actor = new Actor
                {
                    Id = id,
                    FirstName = editModel.FirstName,
                    LastName= editModel.LastName,
                    Birthday = editModel.Birthday
                };
                    
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
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var deleteModel = _context.Actors.Where(a => a.Id == id).Select(a => new DeleteActorViewModel
        {
            FirstName = a.FirstName,
            LastName = a.LastName,
            Birthday = a.Birthday
        }).FirstOrDefault();
            
        if (deleteModel == null)
        {
            return NotFound();
        }

        return View(deleteModel);
    }
    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
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