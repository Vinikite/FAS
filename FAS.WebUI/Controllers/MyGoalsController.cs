using AutoMapper;
using AutoMapper.QueryableExtensions;
using FAS.BLL;
using FAS.Domain;
using FAS.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FAS.WebUI.Controllers
{
    public class MyGoalsController : Controller
    {
        private readonly IMyGoalsService MyGoalsService;

        public MyGoalsController(IMyGoalsService myGoalsService)
        {
            this.MyGoalsService = myGoalsService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await MyGoalsService.Get().ProjectTo<SimpleMyGoalsViewModel>().ToListAsync());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateMyGoalsViewModel model)
        {
            if (ModelState.IsValid)
            {
                await MyGoalsService.CreateAsync(Mapper.Map<MyGoals>(model));
                return RedirectToAction("Index", "Transaction");
            }

            return View(model);
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Delete(Guid id)
        {
            await MyGoalsService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}