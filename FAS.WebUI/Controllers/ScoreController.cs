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
using FAS.Web.Controllers;
using FAS.DAL;

namespace FAS.WebUI.Controllers
{
    [Authorize]
    public class ScoreController : AppController
    {
        private readonly IScoreService ScoreService;

        public ScoreController(IScoreService scoreService, IUserService userService) : base(userService)
        {
            this.ScoreService = scoreService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var model = ScoreService.Get()
                                    .Where(x => x.IdUser == user.Id)
                                    .ProjectTo<ScoreItemModel>()
                                    .ToListAsync();

            return View(await model);
        }
       
        [HttpGet]
        public ActionResult Change()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var db = (AppDbContext)DependencyResolver.Current.GetService<DbContext>(); // !!!!!

            SelectList ViewScores = new SelectList(db.ViewScores, "Id", "Name");
            ViewBag.ViewScores = ViewScores;
            SelectList TypeScores = new SelectList(db.TypeScores, "Id", "Name");
            ViewBag.TypeScores = TypeScores;
            SelectList StatusScores = new SelectList(db.StatusScores, "Id", "Name");
            ViewBag.StatusScores = StatusScores;

            //var userE = await GetCurrentUserAsync();
            //var user = Mapper.Map<ChangeScoreViewModel>(userE);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ChangeScoreViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                var score = new Score { IdUser = user.Id, IdStatus = model.IdStatus, IdTypeScore = model.IdTypeScore, IdViewScore = model.IdViewScore, Balance = model.Balance, Notation = model.Notation };
                user.Scores.Add(score);
                await UserService.UpdateAsync(user);
                return RedirectToAction("Index", "Score");
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
            var score = Mapper.Map<ChangeScoreViewModel>(ScoreService.Get(id));
            return View(score);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Change(ChangeScoreViewModel model)
        {
            if (ModelState.IsValid)
            {
                var score = ScoreService.Get(model.Id);
                score.Balance = model.Balance;
                score.Notation = model.Notation;
                await ScoreService.UpdateAsync(score);
                return RedirectToAction("Index");
            }

            return View(model);
        }

    }
}