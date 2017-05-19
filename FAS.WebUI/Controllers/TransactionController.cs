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
    public class TransactionController : Controller
    {
        private readonly ITransactionService TransactionService;

        public TransactionController(ITransactionService transactionService)
        {
            this.TransactionService = transactionService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await TransactionService.Get().ToListAsync());
        }

        [HttpGet]
        public ActionResult Create(/*Guid id*/)
        {
            //var transaction = Mapper.Map<ChangeTransactionViewModel>(TransactionService.GetAsync(id));
            return View(/*transaction*/);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateTransactionViewModel model)
        {
            if (ModelState.IsValid)
            {
                await TransactionService.CreateAsync(Mapper.Map<Transaction>(model));
                return RedirectToAction("Index");
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
            var transaction = Mapper.Map<ChangeTransactionViewModel>(TransactionService.GetAsync(id));
            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Change(ChangeTransactionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var transaction = await TransactionService.GetAsync(model.Id);
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