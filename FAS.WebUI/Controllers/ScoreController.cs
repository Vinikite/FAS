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

namespace FAS.WebUI.Controllers
{
    public class ScoreController : AppController
    {
        private readonly IScoreService ScoreService;

        public ScoreController(IScoreService scoreService)
        {
            this.ScoreService = scoreService;
        }
        ChangeScoreViewModel db = new ChangeScoreViewModel();
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await UserService.Get().ProjectTo<ChangeUserViewModel>().ToListAsync());
        }
       
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var scoreE = await GetCurrentUserIdAsync();
            var score = Mapper.Map<ChangeScoreViewModel>(scoreE);
            return View(score);
        }
        [HttpGet]
        public ActionResult Change()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ChangeScoreViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserIdAsync();
                var score = await ScoreService.CreateAsync(Mapper.Map<Score>(model)); 
                user.Id = model.IdUser;
                score.LastName = model.IdStatus;
                score.MiddleName = model.IdTypeScore;
                score.PhoneNumber = model.IdViewScore;
                score.AverageIncome = model.Notation;
                score.AverageIncome = model.Balance;
                await UserService.UpdateAsync(score);
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