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
        private readonly ITransactionTypeService transactionTypeService;

        public TransactionController(ITransactionService transactionService, IScoreService scoreService, IUserService userService,
            ITransactionTypeService transactionTypeService) 
            : base(userService)
        {
            this.TransactionService = transactionService;
            this.ScoreService = scoreService;
            this.userService = userService;
            this.transactionTypeService = transactionTypeService;
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
                var score = user.Scores.FirstOrDefault(x => x.Id == model.IdScore);

                if (score == null)
                {
                    return HttpNotFound("Score not found.");
                }

                var transaction = new Transaction {
                    IdTransactionType = model.IdTransactionType,
                    IdBank = model.IdBank,
                    IdCategory = model.IdCategory,
                    Notation = model.Notation,
                    Comission = model.Comission
                };

                var type = await transactionTypeService.GetAsync(model.IdTransactionType);

                if (type == null)
                {
                    return HttpNotFound("Transaction type not found.");
                }

                if (type.Name.Equals("Поступление средств"))
                {
                    score.Balance += model.Comission;
                }
                else
                {
                    if (score.Balance < model.Comission)
                    {
                        throw new ArgumentException(nameof(model.Comission), "There is not enough money on the account");
                    }

                    score.Balance -= model.Comission;
                }

                score.Transactions.Add(transaction);

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