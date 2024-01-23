using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //retriving data from categories table from SQL server. (similar to select * command)
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        //get action method below --> just returns a simple view when user navigates to Create page
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
		public IActionResult Create(Category obj)
		{
            if (ModelState.IsValid) 
            {
				_db.Categories.Add(obj);
				_db.SaveChanges();
				TempData["success"] = "Category created successfully";
				return RedirectToAction("Index", "Category");
			}

            return View();
		}

		public IActionResult Edit(int? id)
		{
			if(id == null || id == 0)
			{
				return NotFound();
			}

			//.Find() method here works with only Id of the table
			//1st method to search
			//Category categoryFromDb1 = _db.Categories.Find(id);

			//2nd and most used method to search
			Category categoryFromDb = _db.Categories.FirstOrDefault(u => u.Id == id);

			//3rd mthod used to search
			//Category categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault(u => u.Id == id);

			if (categoryFromDb.Id == null)
			{
				return NotFound();
			}

			return View(categoryFromDb);
		}

		[HttpPost]
		public IActionResult Edit(Category obj)
		{
			if (ModelState.IsValid)
			{
				_db.Categories.Update(obj);
				_db.SaveChanges();
				TempData["success"] = "Category updated successfully";
				return RedirectToAction("Index", "Category");
			}

			return View();
		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			Category categoryFromDb = _db.Categories.FirstOrDefault(u => u.Id == id);

			if (categoryFromDb.Id == null)
			{
				return NotFound();
			}

			return View(categoryFromDb);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePOST(int? id)
		{
			Category obj = _db.Categories.Find(id);

			if (obj == null)
			{
				return NotFound();
			}

			_db.Categories.Remove(obj);
			_db.SaveChanges();
			TempData["success"] = "Category deleted successfully";
			return RedirectToAction("Index", "Category");
		}
	}
}
