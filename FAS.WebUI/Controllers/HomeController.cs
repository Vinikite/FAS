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
using FAS.Core;
using FAS.Domain;
using FAS.WebUI.Infrastructure.Validators;
using FAS.WebUI.Models;

namespace FAS.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService bookService;

        public HomeController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public async Task<ActionResult> Index()
        {
            return View(await bookService.GetAll().ProjectTo<SimpleBookViewModel>().ToListAsync());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                await bookService.CreateAsync(Mapper.Map<Book>(model));
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Delete(Guid id)
        {
            await bookService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Change(Guid id)
        {
            var book = Mapper.Map<ChangeBookViewModel>(bookService.Get(id));
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Change(ChangeBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var book = bookService.Get(model.Id);
                book.Name = model.Name;
                book.Price = model.Price;
                await bookService.UpdateAsync(book);
                return RedirectToAction("Index");
            }

            return View(model);
        }

    }
}