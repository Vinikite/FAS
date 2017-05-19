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
    public class TypeOfScoreController : Controller
    {
        private readonly IScoreService ScoreService;

        public TypeOfScoreController(IScoreService scoreService)
        {
            this.ScoreService = scoreService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await ScoreService.Get().ToListAsync());//.ProjectTo<SimpleScoreViewModel>().ToListAsync());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateScoreViewModel model)
        {
            if (ModelState.IsValid)
            {
                await ScoreService.CreateAsync(Mapper.Map<Score>(model));
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Delete(Guid id)
        {
            await ScoreService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Change(Guid id)
        {
            var score = Mapper.Map<ChangeScoreViewModel>(ScoreService.GetAsync(id));
            return View(score);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Change(ChangeScoreViewModel model)
        {
            if (ModelState.IsValid)
            {
                var score = await ScoreService.GetAsync(model.Id);
                score.Balance = model.Balance;
                score.Notation = model.Notation;
                await ScoreService.UpdateAsync(score);
                return RedirectToAction("Index");
            }

            return View(model);
        }

    }
}