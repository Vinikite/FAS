using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FAS.BLL;
using FAS.Domain;
using FAS.WebUI.Infrastructure.Validators;
using FAS.WebUI.Models;

namespace FAS.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService CategoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.CategoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await CategoryService.Get().ToListAsync());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                await CategoryService.CreateAsync(Mapper.Map<Category>(model));
                return RedirectToAction("Index", "Transaction");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Delete(Guid id)
        {
            await CategoryService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Change(Guid id)
        {
            var category = Mapper.Map<ChangeCategoryViewModel>(CategoryService.Get(id));
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Change(ChangeCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = CategoryService.Get(model.Id);
                category.Name = model.Name;
                await CategoryService.UpdateAsync(category);
                return RedirectToAction("Index");
            }

            return View(model);
        }

    }
}