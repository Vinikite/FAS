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
    public class TransactionController : AppController
    {
        private readonly ITransactionService TransactionService;
        private readonly IScoreService ScoreService;
        private readonly IUserService userService;

        public TransactionController(ITransactionService transactionService, IScoreService scoreService, IUserService userService) : base(userService)
        {
            this.TransactionService = transactionService;
            this.ScoreService = scoreService;
            this.userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await TransactionService.Get().ProjectTo<SimpleTransactionViewModel>().ToListAsync());
        }

        AppDbContext db = new AppDbContext();
        

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            SelectList TransactionTypes = new SelectList(db.TransactionTypes, "Id", "Name");
            ViewBag.TransactionTypes = TransactionTypes;
            SelectList Categories = new SelectList(db.Categories, "Id", "Name");
            ViewBag.Categories = Categories;
            SelectList Banks = new SelectList(db.Banks, "Id", "Name");
            ViewBag.Banks = Banks;

            var user = await GetCurrentUserAsync();
            SelectList Scores = new SelectList(user.Scores, "Id", "Notation");
            ViewBag.Scores = Scores;
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ChangeTransactionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                var commision = model.Comission;
                SelectList Scores = new SelectList(user.Scores, "Id", "Balance");
                //var rez = Scores - commision;
                var transaction = new Transaction { IdTransactionType = model.IdTransactionType, IdScore = model.IdScore, IdCategory = model.IdCategory, IdBank = model.IdBank, Comission = model.Comission, Notation = model.Notation };
                //user.Scores.
                //db.Transactions.Add(transaction);
                await UserService.UpdateAsync(user);
                return RedirectToAction("Index", "Transaction");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Delete(Guid id)
        {
            await TransactionService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Change(Guid id)
        {
            var transaction = Mapper.Map<ChangeTransactionViewModel>(TransactionService.Get(id));
            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Change(ChangeTransactionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var transaction = TransactionService.Get(model.Id);
                transaction.IdTransactionType = model.IdTransactionType;
                transaction.IdScore = model.IdScore;
                transaction.IdCategory = model.IdCategory;
                transaction.IdBank = model.IdBank;
                transaction.Comission = model.Comission;
                transaction.Notation = model.Notation;
                await TransactionService.UpdateAsync(transaction);
                return RedirectToAction("Index");
            }

            return View(model);
        }

    }
}