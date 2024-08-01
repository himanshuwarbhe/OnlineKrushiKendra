using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineKrushiKendra.Repository.IRepository;
using OnlineKrushiKendra.Repository.Repository;
using OnlineKrushiKendra.Models.ViewModels;
using OnlineKrushiKendra.Models.Models;
namespace OnlineKrushiKendraWeb.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepo _categoryRepo;

        public CategoriesController(ICategoryRepo categoryRepo)
        {
            this._categoryRepo = categoryRepo;
        }

        public IActionResult Index()
        {
            var CategoryList =_categoryRepo.GetCategory();
            return View(CategoryList);
        }

        [HttpGet]
        public IActionResult Create(int? id)
        {
            CategoryViewModel categoryViewModel = new();
            return View(categoryViewModel);
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == null || id.Value == 0)
            {
                return NotFound();
            }
            var category = _categoryRepo.GetCategoryById(id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
            
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryViewModel model)
        {

            if (ModelState.IsValid)
            {
                await _categoryRepo.CreateCategory(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
               await _categoryRepo.EditCategory(model);
            return RedirectToAction("Index");
        }
            return View(model);

    }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var Category = _categoryRepo.DeleteCategoryById(id);
            if (Category == null)
            {
                return NotFound();
            }
            await _categoryRepo.DeleteCategoryById(id);

            return RedirectToAction("Index");
        }
    }
}
