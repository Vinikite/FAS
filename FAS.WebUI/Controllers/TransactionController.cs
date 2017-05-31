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
        private readonly ITransactionTypeService transactionTypeService;
        private readonly ICategoryService categoryService;

        public TransactionController(ITransactionService transactionService, IScoreService scoreService, IUserService userService,
            ITransactionTypeService transactionTypeService, ICategoryService categoryService) 
            : base(userService)
        {
            this.TransactionService = transactionService;
            this.ScoreService = scoreService;
            this.transactionTypeService = transactionTypeService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var scoresList = user.Scores.Select(x => new SelectListItem { Text = x.Notation, Value = x.Notation }).ToList();
            var categories = categoryService.Get().Select(x => new { Id = x.Id, Val = x.Name }).ToList();

            categories.Insert(0, new { Id = default(Guid), Val = "Не учитывать" });

            var categoriesList = categories.Select(x => new SelectListItem { Text = x.Val, Value = x.Id.ToString() }).ToList();

            categoriesList.First().Selected = true;
            if (scoresList.Count > 0)
            {
                scoresList[0].Selected = true;
            }

            return View(Tuple.Create(scoresList, categoriesList));
        }

        [HttpGet]
        [Authorize]
        public async Task<PartialViewResult> List(string score, DateTime? start, DateTime? end, Guid category)
        {
            var user = await GetCurrentUserAsync();
            var userScore = user.Scores.FirstOrDefault(x => x.Notation == score);

            if (userScore == null)
            {
                return PartialView("NotFoundScore");
            }

            var transactions = userScore.Transactions.ToArray();

            if (start != null)
            {
                transactions = transactions.Where(x => x.CreatedOn >= start).ToArray();
            }

            if (end != null)
            {
                transactions = transactions.Where(x => x.CreatedOn <= end).ToArray();
            }

            if (category != default(Guid))
            {
                transactions = transactions.Where(x => x.IdCategory == category).ToArray();
            }

            return PartialView(Mapper.Map<List<TransactionItemModel>>(transactions));
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
                    return HttpNotFound("Счет не найден");
                }

                var transaction = new Transaction {
                    IdTransactionType = model.IdTransactionType,
                    IdBank = model.IdBank,
                    IdCategory = model.IdCategory,
                    Notation = model.Notation,
                    Comission = model.Comission
                };

                var type = transactionTypeService.Get(model.IdTransactionType);

                if (type == null)
                {
                    return HttpNotFound("Тип транзакции не найден.");
                }

                if (type.Name.Equals("Поступление средств"))
                {
                    score.Balance += model.Comission;
                }
                else
                {
                    if (score.Balance < model.Comission)
                    {
                        throw new ArgumentException(nameof(model.Comission), "На счете недостаточно денег");
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
            SelectList TransactionTypes = new SelectList(db.TransactionTypes, "Id", "Name");
            ViewBag.TransactionTypes = TransactionTypes;
            SelectList Categories = new SelectList(db.Categories, "Id", "Name");
            ViewBag.Categories = Categories;
            SelectList Banks = new SelectList(db.Banks, "Id", "Name");
            ViewBag.Banks = Banks;

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