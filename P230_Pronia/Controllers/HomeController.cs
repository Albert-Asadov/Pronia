using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P230_Pronia.DAL;
using P230_Pronia.Entities;
using P230_Pronia.ViewModel;
using System.Diagnostics;

namespace P230_Pronia.Controllers
{
    public class HomeController : Controller
    {
        readonly ProniaDbContext _context;

        public HomeController(ProniaDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                slider = _context.Sliders.OrderBy(o => o.Order).ToList(),
                plant = _context.Plants.ToList()
            };
            return View(homeVM);
        }

        public IActionResult Detail(int? id)
        {
            Plant plant = _context.Plants.
                Include(b => b.PlantCategories).ThenInclude(b=>b.Category).
                Include(b => b.PlantDeliveryInformation).
                Include(b => b.PlantImages).
                Include(b => b.PlantTags).ThenInclude(b=>b.Tag).
                SingleOrDefault(b => b.Id == id);
            return View(plant);
        }
    }
}