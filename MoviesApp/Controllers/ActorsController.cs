using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesApp.Data;
using MoviesApp.Filters;
using MoviesApp.Models;
using MoviesApp.ViewModels;

namespace MoviesApp.Controllers
{
    public class ActorsController: Controller
    {
        private readonly MoviesContext _context;
        public readonly ILogger<HomeController> _logger;

        public ActorsController(MoviesContext context, ILogger<HomeController> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_context.Actors.Select(m => new ActorViewModel
            {
                Id = m.Id,
                Name = m.Name,
                FirstName = m.FirstName,
                DateOfBirth = m.DateOfBirth
            }).ToList());
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = _context.Actors
                .Where(e => e.Id == id)
                .Select(e => new ActorViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    FirstName = e.FirstName,
                    DateOfBirth = e.DateOfBirth
                }).FirstOrDefault();
            
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editModel = _context.Actors.Where(e => e.Id == id).Select(e => new EditActorViewModel
                {
                    Name = e.Name,
                    FirstName = e.FirstName,
                    DateOfBirth = e.DateOfBirth
                }).FirstOrDefault();
            if (editModel == null)
            {
                return NotFound();
            }

            return View(editModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckingTheActorForTheAgeOfSevenToNinetyNineYears]
        public IActionResult Edit(int id, [Bind("Name,FirstName,DateOfBirth")] EditActorViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var actor = new Actor
                    {
                        Id = id,
                        Name = editModel.Name,
                        FirstName = editModel.FirstName,
                        DateOfBirth = editModel.DateOfBirth
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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckingTheActorForTheAgeOfSevenToNinetyNineYears]
        public IActionResult Create([Bind("Name,FirstName,DateOfBirth")] ActorViewModel inputModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new Actor
                {
                    Name = inputModel.Name,
                    FirstName = inputModel.FirstName,
                    DateOfBirth = inputModel.DateOfBirth
                });
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(inputModel);
        }
        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.Id == id);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delModel = _context.Actors
                .Where(e => e.Id == id)
                .Select(e => new DeleteActorViewModel
                {
                    Name = e.Name,
                    FirstName = e.FirstName,
                    DateOfBirth = e.DateOfBirth
                }).FirstOrDefault();
            if (delModel == null)
            {
                return NotFound();
            }
            return View(delModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var delModel = _context.Actors.Find(id);
            _context.Actors.Remove(delModel);
            _context.SaveChanges();
            _logger.LogError($"Movie with id {delModel.Id} has been deleted!");
            return RedirectToAction(nameof(Index));
        }
    }
}