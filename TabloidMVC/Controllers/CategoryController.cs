using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class CategoryController : Controller
   
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IPostRepository _postRepository;
        public CategoryController(ICategoryRepository categoryRepository, IPostRepository postRepository)
        {
            _categoryRepo = categoryRepository;
            _postRepository = postRepository;
        }
        // GET: CategoryController
        public ActionResult Index()
        {
            List<Category> categories = _categoryRepo.GetAll();
            return View(categories);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            Category category = _categoryRepo.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                _categoryRepo.AddCategory(category);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View(category);
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            Category category = _categoryRepo.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category category)
        {
            try
            {
                _categoryRepo.UpdateCategory(category);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View(category);
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        { 
            Category category = _categoryRepo.GetCategoryById(id);
            List<Post> posts = _postRepository.GetPostsByCategoryId(category.Id);

            CategoryDeleteViewModel vm = new CategoryDeleteViewModel()
            {
                Posts = posts,
                Category = category
            };

            return View(vm);
        }

      

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category category)
        {
            try
            {
                _categoryRepo.DeleteCategory(id);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View(category);
            }
        }
    }
}
